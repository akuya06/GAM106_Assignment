using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopTransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShopTransactionController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopTransaction>>> Get()
            => await _context.ShopTransactions.Include(p => p.Player).Include(p => p.Shop).ToListAsync();

        [HttpPost]
        public async Task<ActionResult<ShopTransaction>> Post([FromBody] ShopTransactionRequest req)
        {
            // Nếu DTO vẫn là string, ép kiểu int
            int playerId;
            if (!int.TryParse(req.PlayerId, out playerId))
                return BadRequest("Invalid PlayerId");

            var player = await _context.Players.FindAsync(playerId);
            if (player == null) return NotFound("Player not found");

            var shop = await _context.Shops.FindAsync(req.ShopId);
            if (shop == null) return NotFound("Shop item not found");

            if (player.Xp < shop.PriceXp) return BadRequest("Not enough XP");

            // deduct XP
            player.Xp -= shop.PriceXp ?? 0;

            // record purchase
            var transaction = new ShopTransaction
            {
                PlayerId = playerId,
                ShopId = req.ShopId,
                PricePaid = shop.PriceXp ?? 0
            };
            _context.ShopTransactions.Add(transaction);

            // add to inventory (Vehicle if IsVehicle, else Item)
            var inv = new PlayerInventory
            {
                PlayerId = playerId,
                Kind = shop.IsVehicle == true ? InventoryKind.Vehicle : InventoryKind.Item,
                ItemId = shop.ItemId,
                Quantity = 1
            };
            _context.PlayerInventory.Add(inv);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        // GET: api/ShopTransaction/player/{playerId}/detailed
        [HttpGet("player/{playerId}/detailed")]
        public async Task<ActionResult> GetPlayerTransactionsDetailed(int playerId)
        {
            var transactions = await _context.ShopTransactions
                .Include(st => st.Shop)
                    .ThenInclude(s => s.Item)
                .Where(st => st.PlayerId == playerId)
                .OrderByDescending(st => st.PurchasedAt)
                .ToListAsync();

            var groupedTransactions = new
            {
                totalTransactions = transactions.Count,
                totalSpent = transactions.Sum(t => t.PricePaid),
                items = transactions.Where(t => t.Shop != null && !t.Shop.IsVehicle).Select(t => new
                {
                    itemName = t.Shop.Item?.Name,
                    price = t.PricePaid,
                    purchasedAt = t.PurchasedAt
                }),
                vehicles = transactions.Where(t => t.Shop != null && t.Shop.IsVehicle).Select(t => new
                {
                    vehicleName = t.Shop.Item?.Name,
                    price = t.PricePaid,
                    purchasedAt = t.PurchasedAt
                })
            };

            return Ok(groupedTransactions);
        }

        // GET: api/ShopTransaction/top-items?limit=10
        [HttpGet("top-items")]
        public async Task<ActionResult> GetTopPurchasedItems([FromQuery] int limit = 10)
        {
            var topItems = await _context.ShopTransactions
                .Include(st => st.Shop)
                    .ThenInclude(s => s.Item)
                .GroupBy(st => st.ShopId)
                .Select(g => new
                {
                    shopId = g.Key,
                    itemName = g.First().Shop.Item.Name,
                    purchaseCount = g.Count(),
                    totalRevenue = g.Sum(st => st.PricePaid)
                })
                .OrderByDescending(x => x.purchaseCount)
                .Take(limit)
                .ToListAsync();

            return Ok(topItems);
        }
    }
}
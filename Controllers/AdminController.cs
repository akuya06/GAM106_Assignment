using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) => _context = context;

        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalPlayers = await _context.Players.CountAsync(),
                TotalAccounts = await _context.Accounts.CountAsync(),
                TotalItems = await _context.Items.CountAsync(),
                TotalShopItems = await _context.Shops.CountAsync(),
                TotalTransactions = await _context.ShopTransactions.CountAsync()
            };

            return View(stats);
        }

        // GET: /Admin/Accounts
        public async Task<IActionResult> Accounts()
        {
            var accounts = await _context.Accounts
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
            return View(accounts);
        }

        // GET: /Admin/Players
        public async Task<IActionResult> Players()
        {
            var players = await _context.Players
                .Include(p => p.Account)
                .Include(p => p.CurrentMode)
                .OrderByDescending(p => p.Xp)
                .ToListAsync();
            return View(players);
        }

        // GET: /Admin/Items
        public async Task<IActionResult> Items()
        {
            var items = await _context.Items
                .OrderBy(i => i.Name)
                .ToListAsync();
            return View(items);
        }

        // GET: /Admin/Shop
        public async Task<IActionResult> Shop()
        {
            var shopItems = await _context.Shops
                .Include(s => s.Item)
                .OrderBy(s => s.PriceXp)
                .ToListAsync();
            return View(shopItems);
        }

        // GET: /Admin/Transactions
        public async Task<IActionResult> Transactions()
        {
            var transactions = await _context.ShopTransactions
                .Include(t => t.Player)
                    .ThenInclude(p => p.Account)
                .Include(t => t.Shop)
                    .ThenInclude(s => s.Item)
                .OrderByDescending(t => t.PurchasedAt)
                .ToListAsync();
            return View(transactions);
        }

        // GET: /Admin/Monsters
        public async Task<IActionResult> Monsters()
        {
            var monsters = await _context.Monsters
                .Include(m => m.RewardItem)
                .OrderBy(m => m.Name)
                .ToListAsync();
            return View(monsters);
        }
    }
}
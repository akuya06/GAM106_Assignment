using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerInventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayerInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PlayerInventory/player/{playerId} - Lấy tất cả inventory của player
        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetPlayerInventory(int playerId)
        {
            var inventory = await _context.PlayerInventory
                .Include(pi => pi.Item)
                .Include(pi => pi.Resource)
                .Where(pi => pi.PlayerId == playerId)
                .Select(pi => new
                {
                    id = pi.Id,
                    playerId = pi.PlayerId,
                    kind = pi.Kind.ToString(),
                    itemId = pi.ItemId,
                    quantity = pi.Quantity,
                    slotIndex = pi.SlotIndex,
                    item = pi.Item != null ? new
                    {
                        id = pi.Item.Id,
                        name = pi.Item.Name,
                        description = pi.Item.Description,
                        iconUrl = pi.Item.IconUrl
                    } : null,
                    resource = pi.Resource != null ? new
                    {
                        id = pi.Resource.Id,
                        name = pi.Resource.Name,
                        description = pi.Resource.Description
                    } : null
                })
                .ToListAsync();

            return Ok(inventory);
        }

        // GET: api/PlayerInventory/player/{playerId}/kind/{kind} - Lọc theo loại (Item, Vehicle, Resource)
        [HttpGet("player/{playerId}/kind/{kind}")]
        public async Task<ActionResult<IEnumerable<PlayerInventory>>> GetPlayerInventoryByKind(int playerId, string kind)
        {
            if (!Enum.TryParse<InventoryKind>(kind, true, out var inventoryKind))
                return BadRequest(new { message = "Invalid kind. Use: Item, Vehicle, or Resource" });

            var inventory = await _context.PlayerInventory
                .Include(pi => pi.Item)
                .Include(pi => pi.Resource)
                .Where(pi => pi.PlayerId == playerId && pi.Kind == inventoryKind)
                .ToListAsync();

            return Ok(inventory);
        }

        // GET: api/PlayerInventory/{id} - Lấy chi tiết 1 item trong inventory
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerInventory>> GetInventoryItem(int id)
        {
            var inventory = await _context.PlayerInventory
                .Include(pi => pi.Item)
                .Include(pi => pi.Resource)
                .Include(pi => pi.Player)
                .FirstOrDefaultAsync(pi => pi.Id == id);

            if (inventory == null)
                return NotFound(new { message = "Inventory item not found" });

            return inventory;
        }

        // PUT: api/PlayerInventory/{id}/quantity - Cập nhật số lượng
        [HttpPut("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromBody] UpdateQuantityRequest request)
        {
            var inventory = await _context.PlayerInventory.FindAsync(id);
            
            if (inventory == null)
                return NotFound(new { message = "Inventory item not found" });

            inventory.Quantity = request.Quantity;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Quantity updated", newQuantity = inventory.Quantity });
        }

        // PUT: api/PlayerInventory/{id}/slot - Cập nhật slot
        [HttpPut("{id}/slot")]
        public async Task<IActionResult> UpdateSlot(int id, [FromBody] UpdateSlotRequest request)
        {
            var inventory = await _context.PlayerInventory.FindAsync(id);
            
            if (inventory == null)
                return NotFound(new { message = "Inventory item not found" });

            inventory.SlotIndex = request.SlotIndex;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Slot updated", newSlot = inventory.SlotIndex });
        }

        // DELETE: api/PlayerInventory/{id} - Xóa item khỏi inventory
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var inventory = await _context.PlayerInventory.FindAsync(id);
            
            if (inventory == null)
                return NotFound(new { message = "Inventory item not found" });

            _context.PlayerInventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item removed from inventory" });
        }

        // POST: api/PlayerInventory - Thêm item vào inventory thủ công (nếu cần)
        [HttpPost]
        public async Task<ActionResult<PlayerInventory>> AddToInventory(PlayerInventory playerInventory)
        {
            _context.PlayerInventory.Add(playerInventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventoryItem), new { id = playerInventory.Id }, playerInventory);
        }
    }

    public class UpdateQuantityRequest
    {
        public int Quantity { get; set; }
    }

    public class UpdateSlotRequest
    {
        public int? SlotIndex { get; set; }
    }
}

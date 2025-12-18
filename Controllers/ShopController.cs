using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Shop
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetShops()
    {
        // Trả về cả tên item nếu có
        var shops = await _context.Shops
            .Include(s => s.Item)
            .Select(s => new {
                s.Id,
                s.PriceXp,
                s.AllowedModes,
                s.IsVehicle,
                s.ItemId,
                ItemName = s.Item != null ? s.Item.Name : null
            })
            .ToListAsync();
        return Ok(shops);
    }

    // GET: api/Shop/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetShop(string id)
    {
        var shop = await _context.Shops
            .Include(s => s.Item)
            .Where(s => s.Id == id)
            .Select(s => new {
                s.Id,
                s.PriceXp,
                s.AllowedModes,
                s.IsVehicle,
                s.ItemId,
                ItemName = s.Item != null ? s.Item.Name : null
            })
            .FirstOrDefaultAsync();
        if (shop == null)
            return NotFound();
        return Ok(shop);
    }

    // POST: api/Shop
    [HttpPost]
    public async Task<ActionResult<Shop>> PostShop(Shop shop)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();
        // Trả về shop kèm tên item nếu có
        var result = await _context.Shops
            .Include(s => s.Item)
            .Where(s => s.Id == shop.Id)
            .Select(s => new {
                s.Id,
                s.PriceXp,
                s.AllowedModes,
                s.IsVehicle,
                s.ItemId,
                ItemName = s.Item != null ? s.Item.Name : null
            })
            .FirstOrDefaultAsync();
        return CreatedAtAction(nameof(GetShop), new { id = shop.Id }, result);
    }

    // PUT: api/Shop/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShop(string id, Shop shop)
    {
        if (id != shop.Id)
            return BadRequest();
        var existing = await _context.Shops.FindAsync(id);
        if (existing == null)
            return NotFound();
        // Update các trường
        existing.PriceXp = shop.PriceXp;
        existing.AllowedModes = shop.AllowedModes;
        existing.IsVehicle = shop.IsVehicle;
        existing.ItemId = shop.ItemId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Shop/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShop(string id)
    {
        var shop = await _context.Shops.FindAsync(id);
        if (shop == null)
            return NotFound();
        _context.Shops.Remove(shop);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
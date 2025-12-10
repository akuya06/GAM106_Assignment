namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ItemController(ApplicationDbContext context) => _context = context;

    // GET: api/Item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> Get()
        => await _context.Set<Item>().ToListAsync();

    // GET: api/Item/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> Get(string id)
    {
        var item = await _context.Set<Item>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Item>> Post(Item model)
    {
        _context.Set<Item>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(string id)
    {
        // Xóa liên kết ở Shop
        var shops = await _context.Shops.Where(s => s.ItemId == id).ToListAsync();
        if (shops.Any()) _context.Shops.RemoveRange(shops);

        // Xóa liên kết ở PlayerInventory
        var inventories = await _context.PlayerInventory.Where(pi => pi.ItemId == id).ToListAsync();
        if (inventories.Any()) _context.PlayerInventory.RemoveRange(inventories);

        // Xóa Item
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Item deleted" });
    }
}


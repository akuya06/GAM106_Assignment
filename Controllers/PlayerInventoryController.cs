namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayerInventoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlayerInventoryController(ApplicationDbContext context) => _context = context;

    // GET: api/PlayerInventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerInventory>>> Get()
        => await _context.Set<PlayerInventory>().ToListAsync();

    // GET: api/PlayerInventory/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerInventory>> Get(string id)
    {
        var item = await _context.Set<PlayerInventory>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<PlayerInventory>> Post(PlayerInventory model)
    {
        _context.Set<PlayerInventory>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}

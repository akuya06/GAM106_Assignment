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
}


namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlayerController(ApplicationDbContext context) => _context = context;

    // GET: api/Player
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> Get()
        => await _context.Set<Player>().ToListAsync();

    // GET: api/Player/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> Get(string id)
    {
        var item = await _context.Set<Player>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Player>> Post(Player model)
    {
        _context.Set<Player>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


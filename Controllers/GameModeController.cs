namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GameModeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public GameModeController(ApplicationDbContext context) => _context = context;

    // GET: api/GameMode
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameMode>>> Get()
        => await _context.Set<GameMode>().ToListAsync();

    // GET: api/GameMode/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<GameMode>> Get(string id)
    {
        var item = await _context.Set<GameMode>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<GameMode>> Post(GameMode model)
    {
        _context.Set<GameMode>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayerMonsterKillController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlayerMonsterKillController(ApplicationDbContext context) => _context = context;

    // GET: api/PlayerMonsterKill
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerMonsterKill>>> Get()
        => await _context.Set<PlayerMonsterKill>().ToListAsync();

    // GET: api/PlayerMonsterKill/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerMonsterKill>> Get(string id)
    {
        var item = await _context.Set<PlayerMonsterKill>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<PlayerMonsterKill>> Post(PlayerMonsterKill model)
    {
        _context.Set<PlayerMonsterKill>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


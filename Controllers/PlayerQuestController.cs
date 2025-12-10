namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayerQuestController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlayerQuestController(ApplicationDbContext context) => _context = context;

    // GET: api/PlayerQuest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerQuest>>> Get()
        => await _context.Set<PlayerQuest>().ToListAsync();

    // GET: api/PlayerQuest/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerQuest>> Get(string id)
    {
        var item = await _context.Set<PlayerQuest>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<PlayerQuest>> Post(PlayerQuest model)
    {
        _context.Set<PlayerQuest>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


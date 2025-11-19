namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class QuestController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public QuestController(ApplicationDbContext context) => _context = context;

    // GET: api/Quest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quest>>> Get()
        => await _context.Set<Quest>().ToListAsync();

    // GET: api/Quest/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Quest>> Get(string id)
    {
        var item = await _context.Set<Quest>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Quest>> Post(Quest model)
    {
        _context.Set<Quest>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


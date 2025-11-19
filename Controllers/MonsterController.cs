namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonsterController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public MonsterController(ApplicationDbContext context) => _context = context;

    // GET: api/Monster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Monster>>> Get()
        => await _context.Set<Monster>().ToListAsync();

    // GET: api/Monster/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Monster>> Get(string id)
    {
        var item = await _context.Set<Monster>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Monster>> Post(Monster model)
    {
        _context.Set<Monster>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


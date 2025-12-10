namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ResourceController(ApplicationDbContext context) => _context = context;

    // GET: api/Resource
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resource>>> Get()
        => await _context.Set<Resource>().ToListAsync();

    // GET: api/Resource/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> Get(string id)
    {
        var item = await _context.Set<Resource>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Resource>> Post(Resource model)
    {
        _context.Set<Resource>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


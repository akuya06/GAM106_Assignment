namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ShopController(ApplicationDbContext context) => _context = context;

    // GET: api/Shop
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shop>>> Get()
        => await _context.Set<Shop>().ToListAsync();

    // GET: api/Shop/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Shop>> Get(string id)
    {
        var item = await _context.Set<Shop>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Shop>> Post(Shop model)
    {
        _context.Set<Shop>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


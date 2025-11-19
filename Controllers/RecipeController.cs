namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RecipeController(ApplicationDbContext context) => _context = context;

    // GET: api/Recipe
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> Get()
        => await _context.Set<Recipe>().ToListAsync();

    // GET: api/Recipe/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> Get(string id)
    {
        var item = await _context.Set<Recipe>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<Recipe>> Post(Recipe model)
    {
        _context.Set<Recipe>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


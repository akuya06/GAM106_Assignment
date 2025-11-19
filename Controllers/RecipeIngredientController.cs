namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RecipeIngredientController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RecipeIngredientController(ApplicationDbContext context) => _context = context;

    // GET: api/RecipeIngredient
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeIngredient>>> Get()
        => await _context.Set<RecipeIngredient>().ToListAsync();

    // GET: api/RecipeIngredient/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeIngredient>> Get(string id)
    {
        var item = await _context.Set<RecipeIngredient>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public async Task<ActionResult<RecipeIngredient>> Post(RecipeIngredient model)
    {
        _context.Set<RecipeIngredient>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}


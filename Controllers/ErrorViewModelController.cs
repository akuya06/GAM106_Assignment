namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ErrorViewModelController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ErrorViewModelController(ApplicationDbContext context) => _context = context;

    // GET: api/ErrorViewModel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ErrorViewModel>>> Get()
        => await _context.Set<ErrorViewModel>().ToListAsync();

    // GET: api/ErrorViewModel/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ErrorViewModel>> Get(string id)
    {
        var item = await _context.Set<ErrorViewModel>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // optional: POST
    [HttpPost]
    public ActionResult<ErrorViewModel> Post(ErrorViewModel model)
    {
        // ErrorViewModel is not a persisted entity in the DB schema.
        // Return the received object (no DB write) to avoid referencing model.Id.
        return Ok(model);
    }
}


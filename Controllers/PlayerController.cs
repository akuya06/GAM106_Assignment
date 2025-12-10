namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlayerController(ApplicationDbContext context) => _context = context;

    // GET: api/Player
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> Get()
        => await _context.Set<Player>().ToListAsync();

    // GET: api/Player/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> Get(int id)
    {
        var item = await _context.Set<Player>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Player>> Post(Player model)
    {
        _context.Set<Player>().Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // DELETE: api/Player/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var player = await _context.Players
            .Include(p => p.PlayerInventory)
            .Include(p => p.MonsterKills)
            .Include(p => p.PlayerQuests)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (player == null)
            return NotFound(new { message = "Player not found" });

        // Xóa các bảng liên quan nếu cần
        var transactions = await _context.ShopTransactions
            .Where(t => t.PlayerId == id)
            .ToListAsync();
        if (transactions.Any())
            _context.ShopTransactions.RemoveRange(transactions);

        if (player.PlayerInventory != null && player.PlayerInventory.Any())
            _context.PlayerInventory.RemoveRange(player.PlayerInventory);

        if (player.MonsterKills != null && player.MonsterKills.Any())
            _context.PlayerMonsterKills.RemoveRange(player.MonsterKills);

        if (player.PlayerQuests != null && player.PlayerQuests.Any())
            _context.PlayerQuests.RemoveRange(player.PlayerQuests);

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Player deleted successfully" });
    }
}


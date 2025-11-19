namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Account
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

    // GET: api/Account/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(string id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
            return NotFound();

        return account;
    }

    // POST: api/Account
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Accounts.Add(account);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
        }

        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    // PUT: api/Account/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(string id, Account account)
    {
        if (id != account.Id)
            return BadRequest();

        var existing = await _context.Accounts.FindAsync(id);
        if (existing == null)
            return NotFound();

        // Update explicit scalar properties (adjust fields to your Account model)
        existing.Username = account.Username;
        existing.Email = account.Email;
        existing.PasswordHash = account.PasswordHash;
        existing.CreatedAt = account.CreatedAt;
        existing.CreatedIp = account.CreatedIp;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Account/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(string id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound();

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

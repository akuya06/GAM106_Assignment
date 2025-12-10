namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

    // PUT: api/Account/{id}/change-password
    [HttpPut("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordRequest request)
    {
        if (string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword))
        {
            return BadRequest(new { message = "Old password and new password are required" });
        }

        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            return NotFound(new { message = "Account not found" });
        }

        // Verify old password
        string oldPasswordHash = HashPassword(request.OldPassword);
        if (account.PasswordHash != oldPasswordHash)
        {
            return BadRequest(new { message = "Old password is incorrect" });
        }

        // Validate new password
        if (request.NewPassword.Length < 6)
        {
            return BadRequest(new { message = "New password must be at least 6 characters" });
        }

        // Update password
        account.PasswordHash = HashPassword(request.NewPassword);
        
        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Password changed successfully" });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    // PUT: api/Account/{id}/reset-password (Admin only - không cần old password)
    [HttpPut("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordRequest request)
    {
        if (string.IsNullOrEmpty(request.NewPassword))
        {
            return BadRequest(new { message = "New password is required" });
        }

        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            return NotFound(new { message = "Account not found" });
        }

        // Validate new password
        if (request.NewPassword.Length < 6)
        {
            return BadRequest(new { message = "New password must be at least 6 characters" });
        }

        // Update password
        account.PasswordHash = HashPassword(request.NewPassword);
        
        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Password reset successfully" });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
        }
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

    // Helper method to hash password
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

// DTOs
public class ChangePasswordRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class ResetPasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
}

namespace WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required] public required string Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedIp { get; set; }

    public ICollection<Player>? Players { get; set; }
}
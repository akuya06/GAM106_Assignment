namespace WebApplication1.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class PlayerMonsterKill
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

    // FK to Player
    public string? PlayerId { get; set; }
    public Player? Player { get; set; }

    // FK to Monster
    public string? MonsterId { get; set; }
    public Monster? Monster { get; set; }

    public DateTime? KilledAt { get; set; }
}
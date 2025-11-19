using System;
using System.ComponentModel.DataAnnotations;

public class PlayerQuest
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? PlayerId { get; set; }
    public Player? Player { get; set; }
    public string? QuestId { get; set; }
    public Quest? Quest { get; set; }
    public DateTime? CompletedAt { get; set; }
}
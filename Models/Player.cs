using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

public class Player
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? AccountId { get; set; }
    public Account? Account { get; set; }

    public string? CurrentModeId { get; set; }
    public GameMode? CurrentMode { get; set; }

    public int? Realm { get; set; }
    public int? Hunger { get; set; }
    public int? Xp { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<PlayerInventory>? PlayerInventory { get; set; }
    public ICollection<PlayerMonsterKill>? MonsterKills { get; set; }
    public ICollection<PlayerQuest>? PlayerQuests { get; set; }
}
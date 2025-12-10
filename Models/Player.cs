using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Tự động tăng

        [Required]
        public string AccountId { get; set; } = string.Empty;
        public string? CurrentModeId { get; set; }
        public int? Realm { get; set; }
        public int? Hunger { get; set; }
        public int? Xp { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation properties
        public Account? Account { get; set; }
        public GameMode? CurrentMode { get; set; }
        public ICollection<PlayerInventory>? PlayerInventory { get; set; }
        public ICollection<PlayerMonsterKill>? MonsterKills { get; set; }
        public ICollection<PlayerQuest>? PlayerQuests { get; set; }
    }
}
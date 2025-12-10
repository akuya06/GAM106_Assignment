using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PlayerMonsterKill
    {
        [Key] public int Id { get; set; }
        public int PlayerId { get; set; } // int
        public string MonsterId { get; set; } = string.Empty;
        public int KillCount { get; set; }
        public DateTime? KilledAt { get; set; }

        public Player? Player { get; set; }
        public Monster? Monster { get; set; }
    }
}
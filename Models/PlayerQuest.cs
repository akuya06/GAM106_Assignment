using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PlayerQuest
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        public int PlayerId { get; set; } // int
        public Player? Player { get; set; }
        public string? QuestId { get; set; }
        public Quest? Quest { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
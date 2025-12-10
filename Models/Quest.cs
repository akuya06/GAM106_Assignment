using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Quest
    {
        [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? RewardXp { get; set; }
        public string? RewardItemId { get; set; }
        public Item? RewardItem { get; set; }
    }
}
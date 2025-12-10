using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GameMode
    {
        [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool CanInteractWithShop { get; set; }
        public bool CanUseRecipes { get; set; }
        public ICollection<Player>? Players { get; set; }
    }
}
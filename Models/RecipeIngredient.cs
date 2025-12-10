using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RecipeIngredient
    {
        [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public string? ResourceId { get; set; }
        public Resource? Resource { get; set; }
        public int? Quantity { get; set; }
    }
}
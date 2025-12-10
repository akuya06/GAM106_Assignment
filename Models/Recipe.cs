using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Recipe
    {
        [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? ResultItemId { get; set; }
        public Item? ResultItem { get; set; }
        public int? ResultQuantity { get; set; }
        public ICollection<RecipeIngredient>? Ingredients { get; set; }
    }
}
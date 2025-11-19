using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Recipe
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? ItemId { get; set; }
    public Item? Item { get; set; }
    public string? RequiredModeId { get; set; }
    public GameMode? RequiredMode { get; set; }
    public int? OutputQuantity { get; set; }

    public ICollection<RecipeIngredient>? Ingredients { get; set; }
}
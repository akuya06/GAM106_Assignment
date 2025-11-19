using System.ComponentModel.DataAnnotations;

public class RecipeIngredient
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? ResourceId { get; set; }
    public Resource? Resource { get; set; }
    public string? RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    public int? Quantity { get; set; }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int? BaseValue { get; set; }
    public int? MaxStackSize { get; set; }
    public bool IsSpecial { get; set; }
    public string? AllowedModes { get; set; }
    public bool IsVehicle { get; set; }

    public ICollection<Shop>? Shops { get; set; }
    public ICollection<Monster>? MonstersRewarding { get; set; }
    public ICollection<Quest>? QuestsRewarding { get; set; }
    public ICollection<Recipe>? RecipesProduced { get; set; }
}
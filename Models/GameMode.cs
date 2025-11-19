using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class GameMode
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsSurvival { get; set; }
    public bool IsCreative { get; set; }
    public bool IsAdventure { get; set; }
    public bool IsSpectator { get; set; }

    public ICollection<Player>? Players { get; set; }
    public ICollection<Recipe>? Recipes { get; set; }
}
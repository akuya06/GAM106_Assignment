using System.ComponentModel.DataAnnotations;

public class Monster
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? RewardItemId { get; set; }
    public Item? RewardItem { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? RewardXp { get; set; }
    public int? RewardQuantity { get; set; }
}
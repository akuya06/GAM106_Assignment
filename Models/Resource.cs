using System.ComponentModel.DataAnnotations;

public class Resource
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
}
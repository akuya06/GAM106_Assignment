using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Shop
    {
        [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public int? PriceXp { get; set; }
        public string? AllowedModes { get; set; }
        public bool IsVehicle { get; set; }
        public string? ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
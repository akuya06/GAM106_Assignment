using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ShopTransaction
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        public int PlayerId { get; set; } // int
        public Player? Player { get; set; }
        public string ShopId { get; set; } = string.Empty;
        public Shop? Shop { get; set; }
        public int PricePaid { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}
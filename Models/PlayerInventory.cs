using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public enum InventoryKind
    {
        Item = 0,
        Resource = 1,
        Vehicle = 2
    }

    public class PlayerInventory
    {
        [Key] public int Id { get; set; }

        // Liên kết đến Player
        public int PlayerId { get; set; } // Đổi từ string sang int
        public Player? Player { get; set; }

        // Loại: Item, Resource, hoặc Vehicle
        public InventoryKind Kind { get; set; } = InventoryKind.Item;

        // Nếu Kind == Item hoặc Vehicle thì đây là itemId; nếu Resource thì là resourceId
        public string ItemId { get; set; } = string.Empty;

        // Số lượng / stack
        public int Quantity { get; set; } = 1;

        // Slot index (null nếu là resource không có slot)
        public int? SlotIndex { get; set; }

        // Giữ tham chiếu tùy chọn tới Item / Resource để EF có thể map (nullable)
        public Item? Item { get; set; }
        public Resource? Resource { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

public enum InventoryKind
{
    Item = 0,
    Resource = 1
}

public class PlayerInventory
{
    [Key] public string Id { get; set; } = System.Guid.NewGuid().ToString();

    // Liên kết đến Player
    public string? PlayerId { get; set; }
    public Player? Player { get; set; }

    // Loại: Item hay Resource
    public InventoryKind Kind { get; set; } = InventoryKind.Item;

    // Nếu Kind == Item thì đây là itemId; nếu Resource thì là resourceId
    public string? ItemId { get; set; }

    // Số lượng / stack
    public int Quantity { get; set; } = 1;

    // Slot index (null nếu là resource không có slot)
    public int? SlotIndex { get; set; }

    // Giữ tham chiếu tùy chọn tới Item / Resource để EF có thể map (nullable)
    public Item? Item { get; set; }
    public Resource? Resource { get; set; }
}
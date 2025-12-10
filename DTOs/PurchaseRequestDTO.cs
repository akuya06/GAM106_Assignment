namespace WebApplication1.DTOs
{
    public class PurchaseRequest
    {
        public int PlayerId { get; set; }
        public string ShopItemId { get; set; } = string.Empty;
    }

    public class ShopTransactionRequest
    {
        public string? PlayerId { get; set; }
        public string? ShopId { get; set; }
    }
}
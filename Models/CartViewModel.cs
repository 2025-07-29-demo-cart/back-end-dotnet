namespace dotnet_9.Models;

public class CartViewModel
{
    public required string CartId { get; set; }
    public required string StockId { get; set; }
    public required string CartProductAmount { get; set; }
    public string StockProductId { get; set; } = string.Empty;
    public string StockProductCode { get; set; } = string.Empty;
    public string StockProductTotalAmount { get; set; } = "0";

    public string StockProductUnitPrice { get; set; } = string.Empty;
    public string StockProductName { get; set; } = string.Empty;
}
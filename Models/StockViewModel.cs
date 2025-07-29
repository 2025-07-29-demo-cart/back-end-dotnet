namespace dotnet_9.Models;

public class StockViewModel
{
    public required string StockId { get; set; }
    public required string StockProductId { get; set; }
    public required string StockProductCode { get; set; }
    public required string StockProductTotalAmount { get; set; } = "0";
    public required string StockProductName { get; set; }
    public required string StockProductUnitPrice { get; set; }

    public bool CanUseStock => !string.IsNullOrEmpty(StockProductTotalAmount) && int.Parse(StockProductTotalAmount) > 0;

}
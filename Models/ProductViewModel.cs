namespace dotnet_9.Models;

public class ProductViewModel
{
    public required string ProductId { get; set; }
    public required string ProductCode { get; set; }
    public required string ProductName { get; set; } = string.Empty;
    public required string ProductUnitPrice { get; set; } = string.Empty;
}
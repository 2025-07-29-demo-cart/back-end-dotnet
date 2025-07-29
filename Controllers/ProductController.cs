using dotnet_9.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private List<ProductViewModel> ProductList = [
        new () { ProductId = "PD001230", ProductCode = "MS001", ProductName = "Minimal Shirt", ProductUnitPrice = "350" },
        new () { ProductId = "PD001299", ProductCode = "MP001", ProductName = "Minimal Pants", ProductUnitPrice = "299" },
        new () { ProductId = "PD001314", ProductCode = "MB001", ProductName = "Minimal Bag", ProductUnitPrice = "240" },
        new () { ProductId = "PD001316", ProductCode = "UTM001", ProductName = "UT Minimal Shirt", ProductUnitPrice = "440" },
        new () { ProductId = "PD001517", ProductCode = "UTP001", ProductName = "UT Minimal Pants", ProductUnitPrice = "399" },
        new () { ProductId = "PD001911", ProductCode = "UTB001", ProductName = "UT Minimal Bag", ProductUnitPrice = "480" },
    ];

    [HttpGet("list")]
    public ActionResult GetProductList()
    {
        return Ok(ProductList);
    }
}
namespace dotnet_9.Controllers;

using dotnet_9.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    public static List<StockViewModel> StockList = [
        new () {StockId = "ST001001", StockProductCode = "MS001", StockProductName = "Minimal Shirt", StockProductId = "PD001230", StockProductTotalAmount = "1", StockProductUnitPrice = "350"},
        new () {StockId = "ST001002", StockProductCode = "MP001", StockProductName = "Minimal Pants", StockProductId = "PD001299", StockProductTotalAmount = "2", StockProductUnitPrice = "299"},
        new () {StockId = "ST001004", StockProductCode = "MB001", StockProductName = "Minimal Bag", StockProductId = "PD001314", StockProductTotalAmount = "3", StockProductUnitPrice = "240"},
        new () {StockId = "ST001005", StockProductCode = "UTM001", StockProductName = "UT Minimal Shirt", StockProductId = "PD001316", StockProductTotalAmount = "1", StockProductUnitPrice = "440"},
        new () {StockId = "ST001006", StockProductCode = "UTP001", StockProductName = "UT Minimal Pants", StockProductId = "PD001517", StockProductTotalAmount = "2", StockProductUnitPrice = "399"},
        new () {StockId = "ST001010", StockProductCode = "UTB001", StockProductName = "UT Minimal Bag", StockProductId = "PD001911", StockProductTotalAmount = "3", StockProductUnitPrice = "480"},
    ];


    public static bool CheckStock(string stockId)
    {
        var findStock = StockList.Find(st => st.StockProductId == stockId);
        if (findStock == null)
        {
            return false;
        }
        return findStock.CanUseStock;
    }


    public static dynamic? GetStockByProductId(string productId)
    {
        var findStock = StockList.Find(st => st.StockProductId == productId);
        if (findStock == null)
        {
            return null;
        }
        return findStock;
    }

    [HttpGet("stock_list")]
    public ActionResult<StockController> GetStockList()
    {
        return Ok(StockList);
    }

    [HttpGet("get_stock_by_product/{productId}")]
    public ActionResult<StockController> GetStockByProductIdAPI(string productId)
    {
        if (string.IsNullOrEmpty(productId))
        {
            return NotFound();
        }
        var findStock = GetStockByProductId(productId);
        if (findStock == null)
        {
            return NotFound();
        }
        return Ok(findStock);
    }

    public static void _updateStock(string stockId, string amount)
    {
        var findStock = StockList.Find(st => st.StockProductId == stockId);
        if (findStock != null || CheckStock(stockId))
        {
            int newAmount = int.Parse(findStock.StockProductTotalAmount) - int.Parse(amount);
            findStock.StockProductTotalAmount = newAmount.ToString();
        }
    }

    [HttpPost("update/{stockId}/{amount}")]
    public ActionResult<StockViewModel> UpdateStock(string stockId, string amount)
    {
        var findStock = StockList.Find(st => st.StockProductId == stockId);
        if (findStock == null || !CheckStock(stockId)) {
            return NotFound();
        }

        int newAmount = int.Parse(findStock.StockProductTotalAmount) - int.Parse(amount);
        findStock.StockProductTotalAmount = newAmount.ToString();
        return Ok();
    }
}
using System.Formats.Asn1;
using System.Xml;
using dotnet_9.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace dotnet_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    public static List<CartViewModel> CartList = [];


    [HttpGet("cart_list")]
    public ActionResult GetCartList()
    {
        return Ok(CartList);
    }

    private dynamic GetCartById(string cartId)
    {
        return CartList.Find(c => c.CartId == cartId);
    }

    private string UpdateCartAmount(string cartId, string amount)
    {
        var cart = GetCartById(cartId);
        if (cart == null)
        {
            return "not found";
        }

        CartViewModel currentCart = cart;
        var newAmount = int.Parse(amount);

        StockViewModel stockItem = StockController.GetStockByProductId(currentCart.StockProductId);
        if (stockItem == null)
        {
            return "not found product";
        }

        bool canUpdate = StockController.CheckStock(currentCart.StockProductId);
        if (!canUpdate)
        {
            return "can not update";
        }

        if (newAmount > int.Parse(stockItem.StockProductTotalAmount))
        {
            return "no stock left";
        }

        currentCart.CartProductAmount = newAmount.ToString();
        return "update complete";
    }

    [HttpPost("create_cart/{productId}")]
    public ActionResult<CartViewModel> CreateCart(string productId)
    {
        var findStock = StockController.GetStockByProductId(productId);
        if (findStock == null)
        {
            return Ok("no found");
        }

        var cartExist = CartList.Find(cl => cl.StockId == findStock.StockProductId);
        if (cartExist != null)
        {
            cartExist.CartProductAmount += 1;
            return Ok(UpdateCartAmount(cartExist.CartId, cartExist.CartProductAmount));
        }

        StockViewModel StockList = findStock;
        var now = new UniqueId();
        var cartList = new CartViewModel()
        {
            CartId = now.ToString().Substring(9, 8),
            CartProductAmount = "1",
            StockId = StockList.StockId,
            StockProductId = StockList.StockProductId,
            StockProductCode = StockList.StockProductCode,
            StockProductName = StockList.StockProductName,
            StockProductUnitPrice = StockList.StockProductUnitPrice,
            StockProductTotalAmount = StockList.StockProductTotalAmount,
        };

        CartList.Add(cartList);
        return Ok(cartList);
    }


    [HttpPost("update_cart/{cartId}/{amount}")]
    public ActionResult<CartViewModel> UpdateAmount(string cartId, string amount)
    {
        if (int.Parse(amount) == 0)
        {
            DeleteCart(cartId);
        }
        return Ok(UpdateCartAmount(cartId, amount));
    }

    [HttpPost("remove_cart/{cartId}")]
    public ActionResult DeleteCart(string cartId)
    {
        var cart = GetCartById(cartId);
        if (cart == null)
        {
            return NotFound();
        }

        CartList.Remove(cart);
        return Ok(CartList);
    }

    [HttpPost("checkout/{stockId}/{amount}")]
    public ActionResult Checkout(string stockId, string amount)
    {
        var newAmount = int.Parse(amount) > 0 ? amount : "0";
        StockController._updateStock(stockId, newAmount);
        return Ok();
    }
}
using GitNguonMo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitNguonMo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly DataStore _store;

    public CartController(DataStore store)
    {
        _store = store;
    }

    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] AddCartRequest req)
    {
        var product = _store.GetProduct(req.ProductId);
        if (product == null) return NotFound("Product not found");

        // In this simple demo we don't persist carts — respond with success and product summary
        return Ok(new { message = "Added to cart", product = new { product.Id, product.Name, product.Price, req.Quantity } });
    }

    public record AddCartRequest(string ProductId, int Quantity);
}

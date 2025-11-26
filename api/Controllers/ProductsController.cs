using GitNguonMo.Api.Models;
using GitNguonMo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitNguonMo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataStore _store;

    public ProductsController(DataStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return Ok(_store.GetProducts());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Get(string id)
    {
        var p = _store.GetProduct(id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}

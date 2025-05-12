using System.Threading.Tasks;
using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Service.Products;
using Microsoft.AspNetCore.Mvc;

namespace CTRLKEY_API.Contollers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    // POST: api/product
    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] ProductDto dto)
    {
        var product = await _productService.AddProduct(dto.Name, dto.Description, dto.Price, dto.Stock, dto.ImageUrl, dto.TypeProduct);
        if (product == null)
        {
            return BadRequest("Product already exists");
        }

        return Ok(product);
    }

    // GET: api/product
    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }

    // GET: api/product/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // DELETE: api/product/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductById(int id)
    {
        var deleted = await _productService.DeleteProduct(id);
        if (deleted == null)
        {
            return NotFound();
        }
        return Ok(deleted);
    }

    // PUT: api/product/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProductById(int id, [FromBody] ProductDto dto)
    {
        var product = await _productService.UpdateProduct(id, dto);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // GET: api/product/search?searchQuery=xxx
    [HttpGet("search")]
    public async Task<ActionResult<List<ProductDto>>> SearchProducts([FromQuery] string searchQuery)
    {
        var products = await _productService.SearchProducts(searchQuery);
        if (products.Any())
        {
            var productDtos = products.Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                TypeProduct = p.TypeProduct
            }).ToList();

            return Ok(productDtos);
        }
        return NotFound();
    }
}

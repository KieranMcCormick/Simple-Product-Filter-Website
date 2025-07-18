using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductFilterApi.Data;
using ProductFilterApi.Models;
using Bogus;

namespace ProductFilterApi.Controllers;

// REST API controller for product operations
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductContext _context;

    // Dependency injection of database context
    public ProductsController(ProductContext context)
    {
        _context = context;
    }

    // POST /api/products/generate - Creates fake product data using Bogus library
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateProducts([FromQuery] int count = 1000)
    {
        // Configure Bogus faker to generate realistic product data
        var faker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName()) // Generate product names
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription()) // Generate descriptions
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]) // Pick random category
            .RuleFor(p => p.Brand, f => f.Company.CompanyName()) // Use company names as brands
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000)) // Random price between $10-$1000
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 100)) // Stock level 0-100
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 50)) // Quantity 1-50
            .RuleFor(p => p.SKU, f => f.Random.AlphaNumeric(8).ToUpper()) // 8-character SKU
            .RuleFor(p => p.ReleaseDate, f => f.Date.Past(2)) // Release date within last 2 years
            .RuleFor(p => p.AvailabilityStatus, f => f.PickRandom("In Stock", "Out of Stock", "Limited")) // Random status
            .RuleFor(p => p.CustomerRating, f => f.Random.Double(1, 5)) // Rating 1-5 stars
            .RuleFor(p => p.AvailableSizes, f => string.Join(",", f.PickRandom(new[] { "XS", "S", "M", "L", "XL" }, f.Random.Int(1, 3)))) // Random sizes
            .RuleFor(p => p.AvailableColors, f => string.Join(",", f.PickRandom(new[] { "Red", "Blue", "Green", "Black", "White", "Yellow" }, f.Random.Int(1, 3)))); // Random colors

        // Generate the specified number of products
        var products = faker.Generate(count);
        // Add all products to database context
        _context.Products.AddRange(products);
        // Save changes to database
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Generated {count} products", count });
    }

    // GET /api/products - Returns all products from database
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // GET /api/products/search?query=term - Searches products by query string
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query)
    {
        // If no query provided, return all products
        if (string.IsNullOrWhiteSpace(query))
            return await GetProducts();

        // Search across multiple product fields using LIKE queries
        var products = await _context.Products
            .Where(p => p.Name.Contains(query) || // Search in product name
                       p.Description.Contains(query) || // Search in description
                       p.Category.Contains(query) || // Search in category
                       p.Brand.Contains(query) || // Search in brand
                       p.SKU.Contains(query)) // Search in SKU
            .ToListAsync();

        return products;
    }
}
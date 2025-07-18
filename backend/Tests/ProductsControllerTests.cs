using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductFilterApi.Controllers;
using ProductFilterApi.Data;
using ProductFilterApi.Models;
using Xunit;

namespace ProductFilterApi.Tests;

// Unit tests for ProductsController using in-memory database
public class ProductsControllerTests : IDisposable
{
    private readonly ProductContext _context;
    private readonly ProductsController _controller;

    // Setup test environment with in-memory database
    public ProductsControllerTests()
    {
        // Create unique in-memory database for each test
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new ProductContext(options);
        _controller = new ProductsController(_context);
    }

    // Test that GenerateProducts endpoint creates the correct number of products
    [Fact]
    public async Task GenerateProducts_ShouldCreateProducts()
    {
        // Act: Generate 10 products
        var result = await _controller.GenerateProducts(10);
        
        // Assert: Check response type and database count
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(10, await _context.Products.CountAsync());
    }

    // Test that search functionality returns correct filtered results
    [Fact]
    public async Task SearchProducts_ShouldReturnMatchingProducts()
    {
        // Arrange: Add test products to database
        _context.Products.AddRange(
            new Product { Name = "iPhone", Brand = "Apple", Category = "Electronics", Description = "Smartphone", SKU = "IP001" },
            new Product { Name = "Samsung Galaxy", Brand = "Samsung", Category = "Electronics", Description = "Android phone", SKU = "SG001" }
        );
        await _context.SaveChangesAsync();

        // Act: Search for "iPhone"
        var result = await _controller.SearchProducts("iPhone");
        var products = Assert.IsType<List<Product>>(result.Value);
        
        // Assert: Should return only the iPhone product
        Assert.Single(products);
        Assert.Equal("iPhone", products[0].Name);
    }

    // Test that GetProducts returns all products in database
    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Arrange: Add test products
        _context.Products.AddRange(
            new Product { Name = "Product1", Brand = "Brand1", Category = "Cat1", Description = "Desc1", SKU = "P001" },
            new Product { Name = "Product2", Brand = "Brand2", Category = "Cat2", Description = "Desc2", SKU = "P002" }
        );
        await _context.SaveChangesAsync();

        // Act: Get all products
        var result = await _controller.GetProducts();
        var products = Assert.IsType<List<Product>>(result.Value);
        
        // Assert: Should return both products
        Assert.Equal(2, products.Count);
    }

    // Cleanup: Dispose database context after each test
    public void Dispose()
    {
        _context.Dispose();
    }
}
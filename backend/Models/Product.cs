namespace ProductFilterApi.Models;

// Product entity representing items in the database
public class Product
{
    // Primary key for database
    public int Id { get; set; }
    // Product name - searchable field
    public string Name { get; set; } = string.Empty;
    // Product description - searchable field
    public string Description { get; set; } = string.Empty;
    // Product category - searchable field
    public string Category { get; set; } = string.Empty;
    // Product brand - searchable field
    public string Brand { get; set; } = string.Empty;
    // Product price in decimal format
    public decimal Price { get; set; }
    // Current stock level
    public int Stock { get; set; }
    // Available quantity
    public int Quantity { get; set; }
    // Stock Keeping Unit - searchable field
    public string SKU { get; set; } = string.Empty;
    // When product was released
    public DateTime ReleaseDate { get; set; }
    // Current availability status
    public string AvailabilityStatus { get; set; } = string.Empty;
    // Customer rating out of 5
    public double CustomerRating { get; set; }
    // Comma-separated available sizes
    public string AvailableSizes { get; set; } = string.Empty;
    // Comma-separated available colors
    public string AvailableColors { get; set; } = string.Empty;
}
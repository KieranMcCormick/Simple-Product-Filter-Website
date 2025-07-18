using Microsoft.EntityFrameworkCore;
using ProductFilterApi.Data;

// Create web application builder
var builder = WebApplication.CreateBuilder(args);

// Add MVC controllers to dependency injection
builder.Services.AddControllers();
// Configure SQLite database with Entity Framework
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlite("Data Source=products.db"));

// Configure CORS to allow simple HTML frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allow HTML file from anywhere
              .AllowAnyHeader() // Allow any HTTP headers
              .AllowAnyMethod(); // Allow any HTTP methods
    });
});

// Build the application
var app = builder.Build();

// Ensure database is created on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
    context.Database.EnsureCreated(); // Creates SQLite database file if it doesn't exist
}

// Configure middleware pipeline
app.UseCors("AllowAll"); // Enable CORS
app.UseRouting(); // Enable routing
app.MapControllers(); // Map controller endpoints

// Start server on port 5001
app.Run("http://localhost:5001");
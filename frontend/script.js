// Function to get products from the C# API
function getProducts(searchText = '') {
    // Choose which API endpoint to call
    let url;
    if (searchText) {
        // If user typed something, search for it
        url = `http://localhost:5001/api/products/search?query=${searchText}`;
    } else {
        // If no search text, get all products
        url = `http://localhost:5001/api/products`;
    }
    
    // Make HTTP request to C# API
    fetch(url)
        .then(response => response.json()) // Convert response to JavaScript object
        .then(data => {
            displayProducts(data); // Show the products on the page
        });
}

// Function to create 1000 sample products
function generateProducts() {
    // Call the C# API to generate fake products
    fetch('http://localhost:5001/api/products/generate', { method: 'POST' })
        .then(() => {
            // Hide the generate button since we now have data
            document.getElementById('generateBtn').style.display = 'none';
            // Load and display the new products
            getProducts();
        });
}

// Function to show products on the HTML page
function displayProducts(products) {
    // Get the div where we'll put the products
    const grid = document.getElementById('productsGrid');
    // Clear any existing products
    grid.innerHTML = '';
    
    // Loop through each product and create HTML for it
    products.forEach(product => {
        // Create a new div element for this product
        const div = document.createElement('div');
        // Set the HTML content for this product
        div.innerHTML = `
            <h3>${product.name}</h3>
            <p>${product.brand} - ${product.category}</p>
            <p>$${product.price}</p>
            <p>${product.description}</p>
            <hr>`;
        // Add this product div to the page
        grid.appendChild(div);
    });
}

// Set up event listeners when the page loads
document.addEventListener('DOMContentLoaded', function() {
    // Load products when page first opens
    getProducts();
    
    // Listen for when user types in the search box
    document.getElementById('searchInput').addEventListener('input', function() {
        const searchText = this.value; // Get what user typed
        // Wait 300ms before searching (debounce - prevents too many API calls)
        setTimeout(() => {
            getProducts(searchText); // Search for products
        }, 300);
    });
});
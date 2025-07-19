# Product Filter Web App

A dynamic product filtering application with C# backend API and simple HTML frontend.

## Features

- Generate 1000+ sample products with realistic data
- Real-time search across product properties (name, description, category, brand, SKU)
- Responsive layout
- Debounced search for optimal performance
- SQLite database for data persistence

## Architecture

### Backend (C# ASP.NET Core)
- **Database**: SQLite with Entity Framework Core
- **API Endpoints**:
  - `POST /api/products/generate` - Generate sample products
  - `GET /api/products` - Get all products
  - `GET /api/products/search?query={term}` - Search products
- **Data Generation**: Uses Bogus library for realistic fake data

### Frontend (HTML/JavaScript)
- Simple HTML page with search input
- Real-time API calls on user input changes
- Basic product display
- No frameworks or dependencies

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK

### Usage
1. Run `./run.sh` to start backend and open HTML frontend
2. Click "Generate Sample Products" to create test data
3. Use the search box to filter products in real-time

### Backend Setup
```bash
cd backend
dotnet restore
dotnet run
```
The API will be available at `http://localhost:5001`

### Frontend Setup
```bash
# Just open frontend/index.html in your browser
# Or use the run script which opens it automatically
```

## Testing

Run backend tests:
```bash
cd backend
dotnet test
```

## Performance Considerations

- **Database Indexing**: Consider adding indexes on searchable fields for large datasets
- **Search Debouncing**: 300ms delay prevents excessive API calls
- **CORS Configuration**: Configured for local development

## Assumptions & Trade-offs

### Assumptions
- SQLite sufficient for demo purposes
- Basic UI styling acceptable - ran out of time to learn/implement a react front end
- Search across text fields only (no advanced filtering)

### Trade-offs
- **Simplicity over Performance**: Used LIKE queries instead of full-text search for simplicity
- **Client-side vs Server-side**: Chose server-side filtering for scalability
- **Real-time vs Batch**: Real-time search for better UX despite more API calls

## Potential Improvements

1. **Performance**:
   - Add database indexes
   - Implement pagination
   - Add nicer front end in React

2. **Features**:
   - Advanced filtering (price range, category dropdown)
   - Sorting options
   - Product details modal

3. **Architecture**:
   - Implement proper logging
   - Add authentication/authorization

4. **Testing**:
   - Add integration tests
   - Frontend unit tests
   - E2E tests
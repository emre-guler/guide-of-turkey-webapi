# Guide of Turkey Web API

A web API service providing information about tourist destinations, cultural sites, and travel guides for Turkey.

## Description

Guide of Turkey is a RESTful Web API built with .NET Core that serves as a comprehensive digital guide for travelers and tourists interested in exploring Turkey. The API provides detailed information about various aspects of Turkey including tourist destinations, historical sites, cultural information, and travel recommendations.

## Technologies Used

- .NET Core
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (for data storage)
- Swagger/OpenAPI (for API documentation)

## Project Structure

- `Controllers/`: Contains API endpoint controllers
- `Models/`: Data models and DTOs
- `Data/`: Database context and configurations
- `Program.cs`: Application entry point
- `Startup.cs`: Application configuration and service setup

## Getting Started

### Prerequisites

- .NET Core SDK (Latest version)
- SQL Server
- Your preferred IDE (Visual Studio, VS Code, etc.)

### Installation

1. Clone the repository:
   ```bash
   git clone [repository-url]
   ```

2. Navigate to the project directory:
   ```bash
   cd guideofturkey-webapi
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Update the connection string in `appsettings.json` with your database details.

5. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` or `http://localhost:5000`

## API Documentation

Once the application is running, you can access the Swagger UI documentation at:
```
https://localhost:5001/swagger
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
# TodoList API

A simple REST API for managing todo items, built with ASP.NET Core and PostgreSQL.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) and Docker Compose
- (Optional for local development) PostgreSQL database

## Setup and Running

### Using Docker (Recommended)

1. Ensure Docker and Docker Compose are installed.

2. Clone or navigate to the project directory.

3. Run the application with Docker Compose:

   ```bash
   docker-compose up --build
   ```

   This will start the API on `http://localhost:5000` and PostgreSQL database on port 54321.

4. The API will be available at `http://localhost:5000/swagger` for interactive documentation.

### Local Development

1. Install .NET 9 SDK and PostgreSQL.

2. Set up a PostgreSQL database named `todo_db` with user `admin` and password `mbstdQrYo12A79Dkx0hI68lb` (or update the connection string in `appsettings.Development.json`).

3. Navigate to the project directory: `cd src/Todolist.Api`

4. Restore dependencies:

   ```bash
   dotnet restore
   ```

5. Run database migrations:

   ```bash
   dotnet ef database update
   ```

6. Run the application:

   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5000` (or the port specified in `launchSettings.json`).

7. Access Swagger UI at `http://localhost:5000/swagger` for API documentation.

## API Endpoints

The API provides the following endpoints:

- `POST /api/todos` - Create a new todo item
  - Request body: `{"title": "string", "description": "string?", "priority": "Low|Medium|High"}`
  - Response: 201 Created with todo details

- `GET /api/todos/{id}` - Get a specific todo by ID
  - Response: 200 OK with todo details or 404 Not Found

- `GET /api/todos` - Get all todos
  - Response: 200 OK with array of todos

- `PUT /api/todos/{id}` - Update a todo item
  - Request body: `{"title": "string", "description": "string?", "priority": "Low|Medium|High"}`
  - Response: 204 No Content or 404 Not Found

- `PATCH /api/todos/{id}/complete` - Mark a todo as completed
  - Response: 204 No Content or 404 Not Found

- `DELETE /api/todos/{id}` - Delete a todo item
  - Response: 204 No Content or 404 Not Found

## Building

To build the application:

```bash
dotnet build
```

For a release build:

```bash
dotnet publish -c Release
```

## Configuration

- Database connection string can be configured in `appsettings.json` or via environment variables.
- CORS is enabled for all origins in development.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- FluentValidation
- Swagger/OpenAPI

# TodoList API

REST API for managing todo items, built with ASP.NET Core and PostgreSQL.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) and Docker Compose
- (Optional for local development) PostgreSQL database

## Setup and Running

### Quick Start (Swagger)

1. Start the API (Docker or local; see below).
2. Open Swagger UI at `http://localhost:5000/swagger`.
3. Create an account via `POST /api/auth/register`, confirm the email, then login via `POST /api/auth/login`.
4. Use the returned bearer token to authorize Swagger (button at the top) and call `/api/todos/*`.

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

2. Set up a PostgreSQL database (or update the connection string in `src/Todolist.Api/appsettings.Development.json`).

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

## Using the API (curl)

All `/api/todos/*` endpoints require authentication (`Authorization: Bearer <access_token>`).

### 1) Register

```bash
curl -sS -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"you@example.com","password":"P@ssw0rd!"}'
```

### 2) Confirm email (OTP)

Email confirmation is required before login. The API sends an OTP to the email address via SMTP.

```bash
curl -sS -X POST http://localhost:5000/api/auth/confirm-email \
  -H "Content-Type: application/json" \
  -d '{"email":"you@example.com","otp":"123456"}'
```

If you didn’t receive the code, request a new one:

```bash
curl -sS -X POST http://localhost:5000/api/auth/resend-confirmation-email \
  -H "Content-Type: application/json" \
  -d '{"email":"you@example.com"}'
```

### 3) Login (get access token)

```bash
curl -sS -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"you@example.com","password":"P@ssw0rd!"}'
```

The response includes a bearer access token. Use it as:

```bash
export TOKEN="PASTE_ACCESS_TOKEN_HERE"
```

### 4) Create a todo

```bash
curl -sS -X POST http://localhost:5000/api/todos \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"title":"Buy milk","description":"2L","priority":"Low"}'
```

### 5) List todos

```bash
curl -sS http://localhost:5000/api/todos -H "Authorization: Bearer $TOKEN"
```

### Google Sign-In (optional)

If you use Google Identity Services in a browser, send the Google `id_token` (JWT) to:

```bash
curl -sS -X POST http://localhost:5000/api/auth/external/google \
  -H "Content-Type: application/json" \
  -d '{"idToken":"GOOGLE_ID_TOKEN_JWT"}'
```

The API validates the token audience against `Authentication:Google:ClientId` and returns a bearer token for this API.

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
- SMTP settings for confirmation emails are in `src/Todolist.Api/appsettings.Development.json` under `Email:Smtp`.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- FluentValidation
- Swagger/OpenAPI

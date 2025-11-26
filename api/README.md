# GitNguonMo API (ASP.NET Core)

This folder contains a minimal ASP.NET Core Web API that provides simple in-memory endpoints for the sample site.

Features
- GET `/api/products` — list products
- GET `/api/products/{id}` — get product detail
- POST `/api/auth/register` — register user (in-memory)
- POST `/api/auth/login` — login (returns demo token)
- POST `/api/cart/add` — add to cart (demo)

Quick start (PowerShell)

1. Ensure you have .NET SDK installed (recommended .NET 8). Check with:

```powershell
dotnet --version
```

2. From the workspace root run:

```powershell
cd .\api
dotnet restore
dotnet run --urls "http://localhost:5000"
```

3. The API will be available at `http://localhost:5000` (Swagger UI at `/swagger` in development).

Notes
- This is a demo in-memory API — no database, no persistent authentication. For production use, add a database and proper authentication (JWT).
- CORS is enabled for common local dev hosts (`http://localhost:8000`, `http://localhost:3000`, `http://127.0.0.1:5500`). Adjust as needed.

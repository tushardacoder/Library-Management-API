# Library Management API

A RESTful Library Management API built with ASP.NET Core Web API and Entity Framework Core. The application allows users to manage books, register members, borrow and return books, and track active loans.

## Features

* Manage books (Create, Read, Update, Delete)
* Register and manage library members
* Borrow available books
* Return borrowed books
* View active loan records
* Entity Framework Core integration

---

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* C#
* Swagger/OpenAPI

---

## Project Structure

```text
LibraryManagementAPI/
│
├── Controllers/
│   ├── BooksController.cs
│   ├── MembersController.cs
│   └── LoansController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   ├── Book.cs
│   ├── Member.cs
│   └── Loan.cs
│
├── DTOs/
├── Services/
├── Migrations/
│
├── appsettings.json
└── Program.cs
```

---

## Database Setup

Create the database:

```sql
CREATE DATABASE LibraryManagementDb;
```

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## Running Migrations

Create a migration:

```bash
dotnet ef migrations add InitTeamModule --project Src/Library_APIs --startup-project Src/Library_APIs
```

Update the database:

```bash
dotnet ef database update --project Src/Library_APIs
```

---

## Running the Application

```bash
dotnet run
```

Swagger UI:

```text
https://localhost:5001/swagger
```

---

## API Endpoints

### Books

```http
GET    /api/books
GET    /api/books/{id}
POST   /api/books
PUT    /api/books/{id}
DELETE /api/books/{id}
```

### Members

```http
GET    /api/members
GET    /api/members/{id}
POST   /api/members
PUT    /api/members/{id}
DELETE /api/members/{id}
```

### Loans

```http
POST /api/loans/borrow
POST /api/loans/return
GET  /api/loans/active
```

---

## Example Workflow

### Add a Book

```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin"
}
```

### Register a Member

```json
{
  "name": "John Doe",
  "email": "john@example.com"
}
```

### Borrow a Book

```json
{
  "bookId": 1,
  "memberId": 1
}
```

### View Active Loans

```http
GET /api/loans/active
```

---

## Future Improvements

* JWT Authentication & Authorization
* Book Categories
* Reservation System
* Fine Management
* Search & Filtering
* Pagination
* Audit Logging
* Email Notifications
* Docker Support
* Unit Testing
* CI/CD Pipeline

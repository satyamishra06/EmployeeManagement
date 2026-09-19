# Employee Management API

A RESTful Employee Management API built with **C#**, **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

The project demonstrates practical backend development concepts including CRUD operations, RESTful API design, database integration, input validation, HTTP status codes, Entity Framework Core migrations, and OpenAPI documentation.

---

## 📌 Overview

The **Employee Management API** provides a backend service for managing employee records.

### Core Operations

- Create employees
- Retrieve all employees
- Retrieve an employee by ID
- Update employee information
- Delete employees

The API also includes request validation, SQL Server persistence, Entity Framework Core Code First migrations, and OpenAPI documentation.

---

## 🚀 Features

- RESTful API architecture
- Complete CRUD operations
- SQL Server database integration
- Entity Framework Core
- Code First database migrations
- Input validation using Data Annotations
- Automatic `400 Bad Request` validation responses
- `404 Not Found` handling
- `201 Created` response for successful employee creation
- Decimal salary precision configuration
- OpenAPI documentation
- API testing using VS Code REST Client
- Clean controller, model, and data-layer structure

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| **C#** | Programming language |
| **ASP.NET Core Web API** | Backend framework |
| **Entity Framework Core** | ORM / database access |
| **SQL Server Express** | Relational database |
| **REST API** | API architecture |
| **OpenAPI** | API documentation |
| **VS Code REST Client** | API testing |
| **Git & GitHub** | Version control |

---

## 📂 Project Structure

```text
EmployeeManagement/
│
├── Controllers/
│   └── EmployeesController.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Models/
│   └── Employee.cs
│
├── Migrations/
│   ├── ...
│
├── EmployeeManagement.http
├── Program.cs
├── appsettings.json
├── EmployeeManagement.csproj
├── .gitignore
└── README.md
```

### Folder Responsibilities

- **Controllers** — Handles HTTP requests and API endpoints.
- **Models** — Contains application entities such as `Employee`.
- **Data** — Contains the Entity Framework Core database context.
- **Migrations** — Contains database schema migration files.

---

## 👨‍💻 Employee Model

Each employee contains:

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Unique employee identifier |
| `Name` | `string` | Employee name |
| `Email` | `string` | Employee email |
| `Department` | `string` | Employee department |
| `Salary` | `decimal` | Employee salary |

---

## 🔗 API Endpoints

Base URL:

```text
http://localhost:5125/api/employees
```

| Method | Endpoint | Description | Success |
|---|---|---|---|
| `GET` | `/api/employees` | Get all employees | `200 OK` |
| `GET` | `/api/employees/{id}` | Get employee by ID | `200 OK` |
| `POST` | `/api/employees` | Create employee | `201 Created` |
| `PUT` | `/api/employees/{id}` | Update employee | `200 OK` |
| `DELETE` | `/api/employees/{id}` | Delete employee | `200 OK` |

---

## 📥 Create Employee

### Request

```http
POST /api/employees
Content-Type: application/json
```

### Request Body

```json
{
  "name": "Satya Mishra",
  "email": "satya@example.com",
  "department": "IT",
  "salary": 50000
}
```

### Response

```text
201 Created
```

Example:

```json
{
  "id": 1,
  "name": "Satya Mishra",
  "email": "satya@example.com",
  "department": "IT",
  "salary": 50000
}
```

---

## 📤 Get All Employees

### Request

```http
GET /api/employees
```

### Response

```text
200 OK
```

Example:

```json
[
  {
    "id": 1,
    "name": "Satya Mishra",
    "email": "satya@example.com",
    "department": "IT",
    "salary": 50000
  }
]
```

---

## 🔎 Get Employee By ID

### Request

```http
GET /api/employees/1
```

### Response

```text
200 OK
```

Example:

```json
{
  "id": 1,
  "name": "Satya Mishra",
  "email": "satya@example.com",
  "department": "IT",
  "salary": 50000
}
```

If the employee does not exist:

```text
404 Not Found
```

---

## ✏️ Update Employee

### Request

```http
PUT /api/employees/1
Content-Type: application/json
```

### Request Body

```json
{
  "name": "Satya Mishra Updated",
  "email": "satya.updated@example.com",
  "department": "Software Development",
  "salary": 60000
}
```

### Response

```text
200 OK
```

---

## 🗑️ Delete Employee

### Request

```http
DELETE /api/employees/1
```

### Response

```text
200 OK
```

If the employee does not exist:

```text
404 Not Found
```

---

## ✅ Input Validation

The API uses ASP.NET Core Data Annotations to validate employee information.

### Validation Rules

- `Name` is required.
- `Email` is required.
- `Email` must have a valid email format.
- `Department` is required.
- `Salary` must be between `0` and `10000000`.

### Invalid Request Example

```json
{
  "name": "",
  "email": "wrong-email",
  "department": "",
  "salary": -5000
}
```

The API returns:

```text
400 Bad Request
```

with details about the validation errors.

---

## 🗄️ Database

The application uses **SQL Server Express** with **Entity Framework Core**.

### Database

```text
EmployeeManagementDb
```

### Database Approach

The project uses the **Code First** approach with Entity Framework Core migrations.

### Create Migration

```bash
dotnet ef migrations add InitialCreate
```

### Apply Migration

```bash
dotnet ef database update
```

---

## 💾 Database Configuration

The connection string is configured in:

```text
appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> For production applications, database credentials and connection strings should be stored securely rather than committed to source control.

---

## 📐 Salary Precision

Employee salary is explicitly configured with a precision of **18 total digits** and **2 digits after the decimal point**.

```csharp
.HasPrecision(18, 2);
```

This allows values such as:

```text
50000.00
60000.50
125000.75
```

---

## 📖 OpenAPI Documentation

The project uses ASP.NET Core OpenAPI support.

When running in Development mode, the OpenAPI specification is available at:

```text
http://localhost:5125/openapi/v1.json
```

---

## 🧪 API Testing

API endpoints can be tested using the included:

```text
EmployeeManagement.http
```

The project uses the **VS Code REST Client** extension for testing.

Example:

```http
### Get All Employees
GET http://localhost:5125/api/employees
Accept: application/json
```

The tested workflow covers:

1. Create employee
2. Get all employees
3. Get employee by ID
4. Update employee
5. Verify updated employee
6. Delete employee
7. Verify employee is no longer available
8. Test validation errors
9. Test `404 Not Found` scenarios

---

## ⚙️ Getting Started

### Prerequisites

Install the following:

- .NET SDK
- SQL Server Express
- Visual Studio Code
- VS Code REST Client extension
- Git

### 1. Clone the Repository

```bash
git clone https://github.com/satyamishra06/EmployeeManagement.git
```

### 2. Navigate to the Project

```bash
cd EmployeeManagement
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Apply Database Migrations

```bash
dotnet ef database update
```

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the API

```bash
dotnet run
```

The API will start on the configured local development URL.

---

## 🧭 API Workflow

```text
Client
   │
   ▼
HTTP Request
   │
   ▼
EmployeesController
   │
   ▼
AppDbContext
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
   │
   ▼
HTTP Response
```

---

## 🏗️ Architecture

The project follows a simple layered backend structure:

```text
API Layer
    │
    └── Controllers
            │
            ▼
Data Access Layer
    │
    └── AppDbContext
            │
            ▼
Database
    │
    └── SQL Server
```

---

## 🔐 HTTP Status Codes

| Status Code | Meaning |
|---|---|
| `200 OK` | Request completed successfully |
| `201 Created` | Employee successfully created |
| `400 Bad Request` | Invalid request data |
| `404 Not Found` | Employee does not exist |

---

## 🎯 Learning Objectives

This project demonstrates practical understanding of:

- C# fundamentals
- Object-Oriented Programming
- ASP.NET Core Web API
- RESTful API design
- HTTP methods and status codes
- Entity Framework Core
- SQL Server
- Database migrations
- Data validation
- Dependency Injection
- Asynchronous programming
- API testing
- OpenAPI documentation
- Git and GitHub

---

## 🚧 Future Improvements

Potential future enhancements include:

- Global exception handling middleware
- Service layer architecture
- Repository pattern
- Pagination
- Employee search and filtering
- Sorting
- Authentication and authorization
- JWT-based authentication
- Role-based access control
- Unit and integration testing
- Docker support
- CI/CD pipeline
- Production deployment

---

## 👨‍💻 Author

**Satya Mishra**

B.Tech Computer Science & Engineering (AI/ML)  
ITM University, Gwalior

- **GitHub:** https://github.com/satyamishra06
- **LinkedIn:** https://linkedin.com/in/satyamishra

---

## 📄 License

This project is intended for learning and portfolio purposes.

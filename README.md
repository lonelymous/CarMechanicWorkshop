# CarMechanicWorkshop

## 📖 Table of Contents
- [Description](#-description)
- [Installation](#-installation)
- [Database](#-database)
- [API Endpoints](#-api-endpoints)

---

## 📋 Description
This is the exam project application for the C# course at University of Debrecen in 2025 Autumn.

---

## ⚙️ Installation

```bash
# Clone repository
git clone https://github.com/lonelymous/CarMechanicWorkshop.git
cd CarMechanicWorkshop

# Install dependencies
dotnet restore

# Run the tests 
dotnet test

# Run the sub project (for Windows or Linux)
cd sub_project
dotnet run

# Run the whole project with Docker
docker-compose up -d --build
```

## 🗄️ Database

```mermaid
erDiagram
    CLIENTS {
        int Id PK
        string Name "required, min_length: 2"
        string Address "required"
        string Email "required, valid: email"
    }

    JOBS {
        int Id PK
        int ClientId FK "required"
        Client Client
        string LicensePlate "required, pattern: XXX-000"
        int ManufacturingYear "min: 1900" 
        enum Category "required"
        string Description "required, min_length: 5"
        int Severity "range: 1-10"
        enum Status
    }

    %% Relationships
    CLIENTS ||--o{ JOBS : "has"
```

## 📚 API Endpoints
A teljes OpenAPI/Swagger dokumentáció elérhető itt:  
➡️ [Swagger UI](http://localhost:8080/swagger)

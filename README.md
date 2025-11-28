# SubSentinel 🛡️
### Automated Subscription & Warranty Tracker API

**SubSentinel** is a .NET Web API designed to prevent financial loss from forgotten subscriptions and expiring warranties. Unlike standard CRUD applications, it features an intelligent **Background Hosted Service** that automatically scans the database daily to detect upcoming renewals and triggers alert logs.

![.NET Status](https://img.shields.io/badge/.NET-10.0%20(Preview)-purple)
![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)
![Tests](https://img.shields.io/badge/Tests-Passing-brightgreen)

## 🚀 Key Features

* **🤖 Automated Expiration Detection:** Implements a `BackgroundService` (Singleton) that runs independently of HTTP requests to scan for expiring items.
* **💉 Advanced Dependency Injection:** Solves the "Scoped vs. Singleton" mismatch by manually managing `IServiceScopeFactory` to inject Scoped EF Core contexts into the background worker.
* **🛡️ Secure Data Validation:** Uses **DTOs (Data Transfer Objects)** to decouple the internal database schema from the public API, preventing over-posting attacks.
* **🧪 Unit Testing:** Includes automated tests using **xUnit** and **EF Core In-Memory** database to verify business logic in isolation.
* **📄 OpenAPI / Swagger:** Fully documented API endpoints for easy integration.

## 🛠️ Tech Stack

* **Framework:** .NET 10 (Preview) / ASP.NET Core Web API
* **Language:** C#
* **Database:** SQLite (Entity Framework Core)
* **Testing:** xUnit, EF Core InMemory
* **Architecture:** MVC Controller-based Architecture

## 📂 Project Structure

```text
SubSentinel
├── SubSentinel.API          # Main Web API Project
│   ├── Controllers          # API Endpoints
│   ├── Data                 # DB Context & Migrations
│   ├── DTOs                 # Data Transfer Objects (Validation)
│   ├── Models               # Domain Entities
│   ├── Services             # Background Hosted Services
│   └── Program.cs           # Dependency Injection Setup
├── SubSentinel.Tests        # Unit Test Project
│   └── SubscriptionsControllerTests.cs
└── SubSentinel.sln
```

## 🏃‍♂️ Getting Started

### Prerequisites
* .NET SDK (Version 10 or 8)
* Visual Studio Code or Visual Studio 2022

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/YOUR_USERNAME/SubSentinel.git](https://github.com/YOUR_USERNAME/SubSentinel.git)
    cd SubSentinel
    ```

2.  **Restore Dependencies**
    ```bash
    dotnet restore
    ```

3.  **Run the Application**
    ```bash
    dotnet run --project SubSentinel.API
    ```

4.  **Access the API**
    Open your browser to the Swagger UI to test the endpoints:
    * URL: `http://localhost:5xxx/swagger/index.html`
    *(Check your terminal for the specific port number, usually 5271 or similar)*

## 🧪 Running Tests

This project uses **xUnit** with an **In-Memory Database** to ensure the API logic works without polluting the real database.

```bash
dotnet test
```

## 🧠 Technical Highlights (For Interviewers)

### The Background Service Challenge
One of the core challenges in this project was implementing the `SubscriptionRenewalService`. Since `BackgroundService` is a **Singleton** (lives for the app's lifetime) and `AppDbContext` is **Scoped** (lives for a single request), I could not inject the database directly.

**Solution:** I implemented `IServiceScopeFactory` to create a temporary scope every time the background timer triggers. This allows the service to grab a fresh instance of the database, check for expirations, and then dispose of the scope correctly to prevent memory leaks.

```csharp
using (var scope = _scopeFactory.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Logic to check expiring subscriptions...
}
```

---
*Built by Farhan Zarif as a showcase of .NET Backend Engineering skills.*
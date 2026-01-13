# Paybook

**Payment book for ledger** — a modular .NET application implementing ledger and payment tracking using layered architecture.

## 📌 Overview

`Paybook` is a ledger and payment‑tracking application built using ASP.NET and C#.  
The solution follows a layered architecture separating **Business**, **Service**, **Database**, and **UI** concerns for better scalability and maintainability.

Repository: https://github.com/arpitsdotnet/Paybook

---

## 🧱 Architecture

```
Paybook
├── Paybook.DatabaseLayer         # Data access, repositories, ORM
├── Paybook.SqlStructureLayer     # SQL scripts & DB schema
├── Paybook.BusinessLayer         # Core business & ledger rules
├── Paybook.ServiceLayer          # Application & API services
├── Paybook.Web.MvcUI             # ASP.NET MVC UI
├── Paybook.WebUI                 # Web front‑end
└── Paybook.*.Tests               # Unit / integration tests
```

---

## 🚀 Features

### Ledger & Accounts
- Create and manage ledger accounts
- Maintain account balances
- Debit and credit handling
- Ledger consistency rules

### Transactions
- Record financial transactions
- Transaction history
- Account‑to‑account transfers

### Business Logic
- Centralized domain logic
- Clean separation of concerns
- Reusable business services

### Service Layer
- Application services for UI & external consumers
- Clear service contracts
- Validation and orchestration

### User Interface
- ASP.NET MVC UI
- Ledger and transaction views
- Dashboard‑style screens

### Testing
- Dedicated test projects
- Business and service layer validation

---

## 📦 Installation

### Prerequisites
- .NET Framework / .NET Core (as per solution)
- SQL Server
- Visual Studio 2019+

### Clone Repository
```bash
git clone https://github.com/arpitsdotnet/Paybook.git
cd Paybook
```

### Database Setup
1. Execute scripts from `Paybook.SqlStructureLayer`
2. Configure connection strings in Web & Service projects

### Build & Run
```bash
dotnet build Paybook.sln
dotnet run --project Paybook.Web.MvcUI
```

---

## 📌 Sample Usage (Conceptual)

```csharp
// Create account
accountService.CreateAccount("Cash");

// Post transaction
transactionService.PostTransaction(
    fromAccount: "Cash",
    toAccount: "Bank",
    amount: 1000,
    description: "Deposit"
);

// Get balance
var balance = accountService.GetBalance("Cash");
```

---

## 🧩 Layer Responsibilities

### BusinessLayer
- Ledger rules
- Account validation
- Transaction logic

### ServiceLayer
- Application services
- API orchestration
- DTO mapping

### DatabaseLayer
- Data persistence
- Repositories
- Migrations

### Web UI
- Controllers
- Views
- User interaction

---

## 🛠 Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests where applicable
4. Submit a pull request

---

## 📄 License

License not specified. Please contact the repository owner for usage permissions.

---

## 👤 Author

**Arpit**  
Senior .NET Backend Developer

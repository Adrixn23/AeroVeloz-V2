# Environment Setup & Installation Guide

Welcome to the **AeroVeloz** installation guide. This document contains all the necessary instructions to get the complete system (API and Web Frontend) up and running on your local machine.

---

## 📋 Prerequisites

Before starting, ensure your development environment has the following software installed:
- [**.NET 9.0 SDK**](https://dotnet.microsoft.com/download/dotnet/9.0) or higher.
- **SQL Server Express (LocalDB)** or any SQL Server instance (Docker container).
- [**Visual Studio 2022**](https://visualstudio.microsoft.com/) (Version 17.12+) or **JetBrains Rider**.
- [**Git**](https://git-scm.com/downloads)

---

## 🛠️ Step 1: Project Initialization

First, clone the repository to your local machine:

```bash
git clone https://github.com/your-username/AeroVeloz.git
cd AeroVeloz
```

The solution is divided into multiple class libraries. It is highly recommended to open the **`AeroVeloz.slnx`** file in Visual Studio or run `dotnet build AeroVeloz.slnx` from the CLI to ensure all dependencies are restored.

---

## 🔐 Step 2: Configure User Secrets

To prevent sensitive credentials (like database connection strings and JWT keys) from being accidentally committed to source control, **AeroVeloz** uses `.NET User Secrets` exclusively for local development configuration.

You must configure these secrets for the **API Project** (`Api\AeroVeloz.Api`). Open a terminal inside `Api\AeroVeloz.Api` and execute the following commands:

### 1. Database Connection String
Set your SQL Server connection string. Replace the server details if you are not using LocalDB.

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=AeroVeloz;Trusted_Connection=True;TrustServerCertificate=True"
```

### 2. External Services (Optional)
If your instance uses external flight data APIs (e.g., AirLabs), you can configure the Key:

```bash
dotnet user-secrets set "AirLabs:ApiKey" "your-secret-key-here"
```

*(Note: JWT configurations and SMTP server settings are already preset for development in `appsettings.json`, but can be overridden here if required).*

---

## 🗄️ Step 3: Database Creation & Seeding

The project uses **Entity Framework Core (Code-First Approach)**. To create the database and seed the initial roles, permissions, flight states, and the master administrator organization, you must apply the EF Migrations.

Run this command from the `Infraestructure\AeroVeloz.Infraestructure` directory, pointing to the API as the startup project:

```bash
cd Infraestructure\AeroVeloz.Infraestructure
dotnet ef database update --startup-project ../../Api/AeroVeloz.Api
```


> **Tip:** If the command succeeds, you will see a database named `AeroVeloz` in your SQL Server instance, pre-populated with data.

---

## 🚀 Step 4: Running the System

The AeroVeloz architecture is distributed. You need to run both the **API Backend** and the **Web Frontend** simultaneously.

### Running the API (Backend)
Open a terminal in the root project folder:
```bash
dotnet run --project Api/AeroVeloz.Api
```
*The API will usually start at `https://localhost:5001` or `7024`.*

### Running the Web Application (Frontend)
Open a second terminal in the root project folder:
```bash
dotnet run --project Presentacion/AeroVeloz.Web
```
*The Web App will usually start at `https://localhost:7001`.*

---

## 🔑 Step 5: Initial Login

Once the Web Application is running, open your browser and navigate to `https://localhost:7001`.

Click on **Login** and use the following seeded master credentials:

- **Email Corporativo (Organization):** `Admin@Aeroveloz.com`
- **Username:** `AdminTest`
- **Password:** `Admin123!`

You will be redirected to the Super Admin Dashboard where you can create more users, airlines, and manage flights!
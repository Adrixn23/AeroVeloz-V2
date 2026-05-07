# AeroVeloz - Environment Setup & Installation Guide

<div align="center">

**Complete Setup Instructions for AeroVeloz Flight Management System**

*Step-by-step guide for developers and deployment teams*

</div>

---

## 📋 Table of Contents

1. [System Requirements](#system-requirements)
2. [Prerequisites](#prerequisites)
3. [Installation Steps](#installation-steps)
4. [Database Setup](#database-setup)
5. [Configuration](#configuration)
6. [Running the Application](#running-the-application)
7. [User Secrets Management](#user-secrets-management)
8. [Troubleshooting](#troubleshooting)
9. [Verification](#verification)
10. [Production Deployment](#production-deployment)

---

## 💻 System Requirements

### Minimum Requirements
| Component | Requirement |
|-----------|-------------|
| **Operating System** | Windows 10 Build 19041 or later |
| **RAM** | 4 GB minimum (8 GB recommended) |
| **Disk Space** | 2 GB free space (SSD recommended) |
| **.NET Runtime** | .NET 9.0 or higher |
| **SQL Server** | SQL Server 2019 or higher |

### Recommended Specifications
| Component | Recommendation |
|-----------|---------------|
| **Operating System** | Windows 11 or Windows Server 2022 |
| **RAM** | 8 GB or more |
| **Disk Space** | SSD with 5+ GB free space |
| **.NET SDK** | .NET 9.0 LTS |
| **SQL Server** | SQL Server 2019 Express or Developer Edition |
| **Visual Studio** | Visual Studio 2022 (Community or Professional) |

---

## 📦 Prerequisites

### 1. Install .NET 9.0 SDK

#### Windows via Installer
1. Visit [dotnet.microsoft.com](https://dotnet.microsoft.com/download)
2. Download .NET 9.0 SDK (Windows x64)
3. Run the installer and follow prompts
4. Restart your computer

#### Verify Installation
```powershell
# Open PowerShell and run:
dotnet --version
dotnet --list-sdks

# Expected output:
# 9.0.0 [C:\Program Files\dotnet\sdk]
```

#### Update .NET Runtime (if needed)
```powershell
# Check installed runtimes
dotnet --list-runtimes

# Update NuGet to latest
dotnet nuget update source nuget.org -u
```

### 2. Install SQL Server

#### SQL Server 2019 Express (Free)
1. Download from [Microsoft SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Run installer
3. Choose "Express" edition
4. Select "Install SQL Server 2019 Express"
5. Complete installation with default settings

#### SQL Server Management Studio (SSMS)
1. Download [SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
2. Install and launch
3. Connect to local instance: `(LocalDB)\MSSQLLocalDB` or `.\SQLEXPRESS`

#### Verify Database Connection
```powershell
# Test connection
sqlcmd -S .\SQLEXPRESS -i testquery.sql

# Or use SSMS GUI to test connection
```

### 3. Install Visual Studio 2022

#### Download
1. Go to [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/)
2. Download "Visual Studio Community 2022"

#### Installation Steps
1. Run the installer
2. Select "ASP.NET and web development" workload
3. Select "Desktop development with C++" workload (includes Windows SDK)
4. Select ".NET desktop development" workload
5. Ensure Windows 10 SDK is included
6. Complete installation

#### Required Visual Studio Extensions
```powershell
# Optional but recommended:
# - Entity Framework Core Power Tools
# - NuGet Package Manager
# - GitHub Copilot (optional)
```

### 4. Install Git

#### Windows via Installer
1. Download [Git for Windows](https://git-scm.com/download/win)
2. Run installer with default options
3. Complete installation

#### Verify Installation
```bash
git --version
# Expected: git version 2.x.x...
```

---

## 🚀 Installation Steps

### Step 1: Clone the Repository

```powershell
# Navigate to desired directory
cd C:\Users\YourUsername\Documents

# Clone the repository
git clone https://github.com/JoelZhub/AeroVeloz.git
cd AeroVeloz

# Switch to app-desktop branch (if not already on it)
git checkout app-desktop
```

### Step 2: Verify Repository Structure

```powershell
# Verify key directories exist
Get-ChildItem -Directory

# Expected output:
# - Core
# - Infrastructure
# - IOC
# - Presentacion
# - Api
# - Transversal
# - Screenshots
# - docs
```

### Step 3: Restore NuGet Packages

```powershell
# Restore all project dependencies
dotnet restore

# Expected output: Restore completed in X.XXs

# Verify no restore errors
dotnet build --no-restore --dry-run
```

### Step 4: Open in Visual Studio

```powershell
# Open solution in Visual Studio
Start-Process ".\AeroVeloz.sln"

# Or open manually:
# 1. Start Visual Studio
# 2. File → Open → Project/Solution
# 3. Navigate to AeroVeloz.sln
# 4. Click Open
```

### Step 5: Solution Exploration

In Visual Studio Solution Explorer, verify:

```
AeroVeloz (Solution)
├── Core
│   ├── AeroVeloz.Domain
│   └── AeroVeloz.Application
├── Infrastructure
│   └── AeroVeloz.Infrastructure
├── Presentation
│   └── AeroVeloz.Desktop
├── Api
│   └── AeroVelozDesktop.Api
├── IOC
│   └── AeroVeloz.IOC
├── Transversal
│   └── AeroVeloz.Transversal
└── docs
```

---

## 🗄️ Database Setup

### Step 1: Create Database

#### Via SQL Server Management Studio (GUI)
1. Open SSMS
2. Connect to your SQL Server instance
3. Right-click "Databases" → "New Database"
4. **Database name**: `AeroVelozDB`
5. Click OK

#### Via Command Line (SQL)
```sql
-- Create database
CREATE DATABASE [AeroVelozDB];

-- Create login (if needed)
CREATE LOGIN AeroVelozUser WITH PASSWORD = 'YourSecurePassword123!';

-- Create user for database
USE [AeroVelozDB];
CREATE USER [AeroVelozUser] FOR LOGIN [AeroVelozUser];

-- Grant permissions
ALTER ROLE db_owner ADD MEMBER [AeroVelozUser];
```

#### Via PowerShell
```powershell
$sqlServer = ".\SQLEXPRESS"
$database = "AeroVelozDB"

# Create database
sqlcmd -S $sqlServer -i create_database.sql

# Verify creation
sqlcmd -S $sqlServer -Q "SELECT name FROM sys.databases WHERE name = '$database'"
```

### Step 2: Configure Connection String

#### In Visual Studio
1. Right-click `AeroVeloz.Infraestructure` project
2. Select "User Secrets" (opens `secrets.json`)
3. Add or update:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AeroVelozDB;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

#### Alternative: appsettings.json
Edit `Infraestructure/AeroVeloz.Infraestructure/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AeroVelozDB;Integrated Security=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Step 3: Apply Database Migrations

```powershell
# Open Package Manager Console in Visual Studio
# Tools → NuGet Package Manager → Package Manager Console

# Set Infrastructure project as default
# Console dropdown: Select "AeroVeloz.Infrastructure"

# Create initial migration (if not exists)
Add-Migration InitialCreate

# Apply migrations to database
Update-Database

# Verify migration success
# Should see output: "Done."
```

#### Via Command Line
```bash
cd Infraestructure/AeroVeloz.Infraestructure

# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Verify tables were created
dotnet ef dbcontext info
```

### Step 4: Seed Initial Data (Optional)

```powershell
# In Package Manager Console:
# If seeding method exists in DbContext
Update-Database -Verbose

# Or manually insert initial data
sqlcmd -S .\SQLEXPRESS -d AeroVelozDB -i seed_data.sql
```

### Step 5: Verify Database

#### In SSMS
1. Connect to SQL Server
2. Expand Databases
3. Expand AeroVelozDB
4. Verify tables exist:
   - AspNetUsers
   - AspNetRoles
   - Airports
   - Airlines
   - Flights
   - OperationChanges
   - Audits
   - Subscriptions

#### Via SQL Query
```sql
USE [AeroVelozDB];

-- List all tables
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';

-- Count records in each table
SELECT 
    TABLE_NAME,
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = t.TABLE_NAME) AS RecordCount
FROM INFORMATION_SCHEMA.TABLES t;
```

---

## ⚙️ Configuration

### Step 1: User Secrets Setup

User Secrets store sensitive configuration locally (not committed to Git).

```powershell
# Navigate to Desktop project
cd Presentacion/AeroVeloz.Desktop

# Initialize user secrets (creates secrets.json)
dotnet user-secrets init

# Verify UserSecretsId in project file
# Edit AeroVeloz.Desktop.csproj and ensure <UserSecretsId> is present
```

### Step 2: Configure Secrets

```powershell
# JWT Configuration
dotnet user-secrets set "JwtSettings:SecretKey" "your-minimum-32-character-secret-key-here-xxxxxxxxxxxxxxxx"
dotnet user-secrets set "JwtSettings:Issuer" "AeroVeloz"
dotnet user-secrets set "JwtSettings:Audience" "AeroVelozClient"
dotnet user-secrets set "JwtSettings:ExpirationMinutes" "60"

# API Configuration
dotnet user-secrets set "ApiSettings:BaseUrl" "http://localhost:5000"
dotnet user-secrets set "ApiSettings:TimeoutSeconds" "30"

# Database Connection
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\\SQLEXPRESS;Database=AeroVelozDB;Integrated Security=true;TrustServerCertificate=true;"

# Optional: Notification Settings
dotnet user-secrets set "NotificationSettings:EmailProvider:Enabled" "true"
dotnet user-secrets set "NotificationSettings:EmailProvider:SmtpServer" "smtp.gmail.com"
dotnet user-secrets set "NotificationSettings:EmailProvider:Port" "587"
dotnet user-secrets set "NotificationSettings:EmailProvider:Username" "your-email@gmail.com"
dotnet user-secrets set "NotificationSettings:EmailProvider:Password" "your-app-password"
```

### Step 3: Verify Secrets

```powershell
# List all secrets
dotnet user-secrets list

# Verify specific secret
dotnet user-secrets list | findstr "JwtSettings"

# Expected output should show configured secrets
```

### Step 4: Application Settings

#### appsettings.json (Default Environment)
Create or update `Presentacion/AeroVeloz.Desktop/appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "System": "Warning"
    }
  },
  "ApplicationSettings": {
    "ApplicationName": "AeroVeloz",
    "Version": "1.0.0",
    "Environment": "Development"
  },
  "Features": {
    "EnableAudit": true,
    "EnableNotifications": true,
    "EnableRealTimeUpdates": true
  }
}
```

#### appsettings.Production.json (Production Environment)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error"
    }
  },
  "ApplicationSettings": {
    "ApplicationName": "AeroVeloz",
    "Version": "1.0.0",
    "Environment": "Production"
  }
}
```

---

## ▶️ Running the Application

### Step 1: Build Solution

```bash
# Navigate to solution root
cd C:\Path\To\AeroVeloz

# Clean previous builds
dotnet clean

# Build entire solution
dotnet build

# Expected output: Build succeeded. X warnings, 0 errors
```

### Step 2: Run Desktop Application

#### Via Visual Studio
1. In Solution Explorer, right-click `AeroVeloz.Desktop` project
2. Select "Set as Startup Project"
3. Press F5 or click "Start Debugging"
4. Application launches in debug mode

#### Via Command Line
```powershell
cd Presentacion/AeroVeloz.Desktop

# Run application
dotnet run

# Run in Release mode
dotnet run -c Release
```

### Step 3: Initial Login

#### First-Time Setup
1. Application launches to Login screen
2. Default credentials (if seeded):
   - **Username**: `admin`
   - **Password**: `Admin@123`

#### Create First User (if no seed)
```powershell
# Use Package Manager Console or API endpoint
# POST /api/auth/register
# {
#   "username": "admin",
#   "email": "admin@aeroveloz.com",
#   "password": "SecurePassword123!",
#   "role": "SuperAdmin"
# }
```

### Step 4: Navigate Application

After login:
1. **Super Admin**: Access Super Admin Dashboard
   - User Management
   - Airport Management
   - System Audit Logs
   - Global Statistics

2. **Airport Admin**: Access Airport Dashboard
   - Flight Operations
   - Airline Connections
   - Airport-specific Audit Logs
   - Local Statistics

3. **System User**: Read-only Access
   - View Flights
   - View Operations
   - View Limited Audit Logs

---

## 🔐 User Secrets Management

### Location of Secrets File

```
Windows: %APPDATA%\Microsoft\UserSecrets\{UserSecretsId}\secrets.json
Linux: ~/.microsoft/usersecrets/{UserSecretsId}/secrets.json
macOS: ~/.microsoft/usersecrets/{UserSecretsId}/secrets.json
```

### Backup Secrets

```powershell
# Export secrets to file
$secretPath = "$env:APPDATA\Microsoft\UserSecrets\{UserSecretsId}"
Copy-Item -Path $secretPath -Destination "C:\Backups\AeroVelozSecrets" -Recurse

# Or export as JSON
$secrets = @{
    "JwtSettings:SecretKey" = (dotnet user-secrets list | Select-String "JwtSettings:SecretKey")
}
$secrets | ConvertTo-Json | Out-File "C:\Backups\secrets-backup.json"
```

### Clear Secrets

```powershell
# Clear all secrets for project
dotnet user-secrets clear

# Verify cleared
dotnet user-secrets list
# Expected: No user secrets are currently set
```

### Restore Secrets

```powershell
# If you backed up secrets.json directly
Copy-Item -Path "C:\Backups\AeroVelozSecrets\secrets.json" -Destination $secretPath -Force

# Or set them individually
dotnet user-secrets set "JwtSettings:SecretKey" "your-key"
```

---

## 🔧 Troubleshooting

### Common Issues & Solutions

#### Issue 1: .NET SDK Not Found

**Error**: `'dotnet' is not recognized as an internal or external command`

**Solution**:
```powershell
# 1. Verify installation
dir "C:\Program Files\dotnet"

# 2. Add to PATH if missing
[Environment]::SetEnvironmentVariable(
  "Path",
  "$env:Path;C:\Program Files\dotnet",
  [EnvironmentVariableTarget]::User
)

# 3. Restart PowerShell
exit
# Reopen PowerShell

# 4. Verify again
dotnet --version
```

#### Issue 2: SQL Server Connection Error

**Error**: `Cannot open database "AeroVelozDB" requested by the login.`

**Solution**:
```powershell
# 1. Verify SQL Server running
Get-Service | Where-Object {$_.Name -like "*SQL*"}

# 2. Start SQL Server if stopped
Start-Service -Name "MSSQL$SQLEXPRESS"

# 3. Test connection
sqlcmd -S .\SQLEXPRESS -Q "SELECT @@VERSION"

# 4. Verify database exists
sqlcmd -S .\SQLEXPRESS -Q "SELECT name FROM sys.databases WHERE name = 'AeroVelozDB'"

# 5. If database missing, create it
sqlcmd -S .\SQLEXPRESS -Q "CREATE DATABASE AeroVelozDB;"
```

#### Issue 3: NuGet Package Restore Fails

**Error**: `The request was aborted: Could not create SSL/TLS secure channel.`

**Solution**:
```powershell
# 1. Clear NuGet cache
dotnet nuget locals all --clear

# 2. Update NuGet
dotnet nuget update source nuget.org

# 3. Restore packages with verbose output
dotnet restore --verbosity diagnostic

# 4. If behind proxy, configure nuget.config
cd "%APPDATA%\NuGet"
# Edit nuget.config with proxy settings
```

#### Issue 4: Migration Fails

**Error**: `The EntityType 'User' cannot be mapped because it has no properties mapped to columns in the base type.`

**Solution**:
```powershell
# 1. Remove existing migrations
Remove-Migration

# 2. Verify DbContext configuration
# Check AeroVelozDbContext.cs for proper OnModelCreating

# 3. Create fresh migration
Add-Migration InitialCreate -Force

# 4. Update database
Update-Database
```

#### Issue 5: JWT Secret Too Short

**Error**: `Key size needs to be at least 128 bits for HS256`

**Solution**:
```powershell
# Use minimum 32 characters (256 bits)
dotnet user-secrets set "JwtSettings:SecretKey" "YourMinimum32CharacterKeyHereXXXXXXXX"

# Verify length
$key = (dotnet user-secrets list | Select-String "SecretKey").Line
Write-Host "Key length: $($key.Length) characters"
```

#### Issue 6: Port Already in Use

**Error**: `Address already in use`

**Solution**:
```powershell
# 1. Find process using port 5000
Get-NetTCPConnection -LocalPort 5000

# 2. Kill the process
Stop-Process -Id <PID> -Force

# 3. Or use different port in appsettings.json
# "Kestrel": { "EndpointDefaults": { "Url": "http://localhost:5001" } }
```

#### Issue 7: Permission Denied Writing to User Secrets

**Error**: `Access to the path ... is denied`

**Solution**:
```powershell
# 1. Run as Administrator
Start-Process powershell -Verb RunAs

# 2. Or fix directory permissions
$secretPath = "$env:APPDATA\Microsoft\UserSecrets"
icacls $secretPath /grant "$env:USERNAME`:F" /T

# 3. Retry setting secrets
dotnet user-secrets set "key" "value"
```

### Debug Mode Tips

```powershell
# Run with debug output
dotnet run --configuration Debug --verbosity diagnostic

# Check application logs
Get-Content "$env:APPDATA\Logs\AeroVeloz\*.log" -Tail 50

# Monitor database connections
# In SSMS: Activity Monitor → Processes tab
```

---

## ✅ Verification

### Step 1: Build Verification

```powershell
# Clean and build
dotnet clean
dotnet build

# Check for errors
$buildResult = dotnet build --no-restore 2>&1
if ($buildResult -like "*Build succeeded*") {
    Write-Host "Build: PASSED" -ForegroundColor Green
} else {
    Write-Host "Build: FAILED" -ForegroundColor Red
}
```

### Step 2: Database Verification

```powershell
# Connect to database
sqlcmd -S .\SQLEXPRESS -d AeroVelozDB -Q "SELECT COUNT(*) AS TableCount FROM INFORMATION_SCHEMA.TABLES"

# Should show table count
# Expected: One or more tables created
```

### Step 3: Application Startup Verification

```powershell
# Start application and check for errors
cd Presentacion/AeroVeloz.Desktop
$process = Start-Process -FilePath "dotnet" -ArgumentList "run" -PassThru

# Wait for startup
Start-Sleep -Seconds 5

# Check if process is running
if ($process.HasExited -eq $false) {
    Write-Host "Application: STARTED" -ForegroundColor Green
    $process.Kill()
} else {
    Write-Host "Application: FAILED TO START" -ForegroundColor Red
}
```

### Step 4: Configuration Verification

```powershell
# Verify all required secrets are set
$requiredSecrets = @(
    "JwtSettings:SecretKey",
    "ConnectionStrings:DefaultConnection"
)

foreach ($secret in $requiredSecrets) {
    $value = dotnet user-secrets list | Select-String $secret
    if ($value) {
        Write-Host "✓ $secret" -ForegroundColor Green
    } else {
        Write-Host "✗ $secret MISSING" -ForegroundColor Red
    }
}
```

---

## 🚀 Production Deployment

### Pre-Deployment Checklist

- [ ] All tests pass
- [ ] Code reviewed and approved
- [ ] Database migrations tested on staging
- [ ] Secrets configured on production server
- [ ] Backups created
- [ ] Rollback plan documented
- [ ] Documentation updated

### Deployment Steps

#### 1. Database Migration

```powershell
# Backup production database
# In SSMS or via script:
BACKUP DATABASE [AeroVelozDB] 
TO DISK = 'C:\Backups\AeroVelozDB_Prod_$(Get-Date -Format yyyyMMdd).bak'

# Apply migrations (if any)
# Test migrations on staging first
cd Infraestructure/AeroVeloz.Infraestructure
dotnet ef database update --environment Production
```

#### 2. Publish Application

```powershell
# Build for production
dotnet publish -c Release -o "C:\Deploy\AeroVeloz\Release"

# Or from Visual Studio:
# Right-click AeroVeloz.Desktop → Publish...
# Select Release configuration
# Publish to folder
```

#### 3. Install as Windows Service (Optional)

```powershell
# Create Windows Service
New-Service -Name "AeroVelozDesktop" `
  -BinaryPathName "C:\Deploy\AeroVeloz\Release\AeroVeloz.exe" `
  -DisplayName "AeroVeloz Flight Management System" `
  -StartupType Automatic

# Start service
Start-Service -Name "AeroVelozDesktop"

# Verify
Get-Service "AeroVelozDesktop"
```

#### 4. Verify Deployment

```powershell
# Check application running
Get-Process | Where-Object {$_.Name -like "*AeroVeloz*"}

# Check logs
Get-Content "C:\Deploy\AeroVeloz\Release\logs\*.log" -Tail 20

# Test functionality
# Open application and perform smoke tests
```

### Rollback Procedure

```powershell
# If deployment fails, rollback:

# 1. Stop current version
Stop-Service -Name "AeroVelozDesktop"
Stop-Process -Name "AeroVeloz" -Force -ErrorAction SilentlyContinue

# 2. Restore previous version
Copy-Item -Path "C:\Deploy\AeroVeloz\Previous" -Destination "C:\Deploy\AeroVeloz\Release" -Recurse -Force

# 3. Restore database (if needed)
RESTORE DATABASE [AeroVelozDB] 
FROM DISK = 'C:\Backups\AeroVelozDB_Prod_Backup.bak'

# 4. Start service
Start-Service -Name "AeroVelozDesktop"

# 5. Verify
Get-Service "AeroVelozDesktop" | Select-Object Status, Name
```

---

## 📚 Additional Resources

### Official Documentation
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [WPF Documentation](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/)

---

## 📚 Related Documentation

- [Architecture Guide](./ARCHITECTURE.md) - System design and technical architecture
- [README - Project Overview](../README.md) - Quick reference and feature overview

---

### Community Resources
- [Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
- [GitHub Issues](https://github.com/JoelZhub/AeroVeloz/issues)
- [.NET Community](https://dotnetfoundation.org/)

### Support
- **Project Repository**: https://github.com/JoelZhub/AeroVeloz
- **Documentation**: `/docs` folder
- **Issues & Bugs**: GitHub Issues

---

<div align="center">

**AeroVeloz - Enterprise Deployment & Setup Guide**

*Professional installation and configuration for production environments*

</div>

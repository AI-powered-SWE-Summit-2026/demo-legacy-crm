# LegacyCRM

LegacyCRM is a classic ASP.NET MVC 5 customer relationship management demo targeting .NET Framework 4.8 with Entity Framework 6. It includes customer, contact, opportunity, and support ticket management plus a legacy ASMX endpoint and SQL scripts for LocalDB or SQL Server Express.

## Legacy patterns and where to find them

- ASP.NET MVC 5 / System.Web.Mvc: LegacyCRM.Web\Controllers\*.cs
- Entity Framework 6 DbContext + initializer: LegacyCRM.Core\CrmDbContext.cs
- Synchronous controllers and EF calls only: all controllers in LegacyCRM.Web\Controllers
- WebClient, HttpWebRequest, Thread.Sleep, ConfigurationManager, JsonConvert: LegacyCRM.Web\Services\IntegrationService.cs
- SmtpClient: LegacyCRM.Web\Services\EmailNotificationService.cs
- CSV export + Windows path handling: LegacyCRM.Web\Services\ExportService.cs
- System.Web.Optimization bundles: LegacyCRM.Web\App_Start\BundleConfig.cs
- [OutputCache]: LegacyCRM.Web\Controllers\HomeController.cs
- [HandleError]: LegacyCRM.Web\App_Start\FilterConfig.cs
- [ChildActionOnly]: LegacyCRM.Web\Controllers\CustomerController.cs
- WebGrid: LegacyCRM.Web\Views\Home\Index.cshtml
- ServicePointManager: LegacyCRM.Web\Global.asax.cs
- Direct ResourceManager usage: LegacyCRM.Web\Controllers\HomeController.cs
- EF6 ObjectContext access: LegacyCRM.Web\Controllers\HomeController.cs and LegacyCRM.Core\CrmDbContext.cs
- Legacy raw SQL string concatenation: LegacyCRM.Web\Controllers\CustomerController.cs
- Membership.GetUser(): LegacyCRM.Web\Controllers\AccountController.cs
- ASMX service: LegacyCRM.Web\Integration\CrmDataService.asmx and CrmDataService.asmx.cs
- Bundle rendering in views: LegacyCRM.Web\Views\Shared\_Layout.cshtml

## Setup

1. Open Database\CreateDatabase.sql in SQL Server Management Studio and run it against LocalDB or SQL Server Express.
2. Run Database\SeedData.sql to load demo users, customers, contacts, opportunities, tickets, and activities.
3. Update the LegacyCRMDb connection string in LegacyCRM.Web\Web.config if your SQL instance differs.
4. Restore packages and build:
   - dotnet restore LegacyCRM.sln
   - dotnet build LegacyCRM.sln
5. Run the site from Visual Studio, IIS Express, or your preferred .NET Framework-compatible host.

## Core bounded test coverage

- Run bounded core coverage checks with:
  - `powershell -ExecutionPolicy Bypass -File .\Run-CoreCoverageBounded.ps1`
- The command enforces a minimum line coverage floor of 25% and fails if coverage exceeds the 30% cap.

## Demo credentials

- admin / admin123
- salesrep / sales123

## What must change for a .NET 8 upgrade

- Replace System.Web.Mvc with ASP.NET Core MVC.
- Replace EF6 with EF Core and update migrations.
- Remove Global.asax, ASMX, OutputCache, ChildActionOnly, WebGrid, and bundling APIs.
- Replace WebClient, HttpWebRequest, SmtpClient, and Membership with modern equivalents such as HttpClient, ASP.NET Core Identity, and a modern mail library or service.
- Move configuration from Web.config to appsettings.json and the generic host.
- Replace synchronous data access with async/await and adopt dependency injection.

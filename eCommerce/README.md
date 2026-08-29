# Zac's Smoke Shack

This is an eCommerce website built to demonstrate the fundamentals of ASP.NET Core MVC and Entity Framework Core. It serves as a functional storefront with product management and user authentication.

## Current Features
- **Product Management:** Full CRUD (Create, Read, Update, Delete) functionality for products using Entity Framework Core.
- **Catalog Navigation:** Product listings include pagination, title search, and min/max price filtering.
- **User Access:** Custom member registration, login, and logout functionality utilizing ASP.NET Core Session state.
- **UI Design:** Responsive storefront styling utilizing Bootstrap 5.

## Upcoming Features
- Shopping Cart functionality and session tracking for guest carts.
- Order review and checkout submission.
- Integration of ASP.NET Core Identity for secure role management (Admin vs. Customer).

## Getting Started
- **Prerequisite:** .NET 10 SDK.
- **Recommended IDE:** Visual Studio 2026.
- **Database:** SQL Server.

### Installation Steps
1. Clone the repository from GitHub: `git clone <your-repo-url>`.
2. Open the solution in your IDE.
3. Open the Package Manager Console and run `Update-Database` to apply the Entity Framework Core migrations and generate the `eCommerceDb` database locally.
4. Run the application.

## Technologies & Frameworks Used
- ASP.NET Core MVC (C#).
- Entity Framework Core 10.
- SQL Server.
- Bootstrap 5
- HTML / CSS / JS
# 🗺️ Zac's Smoke Shack - Development Roadmap

This document tracks the planned features, current progress, and future milestones for the Zac's Smoke Shack e-commerce platform. 

### Phase 1: Data Architecture & CRUD Operations
**Goal:** Establish the Entity Framework Core database and basic product management.

| Task | Description | Status |
| :--- | :--- | :--- |
| **Define Models** | Create C# entity classes for `Product` and `Category`. | ⏳ Pending |
| **DbContext Setup** | Configure `ApplicationDbContext` and database connection string. | ⏳ Pending |
| **Initial Migration** | Run `dotnet ef migrations add InitialCreate` and update the database. | ⏳ Pending |
| **Admin Controllers** | Scaffold MVC Controllers and Views for Product/Category CRUD functionality. | ⏳ Pending |

### Phase 2: User Access & Identity
**Goal:** Implement secure user registration and login functionality.

| Task | Description | Status |
| :--- | :--- | :--- |
| **Scaffold Identity** | Integrate ASP.NET Core Identity into the project. | ⏳ Pending |
| **Database Update** | Run migrations to build the Identity schema (Users, Roles, Claims). | ⏳ Pending |
| **Role Management** | Create distinct access roles for "Admin" and "Customer". | ⏳ Pending |
| **Auth UI Styling** | Apply Bootstrap 5 classes to the login and registration Razor views. | ⏳ Pending |

### Phase 3: The Shopping Experience
**Goal:** Build the core shopping cart functionality.

| Task | Description | Status |
| :--- | :--- | :--- |
| **Cart Models** | Create `ShoppingCart` and `CartItem` models. | ⏳ Pending |
| **Session State** | Configure ASP.NET Core Session to track guest carts. | ⏳ Pending |
| **Cart Controller** | Build logic to add, remove, and update item quantities. | ⏳ Pending |
| **Checkout View** | Design the final order review and checkout submission page. | ⏳ Pending |

### Phase 4: UI/UX & Polish
**Goal:** Style the application with Bootstrap 5.

| Task | Description | Status |
| :--- | :--- | :--- |
| **Storefront Design** | Build responsive product grids/cards on the `Index.cshtml` home page. | ⏳ Pending |
| **Navigation** | Update `_Layout.cshtml` with a dynamic navbar showing the cart item count. | ⏳ Pending |
| **Modals & Alerts** | Use Bootstrap modals for delete confirmations and toast alerts for success messages. | ⏳ Pending |




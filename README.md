# Real Estate Manager

Real Estate Manager is a web application built with **ASP.NET MVC Core** that simplifies the management of real estate properties, property owners, and agents. The application provides an intuitive interface for creating, editing, searching, and managing real estate records while implementing authentication and authorization to protect sensitive operations.

The project follows the **Model-View-Controller (MVC)** architectural pattern and separates concerns into multiple projects to improve maintainability and organization.

---

## Features
- User registration and login
- Authentication using ASP.NET Core Identity
- Role-based authorization
- CRUD operations for:
  - Real estate properties
  - Property owners
  - Agents
- Property details view
- AJAX-based property filtering
- RESTful Web API endpoints
- Server-side validation
- Responsive interface using Bootstrap

---

## Authentication & Authorization

The application uses **ASP.NET Identity** for user management and security.

### Authentication

Users can:
- Register a new account
- Log in securely
- Log out
- Access personalized functionality after authentication

Passwords are securely managed by the ASP.NET Identity framework rather than being stored manually.

### Authorization

The application restricts certain actions to authenticated users.

Protected functionality includes operations such as:
- Creating new records
- Editing existing records
- Deleting records

This ensures that only authorized users can modify application data while allowing public users to browse available information where appropriate.

---

## Web API

Besides the MVC web application, the project also exposes RESTful API endpoints.

The API allows working with agent data through HTTP requests and demonstrates:
- GET requests
- POST requests
- JSON serialization
- DTO (Data Transfer Object) usage

This separates API communication from the MVC user interface.

---

## Project Structure

The solution is divided into multiple projects, each with a specific responsibility.

### Agencija.Web

Contains the ASP.NET MVC web application, including:
- Controllers
- Razor Views
- Authentication
- Routing
- User interface

### Agencija.DAL

The Data Access Layer responsible for:
- Entity Framework DbContext
- Database communication
- Data persistence

### Agencija.Model

Contains the application's domain models and entities that represent:
- Real Estate
- Owners
- Agents

Separating these projects improves maintainability and keeps responsibilities organized.

---

## Technologies Used
- ASP.NET Core MVC Core
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- C#
- SQL Server
- LINQ
- Razor Views
- Bootstrap
- HTML5
- CSS3
- JavaScript
- AJAX

---

## Architecture

The application follows the **MVC (Model-View-Controller)** design pattern.
- **Models** represent the application's data.
- **Views** display information to users.
- **Controllers** process requests and coordinate communication between the views and the data layer.

Entity Framework is used as the Object-Relational Mapper (ORM), allowing the application to interact with the SQL Server database through strongly typed C# objects.

Dependency Injection is used throughout the application to provide services such as the database context.

---

## Database

The application stores information about:
- Real estate properties
- Agents
- Property owners
- Neighborhoods
- Registered users

Relationships between entities are managed through Entity Framework, making database operations simpler and easier to maintain.

---

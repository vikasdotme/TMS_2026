# TMS_2026
Task Management System – Project Summary

1. Project Overview
The Task Management System is a web-based application developed using Clean Architecture. Due to time constraints, Dependency Injection has not been fully implemented at this stage.

2. Technology Stack
The project is built using ASP.NET Core 6, C#, and Entity Framework Core (version 9.0.13).
MS SQL Server is used as the database with a Database-First approach.

3. Database Management
The database backup file is available inside the project’s Database folder and can be restored to run the application.

4. Features & Functionality
The application supports Task Create, View, Update, and Delete (CRUD) operations.
Pagination is implemented to efficiently display and manage large task lists.

5. Frontend & Interaction
AJAX and jQuery are used to improve user experience by updating data without full page reloads.

6. Login & Layout
A basic login form is implemented (not fully validated yet).
After login, the main layout displays the software name, logged-in user ID, and user role in the header.

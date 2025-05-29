
Library Management System
This is a C# Windows Forms application designed to manage a library's operations, including adding books, issuing books to members, and managing book returns.

Features
User authentication: Supports user login and new account registration.
Book Management: Add new books to the library inventory.
Borrowing & Returning: Issue books to library members and record book returns.
Dashboard Overview: Provides a summary of key library statistics.
Technologies Used
Programming Language: C#
Framework: .NET Framework (Windows Forms)
Database: SQL Server
IDE: Visual Studio
Getting Started
Follow these steps to get the Library Management System up and running on your local machine.

Prerequisites
Visual Studio: Version 2019 or later.
.NET Desktop Development workload: Must be installed within Visual Studio.
SQL Server Express: Or another SQL Server instance for database hosting.
Installation
Clone the repository:
Bash

git clone https://github.com/bante27/Library-Managem
Open in Visual Studio:
Navigate to the cloned directory.
Open the Library_Managem.sln file in Visual Studio.
Database Setup:
Ensure your SQL Server instance is running.
Update the connection string: Locate the <connectionStrings> section in the App.config file and modify it to point to your SQL Server instance.
If your database and tables are not automatically created on first run, you'll need to manually create them using SQL Server Management Studio or by running a provided SQL script (if available in your project).
Build the project:
In Visual Studio, go to Build > Build Solution.
Running the Application
After a successful build, press F5 or click the Start button in Visual Studio to run the application.
The Login Form will be the first screen you see.
Usage
Login & Signup
Login: Enter your registered username and password.
Signup: To create a new user account, click the "Signup" button on the login form and fill in the required registration details.
Main Navigation
Once logged in, you'll see a navigation panel (likely on the left side) with the following options:

DashBord: View a summary of library statistics.
Add Book: Add new book entries to the library's inventory.
Issue Book: Record books being lent out to members.
Return Book: Process the return of books from members.
Contact
If you have any questions or need further assistance, feel free to reach out:

Account Name: bante27
Project Link: https://github.com/bante27/Library-Management

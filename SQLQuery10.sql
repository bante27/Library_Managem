-- Drop tables in reverse dependency order if they exist
IF OBJECT_ID('ReturnedBooks', 'U') IS NOT NULL
    DROP TABLE ReturnedBooks;

IF OBJECT_ID('IssuedBooks', 'U') IS NOT NULL
    DROP TABLE IssuedBooks;

IF OBJECT_ID('Books', 'U') IS NOT NULL
    DROP TABLE Books;

IF OBJECT_ID('Borrowers', 'U') IS NOT NULL
    DROP TABLE Borrowers;

IF OBJECT_ID('users', 'U') IS NOT NULL
    DROP TABLE users;
GO

-- Create the users table
CREATE TABLE users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) NOT NULL UNIQUE, -- Based on error: 'Email' column does not allow nulls
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    date_register DATE NOT NULL
);
GO

-- Create the Borrowers table
CREATE TABLE Borrowers (
    BorrowerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Contact NVARCHAR(50), -- Changed from 'Phone' to 'Contact' based on image and common naming
    Email NVARCHAR(100) UNIQUE,
    RegisterDate DATE NOT NULL
);
GO

-- Create the Books table
CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Matches C# code `selectedRow.Cells["Id"].Value`
    BookTitle NVARCHAR(255) NOT NULL, -- Matches C# code `BookTitle`
    Author NVARCHAR(255) NOT NULL,
    PublishedDate DATE, -- Allows NULL, adjust if you want it NOT NULL and always provided
    Status NVARCHAR(50) NOT NULL,
    ISBN NVARCHAR(50) UNIQUE, -- Added based on common library fields, if you plan to use it.
    TotalCopies INT DEFAULT 1 NOT NULL, -- Added to manage inventory, defaulted to 1 as a common starting point
    AvailableCopies INT DEFAULT 1 NOT NULL -- Based on error: 'AvailableCopies' cannot be NULL
);
GO

-- Create the IssuedBooks table
CREATE TABLE IssuedBooks (
    IssueId INT PRIMARY KEY IDENTITY(1,1),
    BookID INT NOT NULL, -- Corrected casing from 'BookId' to 'BookID'
    BorrowerID INT NOT NULL,
    IssueDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE, -- Can be NULL until the book is returned
    IsReturned BIT DEFAULT 0 NOT NULL,
    FOREIGN KEY (BookID) REFERENCES Books(Id),
    FOREIGN KEY (BorrowerID) REFERENCES Borrowers(BorrowerID)
);
GO

-- Create the ReturnedBooks table (optional, but good for historical tracking)
-- This table would typically store a record of all returns, possibly duplicating data from IssuedBooks
-- but indicating the actual return action. For now, we can manage returns by updating IssuedBooks.
-- If you choose to use this table, ensure you have logic to populate it.
CREATE TABLE ReturnedBooks (
    ReturnID INT PRIMARY KEY IDENTITY(1,1),
    IssueId INT NOT NULL,
    ReturnDate DATE NOT NULL,
    ConditionOnReturn NVARCHAR(100),
    FOREIGN KEY (IssueId) REFERENCES IssuedBooks(IssueId)
);
GO
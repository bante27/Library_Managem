-- Database: Library.mdf

-- Table: Books
CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookTitle NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255),
    PublishedDate DATE,
    Status NVARCHAR(50), -- e.g., 'Available', 'Issued'
    AvailableCopies INT NOT NULL
);

-- Table: Borrowers
CREATE TABLE Borrowers (
    BorrowerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Contact NVARCHAR(50), -- This is the column for phone/contact
    Email NVARCHAR(255)
);

-- Table: IssuedBooks
CREATE TABLE IssuedBooks (
    IssueId INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL,
    BorrowerId INT NOT NULL,
    IssueDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE, -- NULL if not yet returned
    IsReturned BIT NOT NULL DEFAULT 0, -- 0 for not returned, 1 for returned
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (BorrowerId) REFERENCES Borrowers(BorrowerId)
);

-- Table: Fines
CREATE TABLE Fines (
    FineID INT PRIMARY KEY IDENTITY(1,1),
    IssueID INT NOT NULL,
    BorrowerID INT NOT NULL,
    BookID INT NOT NULL,
    FineAmount DECIMAL(10, 2) NOT NULL,
    FineDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsPaid BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (IssueID) REFERENCES IssuedBooks(IssueId),
    FOREIGN KEY (BorrowerID) REFERENCES Borrowers(BorrowerId),
    FOREIGN KEY (BookID) REFERENCES Books(Id)
);

-- Table: Users (assuming for login)
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    date_register DATETIME DEFAULT GETDATE()
);

PRINT 'All tables created successfully.';
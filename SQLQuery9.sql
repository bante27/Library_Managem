CREATE TABLE users (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing unique ID
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Usernames must be unique and not null
    Password NVARCHAR(255) NOT NULL, -- Storing hashed passwords is best practice, but for now NVARCHAR is fine
    Email NVARCHAR(100) NOT NULL UNIQUE, -- Email must be unique and not null
    date_register DATE DEFAULT GETDATE() -- Automatically sets the registration date
);
IF DB_ID(N'LegacyCRMDb') IS NULL
BEGIN
    CREATE DATABASE LegacyCRMDb;
END
GO

USE LegacyCRMDb;
GO

IF OBJECT_ID(N'dbo.Activities', N'U') IS NOT NULL DROP TABLE dbo.Activities;
IF OBJECT_ID(N'dbo.Tickets', N'U') IS NOT NULL DROP TABLE dbo.Tickets;
IF OBJECT_ID(N'dbo.Opportunities', N'U') IS NOT NULL DROP TABLE dbo.Opportunities;
IF OBJECT_ID(N'dbo.Contacts', N'U') IS NOT NULL DROP TABLE dbo.Contacts;
IF OBJECT_ID(N'dbo.EmailLog', N'U') IS NOT NULL DROP TABLE dbo.EmailLog;
IF OBJECT_ID(N'dbo.Customers', N'U') IS NOT NULL DROP TABLE dbo.Customers;
IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    FullName NVARCHAR(150) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL,
    LastLoginAt DATETIME NULL
);
GO

CREATE TABLE dbo.Customers
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    CompanyName NVARCHAR(150) NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NULL,
    Address NVARCHAR(250) NULL,
    City NVARCHAR(100) NULL,
    Country NVARCHAR(100) NULL,
    Industry NVARCHAR(100) NULL,
    Status NVARCHAR(50) NOT NULL,
    AssignedToUserId INT NULL,
    CreatedAt DATETIME NOT NULL,
    ModifiedAt DATETIME NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Customers_Users FOREIGN KEY (AssignedToUserId) REFERENCES dbo.Users(Id)
);
GO

CREATE TABLE dbo.Contacts
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId INT NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NULL,
    Phone NVARCHAR(50) NULL,
    Title NVARCHAR(100) NULL,
    IsPrimary BIT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CONSTRAINT FK_Contacts_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id)
);
GO

CREATE TABLE dbo.Opportunities
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId INT NOT NULL,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Value DECIMAL(18,2) NOT NULL,
    Stage NVARCHAR(50) NOT NULL,
    ExpectedCloseDate DATETIME NOT NULL,
    AssignedToUserId INT NOT NULL,
    Probability INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ModifiedAt DATETIME NOT NULL,
    CONSTRAINT FK_Opportunities_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id),
    CONSTRAINT FK_Opportunities_Users FOREIGN KEY (AssignedToUserId) REFERENCES dbo.Users(Id)
);
GO

CREATE TABLE dbo.Tickets
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId INT NOT NULL,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Priority NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    AssignedToUserId INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ResolvedAt DATETIME NULL,
    ResolutionNotes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Tickets_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id),
    CONSTRAINT FK_Tickets_Users FOREIGN KEY (AssignedToUserId) REFERENCES dbo.Users(Id)
);
GO

CREATE TABLE dbo.Activities
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId INT NOT NULL,
    ContactId INT NULL,
    OpportunityId INT NULL,
    Type NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    ActivityDate DATETIME NOT NULL,
    CreatedByUserId INT NOT NULL,
    CONSTRAINT FK_Activities_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id),
    CONSTRAINT FK_Activities_Contacts FOREIGN KEY (ContactId) REFERENCES dbo.Contacts(Id),
    CONSTRAINT FK_Activities_Opportunities FOREIGN KEY (OpportunityId) REFERENCES dbo.Opportunities(Id),
    CONSTRAINT FK_Activities_Users FOREIGN KEY (CreatedByUserId) REFERENCES dbo.Users(Id)
);
GO

CREATE TABLE dbo.EmailLog
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ToAddress NVARCHAR(200) NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    SentAt DATETIME NOT NULL,
    ErrorMessage NVARCHAR(MAX) NULL
);
GO

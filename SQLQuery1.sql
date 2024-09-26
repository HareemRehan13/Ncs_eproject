-- Create tables for the Nexus Communication System
Create database Nexus;
Use Nexus;
-- Table to store customer details
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    AccountID CHAR(16) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    Email NVARCHAR(100),
    AddressProof NVARCHAR(255),
    ApplicationForm NVARCHAR(255),
    Receipt NVARCHAR(255),
    Status NVARCHAR(50) NOT NULL
);

-- Table to store employee details
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL,
    Email NVARCHAR(100)
);

-- Table to store retail shop details
CREATE TABLE RetailShops (
    ShopID INT PRIMARY KEY IDENTITY(1,1),
    ShopName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL
);

-- Table to store vendor details
CREATE TABLE Vendors (
    VendorID INT PRIMARY KEY IDENTITY(1,1),
    VendorName NVARCHAR(100) NOT NULL,
    ContactPerson NVARCHAR(100),
    ContactNumber VARCHAR(15) NOT NULL,
    Email NVARCHAR(100)
);

-- Table to store product details
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    ProductType NVARCHAR(50) NOT NULL,
    Stock INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

-- Table to store orders
CREATE TABLE Orders (
    OrderID CHAR(11) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate DATETIME NOT NULL,
    ConnectionType CHAR(1) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FeasibilityStatus NVARCHAR(50) NOT NULL,
    EquipmentID INT FOREIGN KEY REFERENCES Products(ProductID),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Table to store connection details
CREATE TABLE Connections (
    ConnectionID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    ConnectionType CHAR(1) NOT NULL,
    PlanType NVARCHAR(50) NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Table to store billing details
CREATE TABLE Billing (
    BillingID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    ConnectionID INT FOREIGN KEY REFERENCES Connections(ConnectionID),
    BillDate DATETIME NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    AmountPaid DECIMAL(10, 2),
    AmountDue AS (TotalAmount - ISNULL(AmountPaid, 0)),
    FOREIGN KEY (ConnectionID) REFERENCES Connections(ConnectionID)
);

-- Table to store payments
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    BillingID INT FOREIGN KEY REFERENCES Billing(BillingID),
    PaymentDate DATETIME NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (BillingID) REFERENCES Billing(BillingID)
);

-- Table to store feedback
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderID CHAR(11) FOREIGN KEY REFERENCES Orders(OrderID),
    FeedbackText NVARCHAR(MAX),
    FeedbackDate DATETIME NOT NULL
);

-- Table to store discount schemes
CREATE TABLE DiscountSchemes (
    SchemeID INT PRIMARY KEY IDENTITY(1,1),
    MinConnections INT NOT NULL,
    MaxConnections INT,
    DiscountPercentage DECIMAL(5, 2) NOT NULL
);

-- Table to store service plans

CREATE TABLE ServicePlans (
    PlanID INT PRIMARY KEY IDENTITY(1,1),
    PlanType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL
);

-- Table to store feasibility checks
CREATE TABLE FeasibilityChecks (
    CheckID INT PRIMARY KEY IDENTITY(1,1),
    OrderID CHAR(11) FOREIGN KEY REFERENCES Orders(OrderID),
    FeasibilityStatus NVARCHAR(50) NOT NULL,
    Comments NVARCHAR(MAX)
);
-- Table to store admin
CREATE TABLE Users
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [username] NVARCHAR(MAX) NOT NULL, 
    [email] NVARCHAR(MAX) NOT NULL, 
    [password] NVARCHAR(MAX) NOT NULL,
  [roleId] INT NOT NULL DEFAULT 2,
    [status] INT NOT NULL DEFAULT 0,

)

-- Insert sample data for demonstration purposes (modify as necessary)
INSERT INTO ServicePlans (PlanType, Description, Price) VALUES
('Dial-Up Hourly 10 Hrs', '10 Hours Dial-Up Connection', 50.00),
('Dial-Up Unlimited 28Kbps Monthly', 'Unlimited Dial-Up Connection 28Kbps Monthly', 75.00),
('Broadband Unlimited 64Kbps Monthly', 'Unlimited Broadband Connection 64Kbps Monthly', 225.00),
('Landline Local Unlimited Plan', 'Unlimited Local Landline Plan', 75.00);

INSERT INTO DiscountSchemes (MinConnections, MaxConnections, DiscountPercentage) VALUES
(10, 15, 25.00),
(16, 25, 50.00),
(26, 50, 75.00),
(51, NULL, 100.00);

-- Create indexes for performance optimization
CREATE INDEX idx_Customers_Name ON Customers (Name);
CREATE INDEX idx_Orders_CustomerID ON Orders (CustomerID);
CREATE INDEX idx_Billing_CustomerID ON Billing (CustomerID);
CREATE INDEX idx_Payments_CustomerID ON Payments (CustomerID);
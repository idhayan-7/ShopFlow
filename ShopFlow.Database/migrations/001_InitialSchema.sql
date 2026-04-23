-- [CONCEPT] This schema mirrors the EF Core models exactly.
-- [WHY] We keep raw SQL scripts alongside EF Core migrations so we can understand what
--       EF Core generates. Helps in interviews when asked "what SQL does EF Core run?"
-- [WEEK-3] SQL Server: CREATE TABLE, PRIMARY KEY, FOREIGN KEY, INDEXES.

-- Create Customers table
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    -- [CONCEPT] IDENTITY(1,1) = auto-increment. EF Core maps this to int Id auto-generated PK.
    Name NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    -- [CONCEPT] DECIMAL(18,2) for money — never use FLOAT for currency (rounding errors).
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    Category NVARCHAR(100) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    TotalAmount DECIMAL(18,2) NOT NULL,
    OrderDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    -- [CONCEPT] FOREIGN KEY — enforces referential integrity at the database level.
    -- [WEEK-3] SQL: FK prevents orphan orders (orders with no customer).
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    -- [WHY] No ON DELETE CASCADE here — deleting a product should not destroy order history.
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- [CONCEPT] Non-clustered indexes on Foreign Key columns for JOIN performance.
-- [WEEK-3] Indexes: JOINs on CustomerId and ProductId are very common — indexes make them fast.
-- Without these indexes, SQL Server scans the full table on every JOIN.
CREATE NONCLUSTERED INDEX IX_Orders_CustomerId ON Orders(CustomerId);
CREATE NONCLUSTERED INDEX IX_OrderItems_OrderId ON OrderItems(OrderId);
CREATE NONCLUSTERED INDEX IX_OrderItems_ProductId ON OrderItems(ProductId);

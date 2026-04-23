-- [CONCEPT] Seed data — initial records inserted so the app has data to display on first run.
-- [WHY] Without seed data, the product list would be empty and the app would look broken.
-- [WEEK-3] SQL: INSERT INTO with multiple rows in one statement.

INSERT INTO Customers (Name, Email, Phone, Address, CreatedAt) VALUES
('Amit Sharma', 'amit@example.com', '9876543210', 'Chennai, Tamil Nadu', GETUTCDATE()),
('Priya Nair', 'priya@example.com', '9123456780', 'Bengaluru, Karnataka', GETUTCDATE()),
('Ravi Kumar', 'ravi@example.com', '9988776655', 'Mumbai, Maharashtra', GETUTCDATE());

INSERT INTO Products (Name, Description, Price, StockQuantity, Category, CreatedAt) VALUES
('Laptop - Dell XPS 15', '15 inch, Intel i7, 16GB RAM', 89999.00, 15, 'Electronics', GETUTCDATE()),
('Mechanical Keyboard', 'Cherry MX switches, RGB backlit', 4500.00, 50, 'Electronics', GETUTCDATE()),
('USB-C Hub 7-in-1', 'HDMI, USB 3.0, SD Card reader', 2999.00, 100, 'Accessories', GETUTCDATE()),
('Wireless Mouse', 'Ergonomic, 2.4GHz, silent clicks', 1299.00, 75, 'Accessories', GETUTCDATE()),
('Monitor 27 inch', '4K IPS, 144Hz, HDR support', 32999.00, 20, 'Electronics', GETUTCDATE());

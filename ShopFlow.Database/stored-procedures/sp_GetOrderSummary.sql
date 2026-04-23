-- [CONCEPT] Stored Procedure — pre-compiled SQL saved in the database.
-- [WHY] Complex reporting queries (JOINs + GROUP BY) are better as stored procedures than raw LINQ.
-- [WEEK-3] SQL: GROUP BY, COUNT(), SUM(), JOIN, HAVING — all in one query.
-- This procedure shows a summary of each customer's order history.

CREATE OR ALTER PROCEDURE sp_GetOrderSummary
    @CustomerId INT = NULL  -- [CONCEPT] Optional parameter — NULL means return all customers.
AS
BEGIN
    SET NOCOUNT ON;  -- [WHY] Prevents extra "rows affected" messages from cluttering results.

    SELECT
        c.Name AS CustomerName,
        c.Email,
        COUNT(o.Id) AS TotalOrders,
        -- [CONCEPT] SUM with ISNULL handles customers who have no orders (SUM of NULL = NULL).
        ISNULL(SUM(o.TotalAmount), 0) AS TotalSpent,
        MAX(o.OrderDate) AS LastOrderDate
    FROM Customers c
    -- [CONCEPT] LEFT JOIN includes customers even if they have zero orders.
    -- INNER JOIN would exclude customers with no orders — wrong for a summary.
    LEFT JOIN Orders o ON c.Id = o.CustomerId
    WHERE (@CustomerId IS NULL OR c.Id = @CustomerId)
    GROUP BY c.Id, c.Name, c.Email
    -- [CONCEPT] HAVING filters AFTER GROUP BY. WHERE filters BEFORE aggregation.
    HAVING COUNT(o.Id) >= 0
    ORDER BY TotalSpent DESC;
END;

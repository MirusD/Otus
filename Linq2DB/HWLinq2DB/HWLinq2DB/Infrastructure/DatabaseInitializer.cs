using HWLinq2DB.Domain;
using LinqToDB;
using LinqToDB.Data;
using static HWLinq2DB.Program;

namespace HWLinq2DB.Infrastructure
{
    internal class DatabaseInitializer
    {
        private readonly AppDbContext _dbConnection;

        public DatabaseInitializer(AppDbContext dbContext)
        {
            _dbConnection = dbContext;
        }

        public async Task CreateTablesIfNotExists()
        {
            await CreateCustomersTableIfNotExists();
            await CreateProductsTableIfNotExists();
            await CreateOrdersTableIfNotExists();
        }

        public async Task CreateDataWithTables()
        {
            await AddDataWithCustomersTable();
            await AddDataWithProductsTable();
            await AddDataWithOrdersTable();
        }

        private async Task CreateCustomersTableIfNotExists()
        {
            var createTableSql = @"
                CREATE TABLE IF NOT EXISTS Customers (
                    ID SERIAL PRIMARY KEY,
                    FirstName VARCHAR(50) NOT NULL,
                    LastName VARCHAR(50) NOT NULL,
                    Age INT CHECK (Age >= 0),
                    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
                );
            ";
            await _dbConnection.ExecuteAsync(createTableSql);
        }

        private async Task CreateProductsTableIfNotExists()
        {
            var createTableSql = @"
                CREATE TABLE IF NOT EXISTS Products (
                    ID SERIAL PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Description TEXT,
                    StockQuantity INT NOT NULL CHECK (StockQuantity >= 0),
                    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0),
                    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
                );
            ";
            await _dbConnection.ExecuteAsync(createTableSql);
        }

        private async Task CreateOrdersTableIfNotExists()
        {
            var createTableSql = @"
                CREATE TABLE IF NOT EXISTS Orders (
                    ID SERIAL PRIMARY KEY,
                    CustomerID INT NOT NULL ,
                    ProductID INT NOT NULL,
                    Quantity INT NOT NULL CHECK (Quantity > 0),
                    FOREIGN KEY (CustomerID) REFERENCES Customers(ID) ON DELETE CASCADE,
                    FOREIGN KEY (ProductID) REFERENCES Products(ID) ON DELETE CASCADE,
                    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
                );
            ";
            await _dbConnection.ExecuteAsync(createTableSql);

            var createIndexSql = @"
                CREATE INDEX IF NOT EXISTS idx_orders_customerid ON Orders (CustomerID);
            ";

            await _dbConnection.ExecuteAsync(createIndexSql);
        }

        private async Task AddDataWithCustomersTable()
        {
            var dataTableSql = @"
                INSERT INTO Customers(FirstName, LastName, Age)
                VALUES
                ('Ivan', 'Ivanov', 35),
                ('Petr', 'Petrovich', 28),
                ('Konstantine', 'Konstantinovich', 40),
                ('Max', 'Maximovich', 33),
                ('Taras', 'Tarasovic', 25),
                ('Vasya', 'Pupkin', 45),
                ('Stas', 'Stasovich', 31),
                ('Nikita', 'Nikitovich', 27),
                ('Marsel', 'Marselevich', 50),
                ('Chack', 'Noris', 29)
                ON CONFLICT (FirstName, LastName) DO NOTHING;
            ";

            await _dbConnection.ExecuteAsync(dataTableSql);
        }

        private async Task AddDataWithProductsTable()
        {
            var dataTableSql = @"
                INSERT INTO Products (Name, Description, StockQuantity, Price)
                VALUES
                ('Product1', 'Description1', 100, 10.00),
                ('Product2', 'Description2', 200, 20.00),
                ('Product3', 'Description3', 150, 15.00),
                ('Product4', 'Description4', 80, 25.00),
                ('Product5', 'Description5', 120, 30.00),
                ('Product6', 'Description6', 60, 50.00),
                ('Product7', 'Description7', 90, 5.00),
                ('Product8', 'Description8', 110, 8.00),
                ('Product9', 'Description9', 70, 40.00),
                ('Product10', 'Description10', 50, 100.00)
                ON CONFLICT (Name) DO NOTHING;
            ";

            await _dbConnection.ExecuteAsync(dataTableSql);
        }

        private async Task AddDataWithOrdersTable()
        {
            var dataTableSql = @"
                INSERT INTO Orders (CustomerID, ProductID, Quantity)
                VALUES
                (1, 1, 5),
                (2, 2, 3),
                (3, 1, 2),
                (4, 3, 1),
                (5, 4, 4),
                (6, 5, 2),
                (7, 1, 1),
                (8, 6, 3),
                (9, 7, 6),
                (10, 8, 2)
                ON CONFLICT (CustomerID, ProductID) DO NOTHING;
            ";

            await _dbConnection.ExecuteAsync(dataTableSql);
        }
    }
}

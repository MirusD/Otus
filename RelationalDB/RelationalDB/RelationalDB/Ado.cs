using Npgsql;
using RelationalDB.Entities;

namespace RelationalDB
{
    internal class Ado(string cs)
    {
        private readonly string _cs = cs;

        public void CreateProductsTable()
        {
            using (var connection = new NpgsqlConnection(_cs)) 
            {
                connection.Open();

                var sql = @"
                CREATE TABLE IF NOT EXISTS ""Products"" (
                    ""ProductId"" BIGSERIAL PRIMARY KEY,
                    ""ProductName"" TEXT NOT NULL,
                    ""Description"" TEXT NOT NULL,
                    ""Price"" DECIMAL(18, 2) NOT NULL,
                    ""QuantityInStock"" INT NOT NULL,
                    ""CreatedAt"" TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
                );
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> Создана таблица товаров. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void CreateUsersTable()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                CREATE TABLE IF NOT EXISTS ""Users"" (
                    ""UserId"" BIGSERIAL PRIMARY KEY,
                    ""UserName"" CHARACTER VARYING(255) NOT NULL,
                    ""Email"" CHARACTER VARYING(255) NOT NULL,
                    ""RegistrationDate"" TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP NOT NULL,

                    CONSTRAINT users_email_unique UNIQUE (""Email"")
                );

                CREATE INDEX IF NOT EXISTS user_name_idx ON ""Users"" (""UserName"");
                CREATE UNIQUE INDEX IF NOT EXISTS user_email_unq_idx ON ""Users"" (lower(""Email""));
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> Создана таблица пользователей. Количество затронутых строк: {affectedRowsCount}");
            } 
        }

        public void CreateOrdersTable()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                CREATE TABLE IF NOT EXISTS ""Orders"" (
                    ""OrderId"" BIGSERIAL PRIMARY KEY,
                    ""UserId"" BIGINT NOT NULL,
                    ""OrderDate"" TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP NOT NULL,
                    ""Status"" CHARACTER VARYING(255) NOT NULL,
         
                    CONSTRAINT fk_user FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""UserId"")
                );
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> Создана таблица заказов. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void CreateOrderDetailsTable()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                CREATE TABLE IF NOT EXISTS ""OrderDetails"" (
                    ""OrderDetailId"" BIGSERIAL PRIMARY KEY,
                    ""OrderId"" BIGINT NOT NULL,
                    ""ProductId"" BIGINT NOT NULL,
                    ""Quantity"" INT NOT NULL,
                    ""TotalCost"" DECIMAL(18, 2) NOT NULL,
        
                    CONSTRAINT fk_order FOREIGN KEY (""OrderId"") REFERENCES ""Orders"" (""OrderId"") ON DELETE CASCADE,
                    CONSTRAINT fk_product FOREIGN KEY (""ProductId"") REFERENCES ""Products"" (""ProductId"") ON DELETE CASCADE
                );
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> Создана таблица деталей заказа. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void InsertUsersSimple()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                INSERT INTO ""Users""(""UserName"", ""Email"")
                VALUES 
                    ('Дмитрий', 'dmitriy@mail.ru'),
                    ('Иван', 'ivan@mail.ru'),
                    ('Роман', 'roman@mail.ru');
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> В таблицу пользователей добавлены данные. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void InsertProductsSimple()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                INSERT INTO ""Products""(""ProductName"", ""Description"", ""Price"", ""QuantityInStock"")
                VALUES
                    ('Product1', 'Product description 1', '100', '10'),
                    ('Product2', 'Product description 2', '200', '20'),
                    ('Product3', 'Product description 3', '300', '30'),
                    ('Product4', 'Product description 4', '400', '40'),
                    ('Product5', 'Product description 5', '500', '50'),
                    ('Product6', 'Product description 6', '600', '60'),
                    ('Product7', 'Product description 7', '700', '70'),
                    ('Product8', 'Product description 8', '800', '2'),
                    ('Product9', 'Product description 9', '900', '4'),
                    ('Product10', 'Product description 10', '1000', '1');
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> В таблицу товаров добавлены данные. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void InsertOrdersSimple()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                INSERT INTO ""Orders""(""UserId"", ""Status"")
                VALUES
                    ('1', 'В обработке'),
                    ('1', 'В обработке'),
                    ('1', 'В обработке'),
                    ('2', 'Оплачен'),
                    ('2', 'Оплачен');
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> В таблицу заказов добавлены данные. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void InsertOrderDetailsSimple()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                INSERT INTO ""OrderDetails""(""OrderId"", ""ProductId"", ""Quantity"", ""TotalCost"")
                VALUES
                    ('1', '1', '2', '200'),
                    ('1', '2', '1', '200'),
                    ('1', '3', '3', '900'),
                    ('1', '4', '1', '400'),
                    ('2', '5', '1', '500'),
                    ('2', '6', '2', '1200'),
                    ('3', '7', '1', '700'),
                    ('3', '8', '2', '1600'),
                    ('3', '9', '3', '2700'),
                    ('4', '10', '1', '1000');
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"> В таблицу детали заказа добавлены данные. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public long AddNewProduct(Product newProduct)
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                INSERT INTO ""Products"" (""ProductName"", ""Description"", ""Price"", ""QuantityInStock"")
                VALUES (@ProductName, @ProductDescription, @ProductPrice, @ProductQuantityInStock)
                RETURNING ""ProductId"";
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@ProductName", newProduct.ProductName);
                cmd.Parameters.AddWithValue("@ProductDescription", newProduct.Description);
                cmd.Parameters.AddWithValue("@ProductPrice", newProduct.Price);
                cmd.Parameters.AddWithValue("@ProductQuantityInStock", newProduct.QuantityInStock);

                var productId = (long)cmd.ExecuteScalar();

                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"> Добавлен новый товар с ID: {productId}");
                Console.WriteLine($"" +
                    $"ProductId: {productId}, " +
                    $"ProductName: {newProduct.ProductName}, " +
                    $"Description: {newProduct.Description}, " +
                    $"Price: {newProduct.Price}, " +
                    $"QuantityInStock: {newProduct.QuantityInStock}");
                Console.WriteLine(new string('-', 100));

                return productId;
            }
        }

        public void UpdatePriceInProduct(long productId, decimal newPrice)
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                    UPDATE ""Products""
                    SET ""Price"" = @Price
                    WHERE ""ProductId"" = @ProductId;
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Price", newPrice);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                var affectedRowsCount = cmd.ExecuteNonQuery();

                Console.WriteLine($"> Обновлена цена у товара с ID: {productId}. Количество затронутых строк: {affectedRowsCount}");
            }
        }

        public void SelectOrdersByUserAndSumTotalPrice(long userId)
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                SELECT o.""OrderId"", SUM(od.""TotalCost"") AS ""TotalOrderCost""
                FROM ""Orders"" o
                JOIN ""OrderDetails"" od ON o.""OrderId"" = od.""OrderId""
                WHERE o.""UserId"" = @UserId
                GROUP BY o.""OrderId"";
                ";

                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"Выбор всех заказов пользователя c ID: {userId} и расчет общей стоимости заказа");

                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    long orderId = reader.GetInt64(0);
                    decimal totalOrderCost = reader.GetDecimal(1);

                    Console.WriteLine($"Order ID: {orderId}, Total Cost: {totalOrderCost:C}");
                }
                Console.WriteLine(new string('-', 100));
            }
        }

        public void GetStockAndTopProducts()
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var stockSql = @"
                    SELECT SUM(""QuantityInStock"") AS ""TotalStock"" FROM ""Products"";
                ";

                using var stockCmd = new NpgsqlCommand(stockSql, connection);
                var totalStock = Convert.ToDecimal(stockCmd.ExecuteScalar());

                Console.WriteLine($"Всего товаров на складе: {totalStock}");

                var topProductsSql = @"
                SELECT ""ProductId"", ""ProductName"", ""Price""
                FROM ""Products""
                ORDER BY ""Price"" DESC
                LIMIT 5;
                ";

                using var topProductsCmd = new NpgsqlCommand(topProductsSql, connection);
                using var reader = topProductsCmd.ExecuteReader();

                Console.WriteLine("Top 5 самых дорогих товаров");

                while (reader.Read())
                {
                    long productId = reader.GetInt64(0);
                    string productName = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);

                    Console.WriteLine($"ProductId: {productId}, ProductName: {productName}, Price: {price}");
                }

                Console.WriteLine(new string('-', 100));
            }
        }

        public void GetLowStockProducts(int lowerThreshold)
        {
            using (var connection = new NpgsqlConnection(_cs))
            {
                connection.Open();

                var sql = @"
                SELECT ""ProductId"", ""ProductName"", ""QuantityInStock""
                FROM ""Products""
                WHERE ""QuantityInStock"" < @lowerThreshold;
                ";

                using var cmd = new NpgsqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@lowerThreshold", lowerThreshold);

                using var reader = cmd.ExecuteReader();

                Console.WriteLine($"Список товаров которых на складе осталось меньше {lowerThreshold}");

                while (reader.Read())
                {
                    long productId = reader.GetInt64(0);
                    string productName = reader.GetString(1);
                    int quantityInStock = reader.GetInt32(2);

                    Console.WriteLine($"ProductId: {productId}, ProductName: {productName}, QuantityInStock: {quantityInStock}");
                }

                Console.WriteLine(new string('-', 100));
            }
        }
    }
}

using RelationalDB.Entities;

namespace RelationalDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=Vtumdd2LRfNNowacUVQj;Database=shop;";

            var ado = new Ado(connectionString);

            ado.CreateProductsTable();
            ado.CreateUsersTable();
            ado.CreateOrdersTable();
            ado.CreateOrderDetailsTable();

            ado.InsertUsersSimple();
            ado.InsertProductsSimple();
            ado.InsertOrdersSimple();
            ado.InsertOrderDetailsSimple();

            var newProduct = new Product()
            {
                ProductName = "Product11",
                Description = "Description product 11",
                Price = 1100,
                QuantityInStock = 5,
            };

            var newProductId = ado.AddNewProduct(newProduct);
            ado.UpdatePriceInProduct(newProductId, 120);

            ado.SelectOrdersByUserAndSumTotalPrice(1);

            ado.GetStockAndTopProducts();

            ado.GetLowStockProducts(5);
        }
    }
}

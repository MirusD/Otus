using HWLinq2DB.Infrastructure;
using HWLinq2DB.Domain.Repositories;
using HWLinq2DB.BLL.Services;
using HWLinq2DB.Domain;
using HWLinq2DB.DTOs;
using Microsoft.Extensions.DependencyInjection;
using LinqToDB.Data;
using LinqToDB;

namespace HWLinq2DB
{
    internal class Program
    {
        private static CustomerService _customerService;
        private static IProductRepository _productRepository;
        private static IOrderRepository _orderRepository;
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<AppDbContext>(provider => new AppDbContext("Host=localhost;Port=5432;Database=dvdrental;Username=postgres;Password=Vtumdd2LRfNNowacUVQj"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CustomerService>();

            var serviceProvider = services.BuildServiceProvider();

            _customerService = serviceProvider.GetRequiredService<CustomerService>();
            _productRepository = serviceProvider.GetRequiredService<IProductRepository>();
            _orderRepository = serviceProvider.GetRequiredService<IOrderRepository>();

            await QueriesCustomersTable();
            await QueriesProductsTable();
            await QueriesOrdersTable();

            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Запрос который возвращает список всех пользователей старше 30 лет, у которых есть заказ на продукт с ID=1");
            Console.WriteLine(new string('=', 64));
            var customerWithProductList = await _customerService.GetCustomersWithProductAsync(productId: 1, minAge: 30);
            foreach (var item in customerWithProductList)
            {
                Console.WriteLine($"" +
                    $"FirstName: {item.FirstName}, " +
                    $"LastName: {item.LastName}, " +
                    $"ProductID: {item.ProductID}, " +
                    $"ProductQuantity: {item.ProductQuantity}, " +
                    $"ProductPrice: {item.ProductPrice}");
            }
        }

        public static async Task QueriesCustomersTable()
        {
            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем все 'Customer'");
            Console.WriteLine(new string('=', 64));
            var customers = await _customerService.GetAllAsync();
            customers.ForEach(customer => {
                Console.WriteLine($"First name: {customer.FirstName}, Last name: {customer.LastName}, Age: {customer.Age}");
            });

            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем все 'Customer' у которых age > 30");
            Console.WriteLine(new string('=', 64));
            int minAge = 30;
            var customersAboveAge = await _customerService.GetCustomersAboveAge(minAge);
            customersAboveAge.ForEach(customer =>
            {
                Console.WriteLine($"First name: {customer.FirstName}, Last name: {customer.LastName}, Age: {customer.Age}");
            });
        }

        public static async Task QueriesProductsTable()
        {
            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем все 'Product'");
            Console.WriteLine(new string('=', 64));
            var products = await _productRepository.GetAllAsync();
            products.ForEach(product => {
                Console.WriteLine($"" +
                    $"Name: {product.Name}, " +
                    $"Description: {product.Description}, " +
                    $"StockQuantity: {product.StockQuantity}, " +
                    $"Price: {product.Price}");
            });

            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем все 'Product' у которых StockQuantity < 10");
            Console.WriteLine(new string('=', 64));
            var stockLimit = 70;
            var lowStockProducts = await _productRepository.Find(product => product.StockQuantity < stockLimit);
            lowStockProducts.ForEach(product =>
            {
                Console.WriteLine($"" +
                    $"Name: {product.Name}, " +
                    $"Description: {product.Description}, " +
                    $"Price: {product.Price}");
            });
        }

        public static async Task QueriesOrdersTable()
        {
            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем все 'Order'");
            Console.WriteLine(new string('=', 64));
            var orders = await _orderRepository.GetAllAsync();
            orders.ForEach(order => {
                Console.WriteLine($"" +
                    $"CustomerID: {order.CustomerId}, " +
                    $"ProductID: {order.ProductId}, " +
                    $"Quantity: {order.Quantity}");
            });

            Console.WriteLine(new string('=', 64));
            Console.WriteLine("Получаем 'Customer' у которого CustomerId = 5");
            Console.WriteLine(new string('=', 64));
            int customerId = 5;
            var orderByCustomer = await _orderRepository.Find(customer => customer.CustomerId == customerId);
            orderByCustomer.ForEach(order =>
            {
                Console.WriteLine($"" +
                    $"CustomerID: {order.CustomerId}, " +
                    $"ProductID: {order.ProductId}, " +
                    $"Quantity: {order.Quantity}");
            });
        }

        public class AppDbContext : DataConnection
        {
            public AppDbContext(string connectionString) : base(ProviderName.PostgreSQL, connectionString) { }
            public ITable<Customer> Customers => this.GetTable<Customer>();
            public ITable<Product> Products => this.GetTable<Product>();
            public ITable<Order> Orders => this.GetTable<Order>();
        }
    }
}
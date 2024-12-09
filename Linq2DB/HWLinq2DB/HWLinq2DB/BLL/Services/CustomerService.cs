using HWLinq2DB.Domain;
using HWLinq2DB.Domain.Repositories;
using HWLinq2DB.DTOs;

namespace HWLinq2DB.BLL.Services
{
    internal class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CustomerService(ICustomerRepository customerRepository,
                               IOrderRepository orderRepository,
                               IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<List<Customer>> GetCustomersAboveAge(int minAge)
        {
            return await _customerRepository.Find(c => c.Age > minAge);
        }
        public async Task<IEnumerable<CustomerWithProductDto>> GetCustomersWithProductAsync(int productId, int minAge)
        {
            var customers = await _customerRepository.Find(c => c.Age > minAge);
            var orders = await _orderRepository.Find(o => o.ProductId == productId);

            var productIds = orders.Select(o => o.ProductId).Distinct().ToList();
            var products = await _productRepository.Find(p => productIds.Contains(p.ID));

            var result = from customer in customers
                         join order in orders on customer.ID equals order.CustomerId
                         join product in products on order.ProductId equals product.ID
                         select new CustomerWithProductDto
                         {
                             CustomerID = customer.ID,
                             FirstName = customer.FirstName,
                             LastName = customer.LastName,
                             ProductID = order.ProductId,
                             ProductQuantity = order.Quantity,
                             ProductPrice = product.Price
                         };

            return result.ToList();
        }
    }
}

using System.Linq.Expressions;

namespace HWLinq2DB.Domain.Repositories
{
    internal interface IOrderRepository
    {
        public Task<Order> AddAsync(Order newOrder);
        public Task<Order?> DeleteAsync(int orderId);
        public Task<List<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(int orderId);
        public Task<List<Order>> Find(Expression<Func<Order, bool>> predicate);
        public Task<Order?> UpdateAsync(Order updateOrder);
    }
}

using HWLinq2DB.Domain;
using HWLinq2DB.Domain.Repositories;
using LinqToDB;
using System.Linq.Expressions;
using static HWLinq2DB.Program;

namespace HWLinq2DB.Infrastructure
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<Order> AddAsync(Order newOrder)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var id = await _db.InsertWithInt32IdentityAsync(newOrder);
                    await transaction.CommitAsync();

                    newOrder.ID = id;
                    return newOrder;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при добавлении Order: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<Order?> DeleteAsync(int orderId)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingOrder = await _db.Orders.FirstOrDefaultAsync(o => o.ID == orderId);
                    if (existingOrder == null) return null;

                    await _db.Orders.DeleteAsync(o => o.ID == orderId);
                    await transaction.CommitAsync();
                    return existingOrder;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при удалении Order: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<List<Order>> GetAllAsync()
        {
            try
            {
                return await _db.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка Order: {ex.Message}");
                throw;
            }
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            try
            {
                return await _db.Orders.FirstOrDefaultAsync(o => o.ID == orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении Order по ID: {orderId}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Order>> Find(Expression<Func<Order, bool>> predicate)
        {
            try
            {
                return await _db.Orders.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении параметризованного запроса: {ex.Message}");
                throw;
            }
        }

        public async Task<Order?> UpdateAsync(Order updateOrder)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingOrder = await _db.Orders.FirstOrDefaultAsync(o => o.ID == updateOrder.ID);
                    if (existingOrder == null) return null;

                    await _db.UpdateAsync(existingOrder);
                    await transaction.CommitAsync();
                    return existingOrder;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Ошибка при обновлении Order: {ex.Message}");
                    throw;
                }
            }
        }
    }
}

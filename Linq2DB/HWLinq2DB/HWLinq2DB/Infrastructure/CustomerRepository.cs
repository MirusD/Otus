using HWLinq2DB.Domain;
using HWLinq2DB.Domain.Repositories;
using LinqToDB;
using System.Linq.Expressions;
using static HWLinq2DB.Program;

namespace HWLinq2DB.Infrastructure
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;
        public CustomerRepository(AppDbContext dbContext) 
        {
            _db = dbContext;
        }
        public async Task<Customer> AddAsync(Customer newCustomer)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var id = await _db.InsertWithInt32IdentityAsync(newCustomer);
                    await transaction.CommitAsync();

                    newCustomer.ID = id;
                    return newCustomer;
                } 
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при добавлении Сustomer: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<Customer?> DeleteAsync(int customerId)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingCustomer = await _db.Customers.FirstOrDefaultAsync(c => c.ID == customerId);
                    if (existingCustomer != null) return null;

                    await _db.Customers.DeleteAsync(c => c.ID == customerId);
                    await transaction.CommitAsync();
                    return existingCustomer;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при удалении Сustomer: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await _db.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении всех Customer: {ex.Message}");
                throw;
            }

        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            try
            {
                return await _db.Customers.FirstOrDefaultAsync(c => c.ID == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении Customer по ID: {customerId}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Customer>> Find(Expression<Func<Customer, bool>> predicate)
        {
            try
            {
                return await _db.Customers.Where(predicate).ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении параметризованного запроса: {ex.Message}");
                throw;
            }
        }

        public async Task<Customer?> UpdateAsync(Customer updateCustomer)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingCustomer = await _db.Customers.FirstOrDefaultAsync(c => c.ID == updateCustomer.ID);
                    if (existingCustomer != null) return null;

                    await _db.UpdateAsync(updateCustomer);
                    await transaction.CommitAsync();
                    return existingCustomer;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при обновлении Customer {ex.Message}");
                    throw;
                }
            }
        }
    }
}

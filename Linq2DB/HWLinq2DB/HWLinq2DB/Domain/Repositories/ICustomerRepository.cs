using static LinqToDB.Reflection.Methods.LinqToDB.Insert;
using System.Linq.Expressions;

namespace HWLinq2DB.Domain.Repositories
{
    internal interface ICustomerRepository
    {
        public Task<Customer> AddAsync(Customer newCustomer);
        public Task<Customer?> DeleteAsync(int customerId);
        public Task<List<Customer>> GetAllAsync();
        public Task<List<Customer>> Find(Expression<Func<Customer, bool>> predicate);
        public Task<Customer?> GetByIdAsync(int customerId);
        public Task<Customer?> UpdateAsync(Customer updateCustomer);
    }
}

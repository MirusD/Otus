using System.Linq.Expressions;

namespace HWLinq2DB.Domain.Repositories
{
    internal interface IProductRepository
    {
        public Task<Product> AddAsync(Product newProduct);
        public Task<Product?> DeleteAsync(int productId);
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int productId);
        public Task<List<Product>> Find(Expression<Func<Product, bool>> predicate);
        public Task<Product?> UpdateAsync(Product updateProduct);
    }
}

using HWLinq2DB.Domain;
using HWLinq2DB.Domain.Repositories;
using LinqToDB;
using System.Linq.Expressions;
using static HWLinq2DB.Program;

namespace HWLinq2DB.Infrastructure
{
    internal class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext dbContext) 
        {
            _db = dbContext;
        }
        public async Task<Product> AddAsync(Product newProduct)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var id = await _db.InsertWithInt32IdentityAsync(newProduct);
                    await transaction.CommitAsync();

                    newProduct.ID = id;
                    return newProduct;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Ошибка добавления Product: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<Product?> DeleteAsync(int productId)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingProduct = await _db.Products.FirstOrDefaultAsync(c => c.ID == productId);
                    if (existingProduct == null) return null;

                    await _db.Products.DeleteAsync(c => c.ID == productId);
                    await transaction.CommitAsync();
                    return existingProduct;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Ошибка при удалении Product: {ex.Message}");
                    throw;
                }
            }
            
        }

        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                return await _db.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка Product: {ex.Message}");
                throw;
            }
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            try
            {
                return await _db.Products.FirstOrDefaultAsync(c => c.ID == productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении Product по ID: {productId}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> Find(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                return await _db.Products.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении параметризованного запроса: {ex.Message}");
                throw;
            }
        }

        public async Task<Product?> UpdateAsync(Product updateProduct)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                try
                {
                    var existingProduct = await _db.Products.FirstOrDefaultAsync(c => c.ID == updateProduct.ID);
                    if (existingProduct == null) return null;

                    await _db.UpdateAsync(updateProduct);
                    await transaction.CommitAsync();
                    return existingProduct;
                }
                catch (Exception ex) 
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка при обновлении Product: {ex.Message}");
                    throw;
                }
            }
        }
    }
}

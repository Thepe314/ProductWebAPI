using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.PRODUCTAPI.Repository

{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // just like @Query("SELECT p FROM Product p") in Java
        // but here we write actual SQL not JPQL
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .FromSqlRaw("SELECT * FROM Products")
                .ToListAsync();
        }

        // like @Query("SELECT p FROM Product p WHERE p.id = ?1")
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FromSqlRaw("SELECT * FROM Products WHERE ProductId = {0}", id)
                .FirstOrDefaultAsync();
        }

        // INSERT - cant use FromSqlRaw for insert/update/delete
        // so we use ExecuteSqlRawAsync instead - same idea different method
        public async Task AddAsync(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Products (Name, Description, Price, Stock) VALUES ({0}, {1}, {2}, {3})",
                product.Name,
                product.Description ?? "",   // ?? "" means if null use empty string
                product.Price,
                product.Stock
            );
        }

        // UPDATE
        public async Task UpdateAsync(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "UPDATE Products SET Name = {0}, Description = {1}, Price = {2}, Stock = {3} WHERE ProductId = {4}",
                product.Name,
                product.Description ?? "",
                product.Price,
                product.Stock,
                product.ProductId
            );
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Products WHERE ProductId = {0}", id
            );
        }
    }
}
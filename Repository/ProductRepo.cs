using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.PRODUCTAPI.Repository

{
     // Repository implementation for handling product-related database operations
    public class ProductRepository : IProductRepository
    {

         // Readonly: only this class can access the DbContext
        private readonly ApplicationDbContext _context;

        // Constructor: inject ApplicationDbContext via Dependency Injection
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all products from the database
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .FromSqlRaw("SELECT * FROM Products")
                .ToListAsync();
        }

        // Retrieve a single product by ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FromSqlRaw("SELECT * FROM Products WHERE ProductId = {0}", id)
                .FirstOrDefaultAsync();
        }

        // Add a new product to the database
        public async Task AddAsync(Product product)
        {
            // ExecuteSqlRawAsync is used for insert/update/delete operations
            // ?? "" ensures that null Description is stored as an empty string
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Products (Name, Description, Price, Stock) VALUES ({0}, {1}, {2}, {3})",
                product.Name,
                product.Description ?? "",  
                product.Price,
                product.Stock
            );
        }

        // Update an existing product in the database
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

        //  Delete a product from the database by ID
        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Products WHERE ProductId = {0}", id
            );
        }
    }
}
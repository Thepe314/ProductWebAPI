
namespace IMS.PRODUCTAPI.Repository
{
    //Interface defining CRUD operations for products 
    //Acts as a blueprint for the repository implementation.
    //Actual code is written on ProductRepo.cs

    public interface IProductRepository
    {
        //Get all products from database
        Task<IEnumerable<Product>> GetAllAsync();

        //Get one product from database
        Task<Product?>GetByIdAsync(int id);

        //Add new product'
        Task AddAsync(Product product);

        //Update an existing product
        Task UpdateAsync(Product product); 

        //Delete an existing product
        Task DeleteAsync(int id);

        
    }
}


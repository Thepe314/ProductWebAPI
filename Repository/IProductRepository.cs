


namespace IMS.PRODUCTAPI.Repository
{
    //This is an interface (Blueprint)
    //Declares that methods exist and it doesnt write actual code 
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
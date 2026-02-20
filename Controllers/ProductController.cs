using Microsoft.AspNetCore.Mvc;
using IMS.PRODUCTAPI.Repository;
using IMS.PRODUCTAPI.Models;


namespace IMS.PRODUCTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // this sets the route to /api/products automatically
    public class ProductsController : ControllerBase
    {
        // I'm injecting the repository here so the controller doesn't 
        // talk to the database directly - I learned this is called separation of concerns
        private readonly IProductRepository _productRepo;

        // constructor injection - .NET automatically passes the repository in here
        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // GET /api/products
        // returns all products from the database
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepo.GetAllAsync();
            return Ok(products);
        }

        // GET /api/products/1
        // finds one product using the id from the URL
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            // I added this check so it returns a proper 404 message
            // instead of just returning nothing
            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }

        // POST /api/products
        // creates a new product - the product data comes from the request body
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {

             var product = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Stock = dto.Stock
                };
            await _productRepo.AddAsync(product);
            return Ok("Product created successfully");
        }

        // PUT /api/products/1
        // updates an existing product by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDto dto)
        {
            // first I check if the product even exists before trying to update
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound("Product not found");

            // manually updating each field because I want to control what gets changed
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;

            await _productRepo.UpdateAsync(existing);
            return Ok("Product updated successfully");
        }

        // DELETE /api/products/1
        // deletes a product by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // checking if it exists first so we don't get an error
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound("Product not found");

            await _productRepo.DeleteAsync(id);
            return Ok("Product deleted successfully");
        }
    }
}
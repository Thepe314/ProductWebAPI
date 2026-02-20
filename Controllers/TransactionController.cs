using Microsoft.AspNetCore.Mvc;
using IMS.PRODUCTAPI.Repositories;
using IMS.PRODUCTAPI.Repository;


namespace IMS.PRODUCTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        // I need both repos because buying/selling affects stock AND creates a transaction record
        private readonly ITransactionRepository _transactionRepo;
        private readonly IProductRepository _productRepo;

        public TransactionsController(ITransactionRepository transactionRepo, IProductRepository productRepo)
        {
            _transactionRepo = transactionRepo;
            _productRepo = productRepo;
        }

        // POST /api/transactions/restock
        // increases stock when we buy products from supplier
        [HttpPost("restock")]
        public async Task<IActionResult> Buy(int productId, int quantity)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) return NotFound("Product not found");

            // buying/restocking = stock goes up
            product.Stock += quantity;
            await _productRepo.UpdateAsync(product);

            // record this buy transaction in the database
            var transaction = new Transaction
            {
                ProductId = productId,
                Type = "BUY",
                Quantity = quantity,
                TotalAmount = product.Price * quantity,
                CreatedAt = DateTime.Now
            };

            await _transactionRepo.AddAsync(transaction);
            return Ok("Purchase recorded successfully");
        }

        // POST /api/transactions/sell
        // decreases stock when we sell products to customers
        [HttpPost("sell")]
        public async Task<IActionResult> Sell(int productId, int quantity)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) return NotFound("Product not found");

            // I added this so you cant sell more than what you have
            if (product.Stock < quantity)
                return BadRequest("Not enough stock to sell");

            // selling = stock goes down
            product.Stock -= quantity;
            await _productRepo.UpdateAsync(product);

            var transaction = new Transaction
            {
                ProductId = productId,
                Type = "SELL",
                Quantity = quantity,
                TotalAmount = product.Price * quantity,
                CreatedAt = DateTime.Now
            };

            await _transactionRepo.AddAsync(transaction);
            return Ok("Sale recorded successfully");
        }

        // GET /api/transactions/report?month=2&year=2026
        // monthly report - uses raw SQL query in the repository
        [HttpGet("report")]
        public async Task<IActionResult> GetMonthlyReport(int month, int year)
        {

                // I used this to convert the month number to a name for better readability
             string monthName = new DateTime(year, month, 1).ToString("MMMM");

            var transactions = await _transactionRepo.GetByMonthAsync(month, year);

            // calculating totals from the SQL results
            var report = new
            {
                Month = month,
                Year = year,
                TotalTransactions = transactions.Count(),
                TotalQuantitySold = transactions.Sum(t => t.Quantity),
                TotalRevenue = transactions.Sum(t => t.TotalAmount)
            };

            return Ok(report);
        }
    }
}
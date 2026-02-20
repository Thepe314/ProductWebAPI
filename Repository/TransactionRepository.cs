using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.PRODUCTAPI.Repositories
{
    // Repository implementation for handling transaction-related database operations
    public class TransactionRepository : ITransactionRepository 
    {
        // Readonly: only this class can access the DbContext
        private readonly ApplicationDbContext _context;

        // Constructor: inject ApplicationDbContext via Dependency Injection
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // INSERT a new transaction record
        //When event happens whether it be sell or restock
        public async Task AddAsync(Transaction transaction)
        {
            // [Transaction] is enclosed in brackets because "Transaction" is a SQL keyword
            // Use ExecuteSqlRawAsync to perform raw SQL insert
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO [Transaction](ProductId, Type, Quantity, TotalAmount, CreatedAt) VALUES ({0}, {1}, {2}, {3}, {4})",
                transaction.ProductId,
                transaction.Type,
                transaction.Quantity,
                transaction.TotalAmount,
                transaction.CreatedAt
            );
        }


        // it filters SELL transactions by month and year and calculates totals
        // [] tells sql server that Transaction is a table not a keyword
        public async Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year)
        {
            return await _context.Transactions
                .FromSqlRaw(@"
                    SELECT * FROM [Transaction]
                    WHERE Type = 'SELL'
                    AND MONTH(CreatedAt) = {0}
                    AND YEAR(CreatedAt) = {1}
                ", month, year)
                .ToListAsync();
        }
    }
}
using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.PRODUCTAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // INSERT a new transaction record
        public async Task AddAsync(Transaction transaction)
        {
            // [] tells sql server that Transaction is a table not a keyword
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO [Transaction](ProductId, Type, Quantity, TotalAmount, CreatedAt) VALUES ({0}, {1}, {2}, {3}, {4})",
                transaction.ProductId,
                transaction.Type,
                transaction.Quantity,
                transaction.TotalAmount,
                transaction.CreatedAt
            );
        }

        // this is like your @Query in Java but for monthly report
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
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
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Transactions (ProductId, Type, Quantity, TotalAmount, CreatedAt) VALUES ({0}, {1}, {2}, {3}, {4})",
                transaction.ProductId,
                transaction.Type,
                transaction.Quantity,
                transaction.TotalAmount,
                transaction.CreatedAt
            );
        }

        // this is like your @Query in Java but for monthly report
        // it filters SELL transactions by month and year and calculates totals
        public async Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year)
        {
            return await _context.Transactions
                .FromSqlRaw(@"
                    SELECT * FROM Transactions
                    WHERE Type = 'SELL'
                    AND MONTH(CreatedAt) = {0}
                    AND YEAR(CreatedAt) = {1}
                ", month, year)
                .ToListAsync();
        }
    }
}


namespace IMS.PRODUCTAPI.Repositories
{
    // Interface defining transaction-related repository operations
    public interface ITransactionRepository
    {
          // Add a transaction record to the database (buy or sell)
        Task AddAsync(Transaction transaction);

        // Get all transactions for a specific month and year
        // This is for monthly report endpoint
        Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year);
    }
}
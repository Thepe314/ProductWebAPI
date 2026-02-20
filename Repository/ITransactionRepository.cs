

namespace IMS.PRODUCTAPI.Repositories
{
    // Interface for transaction methods
    public interface ITransactionRepository
    {
        // Record a buy or sell transaction
        Task AddAsync(Transaction transaction);

        // Get all transactions for a specific month and year
        // This is for your monthly report endpoint
        Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year);
    }
}
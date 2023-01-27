using BankAccounts.Application.Repositories;
using BankAccounts.Domain.Entities;

namespace BankAccounts.Persistence.Repositories;

internal sealed class TransactionRepository : ITransactionRepository
{
    private readonly BankAccountsAppDbContext _dbContext;

    public TransactionRepository(BankAccountsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(Transaction transaction) => _dbContext.Set<Transaction>().Add(transaction);

}

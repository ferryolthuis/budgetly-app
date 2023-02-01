using BankAccounts.Application.Repositories;
using BankAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Persistence.Repositories;

internal sealed class BankAccountRepository : IBankAccountRepository
{
    private readonly BankAccountsAppDbContext _dbContext;

    public BankAccountRepository(BankAccountsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BankAccount?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<BankAccount>()
            .Where(bankAccount => bankAccount.Name == name)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<BankAccount>()
            .Where(bankAccount => bankAccount.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<BankAccount>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<BankAccount>()
            .ToListAsync(cancellationToken);
    }

    public async Task<BankAccount?> GetByIdWithTransactionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<BankAccount>()
            .Include(bankAccount => bankAccount.Transactions)
            .Where(bankAccount => bankAccount.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(BankAccount bankAccount) => _dbContext.Set<BankAccount>().Add(bankAccount);

    public void Remove(BankAccount bankAccount) => _dbContext.Set<BankAccount>().Remove(bankAccount);
}

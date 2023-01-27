using Shared.Application.Repositories;

namespace BankAccounts.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly BankAccountsAppDbContext _dbContext;

    public UnitOfWork(BankAccountsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}

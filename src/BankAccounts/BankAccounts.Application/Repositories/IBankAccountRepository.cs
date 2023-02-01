using BankAccounts.Domain.Entities;

namespace BankAccounts.Application.Repositories;

public interface IBankAccountRepository
{
    Task<BankAccount?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<BankAccount>> GetAll(CancellationToken cancellationToken = default);

    Task<BankAccount?> GetByIdWithTransactionsAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(BankAccount bankAccount);

    void Remove(BankAccount bankAccount);
}

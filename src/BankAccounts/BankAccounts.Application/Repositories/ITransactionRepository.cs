using BankAccounts.Domain.Entities;

namespace BankAccounts.Application.Repositories;

public interface ITransactionRepository
{
    void Add(Transaction transaction);
}

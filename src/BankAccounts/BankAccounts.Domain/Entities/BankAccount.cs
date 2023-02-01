using BankAccounts.Domain.ValueObjects;
using Shared.Domain.Primitives;
using Shared.Domain.Result;

namespace BankAccounts.Domain.Entities;

public sealed class BankAccount : AggregateRoot
{
    private readonly List<Transaction> _transactions = new();

    private BankAccount(Guid id, string name)
        : base(id)
    {
        Name = name;
    }
    
    private BankAccount()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public static BankAccount Create(Guid id, string name)
    {
        return new BankAccount(id, name);
    }
    
    public void Update(string name)
    {
        Name = name;
    }

    public Result<Transaction> AddTransaction(DateOnly date, string payee, string category, string memo, Money outflow, Money inflow)
    {
        Result<Transaction> transaction = Transaction.Create(Guid.NewGuid(), date, payee, category, memo, outflow, inflow, this);
        if (transaction.IsSuccess)
        {        
            _transactions.Add(transaction.Value);
        }
        return transaction;
    }
}


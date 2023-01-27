using BankAccounts.Domain.ValueObjects;
using Shared.Domain.Primitives;

namespace BankAccounts.Domain.Entities;

public sealed class Transaction : Entity
{
    internal Transaction(
        Guid id, 
        DateOnly date, 
        string payee,
        string category,
        string memo,
        Money outflow,
        Money inflow,
        BankAccount bankAccount)
        : base(id)
    {
        BankAccountId = bankAccount.Id;
        Date = date;
        Payee = payee;
        Category = category;
        Memo = memo;
        Outflow = outflow;
        Inflow = inflow;
    }

    private Transaction()
    {
    }
    
    public Guid BankAccountId { get; private set; }
    
    public DateOnly Date { get; private set; }

    public string Payee { get; private set; } = string.Empty;
    
    public string Category { get; private set; } = string.Empty;
    
    public string Memo { get; private set; } = string.Empty;
    
    public Money Outflow { get; private set; } = null!;

    public Money Inflow { get; private set; } = null!;
}

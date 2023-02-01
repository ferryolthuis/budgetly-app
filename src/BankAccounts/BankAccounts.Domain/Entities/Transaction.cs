using BankAccounts.Domain.Errors;
using BankAccounts.Domain.ValueObjects;
using Shared.Domain.Primitives;
using Shared.Domain.Result;

namespace BankAccounts.Domain.Entities;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id, 
        DateOnly date, 
        string payee,
        string category,
        string memo,
        Money outflow,
        Money inflow,
        Guid bankAccountId)
        : base(id)
    {
        BankAccountId = bankAccountId;
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
    
    public static Result<Transaction> Create(Guid id, 
        DateOnly date, 
        string payee,
        string category,
        string memo,
        Money outflow,
        Money inflow,
        BankAccount bankAccount)
    {
        if (outflow != Money.Zero() && inflow != Money.Zero())
        {
            return Result.Failure<Transaction>(DomainErrors.Transaction.BothInflowAndOutflow());
        }
        if (outflow == Money.Zero() && inflow == Money.Zero())
        {
            return Result.Failure<Transaction>(DomainErrors.Transaction.NoInflowOrOutflow());
        }
        return new Transaction(id, date, payee, category, memo, outflow, inflow, bankAccount.Id);
    }
    
    public Guid BankAccountId { get; private set; }
    
    public DateOnly Date { get; private set; }

    public string Payee { get; private set; } = string.Empty;
    
    public string Category { get; private set; } = string.Empty;
    
    public string Memo { get; private set; } = string.Empty;
    
    public Money Outflow { get; private set; } = null!;

    public Money Inflow { get; private set; } = null!;
}

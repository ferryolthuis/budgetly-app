using BankAccounts.Domain.Errors;
using Shared.Domain.Primitives;
using Shared.Domain.Result;

namespace BankAccounts.Domain.ValueObjects;

public class Money : ValueObject
{
    private Money(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; private set; }

    public static Result<Money> FromAmount(decimal value)
    {
        return value < 0 ? 
            Result.Failure<Money>(DomainErrors.Money.Negative(value))
            : new Money(value);
    }
    
    public static Money Zero() => new(0);
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

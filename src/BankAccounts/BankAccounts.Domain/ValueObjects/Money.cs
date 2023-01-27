using BankAccounts.Domain.Errors;
using Shared.Domain.Primitives;
using Shared.Domain.Result;

namespace BankAccounts.Domain.ValueObjects;

public sealed class Money : ValueObject
{
    public const int MaxLength = 50;

    private Money(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; private set; }

    public static Result<Money> Create(decimal value)
    {
        if (value < 0)
        {
            return Result.Failure<Money>(DomainErrors.Money.Negative(value));
        }

        return new Money(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using Shared.Domain.Result;

namespace BankAccounts.Domain.Errors;

public static class DomainErrors
{
    public static class BankAccount
    {
        public static readonly Func<string, Error> NameNotFound = name => new Error(
            "BankAccount.NameNotFound",
            $"The bank account with the name {name} was not found.");
        
        public static readonly Func<string, Error> DuplicateName = name => new Error(
            "BankAccount.DuplicateName",
            $"A bank account with the name {name} already exists.");
        
        public static readonly Func<Guid, Error> IdNotFound = id => new Error(
            "BankAccount.IdNotFound",
            $"The bank account with id {id} was not found.");
    }
    
    public static class Transaction
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Transaction.NotFound",
            $"The transaction with the identifier {id} was not found.");
    }
    
    public static class Money
    {
        public static readonly Func<decimal, Error> Negative = amount => new Error(
            "Money.NotFound",
            $"Money amount {amount} is invalid because money can never be negative.");
    }
}

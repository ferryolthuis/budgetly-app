using Shared.Application.Messaging;

namespace BankAccounts.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid BankAccountId,
    DateOnly Date,
    string Payee,
    string Category,
    string Memo,
    decimal Outflow,
    decimal Inflow) : ICommand<Guid>;

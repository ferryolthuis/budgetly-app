using Shared.Application.Messaging;

namespace BankAccounts.Application.Transactions.Commands.CreateBankAccount;

public sealed record DeleteBankAccountCommand(Guid BankAccountId) : ICommand<Guid>;

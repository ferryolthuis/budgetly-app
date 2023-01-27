using Shared.Application.Messaging;

namespace BankAccounts.Application.Transactions.Commands.CreateBankAccount;

public sealed record CreateBankAccountCommand(string Name) : ICommand<Guid>;

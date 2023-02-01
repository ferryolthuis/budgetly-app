using Shared.Application.Messaging;

namespace BankAccounts.Application.Transactions.Commands.CreateBankAccount;

public sealed record UpdateBankAccountCommand(Guid BankAccountId, string Name) : ICommand<Guid>;

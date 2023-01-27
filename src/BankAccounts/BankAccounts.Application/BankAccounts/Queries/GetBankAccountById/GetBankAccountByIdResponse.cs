namespace Transactions.Application.BankAccounts.Queries.GetBankAccountById;

public sealed record GetBankAccountByIdResponse(Guid Id, string Name, IReadOnlyCollection<Guid> Transactions);

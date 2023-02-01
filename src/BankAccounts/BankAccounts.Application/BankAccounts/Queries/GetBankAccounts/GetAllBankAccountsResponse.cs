namespace BankAccounts.Application.BankAccounts.Queries.GetBankAccounts;

public sealed record GetAllBankAccountsResponse(IEnumerable<BankAccountResponse> BankAccounts);

public sealed record BankAccountResponse(Guid Id, string Name);

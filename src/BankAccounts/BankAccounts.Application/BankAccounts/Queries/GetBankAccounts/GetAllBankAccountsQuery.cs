using Shared.Application.Messaging;
using Transactions.Application.BankAccounts.Queries.GetBankAccountById;

namespace BankAccounts.Application.BankAccounts.Queries.GetBankAccounts;

public record GetAllBankAccountsQuery : IQuery<GetAllBankAccountsResponse>;

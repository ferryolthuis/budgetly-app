using Shared.Application.Messaging;

namespace Transactions.Application.BankAccounts.Queries.GetBankAccountById;

public sealed record GetBankAccountByIdQuery(Guid Id) : IQuery<GetBankAccountByIdResponse>;

using BankAccounts.Application.Repositories;
using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Errors;
using Shared.Application.Messaging;
using Shared.Domain.Result;

namespace Transactions.Application.BankAccounts.Queries.GetBankAccountById;

internal sealed class GetGatheringByIdQueryHandler : IQueryHandler<GetBankAccountByIdQuery, GetBankAccountByIdResponse>
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public GetGatheringByIdQueryHandler(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<Result<GetBankAccountByIdResponse>> Handle(
        GetBankAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        BankAccount? bankAccount = await _bankAccountRepository.GetByIdWithTransactionsAsync(request.Id, cancellationToken);

        if (bankAccount is null)
        {
            return Result.Failure<GetBankAccountByIdResponse>(DomainErrors.BankAccount.IdNotFound(request.Id));
        }

        var response = new GetBankAccountByIdResponse(
            bankAccount.Id,
            bankAccount.Name,
            bankAccount
                .Transactions
                .Select(transaction => transaction.Id)
                .ToList());

        return response;
    }
}

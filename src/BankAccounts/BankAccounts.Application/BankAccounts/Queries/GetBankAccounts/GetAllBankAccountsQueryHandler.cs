using BankAccounts.Application.Repositories;
using BankAccounts.Domain.Entities;
using Shared.Application.Messaging;
using Shared.Domain.Result;

namespace BankAccounts.Application.BankAccounts.Queries.GetBankAccounts;

internal sealed class GetAllBankAccountsQueryHandler : IQueryHandler<GetAllBankAccountsQuery, GetAllBankAccountsResponse>
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public GetAllBankAccountsQueryHandler(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<Result<GetAllBankAccountsResponse>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<BankAccount> bankAccounts = await _bankAccountRepository.GetAll(cancellationToken);

        IEnumerable<BankAccountResponse> bankAccountsResponse = bankAccounts
            .Select(b => new BankAccountResponse(b.Id, b.Name));
        
        return new GetAllBankAccountsResponse(bankAccountsResponse);
    }
}


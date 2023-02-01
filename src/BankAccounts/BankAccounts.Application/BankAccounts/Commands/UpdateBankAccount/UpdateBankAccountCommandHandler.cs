using BankAccounts.Application.Repositories;
using BankAccounts.Application.Transactions.Commands.CreateBankAccount;
using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Errors;
using Shared.Application.Messaging;
using Shared.Application.Repositories;
using Shared.Domain.Result;

namespace BankAccounts.Application.Gatherings.CreateGathering;

internal sealed class UpdateBankAccountCommandHandler : ICommandHandler<UpdateBankAccountCommand, Guid>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccount? existingBankAccount = await _bankAccountRepository.GetByIdAsync(request.BankAccountId, cancellationToken);

        if (existingBankAccount is null)
        {
            return Result.Failure<Guid>(DomainErrors.BankAccount.IdNotFound(request.BankAccountId));
        }
        
        BankAccount? bankAccountWithName = await _bankAccountRepository.GetByNameAsync(request.Name, cancellationToken);

        if (bankAccountWithName is not null)
        {
            return Result.Failure<Guid>(DomainErrors.BankAccount.DuplicateName(request.Name));
        }

        existingBankAccount.Update(request.Name);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return existingBankAccount.Id;
    }
}

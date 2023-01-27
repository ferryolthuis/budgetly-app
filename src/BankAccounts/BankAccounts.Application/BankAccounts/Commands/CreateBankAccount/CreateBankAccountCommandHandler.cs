using BankAccounts.Application.Repositories;
using BankAccounts.Application.Transactions.Commands.CreateBankAccount;
using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Errors;
using Shared.Application.Messaging;
using Shared.Application.Repositories;
using Shared.Domain.Result;

namespace BankAccounts.Application.Gatherings.CreateGathering;

internal sealed class CreateGatheringCommandHandler : ICommandHandler<CreateBankAccountCommand, Guid>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGatheringCommandHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccount? bankAccount = await _bankAccountRepository.GetByNameAsync(request.Name, cancellationToken);

        if (bankAccount is not null)
        {
            return Result.Failure<Guid>(DomainErrors.BankAccount.DuplicateName(request.Name));
        }

        bankAccount = BankAccount.Create(Guid.NewGuid(), request.Name);

        _bankAccountRepository.Add(bankAccount);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bankAccount.Id;
    }
}

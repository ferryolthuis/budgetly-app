using BankAccounts.Application.Repositories;
using BankAccounts.Application.Transactions.Commands.CreateBankAccount;
using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Errors;
using Shared.Application.Messaging;
using Shared.Application.Repositories;
using Shared.Domain.Result;

namespace BankAccounts.Application.Gatherings.CreateGathering;

internal sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Guid>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccount? bankAccount = await _bankAccountRepository.GetByIdAsync(request.BankAccountId, cancellationToken);

        if (bankAccount is null)
        {
            return Result.Failure<Guid>(DomainErrors.BankAccount.IdNotFound(request.BankAccountId));
        }

        _bankAccountRepository.Remove(bankAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bankAccount.Id;
    }
}

using BankAccounts.Application.Repositories;
using BankAccounts.Application.Transactions.Commands.CreateTransaction;
using BankAccounts.Domain.Errors;
using BankAccounts.Domain.ValueObjects;
using Shared.Application.Messaging;
using Shared.Application.Repositories;
using Shared.Domain.Result;

namespace BankAccounts.Application.Invitations.SendInvitation;

internal sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(
        IBankAccountRepository bankAccountRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await _bankAccountRepository
            .GetByIdAsync(request.BankAccountId, cancellationToken);

        if (bankAccount is null)
        {
            return Result.Failure<Guid>(DomainErrors.BankAccount.IdNotFound(request.BankAccountId));
        }
        
        var outflowResult = Money.Create(request.Outflow);
        var inflowResult = Money.Create(request.Inflow);
        
        if (outflowResult.IsFailure)
        {
            return Result.Failure<Guid>(outflowResult.Error);
        }
        if (inflowResult.IsFailure)
        {
            return Result.Failure<Guid>(inflowResult.Error);
        }
        
        var transaction = bankAccount.AddTransaction(
            request.Date, 
            request.Payee, 
            request.Category, 
            request.Memo,
            outflowResult.Value,
            inflowResult.Value);

        if (transaction.IsFailure)
        {
            // Log error
            return Result.Failure<Guid>(transaction.Error);
        }

        _transactionRepository.Add(transaction.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transaction.Value.Id;
    }
}

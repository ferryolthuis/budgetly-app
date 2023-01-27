using BankAccounts.Application.Transactions.Commands.CreateBankAccount;
using BankAccounts.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Result;
using Shared.Presentation.Abstractions;
using Transactions.Application.BankAccounts.Queries.GetBankAccountById;

namespace BankAccounts.Presentation.Controllers;

[Route("api/bankaccounts")]
public sealed class BankAccountsController : ApiController
{
    public BankAccountsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetBankAccountById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBankAccountByIdQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBankAccount(
        [FromBody] CreateBankAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateBankAccountCommand(request.Name);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        
        return CreatedAtAction(
            nameof(GetBankAccountById),
            new { id = result.Value },
            result.Value);
    }
}

using BankAccounts.Application.BankAccounts.Queries.GetBankAccounts;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBankAccountById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBankAccountByIdQuery(id);

        Result<GetBankAccountByIdResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }

    [HttpGet] 
    public async Task<IActionResult> GetAllBankAccounts(CancellationToken cancellationToken)
    {
        var query = new GetAllBankAccountsQuery();

        Result<GetAllBankAccountsResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBankAccountCommand(request.Name);

        Result<Guid> response = await Sender.Send(command, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        
        return CreatedAtAction(
            nameof(GetBankAccountById),
            new { id = response.Value },
            response.Value);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBankAccount(Guid id, [FromBody] UpdateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBankAccountCommand(id, request.Name);

        Result<Guid> response = await Sender.Send(command, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        
        return CreatedAtAction(
            nameof(GetBankAccountById),
            new { id = response.Value },
            response.Value);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteBankAccount(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteBankAccountCommand(id);

        Result<Guid> response = await Sender.Send(command, cancellationToken);
        
        return response.IsSuccess
            ? Ok(response.Value)
            : HandleFailure(response);
    }
}

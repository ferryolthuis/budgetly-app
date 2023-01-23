using MediatR;
using Shared.Domain.Result;

namespace Shared.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

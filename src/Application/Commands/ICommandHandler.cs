using BadBroker.Application.Common.Models;

namespace BadBroker.Application.Commands
{
    public interface ICommandHandler<TRequest, TResponse>
    {
        Task<Result<TResponse>> HandleAsync(TRequest request);
    }
}
using MediatR;
using Serilog;

namespace Lib.Core.Config.MediatR.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            Log.Error(ex, "API Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}

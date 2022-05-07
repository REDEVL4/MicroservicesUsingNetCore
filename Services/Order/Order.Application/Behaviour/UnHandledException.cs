using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.Application.Behaviour
{
    public class UnHandledException<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest:MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnHandledException(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestname=typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled exception occured at Request {Name} {@Request}", requestname, request);
                throw;
            }
        }
    }
}

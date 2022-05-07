using FluentValidation;
using MediatR;
using ValidationException = Order.Application.Exceptions.ValidationException;

namespace Order.Application.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest:MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            if (_validator.Any())
            {
                var validationResults = await Task.WhenAll(_validator.Select(c => c.ValidateAsync(request, cancellationToken)));
                var failures = validationResults.SelectMany(c => c.Errors).Where(e => e != null).ToList();
                if (failures.Any())
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}

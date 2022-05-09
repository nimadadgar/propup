using Cmms.Core.Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

          
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
             .Select(x => x.Validate(context))
             .SelectMany(x => x.Errors)
             .Where(x => x != null)
             .GroupBy(
                 x => x.PropertyName,
                 x => x.ErrorMessage,
                 (propertyName, errorMessages) => new ValidationExceptionModel
                 {
                     key = propertyName,
                     value = errorMessages.FirstOrDefault()
                 }).ToList();



            if (errorsDictionary.Count != 0)
            {
                throw new Cmms.Core.Application.Models.ValidationException(errorsDictionary);
            }

            return await next();
        }
    }
}

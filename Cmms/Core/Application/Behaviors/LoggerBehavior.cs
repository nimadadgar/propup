using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Behaviors
{
    public class EventLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>

        {
            // readonly IEventStoreDbContext _eventStoreDbContext;
            private readonly ILogger<EventLoggerBehavior<TRequest, TResponse>> _logger;

            public EventLoggerBehavior(ILogger<EventLoggerBehavior<TRequest, TResponse>> logger)
            {
                _logger = logger;
            }

            public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
            {
           
                TResponse response = await next();
                string requestName = request.ToString();
                return response;
            
            //await _eventStoreDbContext.AppendToStreamAsync(eventData);
        }

    }

}

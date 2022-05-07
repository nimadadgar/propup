using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Presentation
{
    public class FunctionBase
    {
        public ObjectResult BadResponse(string message)
        {
            var result = new ObjectResult(new BadResponseViewModel(message, Guid.NewGuid().ToString()));
            result.StatusCode = StatusCodes.Status400BadRequest;
            return result;
        }
        public ObjectResult BadResponse(string message, int statusCode)
        {
            var result = new ObjectResult(new BadResponseViewModel(message, Guid.NewGuid().ToString()));
            result.StatusCode = statusCode;
            return result;

        }

        public ObjectResult BadResponse(string message, int statusCode,object data)
        {
            var result = new ObjectResult(new BadResponseViewModel(message,
                Guid.NewGuid().ToString(),data));
          
            result.StatusCode = statusCode;
            return result;

        }



        public ObjectResult OkResponse(object data)
        {
            var result = new ObjectResult(new ResponseViewModel(data));
            result.StatusCode = StatusCodes.Status200OK;
            return result;

        }

        public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (ValidationException ex)
            {
                return  BadResponse("Please Check Your Forms",StatusCodes.Status400BadRequest,ex.errors);
            }
            catch (BadRequestException ex)
            {
                return BadResponse(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return  BadResponse(ex.Message);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TaskingoAPI.Exceptions;

namespace TaskingoAPI
{
    public class ErrorHandlingMiddleware : IMiddleware
    { /*
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        */
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFound ex)
            {
              //  _logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                //_logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (CannotParse ex)
            {
               // _logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something goes wrong.");
            }
        }
    }
}

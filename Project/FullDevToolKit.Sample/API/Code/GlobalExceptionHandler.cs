using FullDevToolKit.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Context;
using System.Security.Claims;
using FullDevToolKit.Sys.Models.Common;

namespace API.Code
{

    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IContext _context;
      
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
            ISystemContext context)
        {
            _logger = logger;
            _context = context;             
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Caught a global exception: {Message}", exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
           
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred."
            };

            string userid = "";

            ClaimsPrincipal user = httpContext.User; 
            if (user != null)
            {
               userid =  user.Identity.Name;                 
            }
            else
            {
                userid = ""; 
            }

            try
            {
                ExceptionLogEntry reg = new ExceptionLogEntry();
                reg.ExceptionLogID = FullDevToolKit.Helpers.Utilities.GenerateId();
                reg.UserID = userid;
                reg.Date = DateTime.Now;

                if (httpContext.GetEndpoint() is not null)
                {
                    reg.Origin = httpContext.GetEndpoint().DisplayName;
                }
                else
                {
                    reg.Origin = null;
                }
                
                reg.TargetSite = exception.TargetSite.Name;
                reg.StackTrace = exception.StackTrace;
                reg.ErrMessage = exception.Message;
                reg.ClientIP = httpContext.Connection.RemoteIpAddress?.ToString(); 
                              
                await _context.RegisterExceptionLog(reg);
            }
            catch(Exception ex)
            {

            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true; 
        }
    }

}
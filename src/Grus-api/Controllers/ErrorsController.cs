﻿using Grus.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace Grus.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        return Problem(statusCode: statusCode, title: message);
    }
}
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarMechanicWorkshop.Api.Filters;

/// <summary>
/// Logs every incoming request and outgoing response inside a controller action.
/// Useful for development/debugging and service analytics.
/// </summary>
public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor.DisplayName;

        _logger.LogInformation("Executing action: {Action}", action);

        foreach (var arg in context.ActionArguments)
        {
            _logger.LogInformation("Argument: {Name} = {@Value}", arg.Key, arg.Value);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var action = context.ActionDescriptor.DisplayName;

        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, "Exception in action: {Action}", action);
            return;
        }

        _logger.LogInformation("⬅️ Executed action: {Action}", action);
    }
}

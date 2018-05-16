using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace lab4.Extensions.Filters
{
    public class LogFilter : Attribute, IActionFilter
    {
        private ILogger _logger;

        public LogFilter(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger("LogEctionFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action - {context.RouteData.Values["action"]}");
        }
    }
}

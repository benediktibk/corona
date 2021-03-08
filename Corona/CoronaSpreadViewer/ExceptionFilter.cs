using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace CoronaSpreadViewer {
    public class ExceptionFilter : ExceptionFilterAttribute {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext context) {
            _logger.Error(context.Exception);
            base.OnException(context);
        }
    }
}
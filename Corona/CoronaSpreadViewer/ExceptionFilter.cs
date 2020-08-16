using System.Web.Http.Filters;
using NLog;

namespace CoronaSpreadViewer {
    public class ExceptionFilter : ExceptionFilterAttribute {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext context) {
            _logger.Error(context.Exception);
            base.OnException(context);
        }
    }
}
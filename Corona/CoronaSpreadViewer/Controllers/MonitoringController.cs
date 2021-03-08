using Microsoft.AspNetCore.Mvc;

namespace CoronaSpreadViewer.Controllers {
    [ApiController]
    public class MonitoringController : ControllerBase {
        [HttpGet]
        [Route("api/monitoring")]
        public string Get() {
            return "ok";
        }
    }
}

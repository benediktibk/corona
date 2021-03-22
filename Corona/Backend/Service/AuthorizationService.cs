using NLog;
using System.Linq;

namespace Backend.Service {
    public class AuthorizationService : IAuthorizationService {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISettings _settings;

        public AuthorizationService(ISettings settings) {
            _settings = settings;
        }

        public bool IsAllowedToUpdateData(string username, string password) {
            return false;
        }
    }
}

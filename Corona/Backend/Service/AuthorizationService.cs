using NLog;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Backend.Service {
    public class AuthorizationService : IAuthorizationService {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISettings _settings;

        public AuthorizationService(ISettings settings) {
            _settings = settings;
        }

        public bool IsAllowedToUpdateData(string username, string password) {
            using (var context = new PrincipalContext(ContextType.Domain)) {
                if (!context.ValidateCredentials(username, password)) {
                    _logger.Info($"password for user {username} is incorrect");
                    return false;
                }
            }

            var result = _settings.AdminUsers.Contains(username);

            if (!result) {
                _logger.Info($"user {username} is not allowed to execute the update");
            }

            return result;
        }
    }
}

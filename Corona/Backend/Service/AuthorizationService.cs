using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Backend.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettings _settings;

        public AuthorizationService(ISettings settings) {
            _settings = settings;
        }

        public bool IsAllowedToUpdateData(string username, string password) {
            using (var context = new PrincipalContext(ContextType.Domain)) {
                if (!context.ValidateCredentials(username, password)) {
                    return false;
                }
            }

            return _settings.AdminUsers.Contains(username);
        }
    }
}

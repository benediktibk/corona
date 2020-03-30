namespace Backend.Service
{
    public interface IAuthorizationService
    {
        bool IsAllowedToUpdateData(string username, string password);
    }
}
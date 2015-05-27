namespace Mauth.Core
{
    public interface IAuthService
    {
        AuthResult Authenticate(string username, string clearTextPassword);

        void Create(string username, string clearTextPassword, string[] roles);

        void Delete(string username);

        void Update(UserCredentials credentials);
    }
}

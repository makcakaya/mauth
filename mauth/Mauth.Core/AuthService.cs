// A blog post containing some of the methods here: http://blog.magnusmontin.net/2013/03/24/custom-authorization-in-wpf/

using Mutil.Core.Assertion;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mauth.Core
{
    public class AuthService : IAuthService
    {
        private IAuthPersistanceService _persistanceService;

        public AuthService(IAuthPersistanceService persistanceService)
        {
            Ensure.NotNull(persistanceService, "persistanceService");

            _persistanceService = persistanceService;
        }

        public AuthResult Authenticate(string username, string clearTextPassword)
        {
            Ensure.NotNullOrWhitespace(username, "username");
            Ensure.NotNullOrWhitespace(clearTextPassword, "clearTextPassword");

            var hash = ComputeHash(clearTextPassword, username);
            var queryResult = _persistanceService.Get(u => u.Username == username && u.HashedPassword == hash);

            Ensure.If(queryResult != null && queryResult.Count() > 1).Throw(new InvalidOperationException("More than one record exist with the given username and password"));
            if (queryResult != null && queryResult.Count() == 1)
            {
                var credentials = queryResult.ElementAt(0);
                if (!credentials.IsBlocked)
                {
                    return new AuthResult(new UserInfo(credentials.Username, credentials.Roles));
                }
                else
                {
                    return new AuthResult(FailureReason.Blocked);
                }
            }
            else
            {
                return new AuthResult(FailureReason.IncorrectUsernameOrPassword);
            }
        }

        public void Create(string username, string clearTextPassword, string[] roles)
        {
            Ensure.NotNullOrWhitespace(username, "username");
            Ensure.NotNullOrWhitespace(clearTextPassword, "clearTextPassword");
            Ensure.NotNull(roles, "roles");

            var hash = ComputeHash(clearTextPassword, username);
            _persistanceService.Save(new UserCredentials(username, hash, roles));
        }

        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(UserCredentials credentials)
        {
            throw new NotImplementedException();
        }

        private string ComputeHash(string clearTextPassword, string salt)
        {
            var saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            var algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(saltedHashBytes);

            return Convert.ToBase64String(hash);
        }
    }
}

using Mutil.Core.Assertion;

namespace Mauth.Core
{
    public class UserCredentials
    {
        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string[] Roles { get; set; }

        public bool IsBlocked { get; set; }

        public UserCredentials(string username, string hashedPassword, string[] roles, bool isBlocked = false)
        {
            Ensure.NotNullOrWhitespace(username, "username");
            Ensure.NotNullOrWhitespace(hashedPassword, "hashedPassword");

            Username = username;
            HashedPassword = hashedPassword;
            Roles = roles;
            IsBlocked = isBlocked;
            Roles = roles;
        }
    }
}

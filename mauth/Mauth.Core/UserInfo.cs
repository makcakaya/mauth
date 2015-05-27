using Mutil.Core.Assertion;

namespace Mauth.Core
{
    public class UserInfo
    {
        public string Username { get; private set; }

        public string[] Roles { get; private set; }

        public UserInfo(string username, string[] roles)
        {
            Ensure.NotNullOrWhitespace(username, "username");
            Ensure.NotNull(roles, "roles");

            Username = username;
            Roles = roles;
        }
    }
}

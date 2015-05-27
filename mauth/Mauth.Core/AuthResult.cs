using Mutil.Core.Assertion;

namespace Mauth.Core
{
    public sealed class AuthResult
    {
        public bool IsSuccess { get { return UserInfo != null; } }

        public FailureReason? FailureReason { get; private set; }

        public UserInfo UserInfo { get; private set; }

        public AuthResult(FailureReason failureReason)
        {
            FailureReason = failureReason;
        }

        public AuthResult(UserInfo userInfo)
        {
            Ensure.NotNull(userInfo, "userInfo");

            UserInfo = userInfo;
        }
    }
}

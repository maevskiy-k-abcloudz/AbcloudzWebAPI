using AbcloudzWebAPI.Infrastructure.Services;

namespace AbcloudzWebAPI.Domain
{
    public class SecurityService : ISecurityService
    {
        public string HashPwd(string password)
        {
            return password;
        }
    }
}

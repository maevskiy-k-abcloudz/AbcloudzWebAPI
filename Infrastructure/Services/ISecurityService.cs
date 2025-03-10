namespace AbcloudzWebAPI.Infrastructure.Services
{
    public interface ISecurityService
    {
        string HashPwd(string password);
    }
}

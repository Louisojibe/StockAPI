using TestWebApp.Models;

namespace TestWebApp.Interfaces
{
    public interface ITokenService
    {
            string CreateToken(appUser user);
    }
}

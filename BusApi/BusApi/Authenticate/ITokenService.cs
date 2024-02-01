using BusApi.Models;

namespace BusApi.Authenticate
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

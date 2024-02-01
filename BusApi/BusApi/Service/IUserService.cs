using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface IUserService
    {
        Task<bool> AddUser(RegisterUser user, string avatar, string role);
        Task<IEnumerable<User>> GetAllInfoUsers();
        Task<User> GetUserInfoByKeyWord(Dictionary<string, string> keywords);
        Task<bool> EditUser(User user);
        Task<IEnumerable<User>> GetUsersByRole(string role);
        Task<bool> DeleteUser(string id);
        Task<IEnumerable<User>> GetUsersByKeyWordAndRole(string role, string keyword);
        Task<User> GetUserByUsername(string username);
    }
}

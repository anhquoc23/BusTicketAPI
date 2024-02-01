using BusApi.Models;

namespace BusApi.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user, string password);
        Task<IEnumerable<User>> GetAllInfoUsers();
        Task<User> GetUserInfoByKeyWord(Dictionary<string, string> keywords);
        Task<bool> EditUser(User user);
        Task<IEnumerable<User>> GetUsersByRole(string role);
        Task<bool> DeleteUser(string id);
        Task<IEnumerable<User>> GetUsersByKeyWordAndRole(string role, string keyword);
        Task<User> GetUserByUsername(string username);
    }
}

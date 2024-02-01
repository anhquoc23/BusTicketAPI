using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> AddUser(RegisterUser registerUser, string avatar, string role)
        {
            var user = new User
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                PhoneNumber = registerUser.Phone,
                Avatar = avatar,
                RoleName = role
            };
            return await this._userRepository.AddUser(user, registerUser.Password);
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await this._userRepository.DeleteUser(id);
        }

        public Task<bool> EditUser(User user)
        {
            return this._userRepository.EditUser(user);
        }

        public async Task<IEnumerable<User>> GetAllInfoUsers()
        {
            return await this._userRepository.GetAllInfoUsers();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await this._userRepository.GetUserByUsername(username);
        }

        public async Task<User> GetUserInfoByKeyWord(Dictionary<string, string> keywords)
        {
            var user = await this._userRepository.GetUserInfoByKeyWord(keywords);
            return user;
        }

        public Task<IEnumerable<User>> GetUsersByKeyWordAndRole(string role, string keyword)
        {
            return this._userRepository.GetUsersByKeyWordAndRole(role, keyword);
        }

        public Task<IEnumerable<User>> GetUsersByRole(string role)
        {
            return this._userRepository.GetUsersByRole(role);
        }
    }
}

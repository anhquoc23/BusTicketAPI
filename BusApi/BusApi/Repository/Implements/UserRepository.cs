using BusApi.Data;
using BusApi.Data.Enum;
using BusApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BusApi.Repository.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDataContext _context;
        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDataContext applicationDataContext) 
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = applicationDataContext;
        }

        public async Task<bool> AddUser(User user, string password)
        {
            var isRoleExist = await this._roleManager.RoleExistsAsync(user.RoleName);
            if (!isRoleExist)
                await this._roleManager.CreateAsync(new IdentityRole(user.RoleName));
            var result = await this._userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await this._userManager.AddToRoleAsync(user, user.RoleName);
            }
            return true;
        }

        public async Task<bool> DeleteUser(string id)
        {
            User user = await this._userManager.FindByIdAsync(id);
            try
            {
                user.IsActive = false;
                user.LockoutEnabled = false;
                await this._userManager.UpdateAsync(user);
                this._context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EditUser(User user)
        {
            var userUpdate = await this._userManager.FindByNameAsync(user.UserName);
            if (userUpdate != null)
            {
                userUpdate.PhoneNumber = user.PhoneNumber;
                userUpdate.FirstName = user.FirstName;
                userUpdate.LastName = user.LastName;
                userUpdate.Email = user.Email;
                userUpdate.UpdateDate = user.UpdateDate;
            }
            var result = await this._userManager.UpdateAsync(userUpdate);
            if (result.Succeeded)
            {
                this._context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllInfoUsers()
        {
            var infoUsers = await this._context.Users.GroupJoin(this._context.UserRoles,
                                                    user => user.Id, userRole => userRole.UserId,
                                                    (user, userRole) => new { Users = user, UserRoles = userRole }).SelectMany(
                                                    x => x.UserRoles.ToList().DefaultIfEmpty(),
                                                    (user, userRole) => new
                                                    {
                                                        Users = user.Users,
                                                        UserRoles = userRole
                                                    }).GroupJoin(this._context.Roles, ur => ur.UserRoles.RoleId, r => r.Id,
                                                    (ur, r) => new { UserRoles = ur, Roles = r }).SelectMany(
                                                    x => x.Roles.ToList().DefaultIfEmpty(),
                                                    (u, r) => new User
                                                    {
                                                        Id = u.UserRoles.Users.Id,
                                                        FirstName = u.UserRoles.Users.FirstName,
                                                        LastName = u.UserRoles.Users.LastName,
                                                        Email = u.UserRoles.Users.Email,
                                                        PhoneNumber = u.UserRoles.Users.PhoneNumber,
                                                        Avatar = u.UserRoles.Users.Avatar,
                                                        RoleId = r.Id,
                                                        RoleName = r.Name,
                                                    }).ToListAsync();
            return infoUsers;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var result = await _context.Users.Where(u => u.UserName.ToLower() == username && u.IsActive). FirstOrDefaultAsync();
            return result;
        }

        public async Task<User> GetUserInfoByKeyWord(Dictionary<string, string> keywords)
        {
            var infoUser = await this._context.Users.Where(user => keywords.ContainsKey("Id") ? user.Id == keywords["Id"] : keywords.ContainsKey("Username") ? 
                user.UserName == keywords["Username"] : true).Where(user => user.IsActive).GroupJoin(this._context.UserRoles,
                                                    user => user.Id, userRole => userRole.UserId,
                                                    (user, userRole) => new { Users = user, UserRoles = userRole }).SelectMany(
                                                    x => x.UserRoles.ToList().DefaultIfEmpty(),
                                                    (user, userRole) => new
                                                    {
                                                        Users = user.Users,
                                                        UserRoles = userRole
                                                    }).GroupJoin(this._context.Roles, ur => ur.UserRoles.RoleId, r => r.Id,
                                                    (ur, r) => new { UserRoles = ur, Roles = r }).SelectMany(
                                                    x => x.Roles.ToList().DefaultIfEmpty(),
                                                    (u, r) => new User
                                                    {
                                                        Id = u.UserRoles.Users.Id,
                                                        FirstName = u.UserRoles.Users.FirstName,
                                                        LastName = u.UserRoles.Users.LastName,
                                                        Email = u.UserRoles.Users.Email,
                                                        PhoneNumber = u.UserRoles.Users.PhoneNumber,
                                                        Avatar = u.UserRoles.Users.Avatar,
                                                        RoleId = r.Id,
                                                        RoleName = r.Name,
                                                        UserName = u.UserRoles.Users.UserName
                                                    }).FirstOrDefaultAsync();
            return infoUser;
        }

        public async Task<IEnumerable<User>> GetUsersByKeyWordAndRole(string role, string keyword)
        {
            var users = await this._context.Users.Where(x => (x.FirstName.Contains(keyword) || x.LastName.Contains(keyword)
            || x.PhoneNumber.Contains(keyword) || x.Email.Contains(keyword) || x.UserName.Contains(keyword)) && x.IsActive).Join(this._context.UserRoles, user => user.Id, userRole => userRole.UserId,
                (user, userRole) => new
                {
                    Users = user,
                    UserRoles = userRole,
                }).Join(this._context.Roles, userRole => userRole.UserRoles.RoleId, role => role.Id,
                (userRole, role) => new User
                {
                    Id = userRole.Users.Id,
                    FirstName = userRole.Users.FirstName,
                    LastName = userRole.Users.LastName,
                    Email = userRole.Users.Email,
                    PhoneNumber = userRole.Users.PhoneNumber,
                    UserName = userRole.Users.UserName,
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Avatar = userRole.Users.Avatar,
                    CreatedDate = userRole.Users.CreatedDate,
                    UpdateDate = userRole.Users.UpdateDate
                }).Where(x => x.RoleName.Equals(role)).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersByRole(string role)
        {
            var users = await this._context.Users.Where(user => user.IsActive).GroupJoin(this._context.UserRoles, user => user.Id, userRole => userRole.UserId
            , (user, userRole) => new {User = user, UserRole = userRole}).SelectMany(x => x.UserRole.ToList().DefaultIfEmpty(),
            (user, userRole) => new
            {
                Users = user.User,
                UserRoles = userRole
            }).GroupJoin(this._context.Roles, user => user.UserRoles.RoleId, role => role.Id, (userRole, role) => new
            {
                UserRoles = userRole,
                Roles = role
            }).SelectMany(x => x.Roles.ToList().DefaultIfEmpty(),
            (user, role) => new User
            {
                Id = user.UserRoles.Users.Id,
                FirstName = user.UserRoles.Users.FirstName,
                LastName = user.UserRoles.Users.LastName,
                Email = user.UserRoles.Users.Email,
                PhoneNumber = user.UserRoles.Users.PhoneNumber,
                UserName = user.UserRoles.Users.UserName,
                RoleId = role.Id,
                RoleName = role.Name,
                Avatar = user.UserRoles.Users.Avatar,
                CreatedDate = user.UserRoles.Users.CreatedDate,
                UpdateDate = user.UserRoles.Users.UpdateDate
            }).Where(x => x.RoleName == role).ToListAsync();
            return users;
        }
    }
}

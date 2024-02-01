using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repository.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager) 
        {
            this._roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            var result = await this._roleManager.Roles.ToListAsync();
            return result;
        }
    }
}

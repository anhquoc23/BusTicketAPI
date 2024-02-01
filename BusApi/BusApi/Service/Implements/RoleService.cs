using BusApi.Repository;
using Microsoft.AspNetCore.Identity;

namespace BusApi.Service.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) 
        {
            this._roleRepository = roleRepository;
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await this._roleRepository.GetRoles();
        }
    }
}

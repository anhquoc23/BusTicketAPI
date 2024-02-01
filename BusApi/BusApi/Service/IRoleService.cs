using Microsoft.AspNetCore.Identity;

namespace BusApi.Service
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}

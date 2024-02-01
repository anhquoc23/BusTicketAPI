using Microsoft.AspNetCore.Identity;

namespace BusApi.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}

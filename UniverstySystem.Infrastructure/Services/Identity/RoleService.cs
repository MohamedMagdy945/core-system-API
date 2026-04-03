using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces.Identity;

namespace UniverstySystem.Infrastructure.Services.Identity
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public RoleService(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityRole<int>?> GetByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }


        public async Task<List<IdentityRole<int>>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);

            if (exists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Role already exists"
                });
            }

            var role = new IdentityRole<int>
            {
                Name = roleName
            };

            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Role not found"
                });
            }

            return await _roleManager.DeleteAsync(role);
        }


    }
}

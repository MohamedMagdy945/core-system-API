using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Domain.Entities.Identity;
using AppCoreSystem.Application.Interfaces.Identity;

namespace UniverstySystem.Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser?> GetByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }


        public async Task<AppUser?> CheckPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username)
               ?? await _userManager.FindByEmailAsync(username);

            if (user == null) return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            return isPasswordValid ? user : null;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<(IdentityResult Result, AppUser? User)> CreateAsync(string userName, string email, string password)
        {
            var user = new AppUser
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result, user);
        }
        public async Task<IdentityResult> DeleteAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found"
                });

            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> UpdateAsync(int id, string userName, string email)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found"
                });
            }

            user.Email = email;
            user.UserName = userName;

            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> AddRoleAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found"
                });
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Role does not exist"
                });
            }

            var alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);

            if (alreadyInRole)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User already has this role"
                });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveRoleAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found"
                });
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Role does not exist"
                });
            }

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);

            if (!isInRole)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User does not have this role"
                });
            }

            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }
        public async Task<IList<string>> GetRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new List<string>();
            }

            return await _userManager.GetRolesAsync(user);
        }

    }
}

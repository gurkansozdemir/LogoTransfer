using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbset;
        private readonly DbSet<Role> _roledbset;
        public UserRepository(AppDbContext context) : base(context)
        {
            _dbset = context.Set<User>();
            _roledbset = context.Set<Role>();
        }

        public async Task<User> GetByUserNameAndPasswordAsync(User user)
        {
            return await _dbset
                .Include(r => r.Role)
                .Where(x => x.UserName == user.EMail || x.EMail == user.EMail)
                .SingleOrDefaultAsync();
        }

        public async Task<Role> GetMenuItemsAsync(Guid roleId)
        {
            return await _roledbset.Include(x => x.MenuItems).Where(x => x.Id == roleId).SingleOrDefaultAsync();
        }

        public async Task<List<User>> AllWithRoleAsync()
        {
            return await _dbset.Include(x => x.Role).ToListAsync();
        }
    }
}

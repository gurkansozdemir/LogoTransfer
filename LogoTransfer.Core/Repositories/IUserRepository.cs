using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUserNameAndPasswordAsync(User user);
        public Task<Role> GetMenuItemsAsync(Guid roleId);
        public Task<List<User>> AllWithRoleAsync();
    }
}

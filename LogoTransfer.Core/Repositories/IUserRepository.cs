using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAndPasswordAsync(User user);
        Task<Role> GetMenuItemsAsync(Guid roleId);
    }
}

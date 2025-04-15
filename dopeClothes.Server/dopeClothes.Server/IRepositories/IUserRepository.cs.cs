using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {

        void Update(ApplicationUser entity);
    }
}

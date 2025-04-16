using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IWishListRepository : IRepository<WhishList>
    {

        void Update(WhishList entity);
    }
}

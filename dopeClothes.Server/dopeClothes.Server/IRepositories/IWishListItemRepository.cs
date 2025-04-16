using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IWishListItemRepository : IRepository<WishListItem>
    {

        void Update(WishListItem entity);
    }
}

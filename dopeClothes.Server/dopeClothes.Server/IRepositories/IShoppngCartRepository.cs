using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {

        void Update(ShoppingCartItem entity);
    }
}

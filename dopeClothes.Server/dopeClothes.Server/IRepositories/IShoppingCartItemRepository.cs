using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {

        void Update(ShoppingCart entity);
    }
}

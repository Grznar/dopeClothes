using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;

namespace dopeClothes.Server.Repositories
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ShoppingCartItem entity)
        {
            _db.ShoppingCartItems.Update(entity);
        }
    }
}

using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;

namespace dopeClothes.Server.Repositories
{
    public class WhishListItemRepository : Repository<WishListItem>, IWishListItemRepository
    {
        private readonly ApplicationDbContext _db;
        public WhishListItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(WishListItem entity)
        {
            _db.WishListItems.Update(entity);
        }
    }
}

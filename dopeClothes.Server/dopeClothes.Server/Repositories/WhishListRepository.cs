using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;

namespace dopeClothes.Server.Repositories
{
    public class WhishListRepository : Repository<WhishList>, IWishListRepository
    {
        private readonly ApplicationDbContext _db;
        public WhishListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(WhishList entity)
        {
            _db.WhishLists.Update(entity);
        }
    }
}

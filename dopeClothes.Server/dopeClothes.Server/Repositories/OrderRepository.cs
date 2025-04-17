using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;

namespace dopeClothes.Server.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Order entity)
        {
            _db.Orders.Update(entity);
        }
    }
}

using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {

        void Update(Order entity);
    }
}

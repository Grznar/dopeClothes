using dopeClothes.Server.Models;

namespace dopeClothes.Server.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {

        void Update(Product entity);
    }
}

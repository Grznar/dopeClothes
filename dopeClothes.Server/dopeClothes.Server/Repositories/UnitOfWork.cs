using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;

namespace dopeClothes.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _db;
        public IProductRepository Products { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Products = new ProductRepository(_db);
        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

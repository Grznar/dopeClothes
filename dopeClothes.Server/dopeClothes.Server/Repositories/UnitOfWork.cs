using dopeClothes.Server.Data;
using dopeClothes.Server.IRepositories;

namespace dopeClothes.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _db;
        public IProductRepository Products { get; private set; }
        public IUserRepository Users { get; private set; }
        public IWishListRepository WishLists { get; private set; }
        public IWishListItemRepository WishListItems { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IShoppingCartItemRepository ShoppingCartItems { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Products = new ProductRepository(_db);
            Users = new UserRepository(_db);
            WishLists = new WhishListRepository(_db);
            WishListItems = new WhishListItemRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            ShoppingCartItems = new ShoppingCartItemRepository(_db);

        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

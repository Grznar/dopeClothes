namespace dopeClothes.Server.IRepositories
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IUserRepository Users { get; }
        public IWishListRepository WishLists { get; }
        public IWishListItemRepository WishListItems { get; }
        public IShoppingCartRepository ShoppingCarts { get; }
             public IShoppingCartItemRepository ShoppingCartItems { get; }
        void Save();
    }
}

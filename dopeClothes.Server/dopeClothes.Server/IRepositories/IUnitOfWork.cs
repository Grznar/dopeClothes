namespace dopeClothes.Server.IRepositories
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IUserRepository Users { get; }
        void Save();
    }
}

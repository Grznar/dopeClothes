namespace dopeClothes.Server.IRepositories
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        void Save();
    }
}

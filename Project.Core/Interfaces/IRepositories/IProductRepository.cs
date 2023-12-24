using Project.Core.Entities.General;

namespace Project.Core.Interfaces.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<double> PriceCheck(int productId, CancellationToken cancellationToken);
    }
}

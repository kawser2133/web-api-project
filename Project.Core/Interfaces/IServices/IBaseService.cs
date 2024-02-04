using Project.Core.Common;
using Project.Core.Entities.Business;

namespace Project.Core.Interfaces.IServices
{
    public interface IBaseService<TViewModel>
        where TViewModel : class
    {
        Task<IEnumerable<TViewModel>> GetAll(CancellationToken cancellationToken);
        Task<PaginatedDataViewModel<TViewModel>> GetPaginatedData(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PaginatedDataViewModel<TViewModel>> GetPaginatedDataWithFilter(int pageNumber, int pageSize, List<ExpressionFilter> filters, CancellationToken cancellationToken);
        Task<TViewModel> GetById<Tid>(Tid id, CancellationToken cancellationToken);
        Task<bool> IsExists<Tvalue>(string key, Tvalue value, CancellationToken cancellationToken);
        Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value, CancellationToken cancellationToken);
    }

}

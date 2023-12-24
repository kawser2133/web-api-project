using Project.Core.Entities.Business;

namespace Project.Core.Interfaces.IServices
{
    public interface IRoleService : IBaseService<RoleViewModel>
    {
        Task<RoleViewModel> Create(RoleCreateViewModel model, CancellationToken cancellationToken);
        Task Update(RoleUpdateViewModel model, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}

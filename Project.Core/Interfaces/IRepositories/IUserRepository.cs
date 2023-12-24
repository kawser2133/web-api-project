using Project.Core.Entities.Business;
using Project.Core.Entities.General;
using Microsoft.AspNetCore.Identity;

namespace Project.Core.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IdentityResult> Create(UserCreateViewModel model);
        Task<IdentityResult> Update(UserUpdateViewModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel model);
    }
}

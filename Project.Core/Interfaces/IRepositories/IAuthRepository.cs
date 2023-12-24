
using Project.Core.Entities.Business;
using Microsoft.AspNetCore.Identity;

namespace Project.Core.Interfaces.IRepositories
{
    public interface IAuthRepository
    {

        Task<ResponseViewModel<UserViewModel>> Login(string userName, string password);
        Task Logout();
    }
}

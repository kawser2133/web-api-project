using Project.Core.Entities.Business;

namespace Project.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ResponseViewModel<UserViewModel>> Login(string userName, string password);
        Task Logout();
    }
}

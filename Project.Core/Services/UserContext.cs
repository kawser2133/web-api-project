using Project.Core.Interfaces.IServices;

namespace Project.Core.Services
{
    public class UserContext : IUserContext
    {
        public string UserId { get; set; }
    }
}

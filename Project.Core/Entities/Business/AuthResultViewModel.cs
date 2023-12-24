
namespace Project.Core.Entities.Business
{
    public class AuthResultViewModel
    {
        public string? AccessToken { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
    }
}

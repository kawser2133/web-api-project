using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities.Business
{
    public class ResetPasswordViewModel
    {
        [Required, StringLength(20, MinimumLength = 2)]
        public string? UserName { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}

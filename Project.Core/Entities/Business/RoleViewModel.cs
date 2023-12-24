using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities.Business
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoleCreateViewModel
    {
        [Required, StringLength(maximumLength: 10, MinimumLength = 2)]
        public string? Code { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        public bool IsActive { get; set; }
    }

    public class RoleUpdateViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 10, MinimumLength = 2)]
        public string? Code { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}

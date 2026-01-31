using System.ComponentModel.DataAnnotations;

namespace CejkaTomas_UFC_APP.Models.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace CejkaTomas_UFC_APP.Models.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = null!;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}

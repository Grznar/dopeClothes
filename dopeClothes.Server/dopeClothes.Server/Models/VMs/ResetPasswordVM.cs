using System.ComponentModel.DataAnnotations;

namespace dopeClothes.Server.Models.VMs
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = " Old Password is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

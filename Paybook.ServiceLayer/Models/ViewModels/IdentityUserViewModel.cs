using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class IdentityUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool IsPersistent { get; set; }
    }
}
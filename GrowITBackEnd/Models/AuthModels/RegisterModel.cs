using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Models.AuthModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Fist Name is required")]
        [Display(Name = "First Name")]
        public string? FistName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }        

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Delivery Address")]
        public string? Address { get; set; }
    }
}

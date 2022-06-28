using GrowITBackEnd.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PeoplAPV2.Models.AuthModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string? FistName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Delivery Address")]
        public string? Address { get; set; }

        //reference to every table that relates to ApplicationUser
        public ICollection<Support_Tickets>? Tickets { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }

    }
}

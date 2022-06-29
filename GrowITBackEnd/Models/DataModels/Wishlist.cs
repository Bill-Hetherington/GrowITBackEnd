using System.ComponentModel.DataAnnotations;
using PeoplAPV2.Models.AuthModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models.DataModels
{
    public class Wishlist
    {
        [Key]
        public int WishID { get; set; }
        public string UserId { get; set; }

        //foreign Key
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Wishlist_Items>? wishlist_items { get; set; }
    }
}

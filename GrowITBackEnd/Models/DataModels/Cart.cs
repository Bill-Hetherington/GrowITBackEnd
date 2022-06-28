using PeoplAPV2.Models.AuthModels;
using System.ComponentModel.DataAnnotations.Schema;
namespace GrowITBackEnd.Models

{
    public class Cart
    {
        public int CartID { get; set; }
        public String? UserId { get; set; }
        public decimal Cart_Total { get; set; }

        //foreign key
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Cart_Items>? cart_Items { get; set; }
    }
}

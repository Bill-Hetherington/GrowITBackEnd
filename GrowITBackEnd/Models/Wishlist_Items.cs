using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models
{
    public class Wishlist_Items
    {
        public int WishID { get; set; }
        [ForeignKey("WishID")]
        public Wishlist Wishlist { get; set; }

        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }  
    }
}

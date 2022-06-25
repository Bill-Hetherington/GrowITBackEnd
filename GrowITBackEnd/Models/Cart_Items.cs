using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models
{
    public class Cart_Items
    {
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
        public int CartID { get; set;}
        [ForeignKey("CartID")]
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}

using PeoplAPV2.Models.AuthModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models
{
    public class Orders
    {
        public int OrdersID { get; set; }
        public String UserId { get; set; }
        public decimal Order_Total { get; set; }
        public DateTime Date_Started { get; set; }
        public DateTime Date_Completed { get; set; }

        //foreign Key
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<Order_Items> order_items { get; set; }
    }
}

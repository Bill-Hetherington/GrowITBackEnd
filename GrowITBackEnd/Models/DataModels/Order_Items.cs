using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models.DataModels
{
    public class Order_Items
    {
        public int OrdersID { get; set; }
        [ForeignKey("OrdersID")]
        public Orders orders { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item item { get; set; }
        public int Quantity { get; set; }

    }
}

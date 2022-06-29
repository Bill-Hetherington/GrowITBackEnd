using GrowITBackEnd.Models.DataModels;

namespace GrowITBackEnd.Models.RequestsModels
{
    public class createOrderItemRequest
    {
        public int OrdersID { get; set; }
        public int Quantity { get; set; }
        public ICollection<Item> order_items { get; set; }
        
    }
}

using GrowITBackEnd.Models.DataModels;

namespace GrowITBackEnd.Models.RequestsModels
{
    public class createOrderRequest
    {
        public string? Username { get; set; }
        public decimal Order_Total { get; set; }
       // public ICollection<Order_Items>? order_items { get; set; }
        public ICollection<Item> Items { get; set; }
        public DateTime Date_Started { get; set; }
    }
}

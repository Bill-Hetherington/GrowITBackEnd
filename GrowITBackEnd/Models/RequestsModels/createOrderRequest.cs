using GrowITBackEnd.Models.DataModels;

namespace GrowITBackEnd.Models.RequestsModels
{
    public class createOrderRequest
    {
        public string? Username { get; set; }
        public decimal Order_Total { get; set; }
       // public ICollection<Order_Items>? order_items { get; set; }
        public List<itemResquest> Items { get; set; }
        public DateTime Date_Started { get; set; }
    }
    public class itemResquest
    {
        public int ItemID { get; set; }
        public string? Item_Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public string? Category { get; set; }
    }
}

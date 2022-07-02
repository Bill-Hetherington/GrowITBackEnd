namespace GrowITBackEnd.Models.ReturnModels
{
    public class OrdersReturn
    {
        public int OrdersID { get; set; }
        public string UserId { get; set; }
        public decimal Order_Total { get; set; }
        public DateTime Date_Started { get; set; }
        public DateTime? Date_Completed { get; set; }
        public List<ItemsInOrder>? itemsInOrder { get; set; } = new List<ItemsInOrder>();     
       
    }
    public class ItemsInOrder
    {
        public int ItemID { get; set; }
        public string? Item_Name { get; set; }
        public decimal Price { get; set; }        
        public int Quantity { get; set; }       
    }
}

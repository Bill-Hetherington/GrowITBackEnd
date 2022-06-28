namespace GrowITBackEnd.Models.DataModels
{
    public class Item
    {
        public int ItemID { get; set; }
        public string? Item_Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity_on_Hand { get; set; }
        public string? Category { get; set; }

        //foreign key bit
        public ICollection<Cart_Items> cart_Items { get; set; }
        public ICollection<Wishlist_Items> wishlist_Items { get;set; }
        public ICollection<Order_Items> order_items { get; set; }
    }
}

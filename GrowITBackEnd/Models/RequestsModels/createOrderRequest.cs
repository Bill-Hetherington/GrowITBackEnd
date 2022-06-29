namespace GrowITBackEnd.Models.RequestsModels
{
    public class createOrderRequest
    {
        public string? Username { get; set; }
        public decimal Order_Total { get; set; }
        public DateTime Date_Started { get; set; }
    }
}

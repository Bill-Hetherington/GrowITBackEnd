namespace GrowITBackEnd.Models.ReturnModels
{
    public class GetSupportTicketReturn
    {
        public int SuppID { get; set; }
        public string? Username { get; set; }
        public DateTime Date_Generated { get; set; }
        public string? Description { get; set; }
    }
}

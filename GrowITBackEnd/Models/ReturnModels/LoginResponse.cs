namespace GrowITBackEnd.Models.ReturnModels
{
    public class LoginResponse
    {
        public string? username { get; set; }
        public string? role { get; set; }
        public string? token { get; set; }

        public string? expiration { get; set; }
    }
}

using GrowITBackEnd.Models.DataModels;

namespace GrowITBackEnd.Models.RequestsModels
{
    public class FileModel
    {
        public int ItemID { get; set; }
        public string? Item_Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity_on_Hand { get; set; }
        public string? Category { get; set; }

        public string? imageURL { get; set; }

        public Boolean? hotDeal { get; set; }
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
    }
}

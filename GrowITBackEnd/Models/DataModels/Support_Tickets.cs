using PeoplAPV2.Models.AuthModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models.DataModels
{
    public class Support_Tickets
    {
        [Key]
        public int SuppID { get; set; }
        public string UserId { get; set; }
        public DateTime Date_Generated { get; set; }
        public string? Description { get; set; }

        //foreign key
        [ForeignKey("UserId")]       

        public ApplicationUser? ApplicationUser { get; set; }
    }
}

using PeoplAPV2.Models.AuthModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowITBackEnd.Models.DataModels
{
    public class Support_Tickets
    {
        [Key]
        public int SuppID { get; set; }

        public DateTime Date_Generated { get; set; }

        //foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuideOfTurkey.Models
{
    public class CountryGallery 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string photoUrl { get; set; }
        public bool deleteState { get; set; }
    }
}
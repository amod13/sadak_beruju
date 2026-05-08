using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class SetBerujuTargetValue
    {
        [Key]
        public int BerujuTargetId { get; set; }

        public int OfficeId { get; set; }
        [Display(Name ="आर्थिक वर्ष")]
        [Required(ErrorMessage ="आर्थिक वर्ष छान्नुहोस ।")]
        public int FiscalYearId { get; set; }
       
        public decimal? IstQuardTargetVal { get; set; }

       
        public decimal? IIndQuardTargetVal { get; set; }
       
        public decimal? IIIrdQuardTargetVal { get; set; }

        public DateTime CreaatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Display(Name = "नियमित लक्ष्य रकम")]
        [Range(1, int.MaxValue, ErrorMessage = "नियमित लक्ष्य रकम लेख्नुहोस ।")]
        [Required(ErrorMessage = "नियमित लक्ष्य रकम लेख्नुहोस ।")]
        public decimal? NIyamitTargetVal { get; set; }
        [Display(Name = "असुल उपर लक्ष्य रकम")]
        [Range(1, int.MaxValue, ErrorMessage = "असुल उपर लक्ष्य रकम लेख्नुहोस ।")]
        [Required(ErrorMessage = "असुल उपर लक्ष्य रकम लेख्नुहोस ।")]

        public decimal? AshuliTargetVal { get; set; }
        [Display(Name = "पेस्की लक्ष्य रकम")]
        [Range(1, int.MaxValue, ErrorMessage = "पेस्की लक्ष्य रकम लेख्नुहोस ।")]
        [Required(ErrorMessage = "पेस्की लक्ष्य रकम लेख्नुहोस ।")]

        public decimal? PeshkiTargetVal { get; set; }

        [NotMapped]
        public List<SetBerujuTargetValue> SetBerujuTargetValueList { get; set; }

    }
}
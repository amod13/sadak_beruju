using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class FiscalYearRecord
    {
        [Key]

        public int FiscalYearId { get; set; }

        [Display(Name = "आर्थिक वर्ष शिर्षक (NP)")]
        [Required]
        public string FiscalYearTitle { get; set; }

        [Display(Name = "आर्थिक वर्ष शिर्षक(EN)")]
        [Required]
        public string FiscalYearTitleEng { get; set; }

        [Display(Name = "मिति बाट")]

        public DateTime StartFrom { get; set; }

        [Display(Name = "मिति सम्म")]
        public DateTime EndDate { get; set; }

        [Display(Name = "हालको हो?")]

        public bool IsCurrent { get; set; }

        [Display(Name = "")]

        public int? PreFiscalYearId { get; set; }

        [Display(Name = "")]

        public int? DisplayOrder { get; set; }

        [NotMapped]
        public string DateFromStr { get; set; }
        [NotMapped]
        public string DateToStr { get; set; }

        [NotMapped]
        public List<FiscalYearRecord> FiscalYearRecordList { get; set; }


    }
}
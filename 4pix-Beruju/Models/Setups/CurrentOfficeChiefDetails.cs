using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class CurrentOfficeChiefDetails
    {
        [Key]
        public int CurrentOfficeChiefId { get; set; }
        [Display(Name ="कार्यालय प्रमुखको नाम")]
        public string ChiefName { get; set; }
        [Display(Name = "कार्यालय प्रमुखको पद")]
        public string ChiefPost { get; set; }
        public int? OfficeId { get; set; }
        public bool? EmployeeStatus { get; set; }
        [Display(Name = "हालको आर्थिक वर्ष")]
        public int? RunnigFiscalYearId { get; set; }
    }

}
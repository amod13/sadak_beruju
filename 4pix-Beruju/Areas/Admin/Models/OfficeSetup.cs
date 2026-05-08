using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Areas.Admin.Models
{
    public class OfficeSetup
    {

        public int OfficeDetailId { get; set; }
        public string OFficeName { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VDCMUNID { get; set; }
        public int DisplayStatus { get; set; }
        public int UserTypeId { get; set; }
        public int MainOfficeId { get; set; }
        public bool OfficeStatus { get; set; }
        public int ProVdcmunTypeId { get; set; }

        public string ContactPerson { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile number")]
        public string ContactPersonMobile { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]

        public string OfficeEmail { get; set; }
        public string OfficeCode { get; set; }

        public int? @OfficeTypeId { get; set; }

        [NotMapped]
        public int MinistryId { get; set; }

        [NotMapped]
        public int NirdeshanalayaId { get; set; }

        [NotMapped]
        public int BivagId { get; set; }

        [NotMapped]
        public int ProvinceOfficesId { get; set; }
        [NotMapped]
        public int IsCentralOrProvinceViewBag { get; set; }
        [NotMapped]
        public int? NirdeshanalayaUserOrProvinceUserID { get; set; }
        [NotMapped]
        public int? CurrentLoginUserOfficeViewBagID { get; set; }

        public List<OfficeSetup> OfficeSetupList { get; set; }

    }
}
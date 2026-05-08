using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class MergeOfficeMaster
    {
        [Key]

        public int MergeOfficeMasterId { get; set; }

        [Display(Name = "")]

        public DateTime? CreatedDate { get; set; }

        [Display(Name = "नयाँ कार्यालयको नाम")]
        [Required]

        public string MergeOfficeName { get; set; }

        [Display(Name = "नयाँ कार्यालयको ईमेल")]
        [Required]

        public string OfficeEmail { get; set; }


        [Display(Name = "कैफियत")]

        public string RemarksIf { get; set; }

        [Display(Name = "मर्ज गरिएको मिति")]
        public DateTime? MergedDate { get; set; }

        [NotMapped]
        public string MergedDateStr { get; set; }

        [NotMapped]
        public List<MergeOfficeDetails> MergeOfficeDetailsList { get; set; }

        [NotMapped]
        public List<OfficeDetailsWithAddressVM> OfficeDetailsWithAddressVMList { get; set; }

        [NotMapped]
        public OfficeMainViewModel ObjOfficeMainViewModel { get; set; }

    }

    public class OfficeDetailsWithAddressVM
    {
        public int OfficeId { get; set; }
        public string OfficeNameWithAddress { get; set; }
    }

    public class OfficeMainViewModel
    {
        public string OfficeCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }      
        public string OfficeAddress { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VDCMUNID { get; set; }
    }

    public class MergeOfficeDetails
    {
        [Key]

        public int MergeOfficeDetailsId { get; set; }

        [Display(Name = "")]

        public int? MergeOfficeMasterId { get; set; }

        [Display(Name = "")]

        public int? MainOfficeId { get; set; }

        [Display(Name = "")]

        public int? Officeid { get; set; }

    }



}
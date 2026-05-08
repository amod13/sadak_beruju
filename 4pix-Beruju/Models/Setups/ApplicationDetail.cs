using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class ApplicationDetail
    {

        [Key]
        public int ApplicationDetailId { get; set; }
        public string ApplicationName { get; set; }
        public string OfficeTitleName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeEmail { get; set; }
        public bool IsActive { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceTitle { get; set; }
        public string CopyrightText { get; set; }
        public int? OfficeLevel { get; set; }
        public string MinistryName1 { get; set; }
        public string MinistryName2 { get; set; }
        public string ImageName { get; set; }
        public string ImageText { get; set; }
        public int? CentralOrProvince { get; set; }

    }
}
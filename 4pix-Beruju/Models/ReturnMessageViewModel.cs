using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class ReturnMessageViewModel
    {
        public int PrimaryId { get; set; }
        public string ReturnMessage { get; set; }

    }

    public class ListUserForSMSMV
    {
        public string MobielNumber { get; set; }
        public string PersonName { get; set; }
        public int FiscalYearId { get; set; }
        public string FiscalYearTitleEng { get; set; }
        public string TypeName { get; set; }
        public string MyProperty { get; set; }
        public decimal? TotalAmount { get; set; }
        public string OFficeName { get; set; }
        public int? OfficeDetailId { get; set; }
        public int? ToWhomDetailsId { get; set; }
        public int? InternalOrExternalId { get; set; }
        public bool Ischecked { get; set; } = false;

        public List<ListUserForSMSMV> ListUserForSMSMVList { get; set; }
    }

    public class ListMinistryUserForSMSMV
    {
        public string ContactPersonMobile { get; set; }        
        public int FiscalYearId { get; set; }
        public string FiscalYearTitleEng { get; set; }
        public string BerujuTypeName { get; set; }
       
        public decimal? TotalBerujuAmount { get; set; }
        public decimal? TotalSamparikshadAmount { get; set; }
        public string OFficeName { get; set; }
        public int? OfficeDetailId { get; set; }       
        public bool Ischecked { get; set; } = false;

        public List<ListMinistryUserForSMSMV> ListMinistryUserForSMSMVList { get; set; }
    }
}
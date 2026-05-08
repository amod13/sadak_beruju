using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Areas.LocalLevel.Models
{
    public class FIscalYearSetup
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearTitle { get; set; }
        public string FiscalYearTitleEng { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public int? PreFiscalYearId { get; set; }
        public int? DisplayOrder { get; set; }

        public List<FIscalYearSetup> FIscalYearSetupList { get; set; }
    }
}
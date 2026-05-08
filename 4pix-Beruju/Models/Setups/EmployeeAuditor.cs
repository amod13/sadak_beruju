using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class EmployeeAuditor
    {

        public int EmployeeAuditorDetailsId { get; set; }
        public string EmpName { get; set; }
        public DateTime FromDuration { get; set; }
        [NotMapped]
        public string FromDurationStr { get; set; }
        public DateTime ToDuration { get; set; }
        [NotMapped]
        public string ToDurationStr { get; set; }
        public int EmpType { get; set; }
        public bool EmpStatus { get; set; }
        public int OfficeId { get; set; }
        public string AuditorPost { get; set; }

        [NotMapped]
        public int FiscalYearId { get; set; }

        [NotMapped]
        public List<EmployeeAuditor> EmployeeAuditorList { get; set; }




    }
}
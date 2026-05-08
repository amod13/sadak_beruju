using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class SaidantikBeruju
    {
        public int SaidantikBerujuId { get; set; }
        public int FiscalYearId { get; set; }
        public string BerujuDafaNumber { get; set; }
        public string BerujuShortDesc { get; set; }
        public string BerujuLongDesc { get; set; }
        public bool BerujuStatus { get; set; }

        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public int InternalOrExternal { get; set; }
        public int OfficeId { get; set; }
        public int? BudgetSubTitleId { get; set; }
        [NotMapped]
        public int PageCount { get; set; }
        [NotMapped]
        public int CurrentPage { get; set; }

        [NotMapped]
        public string SaidantikDoc { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadSaidantikDocFileType { get; set; }

        [NotMapped]
        public List<SaidantikBeruju> SaidantikBerujuList { get; set; }
        [NotMapped]
        public List<SaidantikBeruju> SaidantikBerujuTopFiveList { get; set; }
    }
}
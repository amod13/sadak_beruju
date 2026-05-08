using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Areas.LocalLevel.Models
{
    public class ExpenseTitleViewModelSetup
    {


        public BudgetSubTitleSetup ObjBudgetSubTitleSetup { get; set; }
        public List<BudgetSubTitleSetup> BudgetSubTitleSetupList { get; set; }

        public ExpenseTitleSetup ObjExpenseTitleSetup { get; set; }
        public List<ExpenseTitleSetup> ExpenseTitleSetupList { get; set; }
    }

    public class BudgetSubTitleSetup
    {

        public int BudgetSubTitleId { get; set; }
        [StringLength(9,ErrorMessage ="धेरै भयो")]
        public string SubTitleCode { get; set; }
        public string SubTitleName { get; set; }
        public bool SubTitleStatus { get; set; }
        public int? ChaluOrPujigatId { get; set; }
        public int? DisplayOrder { get; set; }
        public int? OfficeId { get; set; }
        public int? FiscalYearId { get; set; }
    }

    public class ExpenseTitleSetup
    {
        public int ChaluPujigatId { get; set; }
        public string Code { get; set; }
        public string TItlle { get; set; }
        public int ChaluPujiTypeId { get; set; }
        public bool PujiStatus { get; set; }
        public int KoshTypeId { get; set; }
        public int OfficeId { get; set; }

    }
}
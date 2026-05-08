using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class ChartOfAccount
    {
        public ExpenseTitleViewModel ObjExpenseTitleViewModel { get; set; }
        public List<ExpenseTitleViewModel> ExpenseTitleViewModelList { get; set; }
    }

    public class ExpenseTitleViewModel
    {
        public int ExpenseTitleId { get; set; }
        public string ExpenseTitleName { get; set; }
        public string ExpenseCode { get; set; }
        public int BudgetSubTitleId { get; set; }
        public bool ExpenseStatus { get; set; }
        public int OfficeId { get; set; }

    }

}
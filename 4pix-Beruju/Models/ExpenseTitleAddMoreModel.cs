using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class ExpenseTitleAddMoreModel
    {
        public string ExpenseTitleName { get; set; }
        public int BudgetSubTitleId { get; set; }
        public string ExpenseCode { get; set; }
        [NotMapped]
        public List<ExpenseTitleAddMoreModel> ExpenseTitleAddMoreModelList { get; set; }
    }
}
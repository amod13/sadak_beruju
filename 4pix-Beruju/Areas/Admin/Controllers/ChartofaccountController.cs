using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models.Setups;
using _4pix_Beruju.Services;

namespace _4pix_Beruju.Areas.Admin.Controllers
{
    [Authorize]
    public class ChartofaccountController : Controller
    {
        CommonService cs = new CommonService();
        // GET: Admin/Chartofaccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListExpenseTitle()
        {
            ChartOfAccount model = new ChartOfAccount();
            model.ExpenseTitleViewModelList = new List<ExpenseTitleViewModel>();
            model.ExpenseTitleViewModelList = cs.ListExpenseTitleByOfficeId(1);
            return View(model);
        }
        public ActionResult CreateExpenseTitle()
        {
            ChartOfAccount model = new ChartOfAccount();
            model.ObjExpenseTitleViewModel = new ExpenseTitleViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateExpenseTitle(ChartOfAccount model)
        {
            return View(model);
        }


        public ActionResult EditExpenseTitle(int id)
        {
            ChartOfAccount model = new ChartOfAccount();
            model.ObjExpenseTitleViewModel = new ExpenseTitleViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditExpenseTitle(ChartOfAccount model)
        {
            return View(model);
        }
        public ActionResult DeleteExpenseTitle(int id)
        {
            ChartOfAccount model = new ChartOfAccount();
            model.ObjExpenseTitleViewModel = new ExpenseTitleViewModel();
            return View(model);
        }

    }
}
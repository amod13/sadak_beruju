using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Services;
using _4pix_Beruju.Areas.LocalLevel.Models;
using _4pix_Beruju.Models;
using PagedList;


namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class ExpenseTitleSetupController : Controller
    {
        BudgetSubTitleService BSS = new BudgetSubTitleService();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();

        // GET: LocalLevel/ExpenseTitleSetup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBudgetSubTitle()
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.BudgetSubTitleSetupList = new List<BudgetSubTitleSetup>();
            model.BudgetSubTitleSetupList = BSS.GetBudgetSubTitleListByOfficeId(CurrentUserOfficeId);
            return View(model);
        }

        public ActionResult ListBudgetSubTitlePagination(int? Page, int? FiscalYearId)
        {
            int PageSized = 50;
            int PageIndex = 1;
            PageIndex = Page.HasValue ? Convert.ToInt32(Page) : 1;
            FiscalYearId = FiscalYearId.HasValue ? Convert.ToInt32(FiscalYearId) : 1;
            ViewBag.FiscalYearId = FiscalYearId;
            IPagedList<BudgetSubTitleSetup> PageListBudgetSubTitleSetupList = null;
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.BudgetSubTitleSetupList = new List<BudgetSubTitleSetup>();
            //model.BudgetSubTitleSetupList = BSS.GetBudgetSubTitleListByOfficeId(CurrentUserOfficeId);
            PageListBudgetSubTitleSetupList = BSS.GetBudgetSubTitleListByOfficeId(CurrentUserOfficeId).Where(x=>x.FiscalYearId==FiscalYearId).ToList().ToPagedList(PageIndex, PageSized);
            return View(PageListBudgetSubTitleSetupList);
        }




        public ActionResult CreateBudgetSubTitle()
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBudgetSubTitle(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            model.ObjBudgetSubTitleSetup.SubTitleStatus = true;
            model.ObjBudgetSubTitleSetup.DisplayOrder = 50;
            model.ObjBudgetSubTitleSetup.ChaluOrPujigatId = 1;//change later
            rms = BSS.InsertBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListBudgetSubTitle");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }


        public ActionResult CreateBudgetSubTitleWithP()
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateBudgetSubTitleWithP(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            model.ObjBudgetSubTitleSetup.SubTitleStatus = true;
            model.ObjBudgetSubTitleSetup.DisplayOrder = 50;
            model.ObjBudgetSubTitleSetup.ChaluOrPujigatId = 1;//change later
            rms = BSS.InsertBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListBudgetSubTitlePagination");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }

        public ActionResult EditBudgetSubTitle(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            model.ObjBudgetSubTitleSetup = BSS.GetBudgetSubTitleListByOfficeId(CurrentUserOfficeId).SingleOrDefault(x => x.BudgetSubTitleId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditBudgetSubTitle(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            model.ObjBudgetSubTitleSetup.SubTitleStatus = true;
            model.ObjBudgetSubTitleSetup.DisplayOrder = 50;
            model.ObjBudgetSubTitleSetup.ChaluOrPujigatId = 1;//change later
            rms = BSS.UpdateBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListBudgetSubTitle");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }



        public ActionResult EditBudgetSubTitleWithP(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            model.ObjBudgetSubTitleSetup = BSS.GetBudgetSubTitleListByOfficeId(CurrentUserOfficeId).SingleOrDefault(x => x.BudgetSubTitleId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditBudgetSubTitleWithP(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            model.ObjBudgetSubTitleSetup.SubTitleStatus = true;
            model.ObjBudgetSubTitleSetup.DisplayOrder = 50;
            model.ObjBudgetSubTitleSetup.ChaluOrPujigatId = 1;//change later
            rms = BSS.UpdateBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListBudgetSubTitlePagination");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }






        public ActionResult DeleteBudgetSubTitleDetail(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            model.ObjBudgetSubTitleSetup.BudgetSubTitleId = id;
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = BSS.DeleteBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);

            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो ।";

            }
            else if (rms.ReturnMessage == "Already Inserted")
            {
                TempData["Success"] = "यो शिर्षक हटाउन मिल्दैन । प्रयोग भएका शिर्षकहरू हटाउन मिल्दैन ।";

            }

            else
            {
                TempData["Success"] = rms.ReturnMessage;

            }
            return RedirectToAction("ListBudgetSubTitle");

        }


        public ActionResult DeleteBudgetSubTitleDetailWithP(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjBudgetSubTitleSetup = new BudgetSubTitleSetup();
            model.ObjBudgetSubTitleSetup.BudgetSubTitleId = id;
            model.ObjBudgetSubTitleSetup.OfficeId = CurrentUserOfficeId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = BSS.DeleteBudgetSubTitleDetails(model.ObjBudgetSubTitleSetup);

            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो ।";

            }
            else if (rms.ReturnMessage == "Already Inserted")
            {
                TempData["Success"] = "यो शिर्षक हटाउन मिल्दैन । प्रयोग भएका शिर्षकहरू हटाउन मिल्दैन ।";

            }

            else
            {
                TempData["Success"] = rms.ReturnMessage;

            }
            return RedirectToAction("ListBudgetSubTitlePagination");

        }



        public ActionResult ListExpenseTitle()
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ExpenseTitleSetupList = new List<ExpenseTitleSetup>();
            model.ExpenseTitleSetupList = BSS.GetExpenseTitleListByOfficeId(CurrentUserOfficeId);
            return View(model);
        }

        public ActionResult CreateExpenseTitle()
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjExpenseTitleSetup = new ExpenseTitleSetup();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateExpenseTitle(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjExpenseTitleSetup.OfficeId = CurrentUserOfficeId;
            model.ObjExpenseTitleSetup.PujiStatus = true;
            rms = BSS.InsertExpneseTitleDetails(model.ObjExpenseTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListExpenseTitle");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }



        public ActionResult EditExpenseTitle(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjExpenseTitleSetup = new ExpenseTitleSetup();
            model.ObjExpenseTitleSetup = BSS.GetExpenseTitleListByOfficeId(CurrentUserOfficeId).SingleOrDefault(x => x.ChaluPujigatId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditExpenseTitle(ExpenseTitleViewModelSetup model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjExpenseTitleSetup.PujiStatus = true;
            model.ObjExpenseTitleSetup.OfficeId = CurrentUserOfficeId;
            rms = BSS.UpdateExpneseTitleDetails(model.ObjExpenseTitleSetup);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = rms.ReturnMessage;
                return RedirectToAction("ListExpenseTitle");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage;
                return View(model);
            }
        }

        public ActionResult DeleteChaluOrPujigatTitleDetail(int id)
        {
            ExpenseTitleViewModelSetup model = new ExpenseTitleViewModelSetup();
            model.ObjExpenseTitleSetup = new ExpenseTitleSetup();
            model.ObjExpenseTitleSetup.ChaluPujigatId = id;
            model.ObjExpenseTitleSetup.OfficeId = CurrentUserOfficeId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = BSS.DeleteChaluOrPujigatTitleDetail(model.ObjExpenseTitleSetup);

            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो ।";

            }
            else if (rms.ReturnMessage == "Already Inserted")
            {
                TempData["Success"] = "यो शिर्षक हटाउन मिल्दैन । प्रयोग भएका शिर्षकहरू हटाउन मिल्दैन ।";

            }

            else
            {
                TempData["Success"] = rms.ReturnMessage;

            }
            return RedirectToAction("ListExpenseTitle");

        }








    }
}
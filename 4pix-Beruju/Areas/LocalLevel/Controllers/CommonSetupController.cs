using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models.Setups;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Areas.LocalLevel.Models;
using _4pix_Beruju.Models.ViewModel;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class CommonSetupController : Controller
    {
        CommonService cs = new CommonService();

        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        InternalBerujuService IBS = new InternalBerujuService();

        // GET: LocalLevel/CommonSetup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBerujuOfficeManager()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmployeeAuditorList = new List<EmployeeAuditor>();
            model.EmployeeAuditorList = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 1);//1 is OfficeManager
            return View(model);
        }

        public ActionResult AddBerujuOfficeManager()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmpType = 1;
            return View(model);
        }




        [HttpPost]
        public ActionResult AddBerujuOfficeManager(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 1;
            model.OfficeId = CurrentUserOfficeId;
            model.FromDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDurationStr);
            model.ToDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDurationStr);
            rms = cs.InsertEmployeeAuditorDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ListBerujuOfficeManager");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }


        }

        public ActionResult EditBerujuOfficeManager(int id)
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 1).Where(x => x.EmployeeAuditorDetailsId == id).SingleOrDefault();
            model.FromDurationStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDuration);
            model.ToDurationStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDuration);
            model.EmpType = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBerujuOfficeManager(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 1;
            model.OfficeId = CurrentUserOfficeId;
            model.FromDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDurationStr);
            model.ToDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDurationStr);
            rms = cs.UpdateEmployeeAuditorDetails(model);
            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListBerujuOfficeManager");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult DeleteOfficeManager(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.DeleteOfficeManager(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो । ";
                return RedirectToAction("ListBerujuOfficeManager");

            }
            else
            {
                TempData["Success"] = rms.ReturnMessage.ToString();
                return RedirectToAction("ListBerujuOfficeManager");
            }
        }


        public ActionResult ListBerujuAccountant()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmployeeAuditorList = new List<EmployeeAuditor>();
            model.EmployeeAuditorList = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 2);//1 is OfficeManager
            return View(model);

        }

        public ActionResult AddBerujuAccountant()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmpType = 2;
            return View();
        }

        [HttpPost]
        public ActionResult AddBerujuAccountant(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 2;//2 is accountant
            model.OfficeId = CurrentUserOfficeId;
            model.FromDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDurationStr);
            model.ToDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDurationStr);

            if(model.FromDuration==DateTime.MinValue)
            {
                
                ViewBag.Errormessage = @"कार्य अवधि देखि मिलेन वा  कार्य अवधि देखि को मिति चेक गर्नुहोस नेपाली क्यालेन्डरमा । ";
                return View(model);
            }
            else if (model.ToDuration==DateTime.MinValue)
            {
                ViewBag.Errormessage = @" कार्य अवधि सम्म  मिलेन वा कार्य अवधि सम्म को मिति चेक गर्नुहोस नेपाली क्यालेन्डरमा । ";
                return View(model);
            }
            else
            {
                rms = cs.InsertEmployeeAuditorDetails(model);
                if (rms.PrimaryId > 0)
                {
                    TempData["Success"] = "विवरण सुरक्छित भयो । ";
                    return RedirectToAction("ListBerujuAccountant");
                }
                else
                {
                    ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                    return View(model);
                }

            }

        }
        public ActionResult EditBerujuAccountant(int id)
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 2).SingleOrDefault(x => x.EmployeeAuditorDetailsId == id);
            model.FromDurationStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDuration);
            model.ToDurationStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDuration);
            model.EmpType = 2;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBerujuAccountant(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 2;//2 is accountant
            model.OfficeId = CurrentUserOfficeId;
            model.FromDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDurationStr);
            model.ToDuration = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDurationStr);
            rms = cs.UpdateEmployeeAuditorDetails(model);
            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListBerujuAccountant");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult DeleteOfficeAccountant(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.DeleteOfficeAccountant(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो । ";
                return RedirectToAction("ListBerujuAccountant");

            }
            else
            {
                TempData["Success"] = rms.ReturnMessage.ToString();
                return RedirectToAction("ListBerujuAccountant");
            }
        }

        public ActionResult ListBerujuAuditor()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmployeeAuditorList = new List<EmployeeAuditor>();
            model.EmployeeAuditorList = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 3);//1 is OfficeManager
            return View(model);
        }

        public ActionResult AddBerujuAuditor()
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model.EmpType = 3;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBerujuAuditor(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 3;
            //get from and to date from fiscalYearId
            model.FromDuration = cs.GetStartEndDateFromFiscalYearId(model.FiscalYearId, "Start");
            model.ToDuration = cs.GetStartEndDateFromFiscalYearId(model.FiscalYearId, "End");
            model.OfficeId = CurrentUserOfficeId;
            rms = cs.InsertEmployeeAuditorDetails(model);
            if (rms.ReturnMessage == "Saved Successfully")
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ListBerujuAuditor");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }

        public ActionResult EditBerujuAuditor(int id)
        {
            EmployeeAuditor model = new EmployeeAuditor();
            model = cs.ListEmployeeOrAuditorDetails(CurrentUserOfficeId, 3).SingleOrDefault(x => x.EmployeeAuditorDetailsId == id);
            model.FiscalYearId = cs.GetFiscalYearRecordIdFromStartEndDate(model.FromDuration, model.ToDuration);
            model.EmpType = 3;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBerujuAuditor(EmployeeAuditor model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.EmpType = 3;
            model.FromDuration = cs.GetStartEndDateFromFiscalYearId(model.FiscalYearId, "Start");
            model.ToDuration = cs.GetStartEndDateFromFiscalYearId(model.FiscalYearId, "End");
            rms = cs.UpdateEmployeeAuditorDetails(model);
            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListBerujuAuditor");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }

        public ActionResult DeleteOfficeAuditor(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.DeleteOfficeAuditor(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टम बाट हटाईयो । ";
                return RedirectToAction("ListBerujuAuditor");

            }
            else
            {
                TempData["Success"] = rms.ReturnMessage.ToString();
                return RedirectToAction("ListBerujuAuditor");
            }
        }

        public ActionResult ListFiscalYear()
        {
            FIscalYearSetup model = new FIscalYearSetup();
            return View();
        }

        public ActionResult AddFiscalYear()
        {
            FIscalYearSetup model = new FIscalYearSetup();
            return View();
        }

        [HttpPost]
        public ActionResult AddFiscalYear(FIscalYearSetup model)
        {

            return View();
        }
        public ActionResult EditFiscalYear(int id)
        {
            FIscalYearSetup model = new FIscalYearSetup();
            return View();
        }
        [HttpPost]
        public ActionResult EditFiscalYear(FIscalYearSetup model)
        {

            return View();
        }

        public ActionResult ListExternalBerujuForLetter()
        {
            ExternalBeruju model = new ExternalBeruju();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetExternalBerujuForListForLetter(ExternalBeruju model)
        {
            if (model.KoshTypeId == 6)//saidantik
            {
                model.SaidantikBerujuList = new List<SaidantikBeruju>();

                if (model.FiscalYearId > 0)
                {
                    model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, 2).Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, 2).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.BerujuDafaNumber.Contains(model.BerujuNumber)).ToList();

                }
                model.OfficeId = CurrentUserOfficeId;
                return PartialView("_GetSaidantikBerujuForList", model);

            }
            else
            {
                model.ExternalBerujuList = new List<ExternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    model.ExternalBerujuList = IBS.SPListExternalBerujuByKoshTypeId(CurrentUserOfficeId, model.KoshTypeId).Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.ExternalBerujuList = IBS.SPListExternalBerujuByKoshTypeId(CurrentUserOfficeId, model.KoshTypeId).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                model.OfficeId = CurrentUserOfficeId;
                return PartialView("_GetExternalBerujuForList", model);
            }
        }

        public ActionResult MakeLetterForBeruju(int id)
        {
            BerujuLetterVM vm = new BerujuLetterVM();
            ExternalBeruju model = new ExternalBeruju();
            vm.ObjCommonViewModel = new CommonViewModel();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            vm.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);

            vm.ToWhomDetailListVMForReport = model.ToWhomDetailListVMList;
            vm.BerujuDescription = model.BerujuDetails;
            vm.FiscalyearId = model.FiscalYearId;
            vm.BerujuAmount = model.VoucharAmunt;
            vm.OfficeIdForReportHeader = CurrentUserOfficeId;
            vm.officeId = CurrentUserOfficeId;
            vm.ExternalBerujuId = model.ExternalBerujuId;

            vm.ObjCommonViewModel = cs.GetOfficeChiefOrAuditorNameFromDate(DateTime.Now, 1, CurrentUserOfficeId);
            return View(vm);
        }

        public class BerujuLetterVM
        {
            public int officeId { get; set; }
            public int ExternalBerujuId { get; set; }
            public string BerujuDafanumber { get; set; }
            public string BerujuDescription { get; set; }
            public decimal? BerujuAmount { get; set; }
            public int FiscalyearId { get; set; }
            public int OfficeTypeForReportHeader { get; set; }
            public int OfficeIdForReportHeader { get; set; }

            public CommonViewModel ObjCommonViewModel { get; set; }

            public List<ToWhomDetailListVM> ToWhomDetailListVMForReport { get; set; }


        }
    }
}
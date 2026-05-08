using _4pix_Beruju.ENUM;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static _4pix_Beruju.Controllers.ManageController;


namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class LocalLevelHomeController : Controller
    {
        ReportService RS = new ReportService();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        int CurrentUserOfficeType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
        int CurrentloginProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentApplicationProvinceId();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public LocalLevelHomeController()
        {
        }
        public LocalLevelHomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: LocalLevel/LocalLevelHome
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            return View(model);
        }
        public FileResult Download(string FileName)
        {
            //int CurentLoginUsertype = CommonFunction.GetcurrentLoginUserType();

            var FileVirtualPath = "~/RequiredDocs/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }


        public ActionResult AdminDashBoard()//province 2
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            return View(model);
        }
        public ActionResult GetChartData()
        {

            JsonResult result = new JsonResult();
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardColumnChartViewModelList = new List<DashboardColumnChartViewModel>();
            model.DashboardColumnChartViewModelList = RS.GetChartData(CurrentUserOfficeId);
            result = Json(model.DashboardColumnChartViewModelList, JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult GetChartDataForMinistry()
        {

            JsonResult result = new JsonResult();
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardColumnChartViewModelList = new List<DashboardColumnChartViewModel>();
            model.DashboardColumnChartViewModelList = RS.DashboardColumnChartForMinistry(CurrentUserOfficeId);
            result = Json(model.DashboardColumnChartViewModelList, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult GetPieChartForMinistry()
        {
            int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardPieChartList = new List<DashboardPieChart>();

            if (CurrentUserOfficeType == (int)EUserTypes.ProvinceAdmin || CurrentUserOfficeType == (int)EUserTypes.MinistryAdmin)
            {
                model.DashboardPieChartList = RS.DashboardPieChartForMinistry(0);

            }
            else
            {
                model.DashboardPieChartList = RS.DashboardPieChartForMinistry(CurrentUserOfficeId);

            }
            //result = Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
            return Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPieChart()
        {
            int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardPieChartList = new List<DashboardPieChart>();
            model.DashboardPieChartList = RS.DashboardPieChartForLocalLevel(CurrentUserOfficeId);
            //result = Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
            return Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPieChartForNirdeshanalaya()
        {
            int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardPieChartList = new List<DashboardPieChart>();
            model.DashboardPieChartList = RS.DashboardPieChartForMinistry(CurrentUserOfficeId);
            //result = Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
            return Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPieChartForDistrict()
        {
            int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            int DistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardPieChartList = new List<DashboardPieChart>();
            model.DashboardPieChartList = RS.DashboardPieChartForDistrictUser(CurrentUserOfficeId, DistrictId);

            //result = Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
            return Json(model.DashboardPieChartList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MinistryDashboard()//ministry 3
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            return View(model);
        }


        public ActionResult MinistryDashboardSum()//ministry 3
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumListForMinistryUser(CurrentUserOfficeId);
            return View(model);
        }

        public ActionResult SecretariatDashboard()//sachibalaya
        {
            return View();
        }
        public ActionResult DirectorateDashboard()//NirdeshanalayaUser 4
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            return View(model);
        }

        public ActionResult BivagDashboard()//Bivag user
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            return View(model);
        }




        public ActionResult BivagAdminDashboard()//Bivag user
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            return View(model);
        }


        public ActionResult DirectorateDashboardSum()//NirdeshanalayaUser 4
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumListForMinistryUser(CurrentUserOfficeId);
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            return View(model);
        }

        public ActionResult OfficeDashBoard()//Commission Users...aayog 5
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            
            return View(model);
        }


        public ActionResult OfficeWiseDashBoard()//Commission Users...aayog 5
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            model.DashboardBerujuTypewiseTableList = RS.DashboardGetBerujuSumByOfficeOnly(CurrentUserOfficeId);
            return View(model);
        }

        [HttpPost]
        public ActionResult OfficeWiseDashBoard(DashboardViewModel model)
        {
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            var OfficeId = GetSelectedOfficeId(model)??CurrentUserOfficeId;
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(OfficeId);
            model.DashboardBerujuTypewiseTableList = RS.DashboardGetBerujuSumByOfficeOnly(OfficeId);
            ViewBag.OfficeId = OfficeId;
            return PartialView("_OfficeWiseDashBoard", model);

        }

        private int? GetSelectedOfficeId(DashboardViewModel filter)
        {
            switch (filter.OfficeTypeSearchId)
            {
                case 2: // Ministry
                    return filter.MininstrySearchId > 0
                        ? (int?)filter.MininstrySearchId
                        : (int?)null;

                case 3: // Division
                    return (filter.MininstrySearchId > 0 && filter.BivagSearchId > 0)
                        ? (int?)filter.BivagSearchId
                        : (int?)null;

                case 4: // Directorate
                    return (filter.MininstrySearchId > 0 &&
                            filter.BivagSearchId > 0 &&
                            filter.NirdeshnalayaSearchId > 0)
                        ? (int?)filter.NirdeshnalayaSearchId
                        : (int?)null;

                case 5: // Office
                    return (filter.MininstrySearchId > 0 &&
                            filter.BivagSearchId > 0 &&
                            filter.NirdeshnalayaSearchId > 0 &&
                            filter.KaryalayaSearchId > 0)
                        ? (int?)filter.KaryalayaSearchId
                        : (int?)null;

                default:
                    return filter.OfficeId; // already nullable
            }
        }


        public ActionResult CommissionDashboard()//Commission Users...aayog 6
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            return View(model);
        }



        public ActionResult MakerDashboard()//Commission Users...aayog 6
        {
            DashboardViewModel model = new DashboardViewModel();
     
            model.ObjDashboardMakerDashboardViewModel = RS.GetMakerDashboardCounts();
            return View(model);
        }



        public ActionResult LocalLevelDashboard()//Commission Users...aayog 6
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(CurrentUserOfficeId);
            return View(model);
        }

        public ActionResult ViewMinistryDtl(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.FiscalYearId = 0;//get current fiscal year id;
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWise(id, 0, CurrentloginProvinceId, 0);
            model.ViewBagFiscalYearId = 1;
            model.UserTypeId = id;
            return View(model);
        }

        public ActionResult ViewMinistryDtlAllSum(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.FiscalYearId = 1;//get current fiscal year id;
            int UserTypeId = id;
            int FiscalYearId = 0;
            int MainOfficeId = 0;
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWise(UserTypeId, FiscalYearId, CurrentloginProvinceId, MainOfficeId);
            model.ViewBagFiscalYearId = 1;
            model.UserTypeId = id;
            return View(model);
        }

        public ActionResult ViewNirdeshOfficeDtl(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.FiscalYearId = 5;//get current fiscal year id;
            if (CurrentUserOfficeType == 3)//Ministry User
            {
                model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardBerujuTypeWiseForMinistry(id, 1, CurrentloginProvinceId, CurrentUserOfficeId);

            }
            else//Nirdeshanalaya Users
            {
                model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardBerujuTypeWiseForNirdesh(id, 1, CurrentloginProvinceId, CurrentUserOfficeId);
            }

            model.UserTypeId = id;
            return View(model);
        }

        public ActionResult ViewAayogDtl(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            return View(model);
        }

        public ActionResult ViewLocalLevelDtl(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            return View(model);
        }


        [HttpPost]
        public PartialViewResult GetReportByFYId(DashboardViewModel model)
        {

            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWise(model.UserTypeId, model.FiscalYearId, CurrentloginProvinceId, 0);
            model.UserTypeId = model.UserTypeId;
            return PartialView("_GetReportByFYId", model);
        }

        [HttpPost]
        public PartialViewResult GetReportByFYIdForOfficeHiearchy(DashboardViewModel model)
        {
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWise(model.UserTypeId, model.FiscalYearId, CurrentloginProvinceId, model.CurrentLoginUserOfficeId);
            model.UserTypeId = model.UserTypeId;
            return PartialView("_GetReportByFYIdHiearchy", model);
        }

        [HttpPost]
        public PartialViewResult GetReportByFYIdForMinistry(DashboardViewModel model)//ministry user and nirdeshnalaya users
        {
            model.CurrentLoginUserOfficeId = CurrentUserOfficeId;
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            if (CurrentUserOfficeType == 3)
            {
                model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardBerujuTypeWiseForMinistry(model.UserTypeId, model.FiscalYearId, CurrentloginProvinceId, model.CurrentLoginUserOfficeId);

            }
            else
            {
                model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardBerujuTypeWiseForNirdesh(model.UserTypeId, model.FiscalYearId, CurrentloginProvinceId, model.CurrentLoginUserOfficeId);

            }

            model.UserTypeId = model.UserTypeId;
            return PartialView("_GetReportByFYIdHiearchy", model);
        }
        public ActionResult ChangePassword()
        {
            DashboardViewModel model = new DashboardViewModel();
            return View(model);
        }



        [HttpPost]
        public ActionResult ChangePassword(DashboardViewModel model)
        {
            DashboardUtility cp = new DashboardUtility();

            //find if user exist or not
            if (cp.ChangeUserPassword(model.UserEmail) == "Updated Successfully")
            {
                TempData["Notifications"] = "पासवर्ड परिवर्तन भयो ।";
            }
            else
            {
                TempData["ErrorMessage"] = "प्रयोगकर्ता सिस्टममा छैन ।";
            }
            //here sectionname is username...static code....
            return RedirectToAction("ChangePassword");
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
              //  AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult ProvinceAdminDashBoard()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseCumulativeSumList(CurrentUserOfficeId);
            return View(model);
        }


        public ActionResult CumilativeDashBoard()
        {
           // CurrentUserOfficeId = officeId ?? CurrentUserOfficeId;
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseCumulativeSumList(CurrentUserOfficeId);
            model.DashboardBerujuTypewiseTableList = RS.DashboardGetExternalBerujuSumByOfficeHierarchy(CurrentUserOfficeId);
            model.DashboardOfficeCountVM = RS.GetOfficeCountByLoginOffice(CurrentUserOfficeId);
            ViewBag.UserType = CurrentUserOfficeType;
            return View(model);
        }


    
        [HttpPost]
        public ActionResult CumilativeDashBoard(DashboardViewModel model)
        {
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();

            var OfficeId = GetSelectedOfficeId(model) ?? CurrentUserOfficeId;
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseCumulativeSumList(OfficeId);
            model.DashboardBerujuTypewiseTableList = RS.DashboardGetExternalBerujuSumByOfficeHierarchy(OfficeId);
            model.DashboardOfficeCountVM = RS.GetOfficeCountByLoginOffice(OfficeId);
            ViewBag.UserType = CurrentUserOfficeType;
            ViewBag.OfficeId = OfficeId;
            return PartialView("_CumilativeDashBoard", model);

        }





        public ActionResult PublicAccountCommittee()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(0);
            return View(model);
        }



        

        public ActionResult ProvinceChiefDashBoard()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumList(0);
            return View(model);
        }

        public ActionResult DistrictAdminDashBoard()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardkoshwiseTableList = new List<DashboardkoshwiseTable>();
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            model.DashboardkoshwiseTableList = RS.DashboardKoshwiseSumListForDistrictOnly(0, model.CurrentLoginUserDistrictId);

            return View(model);
        }


        //view saidantik beruju count officeWise
        public ActionResult ViewSaidantikPage(int id)//UserTypeId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.FiscalYearId = 5;
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetSaidantikBerujuCountByOfficeType(id, model.FiscalYearId, CurrentloginProvinceId, 0);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            return View(model);
        }

        public ActionResult SamparikhasadAmountByType(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.SamparikshadDetailByTypeVMList = new List<SamparikshadDetailByTypeVM>();
            model.FiscalYearId = 5;
            model.SamparikshadDetailByTypeVMList = RS.SP_GetSamparikshadDetailsForDashboard(id, model.FiscalYearId);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            return View(model);
        }


        public ActionResult ViewSaidaintikBerujuOfficeWise(int id, int id1)//OfficeId,FiscalYearId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Admin_GetSaidaintikBerujuListByOfficeIdVMList = new List<Admin_GetSaidaintikBerujuListByOfficeIdVM>();
            model.FiscalYearId = id1;
            model.Admin_GetSaidaintikBerujuListByOfficeIdVMList = RS.Admin_GetSaidaintikBerujuListByOfficeId(id, model.FiscalYearId);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            model.CurrentLoginUserOfficeId = id;
            return View(model);
        }





        [HttpPost]
        public PartialViewResult GetSaidantikReportByFYId(DashboardViewModel model)
        {

            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetSaidantikBerujuCountByOfficeType(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0);
            model.FiscalYearId = model.FiscalYearId;
            model.CurrentLoginUserOfficeId = model.CurrentLoginUserOfficeId;
            return PartialView("_GetSaidantikReportByFYId", model);
        }


        //view saidantik beruju count officeWise
        public ActionResult ViewNoneBerujuPage(int id)//UserTypeId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.FiscalYearId = 5;
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetBerujuNotDoneCountByOfficeType(id, model.FiscalYearId, CurrentloginProvinceId, 0);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            return View(model);
        }

        public ActionResult ViewNoneBerujuDetailByOfficeId(int id, int id1)//UserTypeId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Admin_BerujuNotDoneListByOfficeIdVMList = new List<Admin_BerujuNotDoneListByOfficeIdVM>();
            model.FiscalYearId = id1;
            model.Admin_BerujuNotDoneListByOfficeIdVMList = RS.Admin_BerujuNotDoneListByOfficeId(id, model.FiscalYearId);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            model.CurrentLoginUserOfficeId = id;
            return View(model);
        }


        [HttpPost]
        public PartialViewResult GetNoneBerujuByFYId(DashboardViewModel model)
        {

            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetBerujuNotDoneCountByOfficeType(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0);
            return PartialView("_GetNonBerujuReportByFYId", model);
        }

        public ActionResult ViewSamparikshadPage(int id, int id1)//UserTypeId,internalorexternal
        {
            DashboardViewModel model = new DashboardViewModel();
            model.IntExtSamparikshadCountVMList = new List<IntExtSamparikshadCountVM>();
            model.FiscalYearId = 5;
            model.IntExtSamparikshadCountVMList = RS.SP_GetSamparikshadCountByOfficeType(id, model.FiscalYearId, CurrentloginProvinceId, 0);
            if (id1 == 1)
            {
                model.IntExtSamparikshadCountVMList = RS.SP_GetInternalSamparikshadCountByOfficeType(id, model.FiscalYearId, CurrentloginProvinceId, 0);

            }
            model.OfficeTypeSearchId = 2;//by default ministry Id
            model.InternalOrExternalBerujuTypeId = id1;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult ViewSamparikshadPartial(DashboardViewModel model)
        {

            model.IntExtSamparikshadCountVMList = new List<IntExtSamparikshadCountVM>();
            model.IntExtSamparikshadCountVMList = RS.SP_GetInternalSamparikshadCountByOfficeType(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0);

            if (model.InternalOrExternalBerujuTypeId == 2)
            {
                model.IntExtSamparikshadCountVMList = RS.SP_GetSamparikshadCountByOfficeType(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0);

            }

            return PartialView("_GetSamparikshadByFYId", model);
        }

        public ActionResult ViewSamparikshadDetailByOfficeId(int id, int id1, int id2)//officeid, fiscal year, internal or external
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Admin_GetInternalExternalSamparikshadListByOfficeIdVMList = new List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM>();
            model.CurrentLoginUserOfficeId = id;
            model.FiscalYearId = id1;
            model.Admin_GetInternalExternalSamparikshadListByOfficeIdVMList = RS.Admin_GetInternalSamparikshadListByOfficeId(model.CurrentLoginUserOfficeId, model.FiscalYearId, CurrentloginProvinceId, 0);
            model.InternalOrExternalBerujuTypeId = id2;
            if (model.InternalOrExternalBerujuTypeId == 2)
            {
                model.Admin_GetInternalExternalSamparikshadListByOfficeIdVMList = RS.Admin_GetSamparikshadListByOfficeId(model.CurrentLoginUserOfficeId, model.FiscalYearId, CurrentloginProvinceId, 0);
            }
            return View(model);
        }


        public ActionResult ViewDetailSamparikshadForm(int id, int id1, int id2)//SamparikshadID, ExternalBerujuId, officeId
        {
            InternalBerujuService IBS = new InternalBerujuService();
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id1);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id1);
            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id1);
            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel = IBS.GetExternalSamparikshadListByPrimaryId(id, id2);
            model.ObjExternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjExternalSamparikshadViewModel.RevisedDate);
            model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
            model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id1, id2, id);
            model.OfficeId = id2;
            return View(model);
        }

        public ActionResult ViewDetailInternalSamparikshadForm(int id, int id1, int id2)//ExternalBeruju, SamparikshadID, officeId
        {
            InternalBerujuService IBS = new InternalBerujuService();
            InternalBeruju model = new InternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id1);
            model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
            model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(id1);
            model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id1);
            model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
            model.ObjInternalSamparikshadViewModel = IBS.GetInternalSamparikshadListByPrimaryId(id, id2);
            model.ObjInternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjInternalSamparikshadViewModel.RevisedDate);
            model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
            model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(id1, id2, id);
            model.OfficeId = id2;
            return View(model);
        }


        public ActionResult ViewExternalBerujuDetails(int id, int id1)
        {
            InternalBerujuService IBS = new InternalBerujuService();
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(id1, id);
            //model.FromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDate);
            //model.ToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDate);
            //model.AccountantFromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantFromDate);
            //model.AccountantToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantToDate);
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);

            return View(model);
        }

        public ActionResult ViewInternalBerujuDetails(int id, int id1)
        {
            InternalBerujuService IBS = new InternalBerujuService();
            InternalBeruju model = new InternalBeruju();
            model = IBS.ListInternalBerujuByPrimaryId(id1, id);

            model.ObjManagerNameViewModel = new ManagerOrAuditorNameViewModel();
            model.ObjAccountantNameViewModel = new ManagerOrAuditorNameViewModel();
            model.ObjAuditorNameViewModel = new ManagerOrAuditorNameViewModel();

            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);
            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 1);
            return View(model);
        }


        public ActionResult ViewMinistryDtlForDistrictUser(int id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.FiscalYearId = 5;//get current fiscal year id;
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWiseForDistrict(id, 1, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            //model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWise(id, 1, 6, 0);

            model.UserTypeId = id;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetReportByFYIdForDistrict(DashboardViewModel model)
        {

            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            model.DashboardOfficesGetSumBerujuTypeWiseViewModelList = RS.DashboardOfficesGetSumBerujuTypeWiseForDistrict(model.UserTypeId, model.FiscalYearId, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            model.UserTypeId = model.UserTypeId;
            return PartialView("_GetReportByFYIdForDistrict", model);
        }

        public ActionResult ViewSaidantikPageForDistrict(int id)//UserTypeId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.FiscalYearId = 5;
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetSaidantikBerujuCountByOfficeTypeForDistrict(id, 1, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            model.OfficeTypeSearchId = id;//by default ministry Id
            return View(model);
        }


        [HttpPost]
        public PartialViewResult GetSaidantikReportByFYIdForDistrict(DashboardViewModel model)
        {

            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);
            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetSaidantikBerujuCountByOfficeTypeForDistrict(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            return PartialView("_GetSaidantikReportByFYId", model);
        }


        public ActionResult ViewNoneBerujuPageForDistrict(int id)//UserTypeId
        {
            DashboardViewModel model = new DashboardViewModel();
            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.FiscalYearId = 5;
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);

            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetBerujuNotDoneCountByOfficeTypeForDistrict(id, 1, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            model.OfficeTypeSearchId = 2;//by default ministry Id
            return View(model);
        }


        [HttpPost]
        public PartialViewResult GetNoneBerujuByFYIdForDistrict(DashboardViewModel model)
        {

            model.SaidantikCountOfficeWiseViewModelList = new List<SaidantikCountOfficeWiseViewModel>();
            model.CurrentLoginUserDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(CurrentUserOfficeId);

            model.SaidantikCountOfficeWiseViewModelList = RS.SP_GetBerujuNotDoneCountByOfficeTypeForDistrict(model.OfficeTypeSearchId, model.FiscalYearId, CurrentloginProvinceId, 0, model.CurrentLoginUserDistrictId);
            return PartialView("_GetNonBerujuReportByFYId", model);
        }
    }
}
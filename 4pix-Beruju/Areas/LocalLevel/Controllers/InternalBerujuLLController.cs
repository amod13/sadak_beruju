using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;


namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{

    [Authorize]
    public class InternalBerujuLLController : Controller
    {
        InternalBerujuService IBS = new InternalBerujuService();

        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        // GET: LocalLevel/InternalBerujuLL
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(int id)
        {
            InternalBeruju model = new InternalBeruju();
            model.InternalBerujuList = new List<InternalBeruju>();
            model.InternalBerujuList = IBS.ListInternalBeruju(CurrentUserOfficeId).Where(x => x.KoshTypeId == id).ToList();
            model.KoshTypeId = id;
            return View(model);
        }

        public ActionResult ListInternalBeruju(int id)
        {
            InternalBeruju model = new InternalBeruju();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetInternalBerujuList(InternalBeruju model)
        {
            if (model.KoshTypeId == 6)//saidantik
            {
                model.SaidantikBerujuList = new List<SaidantikBeruju>();
                if (model.FiscalYearId > 0)
                {
                    model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, 1).Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, 1).ToList();

                }
                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.BerujuDafaNumber.Contains(model.BerujuNumber)).ToList();

                }
                return PartialView("_GetSaidantikBerujuForList", model);

            }
            else
            {
                model.InternalBerujuList = new List<InternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    model.InternalBerujuList = IBS.ListInternalBeruju(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.InternalBerujuList = IBS.ListInternalBeruju(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


                }
                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.InternalBerujuList = model.InternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }
                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetInternalBerujuList", model);
            }
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Create(int id)
        {
            ViewBag.Mode = "Create";
            InternalBeruju model = new InternalBeruju();
            model.KoshTypeId = id;

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList.Add(newObj);

            model.InternalBerujuTopFiveList = new List<InternalBeruju>();
            model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
            return View(model);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(InternalBeruju model)
        {
            if (model.VoucharAmunt <= 0)
            {
                ViewBag.Mode = "Create";
                ViewBag.ErrorMessage = "बेरुजु रकम मिलेन ।";
                model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }
            model.OfficeId = CurrentUserOfficeId;
            model.BerujuStatus = true;
            model.CreatedBy = CurrentLoginUserId.ToString();
            model.FromDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDateStr);
            model.ToDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDateStr);
            model.AccountantFromDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.AccountantFromDateStr);
            model.AccountantToDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.AccountantToDateStr);
            model.VoucharDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.VoucharDateStr);
            model.OfficeManagerPost = "Default Post";
            model.BerujuNumber = model.BerujuNumber;
            model.ExpenseTItle = "001";
            model.ToWhomName = "Default Name";
            model.BerujuAmount = 0;
            model.IsSaidantikBeruju = false;

            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.InsertInternalBeruju(model);
            //rms.PrimaryId = 12;

            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("Create", new { @id = model.KoshTypeId });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

        }

        public ActionResult Edit(int id)
        {
            InternalBeruju model = new InternalBeruju();
            model = IBS.ListInternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.FromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDate);
            model.ToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDate);
            model.AccountantFromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantFromDate);
            model.AccountantToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantToDate);
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);
            ViewBag.Mode = "Edit";

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 1);


            //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
            //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(InternalBeruju model)
        {
            if (model.VoucharAmunt <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम मिलने ।";
                //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

            model.OfficeId = CurrentUserOfficeId;
            model.BerujuStatus = true;
            model.CreatedBy = CurrentLoginUserId.ToString();
            model.FromDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.FromDateStr);
            model.ToDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ToDateStr);
            model.AccountantFromDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.AccountantFromDateStr);
            model.AccountantToDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.AccountantToDateStr);
            model.VoucharDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.VoucharDateStr);
            model.OfficeManagerPost = "Default Post";
            model.BerujuNumber = model.BerujuNumber;
            model.ToWhomName = "Default Name";
            model.BerujuAmount = 0;
            model.IsSaidantikBeruju = false;
            model.ExpenseTItle = "001";
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.UpdateInternalBeruju(model);

            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("Index", new { @id = model.KoshTypeId });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

        }
        public ActionResult DeleteFromCreate(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.DeleteInternalBeruju(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट आन्तरिक बेरुजुको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }

            return RedirectToAction("Create", new { @id = id1 });
        }

        public ActionResult Delete(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.DeleteInternalBeruju(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट आन्तरिक बेरुजुको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }

            return RedirectToAction("ListInternalBeruju", new { @id = 1 });
        }


        public ActionResult ChangeToFinalBeruju(int id, int id1)
        {
            InternalBeruju model = new InternalBeruju();
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.ChangeInternalBerujuToFinalBeruju(id);
            if (rms.ReturnMessage == "Saved Successfully")
            {
                TempData["Success"] = "आन्तरिक बेरुजु अन्तिम बेरुजुमा परिवर्तन भयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }
            return RedirectToAction("create", new { @id = id1 });
        }

        public ActionResult ViewDetails(int id)
        {
            InternalBeruju model = new InternalBeruju();
            model = IBS.ListInternalBerujuByPrimaryId(CurrentUserOfficeId, id);

            model.ObjManagerNameViewModel = new ManagerOrAuditorNameViewModel();
            model.ObjAccountantNameViewModel = new ManagerOrAuditorNameViewModel();
            model.ObjAuditorNameViewModel = new ManagerOrAuditorNameViewModel();


            //if (model.OfficeManagerId > 0)
            //{
            //    model.ObjManagerNameViewModel = IBS.GetEmployeeOrAuditorByPrimaryId(CurrentUserOfficeId, model.OfficeManagerId);
            //    model.FromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjManagerNameViewModel.FromDuration);
            //    model.ToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjManagerNameViewModel.ToDuration);
            //    model.OfficeManagerName = model.ObjManagerNameViewModel.ManagerOrAuditorName;

            //}
            //if (model.AccountantId > 0)
            //{
            //    model.ObjAccountantNameViewModel = IBS.GetEmployeeOrAuditorByPrimaryId(CurrentUserOfficeId, model.AccountantId);
            //    model.AccountantFromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjAccountantNameViewModel.FromDuration);
            //    model.AccountantToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjAccountantNameViewModel.ToDuration);
            //    model.AccountantName = model.ObjAccountantNameViewModel.ManagerOrAuditorName;
            //}

            //if (model.AuditorId > 0)
            //{
            //    model.ObjAuditorNameViewModel = IBS.GetEmployeeOrAuditorByPrimaryId(CurrentUserOfficeId, model.AuditorId);
            //    model.AuditorName = model.ObjAuditorNameViewModel.ManagerOrAuditorName;
            //    model.AuditorPost = model.ObjAuditorNameViewModel.AuditorPost;
            //}





            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 1);

            return View(model);
        }

        public ActionResult GetEmployeeAndAuditorName(string VoucherDate)
        {
            InternalBeruju model = new InternalBeruju();
            model.ManagerOrAuditorNameViewModelList = new List<ManagerOrAuditorNameViewModel>();
            CommonService cs = new CommonService();
            if (IsValidDateTimeTest(VoucherDate))
            {
                model.FromDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(VoucherDate);
                model.ManagerOrAuditorNameViewModelList = cs.GetEmployeeAndAuditorNameByVoucher(1, 0, model.FromDate);
                // return PartialView("_EmployeeAndAuditorDetials", model);
                return PartialView("~/Areas/LocalLevel/Views/InternalBerujuLL/_EmployeeAndAuditorDetials.cshtml", model);


            }

            {
                return PartialView("~/Areas/LocalLevel/Views/InternalBerujuLL/_EmployeeAndAuditorDetials.cshtml", model);

            }


        }


        public bool IsValidDateTimeTest(string dateTime)
        {
            string[] formats = { "yyyy/mm/dd" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"),
                                           DateTimeStyles.None, out parsedDateTime);
        }



        [HttpPost]
        public ActionResult ToWhomeDetailsList()
        {
            InternalBeruju ib = new InternalBeruju();

            ib.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();

            return PartialView("ToWhomeDetailsList");
        }


        #region beruju not done

        BerujuCommonService bcs = new BerujuCommonService();
        public ActionResult ListBerujuNotDone(int id)//internal or exter
        {
            BerujuNotDoneModel model = new BerujuNotDoneModel();
            model.BerujuNotDoneModelList = new List<BerujuNotDoneModel>();
            model.BerujuNotDoneModelList = bcs.ListBerujuNotDone(CurrentUserOfficeId, 0, id);
            model.InternalOrExternal = id;
            return View(model);

        }

        public ActionResult CreateBerujuNotDone(int id)
        {
            BerujuNotDoneModel model = new BerujuNotDoneModel();
            model.InternalOrExternal = id;
            model.BerujuNotDoneModelListTopFive = new List<BerujuNotDoneModel>();
            model.BerujuNotDoneModelListTopFive = bcs.ListBerujuNotDoneTopFive(CurrentUserOfficeId, id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBerujuNotDone(BerujuNotDoneModel model)
        {
            string FileNameVal = model.UploadFileDetailsFileType == null ? string.Empty : model.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + model.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadFileUrl);
                model.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.UploadFileUrl = string.Empty;
            }
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            var currUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
            model.CreatedBy = currUserId.ToString();
            model.OfficeId = CurrentUserOfficeId;
            rms = bcs.InsertBerujunotdone(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("CreateBerujuNotDone", new { @id = model.InternalOrExternal });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                model.BerujuNotDoneModelListTopFive = new List<BerujuNotDoneModel>();
                model.BerujuNotDoneModelListTopFive = bcs.ListBerujuNotDoneTopFive(CurrentUserOfficeId, model.InternalOrExternal);
                return View(model);
            }
            //return RedirectToAction("ListBerujuNotDone", new { @id=model.InternalOrExternal});
        }

        public ActionResult EditBerujuNotDone(int id, int id1)
        {
            BerujuNotDoneModel model = new BerujuNotDoneModel();
            model = bcs.ListBerujuNotDone(CurrentUserOfficeId, id, id1).SingleOrDefault();
            model.InternalOrExternal = id1;
            model.BerujuNotDoneModelListTopFive = new List<BerujuNotDoneModel>();
            model.BerujuNotDoneModelListTopFive = bcs.ListBerujuNotDoneTopFive(CurrentUserOfficeId, id);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBerujuNotDone(BerujuNotDoneModel model)
        {

            string FileNameVal = model.UploadFileDetailsFileType == null ? string.Empty : model.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + model.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadFileUrl);
                model.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                if (string.IsNullOrEmpty(model.UploadFileUrl))
                {
                    model.UploadFileUrl = string.Empty;
                }
                else
                {
                    model.UploadFileUrl = model.UploadFileUrl;
                }
            }
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.OfficeId = CurrentUserOfficeId;
            rms = bcs.UpdateBerujunotdone(model);
            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परीवर्तन भयो । ";
                return RedirectToAction("CreateBerujuNotDone", new { @id = model.InternalOrExternal });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();

                model.BerujuNotDoneModelListTopFive = new List<BerujuNotDoneModel>();
                model.BerujuNotDoneModelListTopFive = bcs.ListBerujuNotDoneTopFive(CurrentUserOfficeId, model.InternalOrExternal);
                return View(model);
            }


            //return RedirectToAction("ListBerujuNotDone");
        }

        public ActionResult DeleteBerujuNotDone(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = bcs.DeleteBerujunotdone(id);
            return RedirectToAction("ListBerujuNotDone", new { @id = id1 });
        }

        public ActionResult DeleteBerujuNotDoneFromList(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = bcs.DeleteBerujunotdone(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टमबाट हटाईयो। ";

            }
            else
            {
                TempData["Success"] = "सिस्टममा समस्या आयो । पुनह् कोशिस गर्नुहोस। ";
            }
            return RedirectToAction("ListBerujuNotDone", new { @id = id1 });
        }
        public ActionResult DeleteBerujuNotDoneFromCreate(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = bcs.DeleteBerujunotdone(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टमबाट हटाईयो। ";

            }
            else
            {
                TempData["Success"] = "सिस्टममा समस्या आयो । पुनह् कोशिस गर्नुहोस। ";
            }
            return RedirectToAction("CreateBerujuNotDone", new { @id = id1 });
        }
        public ActionResult DeleteBerujuNotDoneFromEdit(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = bcs.DeleteBerujunotdone(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "विवरण सिस्टमबाट हटाईयो। ";

            }
            else
            {
                TempData["Success"] = "सिस्टममा समस्या आयो । पुनह् कोशिस गर्नुहोस। ";
            }
            return RedirectToAction("EditBerujuNotDone", new { @id = id1 });


        }


        public ActionResult ViewDetailsBerujuNotDone(int id, int id1)
        {
            BerujuNotDoneModel model = new BerujuNotDoneModel();
            model = bcs.ListBerujuNotDone(CurrentUserOfficeId, id, id1).SingleOrDefault();
            model.InternalOrExternal = id1;
            return View(model);
        }

        #endregion



        public ActionResult InternalSamparikshadRequestForm()
        {
            InternalBeruju model = new InternalBeruju();
            return View(model);
        }

        //changed Internal or External both.....
        [HttpPost]
        public PartialViewResult InternalSamparikshadRequestList(InternalBeruju model)
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

                return PartialView("_GetSaidantikBerujuForList", model);

            }
            else
            {
                model.InternalBerujuList = new List<InternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();
                    model.InternalBerujuList = IBS.ListInternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();
                    model.InternalBerujuList = IBS.ListInternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.InternalBerujuList = model.InternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetInternalBerujuListForRequest", model);
            }
        }


        public ActionResult MakeInternalSamparikshadRequestForm(int id)
        {


            InternalBeruju model = new InternalBeruju();

            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
            model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(id);
            model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmountForRequest(id);

            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            ViewBag.Mode = "Create";
            model.ObjSamparikshadReqMasterViewModel = new InternalSamparikshadReqMasterViewModel();
            int IfInserted = IBS.CheckIfAlreadyRequestedForInernalSamparikshad(CurrentUserOfficeId, id);
            if (IfInserted > 999999)
            {
                ViewBag.Mode = "Edit";
                model.ObjSamparikshadReqMasterViewModel = IBS.IN_SPGetInternalSamparikshadRequestDetailByEBID(id);
                model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjSamparikshadReqMasterViewModel.RequestedDateEng);
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);

            }

            else
            {
                model.ObjSamparikshadReqMasterViewModel.InternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.InternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.InternalBerujuForSamparikshadVMObj.BerujuShorDesc;

                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);


            }
            model.OfficeId = CurrentUserOfficeId;
            return View(model);

        }



        [HttpPost]
        public ActionResult MakeInternalSamparikshadRequestForm(InternalBeruju model)
        {

            if (model.InternalBerujuForSamparikshadVMObj.RemainingAmount < model.ObjSamparikshadReqMasterViewModel.TotalAmount)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
                model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(model.ObjSamparikshadReqMasterViewModel.InternalBerujuId);
                model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(model.ObjSamparikshadReqMasterViewModel.InternalBerujuId);
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.InternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.InternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.OfficeId = CurrentUserOfficeId;
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(model.ObjSamparikshadReqMasterViewModel.InternalBerujuId, CurrentUserOfficeId, 0);

                return View(model);

            }

            string FileNameVal = model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjSamparikshadReqMasterViewModel.UploadedDoc = Path.GetFileName(PrifixLetter + "_" + model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSamparikshadReqMasterViewModel.UploadedDoc);
                model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.ObjSamparikshadReqMasterViewModel.UploadedDoc = string.Empty;
            }



            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjSamparikshadReqMasterViewModel.RequestedDateEng = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjSamparikshadReqMasterViewModel.RequestedDateNep);
            //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            model.ObjSamparikshadReqMasterViewModel.InternalSamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.IN_InsertInternalSamparikshadReqDetail(model.ObjSamparikshadReqMasterViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित भयो । ";
                //return RedirectToAction("ViewSamparikshadReqForm", new { @id = model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId });
                return RedirectToAction("ViewSamparikshadReqFormById", new { @id = rms.PrimaryId });
            }
            else
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित हुन सकेन । पुनह् कोसिस गर्नुहोस । ";
                return RedirectToAction("SamparikshadRequestForm");
            }

        }

        public ActionResult ViewSamparikshadReqForm(int id)
        {
            InternalBeruju model = new InternalBeruju();
            model.ObjInternalSamparikshadRequestMaterDetailVM = IBS.IN_SPGetInternalSamparikshadRequestletter(CurrentUserOfficeId, id);
            model.GetInternalsamparikshadrequesttowhomforletterViewModelList = new List<GetInternalsamparikshadrequesttowhomforletterViewModel>();
            //get samparikshadreqprimaryid by externalberujuid
            //model.GetsamparikshadrequesttowhomforletterViewModelList = IBS.GetsamparikshadrequesttowhomforletterListForLetter(id).ToList();
            return View(model);



        }

        public ActionResult ViewSamparikshadReqFormById(int id)
        {
            InternalBeruju model = new InternalBeruju();
            model.ObjInternalSamparikshadRequestMaterDetailVM = IBS.IN_SPGetInternalSamparikshadRequestletterByPrimaryId(CurrentUserOfficeId, id);
            model.GetInternalsamparikshadrequesttowhomforletterViewModelList = IBS.IN_Getsamparikshadrequesttowhomforletter(id).ToList();
            return View(model);
        }

        #region Samparikshad Form
        public ActionResult InternalSamparikshadForm()
        {
            InternalBeruju model = new InternalBeruju();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult InternalSamparikshadForm(InternalBeruju model)
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

                return PartialView("_GetSaidantikBerujuForList", model);


            }
            else
            {
                model.InternalBerujuList = new List<InternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    model.InternalBerujuList = IBS.IN_GetInternalBerujulistForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.InternalBerujuList = IBS.IN_GetInternalBerujulistForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.InternalBerujuList = model.InternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetInternalBerujuListForSamparikshad", model);
            }
        }


        public ActionResult MakeSamparikshad(int id)
        {
            InternalBeruju model = new InternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
            model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(id);

            model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmount(id);
            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
            model.ObjInternalSamparikshadViewModel.InternalBerujuId = id;

            model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
            model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeSamparikshad(InternalBeruju model)
        {
            if (model.ObjInternalSamparikshadViewModel.ReviesedVoucherAmount <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम ० भन्दा धेरै हुनुपर्दछ ।";
                model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
                model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(model.ObjInternalSamparikshadViewModel.InternalBerujuId);

                model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmount(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel.InternalBerujuId = model.ObjInternalSamparikshadViewModel.InternalBerujuId;
                model.ObjInternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjInternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjInternalSamparikshadViewModel.RevisedDateStr);
                model.ObjInternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();
                return View(model);

            }


            if (model.ObjInternalSamparikshadViewModel.ReviesedVoucherAmount > model.InternalBerujuForSamparikshadVMObj.VoucharAmunt)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
                model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(model.ObjInternalSamparikshadViewModel.InternalBerujuId);

                model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmount(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel.InternalBerujuId = model.ObjInternalSamparikshadViewModel.InternalBerujuId;
                model.ObjInternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjInternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjInternalSamparikshadViewModel.RevisedDateStr);
                model.ObjInternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();
                return View(model);

            }

            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjInternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjInternalSamparikshadViewModel.UploadFileDetails);
                model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.ObjInternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            model.ObjInternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            model.ObjInternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjInternalSamparikshadViewModel.RevisedDateStr);
            model.ObjInternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();

            rms = IBS.IN_InsertInternalSamparikshadDetail(model.ObjInternalSamparikshadViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("InternalSamparikshadForm");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        #endregion Samparikshad Form


        public ActionResult EditSamparikshadRequestForm(int id, int id1)
        {


            InternalBeruju model = new InternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
            model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(id);
            model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmountForRequest(id);

            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            ViewBag.Mode = "Create";
            model.ObjSamparikshadReqMasterViewModel = new InternalSamparikshadReqMasterViewModel();
            int IfInserted = IBS.CheckIfAlreadyRequestedForInernalSamparikshad(CurrentUserOfficeId, id);
            if (IfInserted > 0)
            {
                ViewBag.Mode = "Edit";
                model.ObjSamparikshadReqMasterViewModel = IBS.IN_SPGetInternalSamparikshadRequestDetailByPrimaryId(id, id1);
                model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjSamparikshadReqMasterViewModel.RequestedDateEng);
                model.ObjSamparikshadReqMasterViewModel.InternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.InternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.InternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.IN_GetSamparikshadToWhomDetailsByBerujuIdForRequest(id, CurrentUserOfficeId, id1);

            }

            else
            {
                model.ObjSamparikshadReqMasterViewModel.InternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.InternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.InternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.IN_GetSamparikshadToWhomDetailsByBerujuIdForRequest(id, CurrentUserOfficeId, 0);


            }
            model.OfficeId = CurrentUserOfficeId;
            return View(model);

        }
        [HttpPost]
        public ActionResult EditSamparikshadRequestForm(InternalBeruju model)
        {
            string FileNameVal = model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjSamparikshadReqMasterViewModel.UploadedDoc = Path.GetFileName(PrifixLetter + "_" + model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSamparikshadReqMasterViewModel.UploadedDoc);
                model.ObjSamparikshadReqMasterViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                if (string.IsNullOrEmpty(model.ObjSamparikshadReqMasterViewModel.UploadedDoc))
                {
                    model.ObjSamparikshadReqMasterViewModel.UploadedDoc = string.Empty;
                }
                else
                {
                    model.ObjSamparikshadReqMasterViewModel.UploadedDoc = model.ObjSamparikshadReqMasterViewModel.UploadedDoc;
                }
            }
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjSamparikshadReqMasterViewModel.RequestedDateEng = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjSamparikshadReqMasterViewModel.RequestedDateNep);
            //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            model.ObjSamparikshadReqMasterViewModel.InternalSamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.UpdateInternalSamparikshadReqDetail(model.ObjSamparikshadReqMasterViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित भयो । ";
                return RedirectToAction("ViewSamparikshadReqFormById", new { @id = model.ObjSamparikshadReqMasterViewModel.InternalSamparikshadReqMasterId });
            }
            else
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित हुन सकेन । पुनह् कोसिस गर्नुहोस । ";
                return RedirectToAction("GetSamparikshadRequestList");
            }

        }


        public ActionResult EditSamparikshadForm(int id, int id1)
        {
            InternalBeruju model = new InternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id1);
            model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
            model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(id1);
            model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmount(id1);
            model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
            model.ObjInternalSamparikshadViewModel = IBS.GetInternalSamparikshadListByPrimaryId(id, CurrentUserOfficeId);
            model.ObjInternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjInternalSamparikshadViewModel.RevisedDate);
            model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
            model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(id1, CurrentUserOfficeId, id);
            return View(model);


        }

        [HttpPost]
        public ActionResult EditSamparikshadForm(InternalBeruju model)
        {
            if (model.ObjInternalSamparikshadViewModel.ReviesedVoucherAmount > model.InternalBerujuForSamparikshadVMObj.VoucharAmunt)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
                model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel = IBS.GetInternalSamparikshadListByPrimaryId(model.ObjInternalSamparikshadViewModel.InternalSamparishadId, CurrentUserOfficeId);
                model.ObjInternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjInternalSamparikshadViewModel.RevisedDate);
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(model.ObjInternalSamparikshadViewModel.InternalBerujuId, CurrentUserOfficeId, model.ObjInternalSamparikshadViewModel.InternalSamparishadId);


            }
            if (model.ObjInternalSamparikshadViewModel.ReviesedVoucherAmount <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम ० भन्दा धेरै हुनुपर्दछ ।";
                model.InternalBerujuForSamparikshadVMObj = new InternalBerujuForSamparikshadVM();
                model.InternalBerujuForSamparikshadVMObj = IBS.IN_GetInternalBerujuDetailForSamparikshad(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                model.InternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.IN_GetInternalSamparikshadRemainingAmount(model.ObjInternalSamparikshadViewModel.InternalBerujuId);
                model.ObjInternalSamparikshadViewModel = new InternalSamparikshadViewModel();
                model.ObjInternalSamparikshadViewModel = IBS.GetInternalSamparikshadListByPrimaryId(model.ObjInternalSamparikshadViewModel.InternalSamparishadId, CurrentUserOfficeId);
                model.ObjInternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjInternalSamparikshadViewModel.RevisedDate);
                model.InternalSamparikshadTowhomDetailVMListMain = new List<InternalSamparikshadTowhomDetailVM>();
                model.InternalSamparikshadTowhomDetailVMListMain = IBS.ListInternalSamparikshadTowhomDetails(model.ObjInternalSamparikshadViewModel.InternalBerujuId, CurrentUserOfficeId, model.ObjInternalSamparikshadViewModel.InternalSamparishadId);
                return View(model);

            }



            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjInternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjInternalSamparikshadViewModel.UploadFileDetails);
                model.ObjInternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else if (string.IsNullOrEmpty(model.ObjInternalSamparikshadViewModel.UploadFileDetails))
            {
                model.ObjInternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            else
            {
                model.ObjInternalSamparikshadViewModel.UploadFileDetails = model.ObjInternalSamparikshadViewModel.UploadFileDetails;
            }

            model.ObjInternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            model.ObjInternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjInternalSamparikshadViewModel.RevisedDateStr);
            model.ObjInternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.InternalSamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.UpdateInternalSamparikshadDetail(model.ObjInternalSamparikshadViewModel);

            if (rms.ReturnMessage == "Udpated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("GetSamparikshadList");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }
        public ActionResult DeleteSamparikshad(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.DeleteInternalSamparikshadDetail(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट सम्परिक्षणको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }

            return RedirectToAction("GetSamparikshadList");
        }

        //public ActionResult ViewSamparikshadReqFormById(int id)
        //{
        //    InternalBeruju model = new InternalBeruju();
        //    model.ObjInternalSamparikshadRequestMaterDetailVM = IBS.IN_SPGetInternalSamparikshadRequestletterByPrimaryId(CurrentUserOfficeId, id);
        //    model.GetInternalsamparikshadrequesttowhomforletterViewModelList = IBS.GetsamparikshadrequesttowhomforletterListForLetter(id).ToList();
        //    return View(model);
        //}


    }
}
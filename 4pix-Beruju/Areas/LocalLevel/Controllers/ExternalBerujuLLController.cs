using _4pix_Beruju.Areas.Admin;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class ExternalBerujuLLController : Controller
    {
        InternalBerujuService IBS = new InternalBerujuService();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        // GET: LocalLevel/ExternalBerujuLL

        public ActionResult Index(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model.ExternalBerujuList = new List<ExternalBeruju>();
            model.ExternalBerujuList = IBS.SPListExternalBerujuByKoshTypeId(CurrentUserOfficeId, id).ToList();
            //model.ExternalBerujuList = IBS.ListExternalBeruju(CurrentUserOfficeId).Where(x => x.KoshTypeId == id).ToList();
            model.KoshTypeId = id;
            return View(model);
        }

        #region List External Beruju
        public ActionResult ListExternalBeruju(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetExternalBerujuForList(ExternalBeruju model)
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
                return PartialView("_GetExternalBerujuForList", model);
            }
        }

        #endregion List External Beruju

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Create(int id)
        {
            ViewBag.Mode = "Create";
            ExternalBeruju model = new ExternalBeruju();
            model.KoshTypeId = id;
            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList.Add(newObj);

            model.ChaluOrPujigatId = 2;
            model.BerujuTypeId = 2;
            model.ToWhomID = 5;
            model.BerujuSubTitleId = 7;

            model.ExternalBerujuListTopFive = new List<ExternalBeruju>();
            model.ExternalBerujuListTopFive = IBS.ListExternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ExternalBeruju model)
        {



            if (model.VoucharAmunt <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम मिलेन ।";
                model.ExternalBerujuListTopFive = new List<ExternalBeruju>();
                model.ExternalBerujuListTopFive = IBS.ListExternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                ViewBag.Mode = "Edit";
                return View(model);
            }


            string FileNameVal = model.SupportingDocFiles == null ? string.Empty : model.SupportingDocFiles.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadedFileUrl = Path.GetFileName(PrifixLetter + "_" + model.SupportingDocFiles.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadedFileUrl);
                model.SupportingDocFiles.SaveAs(path);
            }
            else
            {
                model.UploadedFileUrl = string.Empty;
            }

            model.BerujuStatus = false;

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
            rms = IBS.InsertExternalBeruju(model);
            if (rms.PrimaryId > 0)
            {

              



                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("Create", new { @id = model.KoshTypeId });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                model.ExternalBerujuListTopFive = new List<ExternalBeruju>();
                model.ExternalBerujuListTopFive = IBS.ListExternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

        }


        public ActionResult Edit(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.FromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDate);
            model.ToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDate);
            model.AccountantFromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantFromDate);
            model.AccountantToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantToDate);
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);
            ViewBag.Mode = "Edit"; ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ExternalBeruju model)
        {
            bool hasOldFile = !string.IsNullOrEmpty(model.UploadedFileUrl);
            bool hasNewFile = model.SupportingDocFiles != null && model.SupportingDocFiles.ContentLength > 0;
            string FileNameVal = model.SupportingDocFiles == null ? string.Empty : model.SupportingDocFiles.FileName;
            if (!hasOldFile && !hasNewFile)
            {
                ViewBag.Mode = "Edit";  
                ViewBag.ErrorMessage = "कृपया फाईल राख्नुहोस् ।";
                return View(model);
            }

            if (IBS.CheckIfAlreadyInsertedIntoSamparikshad(CurrentUserOfficeId, model.ExternalBerujuId) > 0)
            {


                ViewBag.Mode = "Edit";
                ViewBag.ErrorMessage = "समपरीक्षण गरिसकेको फारामको विवरण सच्चाउन मिल्दैन  ।";
                //model.ExternalBerujuListTopFive = new List<ExternalBeruju>();
                //model.ExternalBerujuListTopFive = IBS.ListExternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();

                return View(model);
            }

            if (model.VoucharAmunt <= 0)
            {
                ViewBag.Mode = "Edit";
                ViewBag.ErrorMessage = "बेरुजु रकम मिलेन ।";
                //model.ExternalBerujuListTopFive = new List<ExternalBeruju>();
                //model.ExternalBerujuListTopFive = IBS.ListExternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();

                return View(model);
            }
            //check if already inserted into samparikshad

            
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {
                // 🗑️ Delete old file if it exists
                if (!string.IsNullOrEmpty(model.UploadedFileUrl))
                {
                    var oldFilePath = Path.Combine(Server.MapPath("~/RequiredDocs/"), model.UploadedFileUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }


                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadedFileUrl = Path.GetFileName(PrifixLetter + "_" + model.SupportingDocFiles.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadedFileUrl);
                model.SupportingDocFiles.SaveAs(path);




            }

            model.BerujuStatus = false;   /// for data entry user;
        


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
            rms = IBS.UpdateExternalBeruju(model);

            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("Index", new { @id = model.KoshTypeId });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult ViewDetails(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
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

        public ActionResult ViewDetailsNB(int id)//ViewDetails with no back button
        {
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
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




        public ActionResult DeleteFromCreate(int id, int id1)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            //check if already inseted into samparikshad form
            rms = IBS.DeleteExternalBeruju(id);
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
            rms = IBS.DeleteExternalBeruju(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट आन्तरिक बेरुजुको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }

            return RedirectToAction("ListExternalBeruju", new { @id = 1 });
        }



        #region Samparikshad Form
        public ActionResult SamparikshadForm()
        {
            ExternalBeruju model = new ExternalBeruju();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetExternalBerujuList(ExternalBeruju model)
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
                model.ExternalBerujuList = new List<ExternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetExternalBerujuList", model);
            }
        }

        #endregion Samparikshad Form
        #region Samparikshad RequestForm
        public ActionResult SamparikshadRequestForm()
        {
            ExternalBeruju model = new ExternalBeruju();
            return View(model);
        }

        //changed Internal or External both.....
        [HttpPost]
        public PartialViewResult SamparikshadRequestList(ExternalBeruju model)
        {
            if (model.KoshTypeId == 6)//saidantikMakeSamparikshadRequestForm
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
                model.ExternalBerujuList = new List<ExternalBeruju>();
                if (model.FiscalYearId > 0)
                {
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();
                    model.ExternalBerujuList = IBS.SPB_GetListForRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

                }
                else
                {
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();
                    //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();
                    model.ExternalBerujuList = IBS.SPB_GetListForRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetExternalBerujuListForRequest", model);
            }
        }



        public ActionResult ListActionFromRequeestBeruju()
        {
            SamparikshadServices SS = new SamparikshadServices();
            ExternalBeruju eb = new ExternalBeruju();
            eb.ListBerujuForSamparikshadActionVMList = new List<ListBerujuForSamparikshadActionVM>();
            eb.ListBerujuForSamparikshadActionVMList = SS.sp_GetRequestForActionSamparikshan(CurrentUserOfficeId, 2);
            return View(eb);
        }


        [HttpPost]
        public PartialViewResult ListActionFromRequeestBeruju(ExternalBeruju model)
        {
            SamparikshadServices SS = new SamparikshadServices();
            model.ListBerujuForSamparikshadActionVMList = new List<ListBerujuForSamparikshadActionVM>();
            model.ListBerujuForSamparikshadActionVMList = SS.sp_GetRequestForActionSamparikshan(CurrentUserOfficeId, 2);
            if (model.FiscalYearId > 0)
            {
                //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();
                //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();
                model.ListBerujuForSamparikshadActionVMList = SS.sp_GetRequestForActionSamparikshan(CurrentUserOfficeId, 2).Where(x => x.KoshTypeId == model.KoshTypeId && x.FiscalYearId == model.FiscalYearId).ToList();

            }
            else
            {
                //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();
                //model.ExternalBerujuList = IBS.ListExternalBerujuForSamparikshadRequestMake(CurrentUserOfficeId).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();
                model.ListBerujuForSamparikshadActionVMList = SS.sp_GetRequestForActionSamparikshan(CurrentUserOfficeId, 2).Where(x => x.KoshTypeId == model.KoshTypeId).ToList();


            }

            if (!string.IsNullOrEmpty(model.BerujuNumber))
            {
                model.BerujuNumber = model.BerujuNumber.Trim();
                model.ListBerujuForSamparikshadActionVMList = model.ListBerujuForSamparikshadActionVMList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

            }

            model.KoshTypeId = model.KoshTypeId;

           
            return PartialView("_ListActionFromRequeestBeruju", model);
        }




        //public ActionResult MakeSamparikshadRequestForm(int id)
        //{

        //    //checked if already inserted or not

        //    ExternalBeruju model = new ExternalBeruju();
        //    //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
        //    model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
        //    model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id);
        //    model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id);
        //    //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
        //    ViewBag.Mode = "Create";
        //    model.ObjSamparikshadReqMasterViewModel = new SamparikshadReqMasterViewModel();
        //    int IfInserted = IBS.CheckIfAlreadyRequestedForSamparikshad(CurrentUserOfficeId, id);
        //    if (IfInserted > 0)
        //    {
        //        ViewBag.Mode = "Edit";
        //        model.ObjSamparikshadReqMasterViewModel = IBS.SPGetSamparikshadRequestDetailByEBID(id);
        //        model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjSamparikshadReqMasterViewModel.RequestedDateEng);
        //        model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
        //        model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);

        //    }


        //    else
        //    {
        //        model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId = id;
        //        model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
        //        model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
        //        model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;

        //        model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
        //        model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);


        //    }
        //    return View(model);

        //}

        public ActionResult MakeSamparikshadRequestForm(int id)
        {


            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id);
            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(id);

            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            ViewBag.Mode = "Create";
            model.ObjSamparikshadReqMasterViewModel = new SamparikshadReqMasterViewModel();
            int IfInserted = IBS.CheckIfAlreadyRequestedForSamparikshad(CurrentUserOfficeId, id);
            if (IfInserted > 999999)
            {
                ViewBag.Mode = "Edit";
                model.ObjSamparikshadReqMasterViewModel = IBS.SPGetSamparikshadRequestDetailByEBID(id);
                model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjSamparikshadReqMasterViewModel.RequestedDateEng);
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);

            }

            else
            {
                model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(DateTime.Now);
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);


            }
            //get chief and post
            model.ObjSamparikshadReqMasterViewModel.ResponsiblePersonName = GetSamparikshadHeadAndPostTitle(1);
            model.ObjSamparikshadReqMasterViewModel.Post = GetSamparikshadHeadAndPostTitle(2);
            model.OfficeId = CurrentUserOfficeId;
            return View(model);

        }
        public string GetSamparikshadHeadAndPostTitle(int HeadNameOrPost)
        {
            string ReturnStr = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var result = db.CurrentOfficeChiefDetails.Where(x => x.OfficeId == CurrentUserOfficeId).FirstOrDefault();
                    if (result != null)
                    {
                        if (HeadNameOrPost == 1)
                        {
                            ReturnStr = result.ChiefName.ToString();
                        }
                        else
                        {
                            ReturnStr = result.ChiefPost.ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    ReturnStr = string.Empty;
                }

            }
            return ReturnStr;
        }




        [HttpPost]
        public ActionResult MakeSamparikshadRequestForm(ExternalBeruju model, IEnumerable<HttpPostedFileBase> files)
        {

            if (model.ObjSamparikshadReqMasterViewModel.TotalAmount <= 0)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण अनुरोध रकम लेख्नुहोस ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.OfficeId = CurrentUserOfficeId;
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId, CurrentUserOfficeId, 0);

                return View(model);
            }
            if (model.ExternalBerujuForSamparikshadVMObj.RemainingAmount < model.ObjSamparikshadReqMasterViewModel.TotalAmount)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण अनुरोध रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.OfficeId = CurrentUserOfficeId;
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId, CurrentUserOfficeId, 0);

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
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = CurrentUserOfficeId;


            foreach (var item in model.SamparikshadTowhomDetailVMListMain)
            {
                string concateletter = "S-R-" + item.SMTowhomDetailId + item.ExternalBerujuId;
                string requestFile = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                if (string.IsNullOrEmpty(FileNameVal) == false)
                {
                    item.UploadedFileUrl = Path.GetFileName(concateletter + "_" + item.SupportingDocFiles.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), item.UploadedFileUrl);
                    item.SupportingDocFiles.SaveAs(path);
                }
            }

            foreach (var item in model.SamparikshadTowhomDetailVMListMain)
            {
                int TowhomId = item.EBToWhomId;
                decimal? requestAmount = item.RevisedAmount;
                decimal remainingAmount = Utilities.GetSamparikshadIndividualSumForRequest(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId, item.EBToWhomId);
                if(requestAmount>remainingAmount)
                {
                    ViewBag.ErrorMessage = "अनुरोध गर्न लागिएको रकम बेरुजु रकम भन्दा धेरै भयो ।";
                    model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                    model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                    model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId);
                    model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                    model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                    model.OfficeId = CurrentUserOfficeId;
                    model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                    model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId, CurrentUserOfficeId, 0);

                    return View(model);
                }
               

            }




            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjSamparikshadReqMasterViewModel.RequestedDateEng = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjSamparikshadReqMasterViewModel.RequestedDateNep);
            //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            model.ObjSamparikshadReqMasterViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.InsertSamparikshadReqDetail(model.ObjSamparikshadReqMasterViewModel);
            //rms.PrimaryId = 0;
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित भयो । ";
                //return RedirectToAction("ViewSamparikshadReqForm", new { @id = model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId });
                return RedirectToAction("ViewSamparikshadReqFormById", new { @id = rms.PrimaryId });
            }
            else
            {
                TempData["Error"] = "तपाँईको विवरण सुरक्षित हुन सकेन । पुन: कोसिस गर्नुहोस । ";
                return RedirectToAction("SamparikshadRequestForm");
            }

        }


        public ActionResult SendForSamparikchan(int id)
        {

            ExternalBeruju model = new ExternalBeruju();
            model.ObjSamparikshadReqMasterViewModel = new SamparikshadReqMasterViewModel();
            model.ObjSamparikshadReqMasterViewModel.SamparikshadReqMasterId = id;
            model.OfficeId = CurrentUserOfficeId;
            model.ObjSamparikshadReqMasterViewModel.RequestToId = 1;
            return View(model);

        }


        public ActionResult SamparikchanTo(int id)
        {

            ExternalBeruju model = new ExternalBeruju();
            model.ObjSamparikshadReqMasterViewModel = new SamparikshadReqMasterViewModel();
            model.ObjSamparikshadReqMasterViewModel.SamparikshadReqMasterId = id;
            model.OfficeId = CurrentUserOfficeId;

            return View(model);

        }


        [HttpPost]
        public ActionResult SendForSamparikchan(ExternalBeruju model)
        {
            SamparikshadReqMasterViewModel mdl = new SamparikshadReqMasterViewModel();
            mdl.SamparikshadReqMasterId= model.ObjSamparikshadReqMasterViewModel.SamparikshadReqMasterId;
            mdl.RequestToId = model.ObjSamparikshadReqMasterViewModel.RequestToId;
            mdl.OfficeId = CurrentUserOfficeId;
            mdl.ToWhomofficeId = 224;//Utilities.GetCurrentLoginOfficeMainOfficeId(CurrentUserOfficeId);
            mdl.RemarksForRequest = model.ObjSamparikshadReqMasterViewModel.Remarks;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.SendForSamparikchan(mdl);
            if (rms.ReturnMessage == "SUCCESS")
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित भयो । ";
                return RedirectToAction("GetSamparikshadRequestOfOffice");
            }
            else
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित हुन सकेन । पुन: कोसिस गर्नुहोस । ";
                return RedirectToAction("SendForSamparikchan", new { @id = mdl.SamparikshadReqMasterId });
            }



        }

        public ActionResult EditSamparikshadRequestForm(int id, int id1)    //id = ExternalBeruju beruju id, id1 samparikchen req id
        {
         
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id);
            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmountForRequest(id);

            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            ViewBag.Mode = "Create";
            model.ObjSamparikshadReqMasterViewModel = new SamparikshadReqMasterViewModel();
            int IfInserted = IBS.CheckIfAlreadyRequestedForSamparikshad(CurrentUserOfficeId, id);

            if (IfInserted > 0)
            {
                ViewBag.Mode = "Edit";
                model.ObjSamparikshadReqMasterViewModel = IBS.SPGetSamparikshadRequestDetailByPrimaryId(id, id1);
                model.ObjSamparikshadReqMasterViewModel.RequestedDateNep = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjSamparikshadReqMasterViewModel.RequestedDateEng);
                model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetailsForRequest(id, CurrentUserOfficeId, id1);

            }

            else
            {

                model.ObjSamparikshadReqMasterViewModel.ExternalBerujuId = id;
                //model.ObjSamparikshadReqMasterViewModel.TotalAmount = model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt;
                model.ObjSamparikshadReqMasterViewModel.BerujuDafaNumber = model.ExternalBerujuForSamparikshadVMObj.BerujuNumber;
                model.ObjSamparikshadReqMasterViewModel.BerujuShortDescription = model.ExternalBerujuForSamparikshadVMObj.BerujuShorDesc;
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);

            }
            model.OfficeId = CurrentUserOfficeId;
            
            return View(model);

        }

        [HttpPost]
        public ActionResult EditSamparikshadRequestForm(ExternalBeruju model)
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
            foreach (var item in model.SamparikshadTowhomDetailVMListMain)
            {
                string concateletter = "S-R-" + item.SMTowhomDetailId + item.ExternalBerujuId;
                string requestFile = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                if (string.IsNullOrEmpty(FileNameVal) == false)
                {
                    item.UploadedFileUrl = Path.GetFileName(concateletter + "_" + item.SupportingDocFiles.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), item.UploadedFileUrl);
                    item.SupportingDocFiles.SaveAs(path);
                }
            }


            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjSamparikshadReqMasterViewModel.RequestedDateEng = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjSamparikshadReqMasterViewModel.RequestedDateNep);
            //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            model.ObjSamparikshadReqMasterViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.UpdateSamparikshadReqDetail(model.ObjSamparikshadReqMasterViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित भयो । ";
                return RedirectToAction("ViewSamparikshadReqFormById", new { @id = model.ObjSamparikshadReqMasterViewModel.SamparikshadReqMasterId });
            }
            else
            {
                TempData["Success"] = "तपाँईको विवरण सुरक्षित हुन सकेन । पुन: कोसिस गर्नुहोस । ";
                return RedirectToAction("GetSamparikshadRequestList");
            }

        }





        public ActionResult ViewSamparikshadReqForm(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model.ObjSamparikshadRequestMaterDetailVM = IBS.SPGetSamparikshadRequestletter(CurrentUserOfficeId, id);
            model.GetsamparikshadrequesttowhomforletterViewModelList = new List<GetsamparikshadrequesttowhomforletterViewModel>();
            //get samparikshadreqprimaryid by externalberujuid

            model.GetsamparikshadrequesttowhomforletterViewModelList = IBS.GetsamparikshadrequesttowhomforletterListForLetter(id).ToList();
            return View(model);



        }

        public ActionResult ViewSamparikshadReqFormById(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model.ObjSamparikshadRequestMaterDetailVM = IBS.SPGetSamparikshadRequestletterByPrimaryId(CurrentUserOfficeId, id);
            model.GetsamparikshadrequesttowhomforletterViewModelList = IBS.GetsamparikshadrequesttowhomforletterListForLetter(id).ToList();
            return View(model);
        }





        //this delete from samparikshad request list
        public ActionResult DeleteSamparikshadRequestForm(int id)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.SPDeleteSamparikshadReqMasterDetail(CurrentUserOfficeId, id);
            TempData["Success"] = "विवरण सिस्टममा हटाईयो । ";
            return RedirectToAction("GetSamparikshadRequestList");
        }


        #endregion Samparikshad RequestForm




        #region Samparikshad Work
        public ActionResult GetSamparikshadList()
        {
            ExternalBeruju model = new ExternalBeruju();
            model.SamparikhadListViewModelList = new List<SamparikhadListViewModel>();
            model.SamparikhadListViewModelList = IBS.GetExternalSamparikshadList(CurrentUserOfficeId);
            return View(model);
        }


        [HttpPost]
        public ActionResult GetSamparikshadForList(ExternalBeruju model)
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

                model.SamparikhadListViewModelForReportList = new List<SamparikhadListViewModelForReport>();
                if (model.FiscalYearId > 0)
                {
                    model.SamparikhadListViewModelForReportList = IBS.GetExternalSamparikshadListForReport(CurrentUserOfficeId, model.FiscalYearId, model.KoshTypeId);

                }
                else
                {
                    model.SamparikhadListViewModelForReportList = IBS.GetExternalSamparikshadListForReport(CurrentUserOfficeId, 0, model.KoshTypeId);

                }
                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.SamparikhadListViewModelForReportList = model.SamparikhadListViewModelForReportList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                return PartialView("_GetSamparikshadForList", model);

            }

        }


        public ActionResult MakeSamparikshad(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id);

            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id);
            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel.ExternalBerujuId = id;
            //Get Malepa or kumarichowk Details Id
            model.ObjExternalSamparikshadViewModel.MalepaOrKumariChowkId = 1;
            model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
            model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);
            model.ObjExternalSamparikshadViewModel.ExternalBerujuId = id;
            model.ExternalBerujuId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeSamparikshad(ExternalBeruju model)
        {
            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम ० भन्दा धेरै हुनुपर्दछ ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);

                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel.ExternalBerujuId = model.ObjExternalSamparikshadViewModel.ExternalBerujuId;
                model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
                model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
                return View(model);

            }


            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount > model.ExternalBerujuForSamparikshadVMObj.RemainingAmount)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);

                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
                //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
                model.ObjExternalSamparikshadViewModel.ExternalBerujuId = model.ObjExternalSamparikshadViewModel.ExternalBerujuId;
                return View(model);

            }

            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjExternalSamparikshadViewModel.UploadFileDetails);
                model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
            model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();

            //update req --change status samparikshad done

            rms = IBS.InsertSamparikshadDetail(model.ObjExternalSamparikshadViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                //return RedirectToAction("SamparikshadForm");
                return RedirectToAction("ListActionFromRequeestBeruju");


            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }



        public ActionResult EditSamparikshadForm(int id, int id1)
        {
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id1);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id1);
            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id1);
            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel = IBS.GetExternalSamparikshadListByPrimaryId(id, CurrentUserOfficeId);
            model.ObjExternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjExternalSamparikshadViewModel.RevisedDate);
            model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
            model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id1, CurrentUserOfficeId, id);
            return View(model);


        }

        [HttpPost]
        public ActionResult EditSamparikshadForm(ExternalBeruju model)
        {
            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount > model.ExternalBerujuForSamparikshadVMObj.VoucharAmunt)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel = IBS.GetExternalSamparikshadListByPrimaryId(model.ObjExternalSamparikshadViewModel.SamparishadId, CurrentUserOfficeId);
                model.ObjExternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjExternalSamparikshadViewModel.RevisedDate);
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(model.ObjExternalSamparikshadViewModel.ExternalBerujuId, CurrentUserOfficeId, model.ObjExternalSamparikshadViewModel.SamparishadId);


            }
            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम ० भन्दा धेरै हुनुपर्दछ ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel = IBS.GetExternalSamparikshadListByPrimaryId(model.ObjExternalSamparikshadViewModel.SamparishadId, CurrentUserOfficeId);
                model.ObjExternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjExternalSamparikshadViewModel.RevisedDate);
                model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
                model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(model.ObjExternalSamparikshadViewModel.ExternalBerujuId, CurrentUserOfficeId, model.ObjExternalSamparikshadViewModel.SamparishadId);
                return View(model);

            }



            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjExternalSamparikshadViewModel.UploadFileDetails);
                model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else if (string.IsNullOrEmpty(model.ObjExternalSamparikshadViewModel.UploadFileDetails))
            {
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            else
            {
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = model.ObjExternalSamparikshadViewModel.UploadFileDetails;
            }

            model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
            model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
            rms = IBS.UpdateSamparikshadDetail(model.ObjExternalSamparikshadViewModel);

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
            rms = IBS.DeleteSamparikshadDetail(id);
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




        public ActionResult DeleteBerujuAndSampa(int id)
        {
            ReturnMessageViewModel delmsg = new ReturnMessageViewModel();
            SamparikshadDataIdModel rms = new SamparikshadDataIdModel();
            rms = IBS.GetSamparikshadDataByExternalBerujuId(id);

            string message = "";

            // Delete External Beruju
            if (rms.ExternalBerujuId > 0)
            {
                delmsg = IBS.DeleteExternalBeruju(rms.ExternalBerujuId);

                if (delmsg.ReturnMessage == "Deleted Successfully")
                {
                    message += "Beruju Deleted Successfully. ";
                }
            }

            // Delete Samparikshad Req Master
            if (rms.SamparikshadReqMasterId > 0)
            {
                delmsg = IBS.SPDeleteSamparikshadReqMasterDetail(
                    CurrentUserOfficeId,
                    rms.SamparikshadReqMasterId
                );

                if (delmsg.ReturnMessage == "Deleted Successfully")
                {
                    message += "Samparikshad Request Deleted Successfully. ";
                }
            }

            // Delete Samparishad Detail
            if (rms.SamparishadId > 0)
            {
                delmsg = IBS.DeleteSamparikshadDetail(rms.SamparishadId);

                if (delmsg.ReturnMessage == "Deleted Successfully")
                {
                    message += "Samparishad Detail Deleted Successfully.";
                }
            }

            // If nothing deleted
            if (string.IsNullOrEmpty(message))
            {
                message = "No Record Found.";
            }

            TempData["Success"] = message;

            return RedirectToAction("ParameterFilterReport", "ReportLL");
        }
        #endregion Samparikshad Work



        [HttpPost]
        public ActionResult ToWhomeDetailsList()
        {
            InternalBeruju ib = new InternalBeruju();

            ib.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();

            return PartialView("ToWhomeDetailsList");
        }


        //[HttpPost]
        //public ActionResult ToWhomeDetailsListForRequest()
        //{
        //    InternalBeruju ib = new InternalBeruju();

        //    ib.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();

        //    return PartialView("ToWhomeDetailsList");
        //}


        public ActionResult CreateSamparikshad(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);

            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel.ExternalBerujuId = id;
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateSamparikshad(ExternalBeruju model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjExternalSamparikshadViewModel.UploadFileDetails);
                model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            rms = IBS.InsertSamparikshadDetail(model.ObjExternalSamparikshadViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("GetSamparikshadList");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult ViewDetailSamparikshadForm(int id, int id1)
        {
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id1);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id1);
            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id1);
            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel = IBS.GetExternalSamparikshadListByPrimaryId(id, CurrentUserOfficeId);
            model.ObjExternalSamparikshadViewModel.RevisedDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ObjExternalSamparikshadViewModel.RevisedDate);
            model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
            model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id1, CurrentUserOfficeId, id);
            return View(model);
        }


        public ActionResult GetSamparikshadRequestFromOffice()
        {
            ExternalBeruju model = new ExternalBeruju();
            model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
            model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestFromOffice(CurrentUserOfficeId, 0, 1);
            ViewBag.OfficeType = functions.GetCurrentLoginUserOfficeTypeId();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult GetSamparikshadRequestFromOffice(ExternalBeruju model)
        {

            model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
            if (model.FiscalYearId > 0)
            {
                model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestFromOffice(CurrentUserOfficeId, model.FiscalYearId, model.KoshTypeId);

            }
            else
            {
                model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestFromOffice(CurrentUserOfficeId, 0, model.KoshTypeId);

            }
            if (!string.IsNullOrEmpty(model.BerujuNumber))
            {
                model.BerujuNumber = model.BerujuNumber.Trim();
                model.SamparikhadRequestListViewModelForReportList = model.SamparikhadRequestListViewModelForReportList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

            }
        

            return PartialView("_GetSamparikshadRequestFromOffice", model);

        }



        public ActionResult GetSamparikshadRequestOfOffice()
        {
            ExternalBeruju model = new ExternalBeruju();
            model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
            model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestOfOffice(CurrentUserOfficeId, 0, 1);
            ViewBag.OfficeType = functions.GetCurrentLoginUserOfficeTypeId();

            return View(model);
        }

        [HttpPost]
        public ActionResult GetSamparikshadRequestOfOffice(ExternalBeruju model)
        {

            model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
            if (model.FiscalYearId > 0)
            {
                model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestOfOffice(CurrentUserOfficeId, model.FiscalYearId, model.KoshTypeId);

            }
            else
            {
                model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestOfOffice(CurrentUserOfficeId, 0, model.KoshTypeId);

            }
            if (!string.IsNullOrEmpty(model.BerujuNumber))
            {
                model.BerujuNumber = model.BerujuNumber.Trim();
                model.SamparikhadRequestListViewModelForReportList = model.SamparikhadRequestListViewModelForReportList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

            }


            return PartialView("_GetSamparikshadRequestOfOffice", model);

        }







        public ActionResult GetSamparikshadRequestList()
        {
            ExternalBeruju model = new ExternalBeruju();
            model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
            model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestListForReport(CurrentUserOfficeId, 0, 1);
            return View(model);
        }


        [HttpPost]
        public ActionResult GetSamparikshadRequestList(ExternalBeruju model)
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

                model.SamparikhadRequestListViewModelForReportList = new List<SamparikhadRequestListViewModelForReport>();
                if (model.FiscalYearId > 0)
                {
                    model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestListForReport(CurrentUserOfficeId, model.FiscalYearId, model.KoshTypeId);

                }
                else
                {
                    model.SamparikhadRequestListViewModelForReportList = IBS.GetSamparikshadRequestListForReport(CurrentUserOfficeId, 0, model.KoshTypeId);

                }
                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.SamparikhadRequestListViewModelForReportList = model.SamparikhadRequestListViewModelForReportList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                return PartialView("_GetSamparikshadRequestListForList", model);

            }

        }



        public ActionResult ListSaidantikBeruju(int id)
        {
            ExternalBeruju model = new ExternalBeruju();
            return View(model);
        }


        [HttpPost]
        public ActionResult ListSaidantikBeruju(ExternalBeruju model)
        {


            model.SaidantikBerujuList = new List<SaidantikBeruju>();

            if (model.FiscalYearId > 0)
            {
                model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, model.KoshTypeId).Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

            }
            else
            {
                model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, model.KoshTypeId).ToList();


            }

            if (!string.IsNullOrEmpty(model.BerujuNumber))
            {
                model.BerujuNumber = model.BerujuNumber.Trim();
                model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.BerujuDafaNumber.Contains(model.BerujuNumber)).ToList();

            }

            return PartialView("_GetSaidantikBerujuForList", model);

        }

      


        public ActionResult MakeSamparikshadFromRequest(int id, int id1)//externalberujuid, requsamprmasterid
        {
            ExternalBeruju model = new ExternalBeruju();
            //model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
            model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(id);

            model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(id);
            //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
            model.ObjExternalSamparikshadViewModel.ExternalBerujuId = id;
            //Get Malepa or kumarichowk Details Id
            model.ObjExternalSamparikshadViewModel.MalepaOrKumariChowkId = 1;
            model.SamparikshadTowhomDetailVMListMain = new List<SamparikshadTowhomDetailVM>();
            model.SamparikshadTowhomDetailVMListMain = IBS.ListSamparikshadTowhomDetails(id, CurrentUserOfficeId, 0);
            model.ObjExternalSamparikshadViewModel.ExternalBerujuId = id;
            model.ExternalBerujuId = id;
            model.ObjExternalSamparikshadViewModel.SamparikshadReqMasterId = id1;
            return View(model);
        }





        [HttpPost]
        public ActionResult MakeSamparikshadFromRequest(ExternalBeruju model)
        {
            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount <= 0)
            {
                ViewBag.ErrorMessage = "बेरुजु रकम ० भन्दा धेरै हुनुपर्दछ ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);

                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel.ExternalBerujuId = model.ObjExternalSamparikshadViewModel.ExternalBerujuId;
                model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
                model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
                return View(model);

            }


            if (model.ObjExternalSamparikshadViewModel.ReviesedVoucherAmount > model.ExternalBerujuForSamparikshadVMObj.RemainingAmount)
            {
                ViewBag.ErrorMessage = "सम्परिक्षण रकम बेरुजु रकम भन्दा धेरै भयो ।";
                model.ExternalBerujuForSamparikshadVMObj = new ExternalBerujuForSamparikshadVM();
                model.ExternalBerujuForSamparikshadVMObj = IBS.GetExternalBerujuDetailForSamparikshad(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);

                model.ExternalBerujuForSamparikshadVMObj.RemainingAmount = IBS.GetSamparikshadRemainingAmount(model.ObjExternalSamparikshadViewModel.ExternalBerujuId);
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                //model.ObjExternalSamparikshadViewModel = new ExternalSamparikshadViewModel();
                model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
                model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
                //model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();
                model.ObjExternalSamparikshadViewModel.ExternalBerujuId = model.ObjExternalSamparikshadViewModel.ExternalBerujuId;
                return View(model);

            }

            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            string FileNameVal = model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType == null ? string.Empty : model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = Path.GetFileName(PrifixLetter + "_" + model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjExternalSamparikshadViewModel.UploadFileDetails);
                model.ObjExternalSamparikshadViewModel.UploadFileDetailsFileType.SaveAs(path);
            }

            else
            {
                model.ObjExternalSamparikshadViewModel.UploadFileDetails = string.Empty;
            }

            model.ObjExternalSamparikshadViewModel.OfficeId = CurrentUserOfficeId;
            model.ObjExternalSamparikshadViewModel.RevisedDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(model.ObjExternalSamparikshadViewModel.RevisedDateStr);
            model.ObjExternalSamparikshadViewModel.SamparikshadTowhomDetailVMList = model.SamparikshadTowhomDetailVMListMain.ToList();

            rms = IBS.InsertSamparikshadDetail(model.ObjExternalSamparikshadViewModel);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("ListActionFromRequeestBeruju");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }







    }
}
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    public class CheckerController : Controller
    {
        // GET: LocalLevel/Checker

        InternalBerujuService IBS = new InternalBerujuService();
        int CurrentUserOfficeId = 0;
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();

        public ActionResult OfficeWiseSaidantikBerujuEntry(int id)
        {
            BerujuCheckerReportFilter model = new BerujuCheckerReportFilter();
            model.BerujuList = new List<OfficeBerujuDTO>();
           if (id == 0 || id == 1)
            {
                model.BerujuStatus = id == 0 ? false : true;
                model.BerujuList = IBS.GetOfficeWiseSadantikBeruju(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult OfficeWiseSaidantikBerujuReport(BerujuCheckerReportFilter filter)
        {

            filter.OfficeId = GetSelectedOfficeId(filter);

            filter.BerujuList = IBS.GetOfficeWiseSadantikBeruju(filter);

            return PartialView("_OffceiWiseBerujuList", filter);

        }

        public ActionResult OfficeWiseBerujuEntry(int id)
        {  
            BerujuCheckerReportFilter model = new BerujuCheckerReportFilter();
            model.BerujuList = new List<OfficeBerujuDTO>();
            if(id==0 || id == 1)
            {
                model.BerujuStatus = id == 0 ? false : true;
                model.BerujuList = IBS.GetOfficeWiseBeruju(model);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult OfficeWiseBerujuReport(BerujuCheckerReportFilter filter)
        {

            filter.OfficeId = GetSelectedOfficeId(filter);

            filter.BerujuList = IBS.GetOfficeWiseBeruju(filter);

            return PartialView("_OffceiWiseBerujuList", filter);

        }

        public ActionResult ListExternalBeruju(int id)
        {

            int selectedOfficeId = id;

            ExternalBeruju model = new ExternalBeruju();
            model.ExternalBerujuList = IBS.ListMakerEntryBeruju(false).Where(x => x.OfficeId == selectedOfficeId).ToList();
            model.OfficeId = selectedOfficeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetExternalBerujuForList(ExternalBeruju model)
        {

                int selectedOfficeId = model.OfficeId;

       
        
                model.ExternalBerujuList = new List<ExternalBeruju>();

                model.ExternalBerujuList = IBS.ListMakerEntryBeruju(false).Where(x => x.OfficeId == selectedOfficeId).ToList();
                
                

                if (model.FiscalYearId > 0)
                {
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

                }
             

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetExternalBerujuForList", model);
            
        }

        public ActionResult ListSaidantikBeruju(int id)
        {
            int selectedOfficeId = id;
            SaidantikBeruju model = new SaidantikBeruju();
            model.SaidantikBerujuList = IBS.ListMakerSaidantikEntryBeruju(false).Where(x => x.OfficeId == selectedOfficeId).ToList();
            model.OfficeId = selectedOfficeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetSaidantikBerujuForList(SaidantikBeruju model)
        {

            int selectedOfficeId = model.OfficeId;



            model.SaidantikBerujuList = new List<SaidantikBeruju>();

            model.SaidantikBerujuList = IBS.ListMakerSaidantikEntryBeruju(false).Where(x => x.OfficeId == selectedOfficeId).ToList();



            if (model.FiscalYearId > 0)
            {
                model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

            }


            if (!string.IsNullOrEmpty(model.BerujuDafaNumber))
            {
                model.BerujuDafaNumber = model.BerujuDafaNumber.Trim();
                model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.BerujuDafaNumber.Contains(model.BerujuDafaNumber)).ToList();

            }

          //  model.kosh = model.KoshTypeId;
            return PartialView("_GetSaidantikBerujuForList", model);

        }


        private int? GetSelectedOfficeId(BerujuCheckerReportFilter filter)
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



        public ActionResult ListEntryApprovedBeruju(int id)
        {

            int selectedOfficeId = id;
            ExternalBeruju model = new ExternalBeruju();
            model.ExternalBerujuList = IBS.ListMakerEntryBeruju(true).Where(x => x.OfficeId == selectedOfficeId).ToList();
            model.OfficeId = selectedOfficeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetEntryApprovedBeruju(ExternalBeruju model)
        {
            CurrentUserOfficeId = model.OfficeId;

         
                model.ExternalBerujuList = new List<ExternalBeruju>();
                model.ExternalBerujuList = IBS.ListMakerEntryBeruju(true).ToList();
        
                if (model.OfficeId > 0)
                {
                    model.ExternalBerujuList = IBS.SPListExternalBerujuByKoshTypeId(CurrentUserOfficeId, model.KoshTypeId).Where(x => x.BerujuStatus == true).ToList();

                }

               
                if (model.FiscalYearId > 0)
                {
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

                }

                if (!string.IsNullOrEmpty(model.BerujuNumber))
                {
                    model.BerujuNumber = model.BerujuNumber.Trim();
                    model.ExternalBerujuList = model.ExternalBerujuList.Where(x => x.BerujuNumber.Contains(model.BerujuNumber)).ToList();

                }

                model.KoshTypeId = model.KoshTypeId;
                return PartialView("_GetAprovedExternalBerujuForList", model);
            
        }



        public ActionResult ListSaidantikApprovedBeruju(int id)
        {
            int selectedOfficeId = id;
            SaidantikBeruju model = new SaidantikBeruju();
            model.SaidantikBerujuList = IBS.ListMakerSaidantikEntryBeruju(true).Where(x => x.OfficeId == selectedOfficeId).ToList();
            model.OfficeId = selectedOfficeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetApprovedSaidantikBerujuForList(SaidantikBeruju model)
        {

            int selectedOfficeId = model.OfficeId;



            model.SaidantikBerujuList = new List<SaidantikBeruju>();

            model.SaidantikBerujuList = IBS.ListMakerSaidantikEntryBeruju(false).Where(x => x.OfficeId == selectedOfficeId).ToList();



            if (model.FiscalYearId > 0)
            {
                model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.FiscalYearId == model.FiscalYearId).ToList();

            }


            if (!string.IsNullOrEmpty(model.BerujuDafaNumber))
            {
                model.BerujuDafaNumber = model.BerujuDafaNumber.Trim();
                model.SaidantikBerujuList = model.SaidantikBerujuList.Where(x => x.BerujuDafaNumber.Contains(model.BerujuDafaNumber)).ToList();

            }

            //  model.kosh = model.KoshTypeId;
            return PartialView("_GetApprovedSaidantikBerujuForList", model);

        }





        public ActionResult Edit(int id,int id2)
        {
            CurrentUserOfficeId = id2;
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.FromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.FromDate);
            model.ToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ToDate);
            model.AccountantFromDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantFromDate);
            model.AccountantToDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.AccountantToDate);
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);
            ViewBag.Mode = "Edit"; ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            ViewBag.Kosh = _4pix_Beruju.Utilities.GetKoshTypeToFDD();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);
            model.OfficeId = id2;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ExternalBeruju model)
        {
            CurrentUserOfficeId = model.OfficeId;

            bool hasOldFile = !string.IsNullOrEmpty(model.UploadedFileUrl);
            bool hasNewFile = model.SupportingDocFiles != null && model.SupportingDocFiles.ContentLength > 0;
           
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

            string FileNameVal = model.SupportingDocFiles == null ? string.Empty : model.SupportingDocFiles.FileName;
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

            model.BerujuStatus = true;   /// for data entry user;
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
            rms = IBS.UpdateExternalBerujuByChecker(model);

            if (rms.ReturnMessage == "Updated Successfully")
            {
                //TempData["Success"] = "विवरण परिवर्तन भयो । ";
                //return RedirectToAction("OfficeWiseBerujuEntry", new {id=0});
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return View("CloseTab");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }



        [HttpPost]
        public ActionResult ToWhomeDetailsList()
        {
            InternalBeruju ib = new InternalBeruju();

            ib.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();

            return PartialView("ToWhomeDetailsList");
        }



        public ActionResult ViewDetails(int id, int id2)
        {
            CurrentUserOfficeId = id2;
            ExternalBeruju model = new ExternalBeruju();
            model = IBS.ListExternalBerujuByPrimaryId(CurrentUserOfficeId, id);
     
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);

            return View(model);
        }

        public ActionResult Delete(int id, int id2)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.DeleteExternalBerujuFromChecker(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट आन्तरिक बेरुजुको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }
            return RedirectToAction("ListExternalBeruju", new {@id=id2});
        }


        // sadantik

        public ActionResult EditSaidantik(int id, int id1, int id3)//primaryid, internalorexternal
        {
            CurrentUserOfficeId = id3;
            SaidantikBeruju model = new SaidantikBeruju();
            model = IBS.GetSaidantikBerujuByPrimaryId(CurrentUserOfficeId, id);
            ViewBag.Mode = "Edit";
            model.InternalOrExternal = id1;
            model.SaidantikBerujuTopFiveList = new List<SaidantikBeruju>();
            //model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();
            model.InternalOrExternal = id1;
            model.OfficeId = id3;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSaidantik(SaidantikBeruju model)
        {

            CurrentUserOfficeId=model.OfficeId;
            bool hasOldFile = !string.IsNullOrEmpty(model.SaidantikDoc);
            bool hasNewFile = model.UploadSaidantikDocFileType != null && model.UploadSaidantikDocFileType.ContentLength > 0;
            string FileNameVal = model.UploadSaidantikDocFileType == null ? string.Empty : model.UploadSaidantikDocFileType.FileName;
            //model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();

            if (!hasOldFile && !hasNewFile)
            {
                ViewBag.Mode = "Edit";
                ViewBag.ErrorMessage = "कृपया फाईल राख्नुहोस् ।";
                return View(model);
            }


           
            model.BerujuStatus = true;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            //string FileNameVal = model.UploadSaidantikDocFileType == null ? string.Empty : model.UploadSaidantikDocFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.SaidantikDoc = Path.GetFileName(PrifixLetter + "_" + model.UploadSaidantikDocFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.SaidantikDoc);
                model.UploadSaidantikDocFileType.SaveAs(path);
            }

            else
            {
                if (string.IsNullOrEmpty(model.SaidantikDoc))
                {
                    model.SaidantikDoc = string.Empty;
                }
                else
                {
                    model.SaidantikDoc = model.SaidantikDoc;
                }
            }

            model.BerujuStatus = true;
            model.CreatedBy = CurrentLoginUserId.ToString();

            rms = IBS.UpdpateSaidantikBerujuFromChecker(model);

            if (rms.ReturnMessage == "Updated Successfully")
            {
             

                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return View("CloseTab");
                //return RedirectToAction("OfficeWiseSaidantikBerujuEntry", new { id = 0 });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

        }

        public ActionResult DeleteSaidantik(int id, int id2)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = IBS.DeleteSaidantikBeruju(id);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "सिस्टम बाट सैदान्तिक बेरुजुको विवरण हटाईयो । ";
            }
            else
            {
                TempData["Error"] = rms.ReturnMessage.ToString();
            }

            return RedirectToAction("ListSaidantikBeruju", new { @id = id2 });
        }


        public ActionResult ViewSaidantikDetails(int id, int id1, int id3)
        {
            SaidantikBeruju model = new SaidantikBeruju();
            model = IBS.GetSaidantikBerujuByPrimaryId(id3, id);
            model.InternalOrExternal = id1;
            return View(model);
        }
    }
}
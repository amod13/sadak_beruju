using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    public class TransferController : Controller
    {
        // GET: LocalLevel/Transfer
        InternalBerujuService IBS = new InternalBerujuService();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        public ActionResult Index()
        {
            BerujuCheckerReportFilter model = new BerujuCheckerReportFilter();
            return View(model);

        }


        [HttpPost]
        public ActionResult ListExternalBeruju(BerujuCheckerReportFilter model)
        {

            model.OfficeId = GetSelectedOfficeId(model);
            int CurrentUserOfficeId = model.OfficeId??0;

            //filter.BerujuList = IBS.GetOfficeWiseBeruju(filter);


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
            ExternalBeruju datamodel  = new ExternalBeruju();
            datamodel.ExternalBerujuList = model.ExternalBerujuList;
            return PartialView("_GetExternalBerujuForList", datamodel);

        }


        public ActionResult ViewDetails(int id,int id2)
        {
            BerujuCheckerReportFilter model = new BerujuCheckerReportFilter();
            model.ExternalBeruju = IBS.ListExternalBerujuByPrimaryId(id2, id);
            model.ExternalBeruju.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.ExternalBeruju.VoucharDate);
            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ExternalBeruju.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ExternalBeruju.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);
            return View(model);
        }


        [HttpPost]
        public ActionResult BerujuTransfer(BerujuCheckerReportFilter model)
        {
            var officeId= GetSelectedOfficeId(model)??0;
            if (officeId>0 && model.ExternalBeruju.ExternalBerujuId>0)
            {
                model.TransferOfficeId = officeId;
                model.CreatedBy = CurrentLoginUserId.ToString();

                var rms = IBS.TransferBeruju(model);

                if ( rms.ReturnMessage == "Transfer Successful")
                {
                    string message;
                    if (model.Status == 1)
                    {
                        message = "Merged Successfuly";
                    }
                    else
                    {
                        message = "Error Corrected Successfuly";
                    }

                        TempData["Success"] = message;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                    //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                    //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                    return View(model);
                }

            }
            return PartialView("_GetExternalBerujuForList", model);

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





    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using PagedList;
namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class SaidantikBerujuController : Controller
    {
        InternalBerujuService IBS = new InternalBerujuService();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        // GET: LocalLevel/SaidantikBeruju

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(int id)//Internal or external
        {
            SaidantikBeruju model = new SaidantikBeruju();
            model.SaidantikBerujuList = new List<SaidantikBeruju>();
            model.SaidantikBerujuList = IBS.ListSaidantikBeruju(CurrentUserOfficeId, id).ToList();
            model.InternalOrExternal = id;
            return View(model);
        }



        public ActionResult GetSaidaintikList(int? page)//Internal or external
        {
            int PageSized = 2;
            int PageIndex = 1;
            PageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int id = 1;
            IPagedList<SaidantikBeruju> pagelistsaidaintik = null;
            SaidantikBeruju model = new SaidantikBeruju();
            model.SaidantikBerujuList = new List<SaidantikBeruju>();

            model = IBS.spGetSaindantikberujupagination(PageIndex, PageSized, Convert.ToInt32(id));
            model.InternalOrExternal = Convert.ToInt32(id);
            pagelistsaidaintik = IBS.ListSaidantikBeruju(CurrentUserOfficeId, id).ToList().ToPagedList(PageIndex, PageSized);
            return View(pagelistsaidaintik);
        }


        public ActionResult GetSaidantikBerujuPagedList(int? page)
        {
            int PageSized = 2;
            int PageIndex = 0;
            PageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int id = 1;
            SaidantikBeruju model = new SaidantikBeruju();
            model.SaidantikBerujuList = new List<SaidantikBeruju>();
            model = IBS.spGetSaindantikberujupagination(PageIndex, PageSized, Convert.ToInt32(id));
            model.InternalOrExternal = Convert.ToInt32(id);
            model.CurrentPage = PageIndex;
            //model.PageCount = Convert.ToInt32(Math.Ceiling(model.SaidantikBerujuList.Count() / (double)PageSized));
            model.PageCount = Convert.ToInt32(Math.Ceiling(model.PageCount / (double)PageSized));
            return View(model);
        }


        public ActionResult GetSaidantikBerujuList()
        {
            SaidantikBeruju model = new SaidantikBeruju();
            return View(model);

        }
        [HttpPost]
        public ActionResult GetSaidantikBerujuList(SaidantikBeruju model)
        {

            return View(model);

        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Create(int id)
        {
            ViewBag.Mode = "Create";
            SaidantikBeruju model = new SaidantikBeruju();
            model.InternalOrExternal = id;
            model.SaidantikBerujuTopFiveList = new List<SaidantikBeruju>();
            model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();
            return View(model);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SaidantikBeruju model)
        {
            model.OfficeId = CurrentUserOfficeId;
            model.BerujuStatus = false;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            string FileNameVal = model.UploadSaidantikDocFileType == null ? string.Empty : model.UploadSaidantikDocFileType.FileName;
            if (string.IsNullOrEmpty(FileNameVal) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.SaidantikDoc = Path.GetFileName(PrifixLetter + "_" + model.UploadSaidantikDocFileType.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.SaidantikDoc);
                model.UploadSaidantikDocFileType.SaveAs(path);
            }

            else
            {
                model.SaidantikDoc = string.Empty;
            }

            rms = IBS.InsertSaidantikBeruju(model);
            //rms.PrimaryId = 12;

            



            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("Create", new { @id = model.InternalOrExternal });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                model.SaidantikBerujuTopFiveList = new List<SaidantikBeruju>();
                model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();
                return View(model);
            }

        }

        public ActionResult Edit(int id, int id1)//primaryid, internalorexternal
        {
            SaidantikBeruju model = new SaidantikBeruju();
            model = IBS.GetSaidantikBerujuByPrimaryId(CurrentUserOfficeId, id);
            ViewBag.Mode = "Edit";
            model.InternalOrExternal = id1;
            model.SaidantikBerujuTopFiveList = new List<SaidantikBeruju>();
            model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();
            model.InternalOrExternal = id1;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SaidantikBeruju model)
        {

            bool hasOldFile = !string.IsNullOrEmpty(model.SaidantikDoc);
            bool hasNewFile = model.UploadSaidantikDocFileType != null && model.UploadSaidantikDocFileType.ContentLength > 0;
            string FileNameVal = model.UploadSaidantikDocFileType == null ? string.Empty : model.UploadSaidantikDocFileType.FileName;
            model.SaidantikBerujuTopFiveList = IBS.ListSaidantikBerujuTopFive(CurrentUserOfficeId, model.InternalOrExternal).ToList();

            if (!hasOldFile && !hasNewFile)
            {
                ViewBag.Mode = "Edit";
                ViewBag.ErrorMessage = "कृपया फाईल राख्नुहोस् ।";
                return View(model);
            }


            model.OfficeId = CurrentUserOfficeId;
         
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            //FileNameVal = model.UploadSaidantikDocFileType == null ? string.Empty : model.UploadSaidantikDocFileType.FileName;
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

            model.BerujuStatus = false;
            rms = IBS.UpdpateSaidantikBeruju(model);

            if (rms.ReturnMessage == "Updated Successfully")
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("Index", new { @id = model.InternalOrExternal });
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                //model.InternalBerujuTopFiveList = new List<InternalBeruju>();
                //model.InternalBerujuTopFiveList = IBS.ListInternalBerujuTopFive(CurrentUserOfficeId, model.KoshTypeId).ToList();
                return View(model);
            }

        }

        public ActionResult Delete(int id, int id1)
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

            return RedirectToAction("Index", new { @id = id1 });
        }


        public ActionResult ViewDetails(int id, int id1)
        {
            SaidantikBeruju model = new SaidantikBeruju();
            model = IBS.GetSaidantikBerujuByPrimaryId(CurrentUserOfficeId, id);
            model.InternalOrExternal = id1;
            return View(model);
        }



    }
}
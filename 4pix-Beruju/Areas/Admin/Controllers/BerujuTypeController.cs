using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
namespace _4pix_Beruju.Areas.Admin.Controllers
{
    [Authorize]
    public class BerujuTypeController : Controller
    {

        CommonService cs = new CommonService();
        // GET: Admin/BerujuType
        public ActionResult Index()
        {
            BerujuType model = new BerujuType();
            model.BerujuTypeList = cs.GetBerujuTypeList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new BerujuType());
        }
        [HttpPost]
        public ActionResult Create(BerujuType model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            //rms = cs.InsertBerujuType(model);
            rms.PrimaryId = 66;
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }
        public ActionResult Edit(int id)
        {
            BerujuType model = new BerujuType();
            model = cs.GetBerujuTypeList().SingleOrDefault(x => x.BerujuTypeId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BerujuType model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.UpdateBerujuType(model);
            return View(model);
        }

    }
}
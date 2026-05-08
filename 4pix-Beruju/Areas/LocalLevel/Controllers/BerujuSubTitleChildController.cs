using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{

    public class BerujuSubTitleChildController : Controller
    {
        CommonService cs = new CommonService();
        // GET: Admin/BudgetSubTitle
        public ActionResult Index()
        {
            BerujuSubTypeChild model = new BerujuSubTypeChild();
            model.BerujuSubTypeChildList = cs.GetBerujuSubTypeChildList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new BerujuSubTypeChild());
        }

        [HttpPost]
        public ActionResult Create(BerujuSubTypeChild model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.InsertBerujuSubTypeChild(model);
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
            BerujuSubTypeChild model = new BerujuSubTypeChild();
            model = cs.GetBerujuSubTypeChildList().SingleOrDefault(x => x.BerujuSubTitleChildId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BerujuSubTypeChild model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.UpdateBerujuSubTypeChild(model);
            return View(model);
        }


        public ActionResult Delete(int id)
        {


            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.DeleteBerujuTSubypeChild(id);

            TempData["Success"] = rms.ReturnMessage;
            return RedirectToAction("Index");

        }

    }
}
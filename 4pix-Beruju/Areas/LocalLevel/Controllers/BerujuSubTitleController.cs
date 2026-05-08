using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;
using _4pix_Beruju.Services;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{

    public class BerujuSubTitleController : Controller
    {
        CommonService cs = new CommonService();
        // GET: Admin/BudgetSubTitle
        public ActionResult Index()
        {
            BerujuSubType model = new BerujuSubType();
            model.BerujuSubTypeList = cs.GetBerujuSubTypeList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new BerujuSubType());
        }

        [HttpPost]
        public ActionResult Create(BerujuSubType model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.InsertBerujuSubType(model);
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
            BerujuSubType model = new BerujuSubType();
            model = cs.GetBerujuSubTypeList().SingleOrDefault(x => x.BerujuSubTitleId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BerujuSubType model)
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.UpdateBerujuSubType(model);
            return View(model);
        }


        public ActionResult Delete(int id)
        {
          

            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.DeleteBerujuTSubype(id);

            TempData["Success"] = rms.ReturnMessage;
            return RedirectToAction("Index");

        }

    }
}
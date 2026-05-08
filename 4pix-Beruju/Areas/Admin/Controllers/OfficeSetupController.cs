using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Controllers;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;


namespace _4pix_Beruju.Areas.Admin.Controllers
{

    [Authorize]
    public class OfficeSetupController : Controller
    {
        OfficeSetupService OfficeService = new OfficeSetupService();
        int CurrentLoginUserProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
        int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        //int CurrentLoginProvinceId = 6;//
        //int CurrentLoginUserTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();
        // GET: Admin/OffieSetup
        public ActionResult Index()
        {
            return View();
        }

        //2	PradeshMantralaya
        public ActionResult ProvincesMinistry()
        {

            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(2, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddProvincesMinistry()
        {
            OfficeSetup model = new OfficeSetup();
            model.ProvinceId = CurrentLoginUserProvinceId;
            ViewBag.EditMode = "False";
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddProvincesMinistry(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 2;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = 0;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 0;
            model.OfficeTypeId = 2;
            //model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();



            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 2;//ministry users....
                model.OfficeTypeId = 2;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ProvincesMinistry");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }
        public ActionResult EditProvincesMinistry(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(2, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            return View(model);
        }


        [HttpPost]
        public ActionResult EditProvincesMinistry(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 2;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 2;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.UpdateOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ProvincesMinistry");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }


        public ActionResult ProvinceNirdeshnalaya()
        {
            //3	Nirdeshalaya
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddProvinceNirdeshnalaya()
        {
            OfficeSetup model = new OfficeSetup();
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            ViewBag.EditMode = "False";
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddProvinceNirdeshnalaya(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 4;//ProvinceNirdeshanalaya......
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            //Get Main Ministry OfficeId....
            //model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 4;//Nirdeshanalaya
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 4;//ministry users....
                model.OfficeTypeId = 4;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ProvinceNirdeshnalaya");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }


        public ActionResult EditProvinceNirdeshnalaya(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProvinceNirdeshnalaya(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 4;//ProvinceNirdeshayala
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            //model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 4;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.UpdateOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ProvinceNirdeshnalaya");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }


        public ActionResult ProvinceOffices()
        {
            //4	Karyala
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(5, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddProvinceOffices()
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = CurrentLoginUserOfficeId;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ViewBag.EditMode = "False";
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddProvinceOffices(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 5;//Provinceoffice......
            model.OfficeStatus = true;
            //model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 5;//Nirdeshanalaya
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 5;//provinces offices....
                model.OfficeTypeId = 4;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ProvinceNirdeshnalaya");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }

        public ActionResult EditProvinceOffices(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(5, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProvinceOffices(OfficeSetup model)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        // bivag


        public ActionResult ListBivag()
        {
            //3	Nirdeshalaya
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(3, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddBivag()
        {
            OfficeSetup model = new OfficeSetup();
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            ViewBag.EditMode = "False";
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddBivag(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 3;//ProvinceNirdeshanalaya......
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            //Get Main Ministry OfficeId....
            //model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 3;//Bivag
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 3;//ministry users....
                model.OfficeTypeId = 3;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्छित भयो । ";
                return RedirectToAction("ListBivag");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }


        public ActionResult EditBivag(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(3, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBivag(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 3;//ProvinceNirdeshayala
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            //model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;

            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 3;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.UpdateOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListBivag");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }





    }
}
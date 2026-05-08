using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Controllers;
using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;
using _4pix_Beruju.Services;


namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{

    [Authorize]
    public class OfficeSetupLLController : Controller
    {
        OfficeSetupService OfficeService = new OfficeSetupService();
        int CurrentLoginUserProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
        int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        //int CurrentLoginUserTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();
        int IscentralOrProvine = _4pix_Beruju.Areas.Admin.functions.IScentralOrProvinceLevel();//0 central 1 province

        int CurrentLoginUserType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();

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
            model.UserTypeId =  2;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 2;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.InsertOfficeDetails(model);
            //add default data
            OfficeService.SP_InsertDefaultBudgetHeadAndExpenseTitle(CurrentLoginUserOfficeId, rms.PrimaryId);
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

                TempData["Success"] = "विवरण सुरक्षित भयो । ";
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
            model.UserTypeId = 3;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 5;//province
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

        public ActionResult DeleteProvinceMinistry(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = id;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.DeleteOfficesByOfficeId(model);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "कार्यलयको विवरण सिस्टम बाट हटाईयो।";
            }
            else
            {
                TempData["Success"] = "डाटा भएको कार्यलयको विवरण हटाउन मिल्दैन ।";
            }
            return RedirectToAction("ProvincesMinistry");
        }

        public ActionResult ProvinceNirdeshnalaya()
        {

            //list nirdeshalaya according to users type if admin list all
            //If ministry list only minister list
            //3	Nirdeshalaya
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            if(CurrentLoginUserType==2 || CurrentLoginUserType==8)
            {
                model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId);

            }
            else if(CurrentLoginUserType==3)//ministry admin
            {
                model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId).Where(x => x.MainOfficeId == CurrentLoginUserOfficeId).ToList();

            }
            else//nirdeshanalay 
            {
                model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId);

            }


            return View(model);
        }

        //public ActionResult AddProvinceNirdeshnalaya()
        //{
        //    OfficeSetup model = new OfficeSetup();
        //    model.ProvinceId = CurrentLoginUserProvinceId;
        //    model.MainOfficeId = CurrentLoginUserOfficeId;
        //    ViewBag.EditMode = "False";
        //    model.IsCentralOrProvinceViewBag = IscentralOrProvine;
        //    model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
        //    model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;

        //    if (model.IsCentralOrProvinceViewBag == 0)
        //    {
        //        model.ProvinceId = 0;
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddProvinceNirdeshnalaya(OfficeSetup model)
        //{
        //    model.DisplayStatus = 1;
        //    model.UserTypeId = 3;//ProvinceNirdeshanalaya......
        //   // model.MainOfficeId = model.ProvinceId;//drop downlist
        //    //model.MainOfficeId = CurrentLoginUserProvinceId;
        //    //Get Main Ministry OfficeId....
        //    //model.MainOfficeId = CurrentLoginUserOfficeId;
        //    model.OfficeStatus = true;
        //    model.ProvinceId = CurrentLoginUserProvinceId;
        //    model.ProVdcmunTypeId = 5;//province
        //    model.OfficeTypeId = 3;//Nirdeshanalaya
        //    ReturnMessageViewModel rms = new ReturnMessageViewModel();
        //    rms = OfficeService.InsertOfficeDetails(model);
        //    OfficeService.SP_InsertDefaultBudgetHeadAndExpenseTitle(model.MainOfficeId, rms.PrimaryId);

        //    if (rms.PrimaryId > 0)
        //    {
        //        //Insert into aspnetuser....
        //        model.OfficeDetailId = rms.PrimaryId;
        //        model.ProvinceId = CurrentLoginUserProvinceId;
        //        model.UserTypeId = 4;//ministry users....
        //        model.OfficeTypeId = 3;
        //        var controller = DependencyResolver.Current.GetService<AccountController>();
        //        controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
        //        var result = await controller.OfficesRegister(model);

        //        TempData["Success"] = "विवरण सुरक्षित भयो । ";
        //        return RedirectToAction("ProvinceNirdeshnalaya");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
        //        return View(model);
        //    }

        //}


        //public ActionResult EditProvinceNirdeshnalaya(int id)
        //{
        //    OfficeSetup model = new OfficeSetup();
        //    model.OfficeSetupList = new List<OfficeSetup>();
        //    model = OfficeService.ListOfficeByTypeAndProvinceid(3, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
        //    ViewBag.EditMode = "True";
        //    model.IsCentralOrProvinceViewBag = IscentralOrProvine;
        //    model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
        //    model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;
        //    model.MinistryId = model.MainOfficeId;//ministry id is main office id
        //    if (model.IsCentralOrProvinceViewBag == 0)
        //    {
        //        model.ProvinceId = 0;
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult EditProvinceNirdeshnalaya(OfficeSetup model)
        //{
        //    model.DisplayStatus = 1;
        //    model.UserTypeId = 4;//ProvinceNirdeshayala
        //    //model.MainOfficeId = model.ProvinceId;
        //    model.MainOfficeId = model.MinistryId;
        //    //model.MainOfficeId = CurrentLoginUserProvinceId;
        //    //model.MainOfficeId = CurrentLoginUserOfficeId;
        //    model.OfficeStatus = true;
        //    model.ProVdcmunTypeId = 5;//province
        //    model.OfficeTypeId = 3;
        //    model.ProvinceId = CurrentLoginUserProvinceId;
        //    ReturnMessageViewModel rms = new ReturnMessageViewModel();
        //    rms = OfficeService.UpdateOfficeDetails(model);
        //    if (rms.PrimaryId > 0)
        //    {
        //        TempData["Success"] = "विवरण परिवर्तन भयो । ";
        //        return RedirectToAction("ProvinceNirdeshnalaya");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
        //        return View(model);
        //    }
        //}



 

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
                OfficeService.SP_InsertDefaultBudgetHeadAndExpenseTitle(model.MainOfficeId, rms.PrimaryId);
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

        public ActionResult DeleteProvinceNirdeshnalaya(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = id;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.DeleteOfficesByOfficeId(model);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "कार्यलयको विवरण सिस्टम बाट हटाईयो।";
            }
            else
            {
                TempData["Success"] = "डाटा भएको कार्यलयको विवरण हटाउन मिल्दैन ।";
            }
            return RedirectToAction("ProvinceNirdeshnalaya");
        }




        public ActionResult ProvinceOffices()
        {
            //5	Karyala
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
            model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;
            if (model.IsCentralOrProvinceViewBag == 0)
            {
                model.ProvinceId = 0;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddProvinceOffices(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 5;//Provinceoffice......
            model.OfficeStatus = true;
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 5;//Nirdeshanalaya
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.InsertOfficeDetails(model);
            OfficeService.SP_InsertDefaultBudgetHeadAndExpenseTitle(model.MainOfficeId, rms.PrimaryId);

            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 5;//provinces offices....
                model.OfficeTypeId = 5;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("ProvinceOffices");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }

        public ActionResult EditProvinceOffices(int id)
        {
            // OfficeSetup model = new OfficeSetup();
            // model.OfficeSetupList = new List<OfficeSetup>();
            // model = OfficeService.ListOfficeByTypeAndProvinceid(4, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            // ViewBag.EditMode = "True";
            //// model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            // //model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            // //model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;
            // model.MinistryId = Utilities.GetMinistryIdFromNirdeshanalayId(model.MainOfficeId);


            // if (model.IsCentralOrProvinceViewBag == 0)
            // {
            //     model.ProvinceId = 0;
            // }
            // return View(model);
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(5, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            model.BivagId = Utilities.GetMinistryIdFromBivagId(model.MainOfficeId);
            model.MinistryId = Utilities.GetMinistryIdFromBivagId(model.BivagId);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProvinceOffices(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 5;//Provinceoffice......
            model.OfficeStatus = true;
            model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 5;//Nirdeshanalaya
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            rms = OfficeService.UpdateOfficeDetails(model);


            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण सम्पादन भयो । ";
                return RedirectToAction("ProvinceOffices");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return RedirectToAction("ProvinceOffices");
            }
        }


        public ActionResult DeleteProvinceOffices(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = id;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.DeleteOfficesByOfficeId(model);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "कार्यलयको विवरण सिस्टम बाट हटाईयो।";
            }
            else
            {
                TempData["Success"] = "डाटा भएको कार्यलयको विवरण हटाउन मिल्दैन ।";
            }
            return RedirectToAction("ProvinceOffices");
        }

        public ActionResult Create()
        {
            return View();
        }
        //7	LocalLevel Uer
        public ActionResult ListLocalLevel()
        {

            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(6, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddLocalLevel()
        {
            OfficeSetup model = new OfficeSetup();
            model.ProvinceId = CurrentLoginUserProvinceId;
            ViewBag.EditMode = "False";
            model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;

            if (model.IsCentralOrProvinceViewBag == 0)
            {
                model.ProvinceId = 0;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddLocalLevel(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 7;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 6;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 7;//ministry users....
                model.OfficeTypeId = 6;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("ListLocalLevel");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }
        public ActionResult EditLocalLevel(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(6, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;

            if (model.IsCentralOrProvinceViewBag == 0)
            {
                model.ProvinceId = 0;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult EditLocalLevel(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 7;//locallevel
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 6;//local level
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.UpdateOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListLocalLevel");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }
        public ActionResult DeleteLocalLevel(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = id;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.DeleteOfficesByOfficeId(model);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "कार्यलयको विवरण सिस्टम बाट हटाईयो।";
            }
            else
            {
                TempData["Success"] = "डाटा भएको कार्यलयको विवरण हटाउन मिल्दैन ।";
            }
            return RedirectToAction("ListLocalLevel");
        }

        //7	LocalLevel Uer
        public ActionResult ListAayog()
        {

            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model.OfficeSetupList = OfficeService.ListOfficeByTypeAndProvinceid(5, CurrentLoginUserProvinceId);
            return View(model);
        }

        public ActionResult AddAayog()
        {
            OfficeSetup model = new OfficeSetup();
            model.ProvinceId = CurrentLoginUserProvinceId;
            ViewBag.EditMode = "False";
            model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;

            if (model.IsCentralOrProvinceViewBag == 0)
            {
                model.ProvinceId = 0;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddAayog(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 6;//ProvinceMinistryUser
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 5;
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.InsertOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                model.OfficeDetailId = rms.PrimaryId;
                model.ProvinceId = CurrentLoginUserProvinceId;
                model.UserTypeId = 6;//ministry users....
                model.OfficeTypeId = 5;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                var result = await controller.OfficesRegister(model);

                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("ListAayog");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }
        public ActionResult EditAayog(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeSetupList = new List<OfficeSetup>();
            model = OfficeService.ListOfficeByTypeAndProvinceid(5, CurrentLoginUserProvinceId).SingleOrDefault(x => x.OfficeDetailId == id);
            ViewBag.EditMode = "True";
            model.IsCentralOrProvinceViewBag = IscentralOrProvine;
            model.NirdeshanalayaUserOrProvinceUserID = CurrentLoginUserType;
            model.CurrentLoginUserOfficeViewBagID = CurrentLoginUserOfficeId;

            if (model.IsCentralOrProvinceViewBag == 0)
            {
                model.ProvinceId = 0;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult EditAayog(OfficeSetup model)
        {
            model.DisplayStatus = 1;
            model.UserTypeId = 6;//locallevel
            //model.MainOfficeId = CurrentLoginUserProvinceId;
            model.MainOfficeId = CurrentLoginUserOfficeId;
            model.OfficeStatus = true;
            model.ProVdcmunTypeId = 5;//province
            model.OfficeTypeId = 5;//local level
            model.ProvinceId = CurrentLoginUserProvinceId;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.UpdateOfficeDetails(model);
            if (rms.PrimaryId > 0)
            {
                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListAayog");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }
        }

        public ActionResult DeleteAayog(int id)
        {
            OfficeSetup model = new OfficeSetup();
            model.OfficeDetailId = id;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.DeleteOfficesByOfficeId(model);
            if (rms.ReturnMessage == "Deleted Successfully")
            {
                TempData["Success"] = "कार्यलयको विवरण सिस्टम बाट हटाईयो।";
            }
            else
            {
                TempData["Success"] = "डाटा भएको कार्यलयको विवरण हटाउन मिल्दैन ।";
            }
            return RedirectToAction("DeleteAayog");
        }

        public ActionResult ListLetterOfficeSetup()
        {
            CommonService cs = new CommonService();
            LetterSetupViewModel model = new LetterSetupViewModel();
            model.SamparikshadLetterOfficeSetupViewModelList = new List<SamparikshadLetterOfficeSetupViewModel>();
            model.SamparikshadLetterOfficeSetupViewModelList = cs.GetSamparikshadletterofficesetupList(CurrentLoginUserOfficeId, 1);
            return View(model);
        }
        public ActionResult AddLetterSetupOffice()
        {
            LetterSetupViewModel model = new LetterSetupViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddLetterSetupOffice(LetterSetupViewModel model)
        {
            CommonService cs = new CommonService();
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeId = CurrentLoginUserOfficeId;
            model.ObjSamparikshadLetterOfficeSetupViewModel.SetupType = 1;
            model.ObjSamparikshadLetterOfficeSetupViewModel.SetupStatus = true;
            rms = cs.InsertSamparikshadlettersetup(model);
            if (rms.PrimaryId > 0)
            {

                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("ListLetterOfficeSetup");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult EditLetterSetupOffice(int id)
        {
            CommonService cs = new CommonService();
            LetterSetupViewModel model = new LetterSetupViewModel();
            model.SamparikshadLetterOfficeSetupViewModelList = new List<SamparikshadLetterOfficeSetupViewModel>();
            model.ObjSamparikshadLetterOfficeSetupViewModel = cs.GetSamparikshadletterofficesetupList(CurrentLoginUserOfficeId, 1).Where(x => x.SamparikshadLetterSetupId == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditLetterSetupOffice(LetterSetupViewModel model)
        {
            CommonService cs = new CommonService();
            model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeId = CurrentLoginUserOfficeId;
            model.ObjSamparikshadLetterOfficeSetupViewModel.SetupType = 1;
            model.ObjSamparikshadLetterOfficeSetupViewModel.SetupStatus = true;

            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = cs.UpdateSamparikshadlettersetup(model);
            if (rms.PrimaryId > 0)
            {

                TempData["Success"] = "विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListLetterOfficeSetup");
            }
            else
            {
                ViewBag.ErrorMessage = rms.ReturnMessage.ToString();
                return View(model);
            }

        }

        public ActionResult AddCurrentOfficeChief()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCurrentOfficeChief(ReportVIewModel  model)
        {
            return View();
        }



        public ActionResult DeleteLetterSetupOffice(int id)
        {
            TempData["Success"] = "विवरण सिस्टमबाट हटाईयो । ";
            return RedirectToAction("ListLetterOfficeSetup");
        }




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
           // model.ProvinceId = CurrentLoginUserProvinceId;
            model.ProVdcmunTypeId = 0;//province
            model.OfficeTypeId = 3;//Bivag
            ReturnMessageViewModel rms = new ReturnMessageViewModel();

            rms = OfficeService.InsertOfficeDetails(model);
           
            if (rms.PrimaryId > 0)
            {

                OfficeService.SP_InsertDefaultBudgetHeadAndExpenseTitle(CurrentLoginUserOfficeId, rms.PrimaryId);
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
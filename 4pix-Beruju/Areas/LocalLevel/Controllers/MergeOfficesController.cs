using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Controllers;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class MergeOfficesController : Controller
    {
        OfficeSetupService OfficeService = new OfficeSetupService();
        int CurrentLoginUserProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
        int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        //int CurrentLoginUserTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();
        int IscentralOrProvine = Admin.functions.IScentralOrProvinceLevel();//0 central 1 province
        int ApplicationProvinceId = Admin.functions.GetCurrentApplicationProvinceId();
        int CurrentLoginUserType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
        private BerujuEntities db = new BerujuEntities();
        // GET: LocalLevel/MergeOffices
        public ActionResult MergeOfficesIndex()
        {
            MergeOfficeMaster model = new MergeOfficeMaster();
            model.MergeOfficeDetailsList = new List<MergeOfficeDetails>();
            model.MergeOfficeDetailsList = db.MergeOfficeDetails.ToList();
            return View(model);
        }

        public ActionResult MergeAdd()
        {
            MergeOfficeMaster model = new MergeOfficeMaster();
            model.ObjOfficeMainViewModel = new OfficeMainViewModel();
            model.ObjOfficeMainViewModel.ProvinceId = ApplicationProvinceId;
            model.OfficeDetailsWithAddressVMList = new List<OfficeDetailsWithAddressVM>();
            return View(model);
        }






        [HttpPost]
        public async Task<ActionResult> MergeAdd(MergeOfficeMaster model)
        {
            OfficeSetup mainOfficeSetup = new OfficeSetup();

            mainOfficeSetup.DisplayStatus = 1;
            mainOfficeSetup.UserTypeId = 3;//ProvinceMinistryUser           
            mainOfficeSetup.MainOfficeId = CurrentLoginUserOfficeId;
            mainOfficeSetup.OfficeStatus = true;
            mainOfficeSetup.ProVdcmunTypeId = 3;//province
            mainOfficeSetup.OfficeTypeId = 2;
            mainOfficeSetup.ProvinceId = ApplicationProvinceId;
            mainOfficeSetup.OFficeName = model.MergeOfficeName;
            mainOfficeSetup.OfficeCode = model.ObjOfficeMainViewModel.OfficeCode;
            mainOfficeSetup.OfficeEmail = model.OfficeEmail;
            mainOfficeSetup.Address = model.ObjOfficeMainViewModel.OfficeAddress;
            mainOfficeSetup.OfficeStatus = true;
            mainOfficeSetup.ContactPerson = model.ObjOfficeMainViewModel.ContactPerson;
            mainOfficeSetup.ContactPersonMobile = model.ObjOfficeMainViewModel.ContactPersonPhone;
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            rms = OfficeService.InsertOfficeDetails(mainOfficeSetup);
            if (rms.PrimaryId > 0)
            {
                //Insert into aspnetuser....
                mainOfficeSetup.OfficeDetailId = rms.PrimaryId;
                mainOfficeSetup.ProvinceId = CurrentLoginUserProvinceId;
                mainOfficeSetup.UserTypeId = 3;//ministry users....
                mainOfficeSetup.OfficeTypeId = 2;
                var controller = DependencyResolver.Current.GetService<AccountController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                string createuserfailureText = string.Empty;
                try
                {
                    var result = await controller.OfficesRegister(mainOfficeSetup);
                }
                catch (Exception)
                {

                    createuserfailureText = "Failed";
                }

                model.CreatedDate = DateTime.Now;
                model.MergedDate = Utilities.GetEnglishDateFromNP(model.MergedDateStr);
                db.MergeOfficeMaster.Add(model);
                int masterId = db.SaveChanges();
                if (masterId > 0)
                {
                    MergeOfficeDetails dtls = new MergeOfficeDetails();
                    foreach (var item in model.OfficeDetailsWithAddressVMList)
                    {
                        dtls.MergeOfficeMasterId = model.MergeOfficeMasterId;
                        dtls.MainOfficeId = rms.PrimaryId;
                        dtls.Officeid = item.OfficeId;
                        db.MergeOfficeDetails.Add(dtls);
                        db.SaveChanges();
                    }
                }



                TempData["Success"] = "विवरण सुरक्षित भयो । ";
                return RedirectToAction("MergeOfficesIndex");


            }



            return RedirectToAction("MergeOfficesIndex");
        }



        [HttpPost]
        public ActionResult AddMoreOfficesForMerge()
        {
            MergeOfficeMaster mainModel = new MergeOfficeMaster();

            mainModel.OfficeDetailsWithAddressVMList = new List<OfficeDetailsWithAddressVM>();

            return PartialView("_AddMoreOfficeDetails");
        }
    }
}
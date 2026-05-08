using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Areas.LocalLevel.Models;
using _4pix_Beruju.Services;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class BerujuTypeReportsController : Controller
    {
        InternalBerujuService IBS = new InternalBerujuService();
        // GET: LocalLevel/BerujuTypeReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchSaidantikBeruju(int id)//OffficeID 
        {
            BerujuTypeReportsViewModel model = new BerujuTypeReportsViewModel();
            model.OfficeIdSearch = id;
            //Get OfficeName from officeID....
            return View(model);

        }
        [HttpPost]
        public ActionResult ListSaidantikBuruju(BerujuTypeReportsViewModel model)
        {
            model.objBerujuTypeModels = new BerujuTypeModels();
            model.objBerujuTypeModels.SaidantikBerujuListViewModel = new List<_4pix_Beruju.Models.SaidantikBeruju>();
            model.objBerujuTypeModels.SaidantikBerujuListViewModel = IBS.ListSaidantikBerujuForAdmin(model.OfficeIdSearch, model.InternalOrExternalIdSearch,model.FiscalYearIdSearch).ToList();
            model.InternalOrExternalIdSearch = model.InternalOrExternalIdSearch;
            return PartialView("_SaidantikListForAdmin", model);

        }

        public ActionResult ViewDetailsSaidantikBeruju(int id, int id1,int id2)
        {
            BerujuTypeReportsViewModel model = new BerujuTypeReportsViewModel();
            model.objBerujuTypeModels = new BerujuTypeModels();
            model.objBerujuTypeModels.objSaidantikBerujuViewModel = new _4pix_Beruju.Models.SaidantikBeruju();
            model.objBerujuTypeModels.objSaidantikBerujuViewModel = IBS.GetSaidantikBerujuByPrimaryId(id2, id);//officeid and saidantik beruju id
            model.InternalOrExternalIdSearch = id1;//internal or external id
            model.OfficeIdSearch = id2;
            return View(model);
        }


        public ActionResult SearchBerujuNotDone(int id)//OffficeID 
        {
            BerujuTypeReportsViewModel model = new BerujuTypeReportsViewModel();
            model.OfficeIdSearch = id;
            //Get OfficeName from officeID....
            return View(model);

        }
        [HttpPost]
        public ActionResult ListBerujuNotDone(BerujuTypeReportsViewModel model)
        {
            BerujuCommonService BCS = new BerujuCommonService();
            model.objBerujuTypeModels = new BerujuTypeModels();
            model.objBerujuTypeModels.BerujuNotDoneModelList = new List<_4pix_Beruju.Models.BerujuNotDoneModel>();
            model.objBerujuTypeModels.BerujuNotDoneModelList = BCS.ListBerujuNotDoneForAdmin(model.OfficeIdSearch, model.InternalOrExternalIdSearch, model.FiscalYearIdSearch).ToList();
            model.InternalOrExternalIdSearch = model.InternalOrExternalIdSearch;
            return PartialView("_BerujuNotDoneListForAdmin", model);

        }

        public ActionResult ViewDetailBerujuNotDone(int id, int id1, int id2)
        {
            BerujuCommonService BCS = new BerujuCommonService();
            BerujuTypeReportsViewModel model = new BerujuTypeReportsViewModel();
            model.objBerujuTypeModels = new BerujuTypeModels();
            model.objBerujuTypeModels.objBerujuNotDoneModel = new _4pix_Beruju.Models.BerujuNotDoneModel();
            model.objBerujuTypeModels.objBerujuNotDoneModel = BCS.ListBerujuNotDone(id2, id, id1).SingleOrDefault();
            model.InternalOrExternalIdSearch = id1;//internal or external id
            model.OfficeIdSearch = id2;
            return View(model);
        }

    }
}
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static _4pix_Beruju.Helpers.CommonHelper;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class ReportLLController : Controller
    {
        ReportService RS = new ReportService();
        int CurrentLoginOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        int CurrentLoginUserTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
        int CurrentLoginUserProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
        int CurrentLoginuserOfficeTypeForDistrict = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();
        int CurrentDistrictId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserDistrict(_4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId());
        // GET: LocalLevel/ReportLL

        public ActionResult Index()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.BerujuLagatKhataVMList = new List<BerujuLagatKhataVM>();
            model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
            return View(model);
        }

        public ActionResult BerujuLagatKhataAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);
        }

        public ActionResult BerujuLagatKhata()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuLagatKhata(ReportVIewModel model)
        {

            //model.DateFromSearch = Utilities.GetEnglishDateFromNP(model.DateFromStr);
            //model.DateToSearch = Utilities.GetEnglishDateFromNP(model.DateToStr);

            //if (model.DateFromSearch == DateTime.MinValue)
            //{
            //    model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            //    model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //    model.OfficeId = CurrentLoginOfficeId;
            //    model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            //    model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            //    ViewBag.ErrorMessage = @"मिति (देखि) मिलेन";
            //    return PartialView("_ErrorViews",model);
            //}

            //if (model.DateToSearch == DateTime.MinValue)
            //{
            //    model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            //    model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //    model.OfficeId = CurrentLoginOfficeId;
            //    model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            //    model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            //    ViewBag.ErrorMessage = @"मिति (सम्म) मिलेन";
            //    return PartialView("_ErrorViews", model);
            //}
            //if (model.DateToSearch < model.DateFromSearch)
            //{
            //    model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            //    model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //    model.OfficeId = CurrentLoginOfficeId;
            //    model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            //    model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            //    ViewBag.ErrorMessage = @"देखी मिति सम्म मिति भन्दा अगाडिको भयो ।";
            //    return PartialView("_ErrorViews", model);
            //}


            //if (model.KoshID <= 0)
            //{
            //    model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            //    model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //    model.OfficeId = CurrentLoginOfficeId;
            //    model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            //    model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            //    ViewBag.ErrorMessage = @"कृपया कोषको प्रकार छान्नुहोस ।";
            //    return PartialView("_ErrorViews", model);
            //}
            //else
            //{
            //    if (model.KoshID == 1)//biniyojana....
            //    {
            //        if (model.BaushiNumberId <= 0)
            //        {
            //            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            //            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //            model.OfficeId = CurrentLoginOfficeId;
            //            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            //            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            //            ViewBag.ErrorMessage = @"कृपया बउशि नम्बर छान्नुहोस ।";
            //            return PartialView("_ErrorViews", model);

            //        }

            //    }
            //}




            model.BerujuLagatKhataVMList = new List<BerujuLagatKhataVM>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(model.OfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }



                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    //change code jun13 2023
                    if (model.MininstrySearchId == 0)//all
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }
                    else
                    {
                        SearchUserOfficeId = model.MininstrySearchId;
                    }

                    //before....
                    //SearchUserOfficeId = model.MininstrySearchId;
                }



                else if (model.OfficeTypeSearchId == 3)//BIvag
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Nirdeshan
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;

                    //SearchUserOfficeId = model.AayogAndOthers;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    //SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);

                if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
                {
                    model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                    if (model.KoshID == 1)
                    {
                        model.BerujuLagatKhataVMList = model.BerujuLagatKhataVMList.Where(x => x.BudgetSubTitle == model.BaushiNumberId.ToString()).ToList();
                    }
                }
                else
                {
                    model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                    if (model.KoshID == 1)
                    {
                        model.BerujuLagatKhataVMList = model.BerujuLagatKhataVMList.Where(x => x.BudgetSubTitle == model.BaushiNumberId.ToString()).ToList();
                    }
                }







                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);
            if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
            {
                return PartialView("_BerujuLagatKhataMinistryAll", model);

            }
            else
            {
                return PartialView("_BerujuLagatKhata", model);

            }
        }


        public ActionResult BerujuOfficeWiseAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }


        public ActionResult BerujuSampaJistReport()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuSampaJistReport(ReportVIewModel model)
        {
            if (model.FiscalYearId == 0)
            {
                model.ProvinceIdSearch = CurrentLoginUserProvinceId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
                model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
                ViewBag.ErrorMessage = @"कृपया आर्थिक बर्ष छान्नुहोस";
                return PartialView("_ErrorViews", model);
            }
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.BerujuSampaReportVMList = RS.BerujuSampaJistReport(SearchUserOfficeId,model.OfficeTypeSearchId, model.FiscalYearId);
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_PartialBerujuSampaJistReport", model);

        }


        public ActionResult BerujuSampaJistReportAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuSampaJistReportAdmin(ReportVIewModel model)
        {
            if (model.FiscalYearId == 0)
            {
                model.ProvinceIdSearch = CurrentLoginUserProvinceId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
                model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
                ViewBag.ErrorMessage = @"कृपया आर्थिक बर्ष छान्नुहोस";
                return PartialView("_ErrorViews", model);
            }
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.BerujuSampaReportVMList = RS.BerujuSampaJistReportAdmin(SearchUserOfficeId, model.OfficeTypeSearchId, model.FiscalYearId);
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_PartialBerujuSampaJistReport", model);

        }




        public ActionResult BerujuFurcheutToOfficeAndSampaJistReport()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuFurcheutToOfficeAndSampaJistReport(ReportVIewModel model)
        {


            if (model.FiscalYearId == 0)
            {
                model.ProvinceIdSearch = CurrentLoginUserProvinceId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
                model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
                ViewBag.ErrorMessage = @"कृपया आर्थिक बर्ष छान्नुहोस";
                return PartialView("_ErrorViews", model);
            }
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.BerujuFurcheutSampaReportModelVMList = RS.BerujuFurcheutToOfficeAndSampaJistReport(SearchUserOfficeId, model.OfficeTypeSearchId, model.FiscalYearId);
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_PartialBerujuFurcheutToOfficeAndSampaJistReport", model);

        }



        public ActionResult BerujuFurcheutJistReport()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuFurcheutJistReport(ReportVIewModel model)
        {

            if (model.FiscalYearId == 0)
            {
                model.ProvinceIdSearch = CurrentLoginUserProvinceId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
                model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
                ViewBag.ErrorMessage = @"कृपया आर्थिक बर्ष छान्नुहोस";
                return PartialView("_ErrorViews", model);
            }
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.BerujuSampaReportVMList = RS.BerujuFurcheutJistReport(SearchUserOfficeId, model.OfficeTypeSearchId, model.FiscalYearId);
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_PartialBerujuFurcheutJistReport", model);

        }


        public ActionResult BerujuFurcheutJistReportAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult BerujuFurcheutJistReportAdmin(ReportVIewModel model)
        {

            if (model.FiscalYearId == 0)
            {
                model.ProvinceIdSearch = CurrentLoginUserProvinceId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
                model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
                ViewBag.ErrorMessage = @"कृपया आर्थिक बर्ष छान्नुहोस";
                return PartialView("_ErrorViews", model);
            }
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.BerujuSampaReportVMList = RS.BerujuFurcheutJistReportAdmin(SearchUserOfficeId, model.OfficeTypeSearchId, model.FiscalYearId);
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_PartialBerujuFurcheutJistReportAdmin", model);

        }


        public ActionResult BerujuOfficeWise()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }


        [HttpPost]
        public PartialViewResult BerujuOfficeWise(ReportVIewModel model)
        {

            model.FYListViewModellist = new List<FYListViewModel>();
            model.FYListViewModellist = RS.GetFiscalYearList(model.FiscalYearId);

            if (CurrentLoginUserTypeId == 4)
            {
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {

                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    if (model.MininstrySearchId == 0)//all
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }
                    else
                    {
                        SearchUserOfficeId = model.MininstrySearchId;
                    }

                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }




                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            model.BerujuTypeViewModelList = new List<BerujuTypeViewModel>();
            if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
            {
                model.BerujuTypeViewModelList = RS.GetBerujuTypeList();
            }
            else
            {
                model.BerujuTypeViewModelList = RS.GetBerujuTypeList();
            }

            //model.OfficeId = CurrentLoginOfficeId;
            //model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;

            if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
            {
                return PartialView("_PartialBerujuOfficeWiseForMinistry", model);

            }
            else
            {
                return PartialView("_PartialBerujuOfficeWise", model);

            }
        }


        public ActionResult CentralOfficeWiseAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        public ActionResult CentralOfficeWise()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }


        [HttpPost]
        public PartialViewResult CentralOfficeWise(ReportVIewModel model)
        {
            model.KendriyaKaralayagtLaagatViewModelList = new List<KendriyaKaralayagtLaagatViewModel>();
            if (CurrentLoginUserTypeId == 4)
            {
                model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(model.OfficeId, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;

                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }

                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    //change code jun13 2023
                    if (model.MininstrySearchId == 0)//all
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }
                    else
                    {
                        SearchUserOfficeId = model.MininstrySearchId;
                    }

                    //before....
                    //SearchUserOfficeId = model.MininstrySearchId;
                }



                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }


                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }


                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(CurrentLoginOfficeId, model.FiscalYearId);

                if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
                {
                    model.KendriyaKaralayagtLaagatViewModelList = RS.ministry_Get803Reports(SearchUserOfficeId, model.FiscalYearId);

                }
                else
                {
                    model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(SearchUserOfficeId, model.FiscalYearId);

                }


                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }
            if (model.OfficeTypeSearchId == 2 && model.MininstrySearchId == 0)
            {
                return PartialView("_PartialCentralOfficeWiseMinistry", model);

            }
            else
            {
                return PartialView("_PartialCentralOfficeWise", model);

            }
            //_PartialCentralOfficeWiseMinistry

            //return PartialView("_PartialCentralOfficeWise", model);
        }



        public ActionResult OfficialWiseCentralReportAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);
        }
        public ActionResult OfficialWiseCentralReport()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);

        }



        [HttpPost]
        public PartialViewResult OfficialWiseCentralReport(ReportVIewModel model)
        {

            model.KendriyaKaralayagtLaagatViewModelList = new List<KendriyaKaralayagtLaagatViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(model.OfficeId, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;

                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                   // SearchUserOfficeId = model.AayogAndOthers;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }
                //model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(CurrentLoginOfficeId, model.FiscalYearId);

                model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(SearchUserOfficeId, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }

            //model.KendriyaKaralayagtLaagatViewModelList = RS.KendriyaKaralayagtLaagat(CurrentLoginOfficeId, model.FiscalYearId);
            //model.OfficeId = CurrentLoginOfficeId;
            //model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            return PartialView("_PartialOfficialWiseCentralReport", model);
        }

        public ActionResult SamparikshadVoucharLedgerAdmin()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        public ActionResult SamparikshadVoucharLedger()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users

            return View(model);
        }

        [HttpPost]
        public PartialViewResult SamparikshadVoucharLedger(ReportVIewModel model)
        {

            model.SamparikshadGausharaKhataVMList = new List<SamparikshadGausharaKhataVM>();
            if (CurrentLoginUserTypeId == 4)
            {
                model.SamparikshadGausharaKhataVMList = RS.GetSamparikshadGausharaKhata(model.OfficeId, model.FiscalYearId, model.KoshID);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//bivag
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//nirdeshan
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//karyalaya
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }

                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                model.SamparikshadGausharaKhataVMList = RS.GetSamparikshadGausharaKhata(SearchUserOfficeId, model.FiscalYearId, model.KoshID);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            model.ObjSamparikshadGausharaKhataVM = new SamparikshadGausharaKhataVM();
            model.ObjSamparikshadGausharaKhataVM.FiscalYearId = model.FiscalYearId;


            //model.OfficeId = CurrentLoginOfficeId;
            //model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;

            return PartialView("_PartialSamparikshadVoucharLedger", model);
        }

        public ActionResult AntimBerujuDetailRptAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }
        public ActionResult AntimBerujuDetailRpt()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        [HttpPost]
        public PartialViewResult AntimBerujuDetailRpt(ReportVIewModel model)
        {

            model.AntimBerujuDetailsRptViewModelList = new List<AntimBerujuDetailsRptViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.AntimBerujuDetailsRptViewModelList = RS.SPRPT_GetRptKoshtypewise(model.OfficeId, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1 || CurrentLoginUserTypeId == 9)//province user
                {
                    if (CurrentLoginUserTypeId == 8)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AntimBerujuDetailsRptViewModelList = RS.SPRPT_GetRptKoshtypewise(SearchUserOfficeId, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);

            return PartialView("_FinalBerujuDetailRpt", model);
        }

        public ActionResult FinalBerujuDetailByTowhomTypeRptAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        public ActionResult FinalBerujuDetailByTowhomTypeRpt()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        [HttpPost]
        public PartialViewResult FinalBerujuDetailByTowhomTypeRpt(ReportVIewModel model)
        {

            model.AntimBerujuTowhomTypeWiseRptViewModelList = new List<AntimBerujuTowhomTypeWiseRptViewModel>();
            int SearchUserOfficeId = CurrentLoginOfficeId;
            if (CurrentLoginUserTypeId == 4)
            {

                model.AntimBerujuTowhomTypeWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByToWhomType(model.OfficeId, model.FiscalYearId, model.ToWhomTypeId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {

                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    if (model.MininstrySearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया मन्त्रालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    if (model.BivagSearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया विभाग छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.NirdeshnalayaSearchId == 4)//Karyalaya
                {
                    if (model.OfficeId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया निर्देशनालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                { 
                    if (model.KaryalayaSearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया कार्यालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }

                SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }


                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);




                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }

            if (model.SumamryOrDetailsId == 1)
            {
                model.AntimBerujuTowhomTypeWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByToWhomType(SearchUserOfficeId, model.FiscalYearId, model.ToWhomTypeId);

            }
            else
            {
                model.AntimBerujuTowhomTypeWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByToWhomTypeDetail(SearchUserOfficeId, model.FiscalYearId, model.ToWhomTypeId);

            }
            if (model.SumamryOrDetailsId == 1)
            {
                return PartialView("_FinalBerujuDetailByTowhomTypeRpt", model);
            }
            else
            {
                return PartialView("_FinalBerujuDetailByTowhomTypeDetailRpt", model);
            }


        }


        public ActionResult FinalBerujuDetailByChiefWiseRptAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        public ActionResult FinalBerujuDetailByChiefWiseRpt()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        [HttpPost]
        public PartialViewResult FinalBerujuDetailByChiefWiseRpt(ReportVIewModel model)
        {

            model.AntimBerujuOfficeChiefWiseRptViewModelList = new List<AntimBerujuOfficeChiefWiseRptViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.AntimBerujuOfficeChiefWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByOfficeChiefWise(model.OfficeId, model.FiscalYearId, model.ToWhomTypeId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    if (model.MininstrySearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया मन्त्रालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    if (model.BivagSearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया विभाग छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    if (model.NirdeshnalayaSearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया निर्देशनालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }
                    SearchUserOfficeId = model.OfficeId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {
                    if (model.KaryalayaSearchId == 0)
                    {
                        ViewBag.ErrorMessage = @"कृपया कार्यालय छान्नुहोस ।";
                        return PartialView("_ErrorOfficeNotSelected", model);
                    }

                    SearchUserOfficeId = model.AayogAndOthers;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AntimBerujuOfficeChiefWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByOfficeChiefWise(SearchUserOfficeId, model.FiscalYearId, model.ToWhomTypeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);

            return PartialView("_FinalBerujuDetailByOfficeChiefWise", model);
        }



        public ActionResult FinalBerujuDetailByAccountChiefWiseRptAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        public ActionResult FinalBerujuDetailByAccountChiefWiseRpt()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users



            return View(model);
        }

        [HttpPost]
        public PartialViewResult FinalBerujuDetailByAccountChiefWiseRpt(ReportVIewModel model)
        {

            model.AntimBerujuOfficeChiefWiseRptViewModelList = new List<AntimBerujuOfficeChiefWiseRptViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.AntimBerujuOfficeChiefWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByOfficeFinancHeadWise(model.OfficeId, model.FiscalYearId, model.ToWhomTypeId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }
            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)
                {
                    SearchUserOfficeId = model.OfficeId;
                }
                if (model.OfficeTypeSearchId == 1)//province user
                {
                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }

                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {
                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AntimBerujuOfficeChiefWiseRptViewModelList = RS.SPRPT_GetFinalBerujuByOfficeFinancHeadWise(SearchUserOfficeId, model.FiscalYearId, model.ToWhomTypeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);

            return PartialView("_FinalBerujuDetailByFinanceHeadWise", model);
        }

        public ActionResult Anusuchi12()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.AnusuchiTwelveViewModelList = new List<AnusuchiTwelveViewModel>();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult Anusuchi12(ReportVIewModel model)
        {

            //if(model.OfficeTypeSearchId>0)
            //{

            //}
            model.AnusuchiTwelveViewModelList = new List<AnusuchiTwelveViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.AnusuchiTwelveViewModelList = RS.spGetAnusuchiTwelve(model.OfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AnusuchiTwelveViewModelList = RS.spGetAnusuchiTwelve(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);

            return PartialView("_AnusuchiTwelve", model);
        }



        public ActionResult Anusuchi13()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.AnusuchiThirteenViewModelList = new List<AnusuchiThirteenViewModel>();
            return View(model);

        }


        [HttpPost]
        public PartialViewResult Anusuchi13(ReportVIewModel model)
        {

            //if(model.OfficeTypeSearchId>0)
            //{

            //}
            model.AnusuchiThirteenViewModelList = new List<AnusuchiThirteenViewModel>();

            if (CurrentLoginUserTypeId == 4) 
            {
                model.AnusuchiThirteenViewModelList = RS.spGetAnusuchiThirteen(model.OfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AnusuchiThirteenViewModelList = RS.spGetAnusuchiThirteen(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }


            //List<BerujuLagatKhataVM> SumList = new List<BerujuLagatKhataVM>();

            //foreach (var item in model.BerujuLagatKhataVMList)
            //{
            //    SumList.Add(new BerujuLagatKhataVM
            //    {
            //        TypeOne=item.TypeOne.HasValue?item.TypeOne:0,
            //        TypeTwo=item.TypeTwo.HasValue?item.TypeTwo:0,
            //        TypeThree=item.TypeThree.HasValue?item.TypeThree:0,



            //    });

            //}
            //model.BerujuLagatKhataVMList = SumList.ToList();


            //model.ApplicationFormViewModelList = services.PopulateDetailsReportList(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.DivisionHeadId);

            return PartialView("_AnusuchiThirteen", model);
        }


        public ActionResult Anusuchi14()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.AnusuchiFourteenViewModelList = new List<AnusuchiFourteenViewModel>();
            return View(model);

        }


        [HttpPost]
        public PartialViewResult Anusuchi14(ReportVIewModel model)
        {

            //if(model.OfficeTypeSearchId>0)
            //{

            //}
            model.AnusuchiFourteenViewModelList = new List<AnusuchiFourteenViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.AnusuchiFourteenViewModelList = RS.spGetAnusuchiFourteen(model.OfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.AnusuchiFourteenViewModelList = RS.spGetAnusuchiFourteen(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }



            return PartialView("_AnusuchiFourteen", model);
        }


        public ActionResult BerujuDetailsTillDate()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.Anusuchi16ViewModelList = new List<Anusuchi16ViewModel>();
            return View(model);

        }

        [HttpPost]
        public PartialViewResult BerujuDetailsTillDate(ReportVIewModel model)
        {


            model.BerujuDetailsTillDateVMList = new List<BerujuDetailsTillDateVM>();
            
            model.BerujuDetailsTillDateVMList = RS.SuperAdmin_TillDateBerujuDetails(model.FiscalYearId);
            //model.OfficeId = CurrentLoginOfficeId;
            //model.OfficeIdForReportHeader = SearchUserOfficeId;
            //model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            //model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            return PartialView("_BerujuTillDateReports", model);
        }


        public ActionResult Anusuchi16()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users         
            return View(model);

        }


        [HttpPost]
        public PartialViewResult Anusuchi16(ReportVIewModel model)
        {

            //if(model.OfficeTypeSearchId>0)
            //{

            //}
            model.Anusuchi16ViewModelList = new List<Anusuchi16ViewModel>();

            if (CurrentLoginUserTypeId == 4)
            {
                model.Anusuchi16ViewModelList = RS.spGetAnusuchiSixteenNew(model.OfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.Anusuchi16ViewModelList = RS.spGetAnusuchiSixteenNew(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }



            return PartialView("_AnusuchiSixteen", model);
        }





        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportHTML(string ExportData)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(ExportData);
                Document PdfFile = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close();
                return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
            }
        }

        public ActionResult ExternalReportTypeWiseAdmin()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);

        }
        public ActionResult ExternalReportTypeWise()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);
        }

        [HttpPost]
        public PartialViewResult ExternalReportTypeWise(ReportVIewModel model)
        {
            //if(model.FiscalYearId==0)
            //{
            //    ViewBag.ErrorMessage = @"कृपया आर्थिक वर्ष छान्नुहोस ।";
            //    return PartialView("_ErrorOfficeNotSelected", model);
            //}

            model.ExternalBerujuRptByTypeViewModelList = new List<ExternalBerujuRptByTypeViewModel>();
            model.BaushiNumberId = 0;
            if (CurrentLoginUserTypeId == 4)
            {
                model.ExternalBerujuRptByTypeViewModelList = RS.Report_PopulateExternalBerujuByTypeID(model.OfficeId, model.FiscalYearId, model.BaushiNumberId, model.BerujuTypeId, model.BerujuSubTitleId, model.BerujuSubTitleChildId);
                model.OfficeIdForReportHeader = model.OfficeId;
                model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
                model.OfficeId = CurrentLoginOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


            }

            else
            {
                int SearchUserOfficeId = CurrentLoginOfficeId;
                if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
                {
                    SearchUserOfficeId = model.OfficeId;
                }

                if (model.OfficeTypeSearchId == 1)//province user
                {

                    if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
                    {
                        SearchUserOfficeId = 7;
                    }
                    else
                    {
                        SearchUserOfficeId = CurrentLoginOfficeId;
                    }


                }
                else if (model.OfficeTypeSearchId == 2)//mantralaya
                {
                    SearchUserOfficeId = model.MininstrySearchId;
                }
                else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
                {
                    SearchUserOfficeId = model.BivagSearchId;
                }
                else if (model.OfficeTypeSearchId == 4)//Karyalaya
                {

                    SearchUserOfficeId = model.NirdeshnalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 5)//AAyog
                {

                    SearchUserOfficeId = model.KaryalayaSearchId;
                }
                else if (model.OfficeTypeSearchId == 6)//LocalLevel
                {
                    SearchUserOfficeId = model.LocalLevelOfficeId;

                }
                else if (model.OfficeTypeSearchId == 7)//sachiwalaya
                {

                }

                //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
                model.ExternalBerujuRptByTypeViewModelList = RS.Report_PopulateExternalBerujuByTypeID(SearchUserOfficeId, model.FiscalYearId, model.BaushiNumberId, model.BerujuTypeId, model.BerujuSubTitleId, model.BerujuSubTitleChildId);
                model.OfficeId = CurrentLoginOfficeId;
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

            }



            return PartialView("_ExternalReportTypeWise", model);
        }


        public ActionResult FilterByAmountReport()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            return View(model);
        }


        [HttpPost]
        public PartialViewResult FilterByAmountReport(ReportVIewModel model)
        {


            model.ExternalBerujuRptByTypeViewModelList = new List<ExternalBerujuRptByTypeViewModel>();
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.MainOfficeId = officeFilter.MainOfficeId;
            var result = RS.FindExternalBerujuByAmount(model);
            model.ExternalBerujuRptByTypeViewModelList = result.Item1;
            model.TotalRecords = result.Item2;
            model.TotalPages = (int)Math.Ceiling((double)model.TotalRecords / model.PageSize);
            model.OfficeIdForReportHeader = SearchUserOfficeId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
            return PartialView("_FilterByAmountReport", model);


        }



        public ActionResult ParameterFilterOfficeJistReport()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.SumamryOrDetailsId = 2;
            return View(model);
        }


        [HttpPost]
        public PartialViewResult ParameterFilterOfficeJistReport(ReportVIewModel model)
        {

                model.ExternalBerujuRptByTypeViewModelList = new List<ExternalBerujuRptByTypeViewModel>();
                var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
                model.BaushiNumberId = 0;
                int SearchUserOfficeId = officeFilter.OfficeId;
                model.OfficeId = SearchUserOfficeId;
                model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
                model.MainOfficeId = officeFilter.MainOfficeId;
                var result = RS.Report_ExternalBeruju_Hierarchy_Final(model);
                model.ExternalBerujuRptByTypeViewModelList = result.Item1;
                model.TotalRecords = result.Item2;
                model.TotalPages = (int)Math.Ceiling((double)model.TotalRecords / model.PageSize);
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
                return PartialView("_ExternalReportFilterTypeOfficeTotalWise", model);

            
        }


        public ActionResult ParameterFilterReport()
        {

            ReportVIewModel model = new ReportVIewModel();
            model.ProvinceIdSearch = CurrentLoginUserProvinceId;
            model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
            model.OfficeId = CurrentLoginOfficeId;
            model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
            model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users
            model.SumamryOrDetailsId = 1;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult ParameterFilterReport(ReportVIewModel model)
        {
                model.ExternalBerujuRptByTypeViewModelList = new List<ExternalBerujuRptByTypeViewModel>();
                var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
                model.BaushiNumberId = 0;
                int SearchUserOfficeId = officeFilter.OfficeId;
                model.OfficeId = SearchUserOfficeId;
                model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
                model.MainOfficeId = officeFilter.MainOfficeId;
                var result = RS.Report_PopulateExternalBerujuByReportFilter(model);
                model.ExternalBerujuRptByTypeViewModelList = result.Item1;
                model.TotalRecords = result.Item2;
                model.TotalPages = (int)Math.Ceiling((double)model.TotalRecords / model.PageSize);
                model.OfficeIdForReportHeader = SearchUserOfficeId;
                model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
                model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;
                return PartialView("_ExternalReportFilterTypeWise", model);

        }


        public ActionResult ExportExternalBeruju(ReportVIewModel model)
        {
            var officeFilter = OfficeFilterHelper.ResolveOfficeFilter(model);
            model.BaushiNumberId = 0;
            int SearchUserOfficeId = officeFilter.OfficeId;
            model.OfficeId = SearchUserOfficeId;
            model.OfficeTypeSearchId = officeFilter.OfficeTypeId;
            model.MainOfficeId = officeFilter.MainOfficeId;
            if (model.SumamryOrDetailsId == 1)
            {
                RS.ExportExternalBerujuToExcel(Response, model);
            }
            else
            {
                RS.ExportExternalBerujuHierarchyToExcel(Response, model);
            }
       

            return new EmptyResult(); // response already handled
        }




        public ActionResult ViewExternalBerujuDetail(int id, int id1)//externalberujuid, officeid
        {
            ExternalBeruju model = new ExternalBeruju();
            InternalBerujuService IBS = new InternalBerujuService();
            model = IBS.ListExternalBerujuByPrimaryId(id1, id);
            model.VoucharDateStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(model.VoucharDate);

            ToWhomDetailListVM newObj = new ToWhomDetailListVM();
            model.ToWhomDetailListVMList = new List<ToWhomDetailListVM>();
            model.ToWhomDetailListVMList = IBS.ListTowhomDetails(id, 2);
            ViewBag.OfficeId = id1;
            return View(model);
        }

        public ActionResult GetOfficeEmployeeRpt()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.OfficeId = CurrentLoginOfficeId;
            model.OfficeChiefsDetailsVMList = new List<OfficeChiefsDetailsVM>();
            model.OfficeChiefsDetailsVMList = RS.sp_GetOfficeChiefDetails(CurrentLoginOfficeId, 1);
            return View(model);

            //return Json(new { success = true,html=GlobalClass.RenderRazorViewToString(this,"viewAll",GetOfficeEmployeeRpt()),message="Saved succcessfully"},JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMalepaPurnaPath()
        {
            ReportVIewModel model = new ReportVIewModel();
            model.OfficeId = CurrentLoginOfficeId;
            model.MalepaPurnaPathVMList = new List<MalepaPurnaPathVM>();
            model.MalepaPurnaPathVMList = RS.Report_GetMalepaPurnaPathDetails(CurrentLoginOfficeId);
            return View(model);
        }

        //public ActionResult BerujuDetailsTillDate()
        //{
        //    ReportVIewModel model = new ReportVIewModel();
        //    model.ProvinceIdSearch = CurrentLoginUserProvinceId;
        //    model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
        //    model.OfficeId = CurrentLoginOfficeId;
        //    model.CurrentLoginUserofficeTypeID = CurrentLoginuserOfficeTypeForDistrict;//static code for district
        //    model.CurrentLoginUserDistrictId = CurrentDistrictId;//District Users         
        //    return View(model);

        //}


        //[HttpPost]
        //public PartialViewResult BerujuDetailsTillDate(ReportVIewModel model)
        //{

        //    //if(model.OfficeTypeSearchId>0)
        //    //{

        //    //}
        //    model.Anusuchi16ViewModelList = new List<Anusuchi16ViewModel>();

        //    if (CurrentLoginUserTypeId == 4)
        //    {
        //        model.Anusuchi16ViewModelList = RS.spGetAnusuchiSixteenNew(model.OfficeId, model.KoshID, model.FiscalYearId);
        //        model.OfficeIdForReportHeader = model.OfficeId;
        //        model.OfficeTypeForReportHeader = Utilities.GetUsertypeByOfficeDetailId(model.OfficeId);
        //        model.OfficeId = CurrentLoginOfficeId;
        //        model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;


        //    }

        //    else
        //    {
        //        int SearchUserOfficeId = CurrentLoginOfficeId;
        //        if (CurrentLoginuserOfficeTypeForDistrict == 101)//static code for district users
        //        {
        //            SearchUserOfficeId = model.OfficeId;
        //        }

        //        if (model.OfficeTypeSearchId == 1)//province user
        //        {

        //            if (CurrentLoginUserTypeId == 8 || CurrentLoginUserTypeId == 9)//this is for karnali beruju only
        //            {
        //                SearchUserOfficeId = 7;
        //            }
        //            else
        //            {
        //                SearchUserOfficeId = CurrentLoginOfficeId;
        //            }


        //        }
        //        else if (model.OfficeTypeSearchId == 2)//mantralaya
        //        {
        //            SearchUserOfficeId = model.MininstrySearchId;
        //        }
        //        else if (model.OfficeTypeSearchId == 3)//Nirdeshanayalay
        //        {
        //            SearchUserOfficeId = model.NirdeshnalayaSearchId;
        //        }
        //        else if (model.OfficeTypeSearchId == 4)//Karyalaya
        //        {

        //            SearchUserOfficeId = model.OfficeId;
        //        }
        //        else if (model.OfficeTypeSearchId == 5)//AAyog
        //        {

        //            SearchUserOfficeId = model.AayogAndOthers;
        //        }
        //        else if (model.OfficeTypeSearchId == 6)//LocalLevel
        //        {
        //            SearchUserOfficeId = model.LocalLevelOfficeId;

        //        }
        //        else if (model.OfficeTypeSearchId == 7)//sachiwalaya
        //        {

        //        }

        //        //model.BerujuLagatKhataVMList = RS.GetReportBerujuLagatKhata(CurrentLoginOfficeId, model.KoshID, model.FiscalYearId);
        //        model.Anusuchi16ViewModelList = RS.spGetAnusuchiSixteenNew(SearchUserOfficeId, model.KoshID, model.FiscalYearId);
        //        model.OfficeId = CurrentLoginOfficeId;
        //        model.OfficeIdForReportHeader = SearchUserOfficeId;
        //        model.CurrentLoginUserTypeviewModel = CurrentLoginUserTypeId;
        //        model.OfficeTypeForReportHeader = CurrentLoginUserTypeId;

        //    }



        //    return PartialView("_AnusuchiSixteen", model);
        //}

    }




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;

namespace _4pix_Beruju.Areas.Admin.Controllers
{
    [Authorize]
    public class AjaxRequestController : Controller
    {

        int CurrentUserOfficeType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

        // GET: Admin/AjaxRequest
        public ActionResult Index()
        {
            return View();
        }

        public class SelectListModel
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }

        public ActionResult GetNirdeshanalayaList(string id)//user type=4
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                List<SelectListModel> collection = new List<SelectListModel>();

                if (CurrentUserOfficeType == 4)
                {
                    // UserType 3: only that user's own office
                    collection = ent.Database.SqlQuery<SelectListModel>(
                        @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
                  FROM OfficeDetail 
                  WHERE UserTypeId = 4 AND OfficeDetailId = @p0", CurrentUserOfficeId).ToList();
                }
                else
                {
                    collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=4 and MainOfficeId='" + id + "'").ToList();

                }


                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetOfficesList(string id)//user type=5
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where MainOfficeId='" + id + "' and UserTypeId=5").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDistrictListByProvinceIdDefaultValue(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select DistrcitCode as Idstr, DistrictNameNep as Title from DistrictSetup where ProvinceId='" + id + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetVDCMUNDDByDistrictIdWithDefaultValue(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + id + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }



        //public ActionResult GetBivagFromMinistryDD(string id)
        //{

        //        using (BerujuEntities ent = new BerujuEntities())
        //        {

        //            List<SelectListItem> ddlList = new List<SelectListItem>();
        //            var collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=3 and MainOfficeId='" + id + "'").ToList();
        //            //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
        //            foreach (var item in collection)
        //            {
        //                ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
        //            }
        //            var ddlSelectOptionList = ddlList;


        //            return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
        //        }



        //}


        public ActionResult GetBivagFromMinistryDD(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                List<SelectListModel> collection = new List<SelectListModel>();

                if (CurrentUserOfficeType == 2)
                {
                    // UserType 2: get all offices under the ministry
                    collection = ent.Database.SqlQuery<SelectListModel>(
                        @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
                  FROM OfficeDetail 
                  WHERE UserTypeId = 3 AND MainOfficeId = @p0", id).ToList();
                }
                else if (CurrentUserOfficeType == 3)
                {
                    // UserType 3: only that user's own office
                    collection = ent.Database.SqlQuery<SelectListModel>(
                        @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
                  FROM OfficeDetail 
                  WHERE UserTypeId = 3 AND OfficeDetailId = @p0", CurrentUserOfficeId).ToList();
                }

                else if (CurrentUserOfficeType == 4)
                {
                    // Step 1: Get parent office of current user's office
                    var parentId = ent.Database.SqlQuery<int>(
                        @"SELECT MainOfficeId 
                          FROM OfficeDetail 
                          WHERE OfficeDetailId = @p0",
                            new object[] { CurrentUserOfficeId })
                             .FirstOrDefault();

                    if (parentId>0)
                    {
                        // Step 2: Get details of that parent office
                        collection = ent.Database.SqlQuery<SelectListModel>(
                            @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
              FROM OfficeDetail 
              WHERE OfficeDetailId = @p0", new object[] { parentId })
                            .ToList();
                    }
                }
                else if (CurrentUserOfficeType == 5)
                {
                    // Step 1: Get parent office of current user's office
                    var parentId = ent.Database.SqlQuery<int>(
                        @"SELECT MainOfficeId 
          FROM OfficeDetail 
          WHERE OfficeDetailId = @p0", new object[] { CurrentUserOfficeId })
                        .FirstOrDefault();

                    if (parentId>0)
                    {
                        // Step 2: Get grandparent (parent of parent) office
                        var grandParentId = ent.Database.SqlQuery<string>(
                            @"SELECT MainOfficeId 
              FROM OfficeDetail 
              WHERE OfficeDetailId = @p0", new object[] { parentId })
                            .FirstOrDefault();

                        if (!string.IsNullOrEmpty(grandParentId))
                        {
                            // Step 3: Get details of grandparent office
                            collection = ent.Database.SqlQuery<SelectListModel>(
                                @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
                  FROM OfficeDetail 
                  WHERE OfficeDetailId = @p0", new object[] { grandParentId })
                                .ToList();
                        }
                    }
                }

                // Convert to SelectListItem
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem
                    {
                        Text = item.Title.ToString(),
                        Value = item.Id.ToString()
                    });
                }

                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetNirdeshanalayFromBivagDD(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                List<SelectListModel> collection = new List<SelectListModel>();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });

                if (CurrentUserOfficeType == 4)
                {
                    // UserType 3: only that user's own office
                    collection = ent.Database.SqlQuery<SelectListModel>(
                        @"SELECT OfficeDetailId AS Id, OfficeName AS Title 
                  FROM OfficeDetail 
                  WHERE UserTypeId = 4 AND OfficeDetailId = @p0", CurrentUserOfficeId).ToList();
                }
                else
                {
                    collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=4 and MainOfficeId='" + id + "'").ToList();

                }

                foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetNirdeshanalayFromMinistryDD(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=3 and MainOfficeId='" + id + "'").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetNirdeshanalayByProvinceId(string id)
        {
            int CurrentLoginUserProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=4 and ProvinceId='" + CurrentLoginUserProvinceId + "' and MainOfficeId='" + id + "'").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBaushiNumberFromFYID(string id, string id1)//Fyid,officeid
        {
            int CurrentLoginUserOfficeId = 0;
            int FiscalYearId = 0;
            if (!string.IsNullOrEmpty(id1))
            {
                CurrentLoginUserOfficeId = Convert.ToInt32(id1);
            }
            if (!string.IsNullOrEmpty(id))
            {
                FiscalYearId = Convert.ToInt32(id);
            }


            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select Distinct (SubTitleCode+'-'+SubTitleName) as Title,BudgetSubTitleId as Id 
                From BudgetSubTitle BST
                inner join ExternalBeruju EB on BST.BudgetSubTitleId=EB.BudgetSubTitle
                where EB.OfficeId='" + CurrentLoginUserOfficeId + "'").ToList();

                if (FiscalYearId > 0)
                {
                    collection = ent.Database.SqlQuery<SelectListModel>(@"select Distinct (SubTitleCode+'-'+SubTitleName) as Title,BudgetSubTitleId as Id 
                From BudgetSubTitle BST
                inner join ExternalBeruju EB on BST.BudgetSubTitleId=EB.BudgetSubTitle
                where EB.FiscalYearId='" + FiscalYearId+"' and EB.OfficeId='" + CurrentLoginUserOfficeId + "'").ToList();
                }

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult GetBerujuSubTitleDD(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select BerujuSubTitleId as Id, SubTitle as Title From BerujuSubTitle where BerujuTypeId='" + id + "'").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        

        public ActionResult GetBerujuSubTitleChildDD(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select BerujuSubTitleChildId as Id, SubTitleChild as Title From BerujuSubTitleChild where BerujuSubTitleId='" + id + "'").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBudgetSubtitleFromOfficeIdFYID(string id)
        {
            int CurrentLoginOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle where FiscalYearId=" + id + " and OfficeId=" + CurrentLoginOfficeId + "").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetBudgetSubtitleFromOfficeIdFYIDForChecker(string id, int officeId)
        {
            int CurrentLoginOfficeId = officeId;
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle where FiscalYearId=" + id + " and OfficeId=" + CurrentLoginOfficeId + "").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult GetTypeListDD(string id, string id1)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select ChaluPujigatId as Id, CONCAT(Code,'-',Titlle) as Title from ChaluPujigat where KoshTypeId='" + id + "' and OfficeId='" + CurrentLoginUserOfficeId + "'").ToList();

                if (id == "1")//Biniyojan with chalu or expense
                {
                    if (id1 != "0")
                    {
                        collection = ent.Database.SqlQuery<SelectListModel>(@"select ChaluPujigatId as Id, CONCAT(Code,'-',Titlle) as Title from ChaluPujigat where KoshTypeId='" + id + "' and ChaluPujiTypeId='" + id1 + "' and OfficeId='" + CurrentLoginUserOfficeId + "'").ToList();
                    }

                }
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetManagerAuditorDetailsDD(int FyID, int OfficeID, int OfficeType)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                //Get From date and to date from fyid......
                DateTime startDate = GetStartEndDateOfFiscalYear(FyID, 1);
                DateTime Endate = GetStartEndDateOfFiscalYear(FyID, 2);
                string startDateFormat = startDate.ToString("yyyy-MM-dd");
                string EndateFormat = Endate.ToString("yyyy-MM-dd");
                List<SelectListItem> ddlList = new List<SelectListItem>();

                var collection = ent.Database.SqlQuery<SelectListModel>(@"select EmployeeAuditorDetailsId as Id, EmpName as Title 
                            from EmployeeAuditorDetails
                            where (FromDuration <='" + EndateFormat + "') and OfficeId='" + OfficeID + "' and EmpType='" + OfficeType + "'").ToList();


                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBasununberForReport(int FyID, int OfficeID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                List<SelectListItem> ddlList = new List<SelectListItem>();

                var collection = ent.Database.SqlQuery<SelectListModel>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle
                where OfficeId='" + OfficeID + "' and FiscalYearId='" + FyID + "' order by DisplayOrder, BudgetSubTitleId").ToList();


                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult GetDashboardreportByFiscalYearId(string id, string id1)//Fyid, officeTypeId
        {
            return PartialView("_DetailReportByFiscalYearID");
        }

        public DateTime GetStartEndDateOfFiscalYear(int FYID, int StartOrEndID)
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                DateTime startOrEnddate = DateTime.Today;
                if (StartOrEndID == 1)
                {
                    startOrEnddate = ent.Database.SqlQuery<DateTime>(@"select StartFrom From FiscalYearRecord
                    where FiscalYearId='" + FYID + "'").FirstOrDefault();

                }
                else
                {
                    startOrEnddate = ent.Database.SqlQuery<DateTime>(@"select EndDate From FiscalYearRecord
                    where FiscalYearId='" + FYID + "'").FirstOrDefault();


                }

                return startOrEnddate;
            }

        }


        public ActionResult GetOfficeUnderCurrentOfficeLevel(string id)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where UserTypeId=3 and MainOfficeId='" + id + "'").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
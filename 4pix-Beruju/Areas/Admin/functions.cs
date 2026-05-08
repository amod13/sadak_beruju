using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using Microsoft.AspNet.Identity;

namespace _4pix_Beruju.Areas.Admin
{
    [Authorize]
    public class functions
    {
        public class SelectListModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        public class OfficeNameAndCodeReturnModel
        {
            public string OfficeName { get; set; }
            public string OfficeCode { get; set; }
        }

        public class SelectListModelFunctionClass
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }

        public static SelectList GetFiscalYearListDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID,FiscalYearTitle as Title from FiscalYearRecord where ShowHide=1 order by DisplayOrder").ToList(), "Id", "Title");
            }

        }


        public static SelectList GetFiscalYearListWithNull()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=1 order by DisplayOrder").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }

        public static SelectList GetFiscalYearListWithDefaultDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=1 order by DisplayOrder").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }

        public static SelectList GetFiscalYearListWithDefaultDDAll()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=1 order by DisplayOrder").ToList();
                ddlList.Add(new SelectListItem { Text = "--All--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }

        public static  List<SelectListItem> GetOfficeTypes()
        {
            return new List<SelectListItem>()
        {
            new SelectListItem { Value = "2", Text = "Ministry" },
            new SelectListItem { Value = "3", Text = "Department" },
            new SelectListItem { Value = "4", Text = "Nirdeshanalaya" },
            new SelectListItem { Value = "5", Text = "Karyalaya" }
        };
        }


        public static List<SelectListItem> GetSampaTo()
        {
            return new List<SelectListItem>()
        {
            new SelectListItem { Value = "2", Text = "म.ले.प" },
            new SelectListItem { Value = "3", Text = "कुमारी चोक" },
            new SelectListItem { Value = "4", Text = "समिती" },
          
        };
        }


        public static string GetSampaToText(int value)
        {
            var list = GetSampaTo();

            var item = list.FirstOrDefault(x => x.Value == value.ToString());

            return item != null ? item.Text : "";
        }


        public static SelectList GetOfficeListDefaultDDAll()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as ID, OFficeName as Title from OfficeDetail order by UserTypeId").ToList();
                ddlList.Add(new SelectListItem { Text = "--All--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }






        public static SelectList GetFiscalYearListWithDefaultDDForRequest()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=0 order by DisplayOrder").ToList();
                ddlList.Add(new SelectListItem { Text = "--All--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }
        public static SelectList GetFiscalYearListDDForRequest()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=0 order by DisplayOrder").ToList();
                //ddlList.Add(new SelectListItem { Text = "--All--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }
        public static SelectList GetFiscalYearListWithoutDefaultDDForRequest()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                //var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord where ShowHide=0 order by DisplayOrder").ToList();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as ID, FiscalYearTitle as Title from FiscalYearRecord  order by DisplayOrder").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");



            }

        }




        public static SelectList GetBudgetSubTitleDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle order by DisplayOrder, BudgetSubTitleId").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBudgetSubTitleDDByOfficeIdAndFYID(int FYID)
        {

            int OFficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle
                where OfficeId='" + OFficeId + "' and FiscalYearId='" + FYID + "' order by DisplayOrder, BudgetSubTitleId").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBudgetSubTitleDDByOfficeIdAndFYID(int FYID, int OfficeId)
        {

            int OFficeId = OfficeId;
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle
                where OfficeId='" + OFficeId + "' and FiscalYearId='" + FYID + "' order by DisplayOrder, BudgetSubTitleId").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBudgetSubTitleDDByOfficeIdAndFYIDForReport(int FYID, int OfficeId)
        {


            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle
                where OfficeId='" + OfficeId + "' and FiscalYearId='" + FYID + "' order by DisplayOrder, BudgetSubTitleId").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBudgetSubTitleWithNull()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                //var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle order by DisplayOrder, BudgetSubTitleId").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }

        }
        public static SelectList GetBudgetSubTitleWithDefaultValue()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                //var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle order by DisplayOrder, BudgetSubTitleId").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0" });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }

        }


        public static SelectList GetBudgetSubTitleWithDefaultDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BudgetSubTitleId as Id, CONCAT(SubTitleCode,'-',SubTitleName) as Title From BudgetSubTitle order by DisplayOrder, BudgetSubTitleId").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }

        }
        public static string GetBudgetSubTitleById(string id)
        {
            string BudgetSubTitleName = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    BudgetSubTitleName = db.Database.SqlQuery<string>(@"select CONCAT(SubTitleCode,' - ',SubTitleName ) From BudgetSubTitle where BudgetSubTitleId=@id", new SqlParameter("@id", id))
                            .FirstOrDefault();

                }
                catch (Exception)
                {

                    return @"-";
                }


            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(BudgetSubTitleName);

        }

        public static string GetBudgetSubTitleById(int id)
        {
            string BudgetSubTitleName = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    BudgetSubTitleName = db.Database.SqlQuery<string>(@"select CONCAT(SubTitleCode,' - ',SubTitleName ) From BudgetSubTitle where BudgetSubTitleId=@id", new SqlParameter("@id", id))
                            .FirstOrDefault();

                }
                catch (Exception)
                {

                    return @"-";
                }


            }

            return BudgetSubTitleName;

        }

        public static SelectList GetBudgetExpenseDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ExpenseTitleId as Id, CONCAT(ExpenseCode,'-',ExpenseTitleName) as Title From ExpenseTitle").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBerujuTypeDefaultDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BerujuTypeId as ID, TYPEName as Title from BerujuType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
                //List<SelectListItem> ddlList = new List<SelectListItem>();
                //return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BerujuTypeId as ID, TYPEName as Title from BerujuType").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetBerujuTypeDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BerujuTypeId as ID, TYPEName as Title from BerujuType").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetToWhomTypeDD()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ToWhomTypeId as ID, TypeName as Title From ToWhomType").ToList(), "Id", "Title");
            }
        }

        public static SelectList GetToWhomTypeDDswithDefault()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ToWhomTypeId as ID, TypeName as Title From ToWhomType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");


            }
        }

        public static SelectList GetToWhomTypeDDForSelect()
        {

            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ToWhomTypeId as ID, TypeName as Title From ToWhomType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });
                //foreach (var item in collection)
                //{
                //    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                //}
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");


            }
        }

        public static SelectList GetKoshTypeDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select KoshTypeId as ID, KoshTypeName as Title From KoshType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }

        public static SelectList GetBaushiNumberNoListDD()
        {


            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });

            var ddlSelectOptionList = ddlList;
            return new SelectList(ddlList.ToList(), "Value", "Text");


        }

        public static SelectList GetKoshTypeDDWithDefaultDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select KoshTypeId as ID, KoshTypeName as Title From KoshType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }

        public static SelectList GetSummaryOrDetailsDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "--संक्षिप्त--", Value = "1" });
                ddlList.Add(new SelectListItem { Text = "--बिस्तृत--", Value = "2" });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }



        public static SelectList GetOfficeOrDetailsDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "--कार्यालयगत--", Value = "1" });
                ddlList.Add(new SelectListItem { Text = "--कायालय तहगत --", Value = "2" });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }



        public static SelectList GetKoshTypeWithoutDefault()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select KoshTypeId as ID, KoshTypeName as Title From KoshType").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }

        public static SelectList GetKoshTypeWithDefault()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select KoshTypeId as ID, KoshTypeName as Title From KoshType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }


        public static SelectList GetKoshTypeWithSaidantikDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select KoshTypeId as ID, KoshTypeName as Title From KoshType").ToList();
                ddlList.Add(new SelectListItem { Text = "--सैदान्तिक--", Value = "6" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }


        [Authorize]
        public static Guid GetCurrentUser()
        {

            try
            {
                var user = HttpContext.Current.User.Identity.GetUserId();
                Guid CurrentUserid = new Guid(user);
                return CurrentUserid;

            }
            catch (Exception)
            {

                return new Guid();
            }






        }



        public static int GetCurrentLoginUserType()
        {
            int CurretnUserTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>("select UserType From AspNetUsers where id=@id", new SqlParameter("@id", UserId))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }

        public static int GetCurrentLoginUserOfficeTypeId()
        {
            int CurretnUserTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>("select OfficeTypeId From AspNetUsers where id=@id", new SqlParameter("@id", UserId))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }


        public static string GetCurrentLoginUserOfficeNameAndCode(int NameOrCode)
        {
            int CurrentloginOfficeId = GetCurrentLoginUserClientId();
            string OfficeName = string.Empty;
            string OfficeCode = string.Empty;
            OfficeNameAndCodeReturnModel model = new OfficeNameAndCodeReturnModel();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    model = db.Database.SqlQuery<OfficeNameAndCodeReturnModel>("select OfficeName, OfficeCode as OfficeCode From OfficeDetail where OfficeDetailId=@id", new SqlParameter("@id", CurrentloginOfficeId)).FirstOrDefault();
                   OfficeName = model.OfficeName;
                    OfficeCode = model.OfficeCode;

                }
                catch (Exception)
                {

                    OfficeName = string.Empty;
                    OfficeCode = string.Empty;
                }

            }
            if (NameOrCode == 1)
            {
                return OfficeName;
            }
            else
            {
                return OfficeCode;
            }
        }

        public static int GetCurrentLoginUserProvinceId()
        {
            int CurretnUserTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>("select ProvinceId From AspNetUsers where id=@id", new SqlParameter("@id", UserId))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }

        public static int GetCurrentLoginUserOfficeTypeForDistrict()
        {
            int CurretnOFficeTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    CurretnOFficeTypeId = db.Database.SqlQuery<int>("select OfficeTypeId From AspNetUsers where id=@id", new SqlParameter("@id", UserId))
                            .FirstOrDefault();
                }
                catch (Exception)
                {

                    CurretnOFficeTypeId = 0;
                }



            }

            return CurretnOFficeTypeId;
        }


        public static int GetDistrictCodeFromExternalBerujuId(int ExternalBerujuID)
        {
            int CurretnOFficeTypeId = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    CurretnOFficeTypeId = db.Database.SqlQuery<int>(@"select cast(DS.DistrcitCode as int) From ExternalBeruju EB
left join OfficeDetail Od on EB.OfficeId=Od.OfficeDetailId
left join DistrictSetup DS on DS.DistrcitCode=Od.DistrictId
where EB.ExternalBerujuId=@id", new SqlParameter("@id", ExternalBerujuID))
                            .FirstOrDefault();
                }
                catch (Exception)
                {

                    CurretnOFficeTypeId = 0;
                }



            }

            return CurretnOFficeTypeId;
        }


        public static string GetBankAndChequeNumberFromEBID(int ExternalBerujuID, int BankOrCheque)
        {
            int DistrictCode = GetDistrictCodeFromExternalBerujuId(ExternalBerujuID);
            string BankOrChequeNumber = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    if (BankOrCheque == 1)
                    {
                        BankOrChequeNumber = db.Database.SqlQuery<string>(@"select BankName From BankChequeDetails
                        where DistrictCode=@id", new SqlParameter("@id", DistrictCode))
                           .FirstOrDefault();
                    }
                    else
                    {
                        BankOrChequeNumber = db.Database.SqlQuery<string>(@"select ChequeNumber From BankChequeDetails
                        where DistrictCode=@id", new SqlParameter("@id", DistrictCode))
                           .FirstOrDefault();
                    }

                }
                catch (Exception)
                {

                    BankOrChequeNumber = string.Empty;
                }



            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(BankOrChequeNumber);
        }

        public static int GetOfficeIdFromExternalBerujuId(int ExternalBerujuId)
        {
            int OfficeIdFromExternalBerujuId = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    OfficeIdFromExternalBerujuId = db.Database.SqlQuery<int>(@"select OfficeId From ExternalBeruju where ExternalBerujuId=@id", new SqlParameter("@id", ExternalBerujuId))
                            .FirstOrDefault();
                }
                catch (Exception)
                {

                    OfficeIdFromExternalBerujuId = 0;
                }



            }

            return OfficeIdFromExternalBerujuId;
        }

        public static string GetCuurentOfficeChiefDetails(int ExternalBerujuID, int NameOrPost)
        {
            int OfficeId = GetOfficeIdFromExternalBerujuId(ExternalBerujuID);
            string ChiefNameOrPost = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    if (NameOrPost == 1)
                    {
                        ChiefNameOrPost = db.Database.SqlQuery<string>(@"select ChiefName From CurrentOfficeChiefDetails
where OfficeId=@id", new SqlParameter("@id", OfficeId))
                           .FirstOrDefault();
                    }
                    else
                    {
                        ChiefNameOrPost = db.Database.SqlQuery<string>(@"select ChiefPost From CurrentOfficeChiefDetails
where OfficeId=@id", new SqlParameter("@id", OfficeId))
                            .FirstOrDefault();
                    }

                }
                catch (Exception)
                {

                    ChiefNameOrPost = string.Empty;
                }



            }

            return ChiefNameOrPost;
        }



        public static int GetCurrentLoginUserClientId()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    Guid UserId = GetCurrentUser();
                    string UserIdstr = UserId.ToString();
                    var Result = ent.Database.SqlQuery<SelectListModel>("GetCurrentLoginUserClientId {0}", UserIdstr).Single();
                    return Convert.ToInt32(Result.Id);

                }

                catch (Exception ex)
                {

                    return 0;
                }


            }

        }


        public static string GetFiscalyearTitleFromId(int FyID)
        {
            string FiscalYearTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                FiscalYearTitle = db.Database.SqlQuery<string>("select FiscalYearTitle as Title From FiscalYearRecord where FiscalYearId=@id", new SqlParameter("@id", FyID))
                            .FirstOrDefault();


            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(FiscalYearTitle);
        }


        public static string GetFiscalyearTitleFromId(int? FyID)
        {
            string FiscalYearTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                FiscalYearTitle = db.Database.SqlQuery<string>("select FiscalYearTitle as Title From FiscalYearRecord where FiscalYearId=@id", new SqlParameter("@id", FyID))
                            .FirstOrDefault();


            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(FiscalYearTitle);
        }

        public static string GetBerujuTypeFromId(int BerujuTypeId)
        {
            string BerujuTypeTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                BerujuTypeTitle = db.Database.SqlQuery<string>("select TypeName as Title from BerujuType where BerujuTypeId=@id", new SqlParameter("@id", BerujuTypeId))
                            .FirstOrDefault();


            }

            return BerujuTypeTitle;
        }

        public static string GetKoshTypeFromId(int KoshTypeId)
        {
            string KoshTypeTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                KoshTypeTitle = db.Database.SqlQuery<string>("select KoshTypeName as Title From KoshType where KoshTypeId=@id", new SqlParameter("@id", KoshTypeId))
                            .FirstOrDefault();


            }

            return KoshTypeTitle;
        }

        public static string ToWhomTypeById(int ToWhomTypeId)
        {
            string ToWhomTypeTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                ToWhomTypeTitle = db.Database.SqlQuery<string>("select TypeName as Title From ToWhomType where ToWhomTypeId=@id", new SqlParameter("@id", ToWhomTypeId))
                            .FirstOrDefault();


            }

            return ToWhomTypeTitle;
        }


        public static decimal? GetSamparikshadAmountTypeWise(int ExternalBeruju, int BerujuTypeId, int SamparikshadId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as TotalAmount From SamparishadDetail
where ExternalBerujuId = @id and BerujuTypeId = @id1 and SamparishadId=@id2", new SqlParameter("@id", ExternalBeruju), new SqlParameter("@id1", BerujuTypeId), new SqlParameter("@id2", SamparikshadId))
                            .FirstOrDefault();
            }

            return decimalval;
        }

        public static decimal? GetSamparikshadSumAmount(int ExternalBeruju, int SamparikshadId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as TotalAmount From SamparishadDetail
where ExternalBerujuId = @id and SamparishadId=@id1", new SqlParameter("@id", ExternalBeruju), new SqlParameter("@id1", SamparikshadId))
                            .FirstOrDefault();
            }

            return decimalval;
        }


        public static decimal? GetRemainingBerujuSam(int ExternalBeruju, int SamparikshadId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(EB.VoucharAmunt-SD.ReviesedVoucherAmount),0) from ExternalBeruju EB
left join SamparishadDetail SD
on SD.ExternalBerujuId=EB.ExternalBerujuId
where EB.ExternalBerujuId = @id and SD.SamparishadId=@id1", new SqlParameter("@id", ExternalBeruju), new SqlParameter("@id1", SamparikshadId))
                            .FirstOrDefault();
            }

            return decimalval;
        }


        public static decimal? GetRemainingBerujuTypeWise(int ExternalBeruju, int BerujuTypeId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(EB.VoucharAmunt-SD.ReviesedVoucherAmount),0) from ExternalBeruju EB
left join SamparishadDetail SD
on SD.ExternalBerujuId=EB.ExternalBerujuId
where EB.ExternalBerujuId=@id and EB.BerujuTypeId=@id1", new SqlParameter("@id", ExternalBeruju), new SqlParameter("@id1", BerujuTypeId))
                            .FirstOrDefault();
            }

            return decimalval;
        }


        public static decimal? GetBakiBerujuAmount(int EBId, int SamparikshadId, int KoshTypeId)
        {
            decimal? ReturnVal = 0;
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {

                    ReturnVal = ent.Database.SqlQuery<decimal>("GetBakiBerujuAmount {0},{1},{2}", EBId, SamparikshadId, KoshTypeId).Single();

                    return ReturnVal;


                }

                catch (Exception)
                {
                    return 0;

                }


            }

        }



        public static string GetInternalOrExternalTitle(int id)
        {
            if (id == 1)
            {
                return @"आन्तरिक";
            }
            else
            {
                return @"अन्तिम";
            }
        }
        public static IEnumerable<SelectListItem> GetInternalOrExternalTitleDD()
        {
            return new SelectList(new[]
            {
                new {Id="2",Value="अन्तिम"},
                new {Id="1",Value="आन्तरिक"},


            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> ChaluOrPujigatDD()
        {
            return new SelectList(new[]
            {   new {Id="0",Value="--छान्नुहोस--"},
                new {Id="1",Value="चालु"},
                new {Id="2",Value="पूँजीगत"},

            }, "Id", "Value");

        }




        public static IEnumerable<SelectListItem> KoshTypeListTitleDefaultDD()
        {
            return new SelectList(new[]
            {   new {Id="0",Value="--छान्नुहोस--"},


            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> KoshTypeListTitleDD(int KoshTypeId)
        {
            int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ChaluPujigatId as Id, CONCAT(Code,'-',Titlle) as Title from ChaluPujigat where KoshTypeId='" + KoshTypeId + "' and OfficeId='" + CurrentLoginUserOfficeId + "'").ToList(), "Id", "Title");
            }

        }


        //for data entry sadak vibag alwasys sadak purvadar at first

        public static IEnumerable<SelectListItem> KoshTypeListTitleByCode(int code)
        {
            int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ChaluPujigatId as Id, CONCAT(Code,'-',Titlle) as Title from ChaluPujigat where KoshTypeId='" + 1 + "' and code='"+ + code + "' and OfficeId='" + CurrentLoginUserOfficeId + "'").ToList(), "Id", "Title");
            }

        }

        public static string GetChaluOrPujigatKharchaTitleBId(int? id)
        {
            string BudgetSubTitleName = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    BudgetSubTitleName = db.Database.SqlQuery<string>(@"select CONCAT(Code,'-',Titlle) as Title from ChaluPujigat where ChaluPujigatId=@id", new SqlParameter("@id", id))
                            .FirstOrDefault();

                }
                catch (Exception)
                {

                    return @"-";
                }


            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(BudgetSubTitleName);

        }

        public static decimal? GetSamparikshadAmountFiscalYearOfficeWise(int OfficeId, int FiscalYearId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(sd.ReviesedVoucherAmount),0) as SamparikshadAmount 
                            From SamparishadDetail sd
                            inner join ExternalBeruju EB on eb.ExternalBerujuId=sd.ExternalBerujuId
                            where EB.OfficeId=@id and EB.FiscalYearId=@id1", new SqlParameter("@id", OfficeId), new SqlParameter("@id1", FiscalYearId))
                            .FirstOrDefault();
            }

            return decimalval;
        }

        public static decimal? GetSamparikshadAmountFiscalYearOfficeWise(int OfficeId, int FiscalYearId, int TowhomTypeId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(sd.ReviesedVoucherAmount),0) as SamparikshadAmount 
                            From SamparishadDetail sd
                            inner join ExternalBeruju EB on eb.ExternalBerujuId=sd.ExternalBerujuId
                            where EB.OfficeId=@id and EB.FiscalYearId=@id1 and EB.ToWhomID=@id2", new SqlParameter("@id", OfficeId), new SqlParameter("@id1", FiscalYearId), new SqlParameter("@id2", TowhomTypeId))
                            .FirstOrDefault();
            }

            return decimalval;

        }

        public static decimal? GetSamparikshadAmountIndividualWise(int OfficeId, int FiscalYearId, int EBTowhomId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(SUM(RevisedAmount),0) as TotalSamparkishadAmount From SamparikshadToWhomDetail STD
                        inner join ExternalBeruju EB on EB.ExternalBerujuId=STD.ExternalBerujuId
                        where STD.EBToWhomId=@id and STD.OfficeId=@id1 and EB.FiscalYearId=@id2", new SqlParameter("@id", EBTowhomId), new SqlParameter("@id1", OfficeId), new SqlParameter("@id2", FiscalYearId))
                            .FirstOrDefault();
            }

            return decimalval;

        }


        public static decimal? GetSamparikshadAmountFiscalYearOfficeChiefWise(int OfficeId, int FiscalYearId, int ChiefId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(sd.ReviesedVoucherAmount),0) as SamparikshadAmount 
                            From SamparishadDetail sd
                            inner join ExternalBeruju EB on eb.ExternalBerujuId=sd.ExternalBerujuId
                            where EB.OfficeId=@id and EB.FiscalYearId=@id1 and EB.OfficeManagerId=@id2", new SqlParameter("@id", OfficeId), new SqlParameter("@id1", FiscalYearId), new SqlParameter("@id2", ChiefId))
                            .FirstOrDefault();
            }

            return decimalval;
        }

        public static decimal? GetSamparikshadAmountFiscalYearAccountHeadWise(int OfficeId, int FiscalYearId, int ChiefId)
        {
            decimal? decimalval = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimalval = db.Database.SqlQuery<decimal>(@"select isnull(sum(sd.ReviesedVoucherAmount),0) as SamparikshadAmount 
                            From SamparishadDetail sd
                            inner join ExternalBeruju EB on eb.ExternalBerujuId=sd.ExternalBerujuId
                            where EB.OfficeId=@id and EB.FiscalYearId=@id1 and EB.AccountantId=@id2", new SqlParameter("@id", OfficeId), new SqlParameter("@id1", FiscalYearId), new SqlParameter("@id2", ChiefId))
                            .FirstOrDefault();
            }

            return decimalval;
        }


        public static SelectList GetOfficeCheifAuditorListDD(int OfficeId, int EmpTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select EmployeeAuditorDetailsId as Id, CONCAT(EmpName,' ,',AuditorPost) as Title From EmployeeAuditorDetails where EmpType='" + EmpTypeId + "' and EmpStatus=1 and OfficeId='" + OfficeId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--सबै--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }
        public static SelectList GetOfficeCheifAuditorListDDDefault(int OfficeId, int EmpTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                //var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select EmployeeAuditorDetailsId as Id, CONCAT(EmpName,' ,',AuditorPost) as Title From EmployeeAuditorDetails where EmpType='" + EmpTypeId + "' and EmpStatus=1 and OfficeId='" + OfficeId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--सबै--", Value = "0" });
                //foreach (var item in collection)
                //{
                //    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                //}
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }




        public static int IScentralOrProvinceLevel()
        {
            int centralOrProvinceLevel = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                centralOrProvinceLevel = db.Database.SqlQuery<int>(@"select isnull(CentralOrProvince,0) as CentralOrProvince From ApplicationDetail ").FirstOrDefault();


            }

            return centralOrProvinceLevel;
        }

        public static int GetCurrentApplicationProvinceId()
        {
            int centralOrProvinceLevel = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                centralOrProvinceLevel = db.Database.SqlQuery<int>(@"select isnull(ProvinceId,0) as ProvinceId From ApplicationDetail ").FirstOrDefault();


            }

            return centralOrProvinceLevel;
        }


        public static int CheckIfBerujuSentToUpperOffice(int SamparikshadReqOfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                //var result = db.Database.SqlQuery<int>(
                //    @"SELECT CASE 
                //    WHEN EXISTS (
                //        SELECT 1 
                //        FROM SamparikshadReqOffice 
                //        WHERE SamparikshadReqOfficeId = @p0
                //        And RequestToMinistry = 1
                //    ) 
                //    THEN 1 ELSE 0 END",
                //    SamparikshadReqOfficeId
                //).FirstOrDefault();

                var result = db.Database.SqlQuery<int>(
                  @"SELECT Top 1 RequestToMinistry  FROM SamparikshadReqOffice 
                        WHERE SamparikshadReqOfficeId = @p0
                            ORDER BY RequestToMinistry desc",
                  SamparikshadReqOfficeId
              ).FirstOrDefault();

                return result;
            }
        }


        public static List<int> GetRequestToMinistryValues(int masterId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                var result = db.Database.SqlQuery<int>(
                    @"SELECT RequestToMinistry  
              FROM SamparikshadReqOffice 
              WHERE MasterId = @p0",
                    masterId
                ).ToList();

                return result;
            }
        }


        //This is for district admin
        public static int GetCurrentLoginUserDistrict(int CurrentLoginUserId)
        {
            //int CurrentLoginUserId = GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                CurrentLoginUserId = db.Database.SqlQuery<int>(@"select DistrictId From OfficeDetail where OfficeDetailId = @id", new SqlParameter("@id", CurrentLoginUserId))
                            .FirstOrDefault();


            }

            return CurrentLoginUserId;
        }


        public static SelectList ExternalOrInternalDDText()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "आन्तरीक", Value = "1" });
                ddlList.Add(new SelectListItem { Text = "अन्तिम", Value = "2" });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }

        }

        public static string GetFiscalyearTitleFromExternalBerujuId(int ExternalBerujuId)
        {
            string FiscalYearTitle = string.Empty; ;
            Guid UserId = GetCurrentUser();
            using (BerujuEntities db = new BerujuEntities())
            {
                FiscalYearTitle = db.Database.SqlQuery<string>(@"select FYR.FiscalYearTitle From ExternalBeruju EB
                                inner join FiscalYearRecord FYR on FYR.FiscalYearId = EB.FiscalYearId
                                where EB.ExternalBerujuId= @id", new SqlParameter("@id", ExternalBerujuId))
                            .FirstOrDefault();


            }

            return FiscalYearTitle;
        }

    }
}
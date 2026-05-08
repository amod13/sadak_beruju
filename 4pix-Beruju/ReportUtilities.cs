using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Models;

namespace _4pix_Beruju
{
    public class ReportUtilities
    {


        public static decimal? GetOfficeWiseKoshTypeWiseAmount(int OfficeId, int BerujuTypeId, int FiscalYearId, int KoshTypeId)
        {
            decimal? ReturnValue = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = KoshTypeId };
                    var FiscalyearIdParam = new SqlParameter { ParameterName = "@FiscalyearId", Value = FiscalYearId };
                    var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = BerujuTypeId };
                    ReturnValue = db.Database.SqlQuery<decimal>("rpt_GetAmountKoshWise @OfficeId,@KoshTypeId,@FiscalyearId,@BerujuTypeId", OfficeIdParam, KoshTypeIdParam, FiscalyearIdParam, BerujuTypeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return ReturnValue;
                }


            }
            return ReturnValue;
        }
        public static decimal? rpt_GetAmountKoshWiseForMinistry(int OfficeId, int BerujuTypeId, int FiscalYearId, int KoshTypeId)
        {
            decimal? ReturnValue = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = KoshTypeId };
                    var FiscalyearIdParam = new SqlParameter { ParameterName = "@FiscalyearId", Value = FiscalYearId };
                    var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = BerujuTypeId };
                    ReturnValue = db.Database.SqlQuery<decimal>("rpt_GetAmountKoshWiseForMinistry @OfficeId,@KoshTypeId,@FiscalyearId,@BerujuTypeId", OfficeIdParam, KoshTypeIdParam, FiscalyearIdParam, BerujuTypeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return ReturnValue;
                }


            }
            return ReturnValue;
        }




        public static decimal? GetSumTotalByKoshTypeId(int OfficeId, int BerujuTypeId, int KoshTypeId)
        {
            decimal? ReturnValue = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = KoshTypeId };
                    var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = BerujuTypeId };
                    ReturnValue = db.Database.SqlQuery<decimal>("rpt_GetSumAmountByKoshTypeId @OfficeId,@KoshTypeId,@BerujuTypeId", OfficeIdParam, KoshTypeIdParam, BerujuTypeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return ReturnValue;
                }


            }
            return ReturnValue;
        }



        public static SelectList GetMinistryList(int ProvinceId)//ministry list type 3
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where ProvinceId='" + ProvinceId + "' and UserTypeId=3").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }



        public static SelectList GetNirdeshanalayListByMinistryId(int MinistryId, int ProvinceId, int UserTypeId)//ministry list type 3
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "' and MainOfficeId='" + MinistryId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetOfficeListByNirdeshanalayId(int ProvinceId, int Nirdeshanalaya)//ministry list type 3
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail where ProvinceId='" + ProvinceId + "' and UserTypeId=3").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        #region TypeThree Report


        public static decimal? GetSamparikshadAmountBerujuTypeWise(int OfficeId, int BerujuTypeId, int FiscalYearId)
        {
            decimal? ReturnValue = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = BerujuTypeId };
                    var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                    ReturnValue = db.Database.SqlQuery<decimal>("RPT_GetSamparikshadAmountBerujuTypeWise @OfficeId,@BerujuTypeId,@FiscalYearId", OfficeIdParam, BerujuTypeIdParam, FiscalYearIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return ReturnValue;
                }


            }
            return ReturnValue;
        }

        public static decimal? Ministry_GetSamparikshadAmountBerujuTypeWise(int OfficeId, int BerujuTypeId, int FiscalYearId)
        {
            decimal? ReturnValue = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = BerujuTypeId };
                    var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                    ReturnValue = db.Database.SqlQuery<decimal>("Ministry_GetSamparikshadAmountBerujuTypeWise @OfficeId,@BerujuTypeId,@FiscalYearId", OfficeIdParam, BerujuTypeIdParam, FiscalYearIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return ReturnValue;
                }


            }
            return ReturnValue;
        }
        #endregion



        public class SelectListModelFunctionClass
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }

        
    }
}
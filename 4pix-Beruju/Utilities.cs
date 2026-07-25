using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;

namespace _4pix_Beruju
{
    public class Utilities
    {
        public class SelectListModelFunctionClass
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }
        public static SelectList GetDistrictByStateId(int StateId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + StateId + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static DateTime GetEnglishDateFromNP(string NepDate)
        {
            DateTime RetrunDate = DateTime.Now;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var NepaliDateParam = new SqlParameter { ParameterName = "@NepaliDate", Value = NepDate };
                    RetrunDate = db.Database.SqlQuery<DateTime>("GetEnglishDateFromNP @NepaliDate", NepaliDateParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return RetrunDate;
                }


            }
            return RetrunDate;
        }


        public static string GetNepaliDateFromEng(DateTime EnglishDate)
        {
            string RetrunDate = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var EnghlisDateParam = new SqlParameter { ParameterName = "@EnglisDate", Value = EnglishDate };
                    RetrunDate = db.Database.SqlQuery<string>("GetNepaliDateFromEng @EnglisDate", EnghlisDateParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return RetrunDate;
                }


            }
            return RetrunDate;
        }

        public static string SPGetNepaliFullDateForReportHeader(DateTime EnglishDate)
        {
            string RetrunDate = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var EnghlisDateParam = new SqlParameter { ParameterName = "@EnglisDate", Value = EnglishDate };
                    RetrunDate = db.Database.SqlQuery<string>("SPGetNepaliFullDateForReportHeader @EnglisDate", EnghlisDateParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return RetrunDate;
                }


            }
            return RetrunDate;
        }


        public static string GetNepaliDateFromEng(DateTime? EnglishDate)
        {
            string RetrunDate = string.Empty;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var EnghlisDateParam = new SqlParameter { ParameterName = "@EnglisDate", Value = EnglishDate };
                    RetrunDate = db.Database.SqlQuery<string>("GetNepaliDateFromEng @EnglisDate", EnghlisDateParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return RetrunDate;
                }


            }
            return RetrunDate;
        }



        public static SelectList GetProvincesDD()
        {
            int CentralOrProvince = _4pix_Beruju.Areas.Admin.functions.IScentralOrProvinceLevel();
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ProvinceId as Id, ProvinceTitleNep as Title From Province").ToList();

                int CurrentProvinceId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserProvinceId();
                if (CentralOrProvince == 1)
                {
                    collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ProvinceId as Id, ProvinceTitleNep as Title From Province where ProvinceId='" + CurrentProvinceId + "'").ToList();

                }

                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetProvincesDDWithoutSelect()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ProvinceId as Id, ProvinceTitleNep as Title From Province").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetDistrictByStateIdDD(int ProvinceId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + ProvinceId + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetDistrictByStateIdDDForOfficeSetup(int ProvinceId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + ProvinceId + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetRuralMunicipalitybyDistrictDD(int DistrictId)
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + DistrictId + "'").ToList(), "Idstr", "Title");
            }

        }


        public static SelectList GetRuralMunicipalitybyDistrictDDDefault(string DistrictCode)
        {

            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select VdcMunCode as Idstr,VdcMunNameEng as Title from VdcMun where DistrictCode='" + DistrictCode + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }


        }


        public static string GetProvinceNameByID(int ProvinceId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string ProvincesName = db.Database.SqlQuery<string>(@"select ProvinceTitleNep From Province where ProvinceId='" + ProvinceId + "'").FirstOrDefault();
                return ProvincesName.ToString();

            }

        }

        public static string GetDistrictNameByDistrctitCode(string DistrictCode)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string DistrictName = db.Database.SqlQuery<string>(@"select DistrictNameNep From DistrictSetup where DistrcitCode='" + DistrictCode + "'").FirstOrDefault();
                return DistrictName.ToString();

            }

        }

        public static string GetVDCNPByVDCCode(string VDCNPCode)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string VDCMUN = db.Database.SqlQuery<string>(@"select VdcMunNameNep From VdcMun where VdcMunCode='" + VDCNPCode + "'").FirstOrDefault();
                return VDCMUN.ToString();

            }

        }


        public static SelectList GetUserTypeDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select UserTypeId as Id, UserTypeName as Title From UserType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetOfficeTypeDDWithoutDefault()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetOfficeTypeDDWithDefaultForSuperAdmin()
        {

            int CurrentLoginUserType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
            using (BerujuEntities ent = new BerujuEntities())
            {

                if (CurrentLoginUserType == 2)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else if(CurrentLoginUserType == 3) {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>2 and OfficeTypeId<>3 and  OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                 else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>2 and OfficeTypeId<>3 and OfficeTypeId<>4 and OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }

            }
        }


        public static SelectList GetOfficeTypeDDWithDefaultForCumulative()
        {

            int CurrentLoginUserType = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserType();
            using (BerujuEntities ent = new BerujuEntities())
            {

                if (CurrentLoginUserType == 2)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6 and OfficeTypeId<>5").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else if(CurrentLoginUserType == 2)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>2 and  OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6 and OfficeTypeId<>5").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>2 and  OfficeTypeId<>1007 and OfficeTypeId<>1 and OfficeTypeId<>6 and OfficeTypeId<>5 and OfficeTypeId<>3").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");

                }

            }
        }



        public static SelectList GetOfficeTypeDDWithDefault()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId<>1007").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetOfficeTypeDDWithoutDefaultForMinistryUser()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeNepali as Title From OfficeType where OfficeTypeId in (2,3,4)").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetOfficeTypeDDWithoutDefaultForNirdeshanayalaUser()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeName as Title From OfficeType where OfficeTypeId in (3,4)").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetOfficeTypForNirdeshanalayaUserReport()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeName as Title From OfficeType where OfficeTypeId in (3)").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetOfficeTypeDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeTypeId as Id, OfficeTypeName as Title From OfficeType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetOfficeListDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"Select OfficeDetailId as Id, OFficeName as Title From OfficeDetail").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetMinistryListForMinistryUserForReport(int ProvinceId, int UserTypeId, int CurrentLoginUserID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "' and OfficeDetailId='" + CurrentLoginUserID + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetMinistryListForMinistryUserForReportWithAll(int ProvinceId, int UserTypeId, int CurrentLoginUserID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "' and OfficeDetailId='" + CurrentLoginUserID + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--सबै--", Value = "0" });

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }




        public static SelectList GetMinistryListWithDefaultForProvinceAdmin(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                if (ProvinceId == 0)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                     Where UserTypeId='" + UserTypeId + "'").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस", Value = "0" });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                    Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();
                    ddlList.Add(new SelectListItem { Text = "--छान्नुहोस", Value = "0" });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }

            }
        }

        public static SelectList GetMinistryList(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                if (ProvinceId == 0)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                     Where UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                    Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }

            }
        }

        public static SelectList GetMinistryListForMinistryUserOnly(int? CurrentLoginUserOFficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                     Where OfficeDetailId='" + CurrentLoginUserOFficeId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");




            }
        }



        public static SelectList GetMinistryListWithDefault(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                if (ProvinceId > 0)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });

                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });

                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                    Where UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }

            }
        }


        //usertype =3
        public static SelectList GetMinistryListForEdit(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                if (ProvinceId > 0)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where ProvinceId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
                else
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                     Where UserTypeId='" + UserTypeId + "'").ToList();

                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                    }
                    var ddlSelectOptionList = ddlList;
                    return new SelectList(ddlList.ToList(), "Value", "Text");
                }
            }
        }//usertype =4
        public static SelectList GetNirdeshayalaListByMinistryIdForReport(int ProvinceId, int UserTypeId, int MinistryId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where MainOfficeId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "' and MainOfficeId='" + MinistryId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetNirdesListByMinisIdForReport(int ProvinceId, int UserTypeId, int MinistryId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                Where UserTypeId='" + UserTypeId + "' and MainOfficeId='" + MinistryId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetNirdeshayalaList(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where MainOfficeId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetNirdeshayalaDefault()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                //var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetNirdeshayalaByMinsistryId(int ProvinceId, int UserTypeId, int MinistryId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                Where UserTypeId='" + UserTypeId + "' and MainOfficeId='" + MinistryId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetNirdeshayalaByMinsistryIdForMinistryUser(int ProvinceId, int UserTypeId, int? MinistryId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
                Where UserTypeId='" + UserTypeId + "' and MainOfficeId='" + MinistryId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }



        public static SelectList GetOfficesByNirdeshanalayaID(int NirdeshanalayaID, int OfficeId, int UserTypeID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                var NirdeshanalayaIDParam = new SqlParameter { ParameterName = "@MainOfficeId", Value = NirdeshanalayaID };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var UserTypeIDParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UserTypeID };
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"GetOfficeLevelHierarchy @MainOfficeId, @OfficeId, @UserTypeId", NirdeshanalayaIDParam, OfficeIdParam, UserTypeIDParam).ToList(), "ID", "Title");

            }
        }


        public static SelectList GetOfficeLevelHierarchyForDistrict(int NirdeshanalayaID, int OfficeId, int UserTypeID, int DistrictId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                var NirdeshanalayaIDParam = new SqlParameter { ParameterName = "@MainOfficeId", Value = NirdeshanalayaID };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var UserTypeIDParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UserTypeID };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"GetOfficeLevelHierarchyForDistrict @MainOfficeId, @OfficeId, @UserTypeId,@DistrictId", NirdeshanalayaIDParam, OfficeIdParam, UserTypeIDParam, DistrictIdParam).ToList(), "ID", "Title");

            }
        }

        public static SelectList GetNirdeshayalaDefaultForEdit(int ProvinceId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                //var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        //usertype =5
        public static SelectList GetOfficesList(int ProvinceId, int UserTypeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as ID as Idstr, OFficeName as Title From OfficeDetail
Where MainOfficeId='" + ProvinceId + "' and UserTypeId='" + UserTypeId + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetOfficeListWithAddress()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName+'-'+[Address] as Title From OfficeDetail
                where UserTypeId in (5,6)").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }



        public static SelectList GetAagyoAndOtherOfficeList(int ProvinceId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where ProvinceId='" + ProvinceId + "' and UserTypeId in (6)").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetLocalLevelForListReport(int ProvinceId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeDetailId as Id, OFficeName as Title From OfficeDetail
Where ProvinceId='" + ProvinceId + "' and UserTypeId in (7)").ToList();
                ddlList.Add(new SelectListItem { Text = "--छान्नुहोस--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetBerujuSubtitleDefaultDD()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });

                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetBerujuSubtitleDD(int BerujuTypeID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                var data = ent.Database.SqlQuery<SelectListModelFunctionClass>(
                    @"SELECT BerujuSubTitleId AS Id, SubTitle AS Title 
              FROM BerujuSubTitle 
              WHERE BerujuTypeId = @p0",
                      BerujuTypeID).ToList();

                var ddlList = data.Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).ToList();

                return new SelectList(ddlList, "Value", "Text");
            }
        }


        public static SelectList GetBerujuSubtitleChildDD(int? BerujuSubTitleID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select BerujuSubTitleChildId as Id, SubTitleChild as Title From BerujuSubTitleChild where BerujuSubTitleId='" + BerujuSubTitleID + "'").ToList();

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static string GetBerujuSubtitleNameFromId(int? BerujuSubTitleId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string SubTitleName = string.Empty;
                try
                {
                    SubTitleName = ent.Database.SqlQuery<string>(@"select SubTitle as Title From BerujuSubTitle where BerujuSubTitleId='" + BerujuSubTitleId + "'").FirstOrDefault();
                    return SubTitleName;
                }
                catch (Exception)
                {

                    return "--";
                }

            }
        }

        public static int GetUsertypeByOfficeDetailId(int OfficeDetailsId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int UserTypeId = 0;
                try
                {
                    UserTypeId = ent.Database.SqlQuery<int>(@"select UserTypeId From OfficeDetail where OfficeDetailId='" + OfficeDetailsId + "'").FirstOrDefault();
                    return UserTypeId;
                }
                catch (Exception)
                {

                    return 0;
                }

            }
        }

        public static ApplicationDetail GetApplicationDetails()
        {
            ApplicationDetail model = new ApplicationDetail();
            using (BerujuEntities db = new BerujuEntities())
            {
                model = db.ApplicationDetail.FirstOrDefault();
                if (model == null)
                {
                    model = new ApplicationDetail();
                }
                return model;
            }
        }

        public static decimal GetSamparikshadIndividualSum(int ExternalberujuId, int ObjectId)
        {
            decimal TotalAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = ExternalberujuId };
                    var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = ObjectId };
                    TotalAmount = db.Database.SqlQuery<decimal>("GetSumRemainingIndividualAmount @ExternalBerujuId,@EBToWhomId", ExternalBerujuIdParam, EBToWhomIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalAmount;
                }


            }
            return TotalAmount;

        }

        public static decimal GetInternalSamparikshadIndividualSum(int InternalberujuId, int ObjectId)
        {
            decimal TotalAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = InternalberujuId };
                    var IBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = ObjectId };
                    TotalAmount = db.Database.SqlQuery<decimal>("IN_GetSumRemainingIndividualAmount InternalBerujuId,@IBToWhomId", InternalBerujuIdParam, IBToWhomIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalAmount;
                }


            }
            return TotalAmount;

        }




        public static decimal GetSamparikshadIndividualSumForRequest(int ExternalberujuId, int ObjectId)
        {
            decimal TotalAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = ExternalberujuId };
                    var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = ObjectId };
                    TotalAmount = db.Database.SqlQuery<decimal>("GetSumRemainingIndividualAmountForRequest @ExternalBerujuId,@EBToWhomId", ExternalBerujuIdParam, EBToWhomIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalAmount;
                }


            }
            return TotalAmount;

        }

        public static decimal IN_GetSumRemainingIndividualAmountForRequest(int InternalberujuId, int ObjectId)
        {
            decimal TotalAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = InternalberujuId };
                    var IBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = ObjectId };
                    TotalAmount = db.Database.SqlQuery<decimal>("IN_GetSumRemainingIndividualAmountForRequest @InternalBerujuId,@IBToWhomId", InternalBerujuIdParam, IBToWhomIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalAmount;
                }


            }
            return TotalAmount;

        }


        public static string GetCurrentLoginOfficeName(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string OfficeName = string.Empty;
                OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                return OfficeName;
            }
        }

        public static string GetCurrentUserEmail()
        {
            Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
            using (BerujuEntities ent = new BerujuEntities())
            {
                string Email = string.Empty;
                Email = ent.Database.SqlQuery<string>(@"select Email From AspNetUsers where Id='" + CurrentLoginUserId + "'").FirstOrDefault();
                return Email;
            }
        }

        public static string GetMinistryNameFromNirdeshanalaya(int MainOfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    int ManatrayalaId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + MainOfficeId + "'").FirstOrDefault();


                    string OfficeName = string.Empty;
                    OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + ManatrayalaId + "'").FirstOrDefault();

                    return OfficeName;
                }
                catch (Exception)
                {

                    return string.Empty;
                }
                
            }
        }


        public static string GetBivagNameFromNirdeshanalaya(int MainOfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    int L3Office = ent.Database.SqlQuery<int>(@"select OfficeDetailId From OfficeDetail where OfficeDetailId='" + MainOfficeId + "'").FirstOrDefault();

                    int L2Office = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + L3Office + "'").FirstOrDefault();

                    string OfficeName = string.Empty;
                    OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + L2Office + "'").FirstOrDefault();

                    return OfficeName;
                }
                catch (Exception)
                {

                    return string.Empty;
                }

            }
        }
        public static string GetCurrentLoginOfficeName(int? OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string OfficeName = string.Empty;
                OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                return OfficeName;
            }
        }

        public static int GetMinistryIdFromBivagId(int BivagID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int MinistryId = 0;
                MinistryId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + BivagID + "'").FirstOrDefault();

                return MinistryId;
            }
        }


        public static int GetBivagIdFromNirdeshanalayId(int NirdeshanalayID)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int BivagId = 0;
                BivagId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + NirdeshanalayID + "'").FirstOrDefault();

                return BivagId;
            }
        }



        public static string GetCurrentLoginOfficeCode(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string OfficeCode = string.Empty;
                OfficeCode = ent.Database.SqlQuery<string>(@"select OfficeCode From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                return EnglishToNepaliNumber(OfficeCode);
            }
        }



        public static string GetCurrentLoginOfficeMinistryName(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string OfficeName = string.Empty;
                try
                {
                    int NirdeshanalayaId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();
                    int MinistryId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + NirdeshanalayaId + "'").FirstOrDefault();
                    if (MinistryId == 2282)
                    {
                        OfficeName = string.Empty;
                    }
                    else
                    {
                        OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + MinistryId + "'").FirstOrDefault();
                    }


                }
                catch (Exception)
                {
                    OfficeName = string.Empty;
                }
                return OfficeName;
            }
        }
        public static string GetCurrentLoginOfficeMainOfficenName(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string OfficeName = string.Empty;
                try
                {
                    int MainOFficeId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();
                    OfficeName = ent.Database.SqlQuery<string>(@"select OFficeName From OfficeDetail where OfficeDetailId='" + MainOFficeId + "'").FirstOrDefault();

                }
                catch (Exception)
                {
                    OfficeName = string.Empty;
                }
                return OfficeName;
            }
        }


        public static int GetCurrentLoginOfficeMainOfficeId(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                int MainOfficeId;
                try
                {
                    MainOfficeId = ent.Database.SqlQuery<int>(@"select MainOfficeId From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                }
                catch (Exception)
                {
                    MainOfficeId = 0;
                }
                return MainOfficeId;
            }
        }

        public static string GetCurrentLoginOfficeHeadingTitle(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    string OfficeHeadingTitle = string.Empty;
                    OfficeHeadingTitle = ent.Database.SqlQuery<string>(@"select OfficeHeadingTitle From OfficeDetail where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                    return OfficeHeadingTitle;
                }
                catch (Exception)
                {

                    return @"नेपाल सरकार";
                }

            }
        }

        public static string GetCurrentLoginOfficeAddress(int OfficeId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    string OfficeHeadingTitle = string.Empty;
                    OfficeHeadingTitle = ent.Database.SqlQuery<string>(@"select [Address] as OfficeAddress From OfficeDetail
                        where OfficeDetailId='" + OfficeId + "'").FirstOrDefault();

                    return OfficeHeadingTitle;
                }
                catch (Exception)
                {

                    return @"कार्यालयको नाम";
                }

            }
        }


        public static SelectList GetSamparikshadletterofficelistdd(int OfficeId, int BerujuId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                // Get BerujuType
                var berujuType = ent.Database.SqlQuery<int>(
                    "SELECT BerujuTypeID FROM dbo.ExternalBeruju WHERE ExternalBerujuId = @p0",
                    BerujuId
                ).FirstOrDefault();

                // Get OfficeType
                var officeType = ent.Database.SqlQuery<int>(
                    "SELECT OfficeTypeId FROM dbo.OfficeDetail WHERE OfficeDetailId = @p0",
                    OfficeId
                ).FirstOrDefault();

                // ✅ Fix: conditional logic
                int toselectofficetype = 0;
                if(officeType == 2 || officeType == 3)
                {
                    toselectofficetype = 2;
                }
                else if (officeType > 3)
                {
                    toselectofficetype = 3;
                }

                List<SelectListItem> ddlList = new List<SelectListItem>();
                List<SelectListModelFunctionClass> collection;

                if (berujuType == 1)
                {
                    collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(
                        @"SELECT SamparikshadLetterSetupId AS Id,
                         CONCAT(OfficeName, ' , ', OfficeAddress) AS Title
                  FROM SamparikshadLetterSetup
                  WHERE SetupStatus = 1 
                  AND (
                         OfficeType = @p0
                   
                    )
                 
                    ",
                        toselectofficetype
                    ).ToList();
                }
                else
                {
                    collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(
                        @"SELECT SamparikshadLetterSetupId AS Id,
                         CONCAT(OfficeName, ' , ', OfficeAddress) AS Title
                  FROM SamparikshadLetterSetup
                  WHERE SetupStatus = 1 
                  AND OfficeType = 0"
                    ).ToList();
                }

                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    });
                }

                return new SelectList(ddlList, "Value", "Text");
            }
        }

        //public static SelectList GetSamparikshadletterofficelistdd(int officeId)
        //{
        //    using (BerujuEntities ent = new BerujuEntities())
        //    {
        //        // Step 1: Get hierarchy office IDs
        //        var offices = ent.Database.SqlQuery<int>(@"
        //                   WITH OfficeHierarchy AS (
        //                        SELECT OfficeDetailId, MainOfficeId
        //                        FROM OfficeDetail
        //                        WHERE OfficeDetailId = @p0

        //                        UNION ALL

        //                        SELECT o.OfficeDetailId, o.MainOfficeId
        //                        FROM OfficeDetail o
        //                        INNER JOIN OfficeHierarchy oh 
        //                            ON o.OfficeDetailId = oh.MainOfficeId
        //                        WHERE oh.MainOfficeId <> 0 
        //                    )
        //                    SELECT OfficeDetailId FROM OfficeHierarchy
        //                ", officeId).ToList();

        //        List<SelectListItem> ddlList = new List<SelectListItem>();

        //        // Step 2: Build dynamic IN clause
        //        if (offices.Any())
        //        {
        //            string ids = string.Join(",", offices);

        //            var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>($@"
        //        SELECT 
        //            SamparikshadLetterSetupId AS Id, 
        //            CONCAT(OfficeName, ' , ', OfficeAddress) AS Title 
        //        FROM SamparikshadLetterSetup 
        //        WHERE SetupStatus = 1  
        //           AND (
        //            OfficeId IN ({ids})
        //            OR OfficeType = 0
        //        )
        //        ORDER BY OfficeType DESC
        //    ").ToList();

        //            foreach (var item in collection)
        //            {
        //                ddlList.Add(new SelectListItem
        //                {
        //                    Text = item.Title,
        //                    Value = item.Id.ToString()
        //                });
        //            }
        //        }

        //        return new SelectList(ddlList, "Value", "Text");
        //    }
        //}

        public static string GetSamparikshadletterofficenameByOfficeId(int letterId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    string OfficeNameTitle = string.Empty;
                    OfficeNameTitle = ent.Database.SqlQuery<string>(@"select OfficeName From SamparikshadLetterSetup where SamparikshadLetterSetupId='" + letterId + "'").FirstOrDefault();

                    return OfficeNameTitle;
                }
                catch (Exception)
                {

                    return @"नेपाल सरकार";
                }

            }
        }


        public static string GetSamparikshadletterofficeaddressByOfficeId(int letterId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                try
                {
                    string OfficeAddressTitle = string.Empty;
                    OfficeAddressTitle = ent.Database.SqlQuery<string>(@"select OfficeAddress From SamparikshadLetterSetup where SamparikshadLetterSetupId='" + letterId + "'").FirstOrDefault();

                    return OfficeAddressTitle;
                }
                catch (Exception)
                {

                    return @"नेपाल सरकार";
                }

            }
        }

        public static string GetTodayNepaliDateForReport()
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string NepaliDate = string.Empty;
                NepaliDate = ent.Database.SqlQuery<string>(@"select [NepaliDate] from [EnglishNepaliDate] where [EnglishDate] =cast(getdate() as date)").FirstOrDefault();

                return NepaliDate;
            }
        }

        public static string SP_GetAuthorizedEmailOfOffice(string UserId)
        {
            string AuthorizedEmail = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {
                var UserIdParam = new SqlParameter { ParameterName = "@UserId", Value = UserId };
                AuthorizedEmail = db.Database.SqlQuery<string>("SP_GetAuthorizedEmailOfOffice @UserId", UserIdParam).FirstOrDefault();
            }
            return AuthorizedEmail;
        }

        public static string GetSamparikshadDetailToFromId(int id)
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
        public static IEnumerable<SelectListItem> GetSamparikshadDetailToFDD()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="महालेखा परीक्षकको कार्यालय"},
                new {Id="2",Value="कुमारी चोक तथा केन्द्रिय तहसिल कार्यालय"},
                new {Id="4",Value="सार्वजनिक लेखा समिति"},
                 new {Id="3",Value="अन्य"},


            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetKoshTypeToFDD()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="विनियोजन"},
                new {Id="3",Value="राजश्व"},
                new {Id="4",Value="धरौटी"},
                 new {Id="5",Value="अन्य कोष"},


            }, "Id", "Value");

        }

        public static string GetTowhomeNameByBerujuId(int BerujuId, int InternalorExternal)
        {
            string ToWhomNames = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {

                ToWhomNames = db.Database.SqlQuery<string>(@"SELECT STRING_AGG(PersonName, ', ') AS TowhomNames
                        FROM ToWhomDetails
                where InternalOrExternal='" + InternalorExternal + "' and InternalOrExternalId='" + BerujuId + "'").FirstOrDefault();

            }

            return ToWhomNames;
        }

        public static DateTime GetStartEndDateFromFiscalYearId(int FYID, int StartorEnd)
        {
            DateTime startOrEndDate = DateTime.Now;
            using (BerujuEntities db = new BerujuEntities())
            {

                if (StartorEnd == 1)//start Date
                {
                    startOrEndDate = db.Database.SqlQuery<DateTime>(@"select StartFrom From FiscalYearRecord
                    where FiscalYearId='" + FYID + "'").FirstOrDefault();
                }
                else
                {
                    startOrEndDate = db.Database.SqlQuery<DateTime>(@"select EndDate From FiscalYearRecord
                        where FiscalYearId='" + FYID + "'").FirstOrDefault();
                }

                return startOrEndDate;


            }
        }


        public static int GetFiscalYearIdFromDate(DateTime date)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                var fiscalYearId = db.Database.SqlQuery<int>(@"
            SELECT FiscalYearId
            FROM FiscalYearRecord
            WHERE @p0 BETWEEN StartFrom AND EndDate", date)
                    .FirstOrDefault();

                return fiscalYearId;
            }
        }

        public static string GetBerujuDafaNumberAndShortDescByBerujuId(int BerujuID)
        {
            string shortDesc = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {

                shortDesc = db.Database.SqlQuery<string>(@"select BerujuShorDesc+','+BerujuNumber as Details from ExternalBeruju
                        where InternalBerujuId='" + BerujuID + "'").FirstOrDefault();
            }

            return shortDesc;
        }

        //For Anushuchi 13
        public static decimal? GetTotalSamparikshadAmountPreviousYear(int OfficeId, int FYID, int ExternalBerujuId)
        {
            decimal ReturnAmount = 0;
            int PreviousFYID = 0;
            DateTime FYStartDate = Utilities.GetStartEndDateFromFiscalYearId(FYID, 1);


            using (BerujuEntities db = new BerujuEntities())
            {
                PreviousFYID = db.Database.SqlQuery<int>(@"select PreFiscalYearId from FiscalYearRecord
                    where FiscalYearId='" + FYID + "'").FirstOrDefault();

                ReturnAmount = db.Database.SqlQuery<decimal>(@"select isnull(ReviesedVoucherAmount,0) as TotalAmount from SamparishadDetail
                where FiscalYearId=@p0 and OfficeId=@p1
                and ExternalBerujuId=@p2", PreviousFYID, OfficeId, ExternalBerujuId).FirstOrDefault();
            }
            return ReturnAmount;

        }

        public static decimal? GetTotalSamparikshadAmountTillCurrentFY(int OfficeId, int FYID, int ExternalBerujuId)
        {
            decimal ReturnAmount = 0;
            int CurrentFYID = 0;
            DateTime? FYStartDate = DateTime.Now;



            using (BerujuEntities db = new BerujuEntities())
            {
                CurrentFYID = db.Database.SqlQuery<int>(@"select FiscalYearId from FiscalYearRecord
                        where IsCurrent=1").FirstOrDefault();

                FYStartDate = GetStartEndDateFromFiscalYearId(CurrentFYID, 1);

                ReturnAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as TotalAmount from SamparishadDetail
                where RevisedDate<@p0 and OfficeId=@p1
                and ExternalBerujuId=@p2", FYStartDate, OfficeId, ExternalBerujuId).FirstOrDefault();
            }
            return ReturnAmount;

        }

        public static decimal? GetTotalSamparikshadAmountCurrentFY(int OfficeId, int FYID, int ExternalBerujuId)
        {
            decimal ReturnAmount = 0;
            int CurrentFYID = 0;
            DateTime? FYStartDate = DateTime.Now;



            using (BerujuEntities db = new BerujuEntities())
            {
                CurrentFYID = db.Database.SqlQuery<int>(@"select FiscalYearId from FiscalYearRecord
                        where IsCurrent=1").FirstOrDefault();

                FYStartDate = GetStartEndDateFromFiscalYearId(CurrentFYID, 1);

                ReturnAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as TotalAmount from SamparishadDetail
                where RevisedDate>=@p0 and OfficeId=@p1
                and ExternalBerujuId=@p2", FYStartDate, OfficeId, ExternalBerujuId).FirstOrDefault();
            }
            return ReturnAmount;

        }




        public static string GetOfficeChiefAndAuditorName(int OfficeId, int EmployeeTypeId)
        {
            string ToWhomNames = string.Empty;
            using (BerujuEntities db = new BerujuEntities())
            {

                ToWhomNames = db.Database.SqlQuery<string>(@"SELECT STRING_AGG(EmpName, ', ') AS EmployeeAuditorDetails
                    FROM EmployeeAuditorDetails where OfficeId=@p0 and EmpType=@p1", OfficeId, EmployeeTypeId).FirstOrDefault();

            }

            return ToWhomNames;
        }


        public static string GetOfficeChiefAndAuditorName(int OfficeId, int EmployeeTypeId, int FYID)
        {
            string ToWhomNames = string.Empty;
            DateTime StartDate = GetStartEndDateFromFiscalYearId(FYID, 1);
            using (BerujuEntities db = new BerujuEntities())
            {

                ToWhomNames = db.Database.SqlQuery<string>(@"SELECT 
	                STRING_AGG(EmpName, ', ') AS EmployeeAuditorDetails	
                FROM EmployeeAuditorDetails where OfficeId=@p0 and EmpType=@p1
                and @p2 between FromDuration and ToDuration", OfficeId, EmployeeTypeId, StartDate).FirstOrDefault();

            }

            return ToWhomNames;
        }





        public static decimal? GetTotalRevisedAmountForRequestAmountOnly(int ExBerujuID, int officeId)//this amount is request amount
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(SUM(RevisedAmount),0) as TotalRequestedAmount From SamparikshadReqToWhomDetail
                where ExternalBerujuId='" + ExBerujuID + "' and OfficeId='" + officeId + "'").FirstOrDefault();
            }

            return totalAmount;
        }


        public static decimal? GetTotalRevisedAmountForRequest(int ExBerujuID)//this amount is from samparikshad
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as TotalRevisedAmount From SamparishadDetail
                                where ExternalBerujuId='" + ExBerujuID + "'").FirstOrDefault();
            }

            return totalAmount;
        }

        public static string GetBerujuShortDescriptionByEBID(int ExternalBerujuId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string SubTitleName = string.Empty;
                try
                {
                    SubTitleName = ent.Database.SqlQuery<string>(@"select BerujuShorDesc From ExternalBeruju
where ExternalBerujuId='" + ExternalBerujuId + "'").FirstOrDefault();
                    return SubTitleName;
                }
                catch (Exception)
                {

                    return "--";
                }

            }
        }


        public static string GetFiscalYearTitleByID(int FiscalYearId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string FiscalYearName = string.Empty;
                try
                {
                    FiscalYearName = ent.Database.SqlQuery<string>(@"select FYR.FiscalYearTitle as Title From 
                                            FiscalYearRecord FYR where FYR.FiscalYearId='" + FiscalYearId + "'").FirstOrDefault();
                    return EnglishToNepaliNumber(FiscalYearName);
                }
                catch (Exception)
                {

                    return "--";
                }

                //return EnglishToNepaliNumber(SubTitleName);

            }
        }


        public static string GetFiscalYearTitleFromExternalBerujuID(int ExternalBerujuId)
        {
            using (BerujuEntities ent = new BerujuEntities())
            {
                string SubTitleName = string.Empty;
                try
                {
                    SubTitleName = ent.Database.SqlQuery<string>(@"select FYR.FiscalYearTitle as Title From ExternalBeruju EB
inner join FiscalYearRecord FYR on EB.FiscalYearId=FYR.FiscalYearId
where EB.ExternalBerujuId='" + ExternalBerujuId + "'").FirstOrDefault();
                    return EnglishToNepaliNumber(SubTitleName);
                }
                catch (Exception)
                {

                    return "--";
                }

                //return EnglishToNepaliNumber(SubTitleName);

            }
        }


        public static int checkIfOfficeAddedInSamparikshadLetter(int OfficeId)
        {
            int totalcount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                totalcount = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From SamparikshadLetterSetup where OfficeId='" + OfficeId + "'").FirstOrDefault();
                return totalcount;

            }

        }


        public static decimal GetSamparikshadreqamoutByIndividualId(int ExternalberujuId, int reqMasterid, int ObjectId)
        {
            decimal TotalAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = ExternalberujuId };
                    var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = ObjectId };
                    var SamparikshadReqIdParam = new SqlParameter { ParameterName = "@SamparikshadReqId", Value = reqMasterid };
                    TotalAmount = db.Database.SqlQuery<decimal>("GetSamparikshadreqamoutByIndividualId @ExternalBerujuId,@EBToWhomId,@SamparikshadReqId", ExternalBerujuIdParam, EBToWhomIdParam, SamparikshadReqIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalAmount;
                }


            }

            return TotalAmount;

        }
        //this is for Samparikshad request form ..Check if beruju amount greater then request amount or not
        public static decimal? GetTotalRequestedAmountForRequest(int ExBerujuID, int FYID)//this amount is request amount
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(SUM(TotalAmount),0) as TotalrequestedAmount From SamparikshadReqMaster
                        where ExternalBerujuId = '" + ExBerujuID + "'").FirstOrDefault();
            }

            return totalAmount;
        }

        public static decimal? GetTotalBerujuAmountForRequest(int ExBerujuID)//this amount is request amount
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select SUM(VoucharAmunt) as TotalVoucherAmount From ExternalBeruju
        where ExternalBerujuId='" + ExBerujuID + "'").FirstOrDefault();
            }

            return totalAmount;
        }

        public static decimal? GetTotalSamparikshadAmount(int ExBerujuID)//this amount is request amount
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(SUM(ReviesedVoucherAmount),0) as TotalReviesedAmount from SamparishadDetail
                where ExternalBerujuId='" + ExBerujuID + "'").FirstOrDefault();
            }

            return totalAmount;
        }

        public static int IsAreadySamparikshadRequest(int ExBerujuID)//this amount is request amount
        {
            int count = 0;
            using (BerujuEntities db = new BerujuEntities())
            {

                count = db.Database.SqlQuery<int>(@"select count(ExternalBerujuId) From SamparikshadReqMaster
                where IsSamparikshadDone is null and ExternalBerujuId='" + ExBerujuID + "'").FirstOrDefault();
            }

            return count;
        }




        public static decimal? GetRemainingOriginalBerujuAmount(int ExBerujuID, int TowhomId)//this amount is request amount
        {
            decimal? totalAmount = 0m;
            using (BerujuEntities db = new BerujuEntities())
            {

                totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(SUM(RevisedAmount),0) as TotalSamparikhadByTowhomId From SamparikshadToWhomDetail
                    where ExternalBerujuId='" + ExBerujuID + "' and EBToWhomId ='" + TowhomId + "'").FirstOrDefault();
            }

            return totalAmount;
        }




        //        select SUM(RevisedAmount) as TotalRequesetedAmount from SamparikshadReqToWhomDetail
        //where SamparikshadReqId in (
        //select SamparikshadReqMasterId From SamparikshadReqMaster where ExternalBerujuId=5125
        //)


        //select SUM(RevisedAmount) as TotalSamparikshad From SamparikshadToWhomDetail
        //where SamparikshadId in (
        //select SamparishadId From SamparishadDetail where ExternalBerujuId=5125
        //)




        public static string EnglishToNepaliNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            return input.Replace('0', '०')
                    .Replace('1', '१')
                    .Replace('2', '२')
                    .Replace('3', '३')
                    .Replace('4', '४')
                    .Replace('5', '५')
                    .Replace('6', '६')
                    .Replace('7', '७')
                    .Replace('8', '८')
                    .Replace('9', '९')
                    .Replace('.', '.');
        }



    }
}
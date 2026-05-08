using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;

namespace _4pix_Beruju
{

    public class DashboardUtility
    {
        public static int GetTotalOfficeByTypeId(int OfficeTypeId, int ProvinceId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
               
                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "'").FirstOrDefault();
                return TotalNumber;

            }
        }

        public static string GetTotalOfficeByTypeIdStr(int OfficeTypeId, int ProvinceId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {

                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalNumber.ToString());

            }
        }


        public static int GetTotalOfficeByTypeIdForMinistryUser(int OfficeTypeId, int ProvinceId, int NirdeshOrOffice)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                int TotalNumber = 0;
                //int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and ProvinceId='"+ProvinceId+"'").FirstOrDefault();
                //return TotalNumber;

                if(NirdeshOrOffice==1)
                {
                    TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as Total From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and MainOfficeId='" + CurrentLoginUserofficeId + "'").FirstOrDefault();

                }
                else
                {
                    TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as Total From OfficeDetail
                    where OfficeTypeId='" + OfficeTypeId+"' and MainOfficeId in (select OfficeDetailId From OfficeDetail where MainOfficeId='"+CurrentLoginUserofficeId+"' and OfficeTypeId=3)").FirstOrDefault();


                }


                return TotalNumber;

            }
        }

        public static int GetTotalOfficeByTypeIdForDistrict(int OfficeTypeId, int ProvinceId,int DistrictId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                //int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and ProvinceId='"+ProvinceId+"'").FirstOrDefault();
                //return TotalNumber;
                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber 
                From OfficeDetail OD where OD.OfficeTypeId='"+OfficeTypeId+"' and OD.DistrictId='" + DistrictId + "'").FirstOrDefault();
                return TotalNumber;

            }
        }

        public static int GetTotalOfficeByHiearchyIdd(int OfficeTypeId,int MainofficeId, int ProvinceId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and ProvinceId=6 and MainOfficeId='"+MainofficeId+"'").FirstOrDefault();
                return TotalNumber;

            }
        }

        public static string GetTotalOfficeByHiearchyIddStr(int OfficeTypeId, int MainofficeId, int ProvinceId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and ProvinceId=6 and MainOfficeId='" + MainofficeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalNumber.ToString());

            }
        }


        public static string GetTotalOfficeByHiearchyIddStr(int OfficeTypeId, int MainofficeId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                int TotalNumber = db.Database.SqlQuery<int>(@"select count(*) as TotalNumber From OfficeDetail where OfficeTypeId='" + OfficeTypeId + "' and MainOfficeId='" + MainofficeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalNumber.ToString());

            }
        }


        public static string GetTotalOfficeForBivagByHiearchyIddStr(int OfficeTypeId, int MainofficeId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

            using (BerujuEntities db = new BerujuEntities())
            {
                int TotalNumber = db.Database.SqlQuery<int>(
                    @"SELECT COUNT(*) 
              FROM OfficeDetail
              WHERE OfficeTypeId = '" + OfficeTypeId + @"'
              AND MainOfficeId IN (
                    SELECT OfficeDetailId 
                    FROM OfficeDetail 
                    WHERE OfficeTypeId = 4
                    AND MainOfficeId = '" + CurrentLoginUserofficeId + @"'
              )"
                ).FirstOrDefault();

                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalNumber.ToString());
            }
        }







        public static decimal GetTotalSumAsBerujuType(int BerujuTypeId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju EB where OfficeId='" + CurrentLoginUserofficeId + "' and BerujuTypeId='" + BerujuTypeId + "'").FirstOrDefault();
                return TotalAmount;

            }
        }
        public static string GetTotalSumAsBerujuTypeStr(int BerujuTypeId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju EB where OfficeId='" + CurrentLoginUserofficeId + "' and BerujuTypeId='" + BerujuTypeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString());

            }
        }


        public static string GetTotalSumAsBerujuOfficeWiseTypeStr(int BerujuTypeId, int searchOfficeId)
        {
            int CurrentLoginUserofficeId = searchOfficeId;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju EB where OfficeId='" + CurrentLoginUserofficeId + "' and BerujuTypeId='" + BerujuTypeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString());

            }
        }


        public static decimal GetTotalSamparikshadAmount()
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as Samparikshadamount from SamparishadDetail where OfficeId='"+CurrentLoginUserofficeId+"'").FirstOrDefault();
                return TotalAmount;

            }
        }

        public static string GetTotalSamparikshadAmountStr()
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as Samparikshadamount from SamparishadDetail where OfficeId='" + CurrentLoginUserofficeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString()) ;

            }
        }

        public static string GetTotalSamparikshadAmountStrByOffice(int searchOfficeId)
        {
            int CurrentLoginUserofficeId = searchOfficeId;
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as Samparikshadamount from SamparishadDetail where OfficeId='" + CurrentLoginUserofficeId + "'").FirstOrDefault();
                return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString());

            }
        }

        public static decimal GetTotalRemainingBerujuAmountByOfficeId()
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuAmountByOfficeId @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }

        public static string GetTotalRemainingBerujuAmountByOfficeIdStr()
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuAmountByOfficeId @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
                }


            }
            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
        }



        public static string GetTotalRemainingBerujuAmountByPassedOfficeIdStr(int officeId)
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = officeId;


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuAmountByOfficeId @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
                }


            }
            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
        }


        public string ChangeUserPassword(string Username)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string msg = string.Empty;
                var UserNameParam = new SqlParameter { ParameterName = "@UserName", Value = Username.Trim() };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangedUserPassword @UserName,@Message OUT",
                    UserNameParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }

        //dashboard for ministry User
        public static decimal Dashboard_GetBerujuSumToMinistryUser(int KoshTypeID)
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = KoshTypeID };

                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("Dashboard_GetBerujuSumToMinistryUser @OfficeId,@KoshTypeId", OfficeIdParam, KoshTypeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }

        public static decimal Dashboard_GetSamparkishadSumToMinistryUser()
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                    

                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("Dashboard_GetSamparkishadSumToMinistryUser @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }

        public static decimal Dashboard_GetRemainingSumToMinistryUser()
        {
            decimal TotalRemBerujuAmount = 0;
            int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                   

                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("Dashboard_GetRemainingSumToMinistryUser @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }
    }
}
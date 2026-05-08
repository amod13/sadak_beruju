using _4pix_Beruju.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Areas.Admin
{
    public class AdminDashboard
    {
        public static decimal GetTotalSumAsBerujuTypeForAdmin(int BerujuTypeId)
        {
            //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            decimal ReturnAmount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    if (BerujuTypeId > 0)
                    {
                        decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju EB where BerujuTypeId='" + BerujuTypeId + "'").FirstOrDefault();
                        ReturnAmount = TotalAmount;
                    }
                    else
                    {
                        decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju").FirstOrDefault();
                        ReturnAmount = TotalAmount;
                    }
                }
                catch (Exception)
                {

                    ReturnAmount = 0m;
                }
                
                

            }
            return ReturnAmount;
        }

        //public static string GetTotalSumAsBerujuTypeForAdminStr(int BerujuTypeId)
        //{
        //    //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        //    decimal ReturnAmount = 0;
        //    using (BerujuEntities db = new BerujuEntities())
        //    {
        //        try
        //        {
        //            if (BerujuTypeId > 0)
        //            {
        //                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju EB where BerujuTypeId='" + BerujuTypeId + "'").FirstOrDefault();
        //                ReturnAmount = TotalAmount;
        //            }
        //            else
        //            {
        //                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(VoucharAmunt),0) as BerujuTypeAmount From ExternalBeruju").FirstOrDefault();
        //                ReturnAmount = TotalAmount;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            ReturnAmount = 0m;
        //        }



        //    }
        //    return _4pix_Beruju.Utilities.EnglishToNepaliNumber(ReturnAmount.ToString());
        //}


        public static string GetTotalSumAsBerujuTypeForAdminStr(int BerujuTypeId)
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            int CurrentLoginUserOfficeTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();

            decimal ReturnAmount = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    // ================= MINISTRY USER =================
                    if (CurrentLoginUserOfficeTypeId == 2)
                    {
                        if (BerujuTypeId > 0)
                        {
                            ReturnAmount = db.Database.SqlQuery<decimal>(
                                @"SELECT ISNULL(SUM(VoucharAmunt),0)
                          FROM ExternalBeruju
                          WHERE BerujuTypeId = '" + BerujuTypeId + "'"
                            ).FirstOrDefault();
                        }
                        else
                        {
                            ReturnAmount = db.Database.SqlQuery<decimal>(
                                @"SELECT ISNULL(SUM(VoucharAmunt),0)
                          FROM ExternalBeruju"
                            ).FirstOrDefault();
                        }
                    }

                    // ================= BIVAG USER =================
                    else if (CurrentLoginUserOfficeTypeId == 3)
                    {
                        if (BerujuTypeId > 0)
                        {
                            ReturnAmount = db.Database.SqlQuery<decimal>(
                                @"SELECT ISNULL(SUM(EB.VoucharAmunt),0)
                          FROM ExternalBeruju EB
                          WHERE EB.BerujuTypeId = '" + BerujuTypeId + @"'
                          AND EB.OfficeId IN (
                                SELECT k.OfficeDetailId
                                FROM OfficeDetail k
                                WHERE k.OfficeTypeId = 5
                                AND k.MainOfficeId IN (
                                    SELECT n.OfficeDetailId
                                    FROM OfficeDetail n
                                    WHERE n.OfficeTypeId = 4
                                    AND n.MainOfficeId = '" + CurrentLoginUserofficeId + @"'
                                )
                          )"
                            ).FirstOrDefault();
                        }
                        else
                        {
                            ReturnAmount = db.Database.SqlQuery<decimal>(
                                @"SELECT ISNULL(SUM(EB.VoucharAmunt),0)
                          FROM ExternalBeruju EB
                          WHERE EB.OfficeId IN (
                                SELECT k.OfficeDetailId
                                FROM OfficeDetail k
                                WHERE k.OfficeTypeId = 5
                                AND k.MainOfficeId IN (
                                    SELECT n.OfficeDetailId
                                    FROM OfficeDetail n
                                    WHERE n.OfficeTypeId = 4
                                    AND n.MainOfficeId = '" + CurrentLoginUserofficeId + @"'
                                )
                          )"
                            ).FirstOrDefault();
                        }
                    }
                }
                catch (Exception)
                {
                    ReturnAmount = 0m;
                }
            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(ReturnAmount.ToString());
        }


        public static decimal GetTotalSumAsBerujuTypeForDistrict(int BerujuTypeId,int DistrictId)
        {
            //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(EB.VoucharAmunt),0) as BerujuTypeAmount 
                            From ExternalBeruju EB
                            inner join OfficeDetail OD on OD.OfficeDetailId=EB.OfficeId
                            where BerujuTypeId='" + BerujuTypeId + "' and OD.DistrictId='" + DistrictId + "'").FirstOrDefault();
                    return TotalAmount;
                }
                catch (Exception e)
                {

                    return 0;
                }
               

            }
        }
        public static decimal GetTotalSamparikshadAmountForAdmin()
        {
            //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as Samparikshadamount from SamparishadDetail").FirstOrDefault();
                return TotalAmount;

            }
        }

        //public static string GetTotalSamparikshadAmountForAdminStr()
        //{
        //    //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        //    using (BerujuEntities db = new BerujuEntities())
        //    {
        //        try
        //        {
        //            decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(ReviesedVoucherAmount),0) as Samparikshadamount from SamparishadDetail").FirstOrDefault();
        //            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString());
        //        }
        //        catch (Exception)
        //        {

        //            return "०";
        //        }


        //    }
        //}


        public static string GetTotalSamparikshadAmountForAdminStr()
        {
            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            int CurrentLoginUserOfficeTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();

            decimal TotalAmount = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    if (CurrentLoginUserOfficeTypeId == 2) // Ministry
                    {
                        TotalAmount = db.Database.SqlQuery<decimal>(
                            @"SELECT ISNULL(SUM(ReviesedVoucherAmount),0) FROM SamparishadDetail"
                        ).FirstOrDefault();
                    }
                    else if (CurrentLoginUserOfficeTypeId == 3) // Bivag
                    {
                        TotalAmount = db.Database.SqlQuery<decimal>(
                            @";WITH OfficeHierarchy AS (
                        SELECT OfficeDetailId
                        FROM OfficeDetail
                        WHERE OfficeDetailId = @OfficeId
                        UNION ALL
                        SELECT od.OfficeDetailId
                        FROM OfficeDetail od
                        INNER JOIN OfficeHierarchy oh ON od.MainOfficeId = oh.OfficeDetailId
                    )
                    SELECT ISNULL(SUM(SD.ReviesedVoucherAmount),0)
                    FROM SamparishadDetail SD
                    WHERE SD.OfficeId IN (SELECT OfficeDetailId FROM OfficeHierarchy)",
                            new SqlParameter("@OfficeId", CurrentLoginUserofficeId)
                        ).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    TotalAmount = 0m;
                }
            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalAmount.ToString());
        }



        public static decimal GetTotalSamparikshadAmountForDistrict(int DistrictId)
        {
            //int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    decimal TotalAmount = db.Database.SqlQuery<decimal>(@"select isnull(sum(SD.ReviesedVoucherAmount),0) as Samparikshadamount 
from SamparishadDetail SD
inner join OfficeDetail OD on OD.OfficeDetailId=SD.OfficeId
where OD.DistrictId='" + DistrictId + "'").FirstOrDefault();
                    return TotalAmount;
                }
                catch (Exception)
                {

                    return 0;
                }
                

            }
        }

        public static decimal GetTotalRemainingBerujuAmountForAdmin()
        {
            decimal TotalRemBerujuAmount = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuAmountByOfficeId @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }


        //public static string GetTotalRemainingBerujuAmountForAdminStr()
        //{
        //    decimal TotalRemBerujuAmount = 0;
        //    //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


        //    using (BerujuEntities db = new BerujuEntities())
        //    {
        //        try
        //        {
        //            var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
        //            TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuAmountByOfficeId @OfficeId", OfficeIdParam).FirstOrDefault();
        //        }
        //        catch (Exception)
        //        {

        //            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
        //        }


        //    }
        //    return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
        //}

        public static string GetTotalRemainingBerujuAmountForAdminStr()
        {
            decimal TotalRemBerujuAmount = 0;

            int CurrentLoginUserofficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            int CurrentLoginUserOfficeTypeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();

            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    SqlParameter OfficeIdParam;

                    // ================= MINISTRY USER =================
                    if (CurrentLoginUserOfficeTypeId == 2)
                    {
                        OfficeIdParam = new SqlParameter
                        {
                            ParameterName = "@OfficeId",
                            Value = 0
                        };
                    }
                    // ================= BIVAG USER =================
                    else if (CurrentLoginUserOfficeTypeId == 3)
                    {
                        OfficeIdParam = new SqlParameter
                        {
                            ParameterName = "@OfficeId",
                            Value = CurrentLoginUserofficeId
                        };
                    }
                    else
                    {
                        OfficeIdParam = new SqlParameter
                        {
                            ParameterName = "@OfficeId",
                            Value = 0
                        };
                    }

                    TotalRemBerujuAmount = db.Database
                        .SqlQuery<decimal>(
                            "GetTotalRemainingBerujuAmountByOfficeId @OfficeId",
                            OfficeIdParam
                        )
                        .FirstOrDefault();
                }
                catch (Exception)
                {
                    return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
                }
            }

            return _4pix_Beruju.Utilities.EnglishToNepaliNumber(TotalRemBerujuAmount.ToString());
        }



        public static decimal GetTotalRemainingBerujuAmountForDistrict(int DistrictId)
        {
            decimal TotalRemBerujuAmount = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                    TotalRemBerujuAmount = db.Database.SqlQuery<decimal>("GetTotalRemainingBerujuForDistrictAdmin @OfficeId,@DistrictId", OfficeIdParam,DistrictIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalRemBerujuAmount;
                }


            }
            return TotalRemBerujuAmount;
        }


        public static int GetTotalSaidantikBerujuSum()
        {
            int TotalSaidantikBeruju = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    TotalSaidantikBeruju = db.Database.SqlQuery<int>("GetTotalSaidantikBerujuSum @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalSaidantikBeruju;
                }


            }
            return TotalSaidantikBeruju;
        }

        public static int GetTotalSaidantikBerujuSumForDistrict(int DistrictId)
        {
            int TotalSaidantikBeruju = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                    TotalSaidantikBeruju = db.Database.SqlQuery<int>("GetTotalSaidantikBerujuSumForDistrict @OfficeId,@DistrictId", OfficeIdParam, DistrictIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalSaidantikBeruju;
                }


            }
            return TotalSaidantikBeruju;
        }

        public static int GetTotalBerujuNotDone()
        {
            int TotalSaidantikBeruju = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    TotalSaidantikBeruju = db.Database.SqlQuery<int>("GetTotalBerujuNotDoneSum @OfficeId", OfficeIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalSaidantikBeruju;
                }


            }
            return TotalSaidantikBeruju;
        }

        public static int GetTotalBerujuNotDoneForDistrict(int DistrictId)
        {
            int TotalSaidantikBeruju = 0;
            //int OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();


            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = 0 };
                    var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                    TotalSaidantikBeruju = db.Database.SqlQuery<int>("GetTotalBerujuNotDoneSumForDistrict @OfficeId,@DistrictId", OfficeIdParam, DistrictIdParam).FirstOrDefault();
                }
                catch (Exception)
                {

                    return TotalSaidantikBeruju;
                }


            }
            return TotalSaidantikBeruju;
        }
    }
}
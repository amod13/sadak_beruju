using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Helpers;
using _4pix_Beruju.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace _4pix_Beruju.Services
{
    public class ReportService
    {
        public List<BerujuLagatKhataVM> GetReportBerujuLagatKhata(int OfficeId, int KoshId, int FiscalYearId)
        {
            List<BerujuLagatKhataVM> returnList = new List<BerujuLagatKhataVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuLagatKhataVM>("GetReportBerujuLagatKhata {0},{1},{2}", OfficeId, KoshId, FiscalYearId).ToList();

            }
            return returnList;
        }


        public List<FYListViewModel> GetFiscalYearList()
        {
            List<FYListViewModel> returnList = new List<FYListViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<FYListViewModel>("select FiscalYearId, FiscalYearTitle from FiscalYearRecord").ToList();

            }
            return returnList;
        }

        public List<FYListViewModel> GetFiscalYearList(int FyId)
        {
            List<FYListViewModel> returnList = new List<FYListViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                if (FyId == 0)
                {
                    returnList = db.Database.SqlQuery<FYListViewModel>("select FiscalYearId, FiscalYearTitle from FiscalYearRecord").ToList();
                }
                else
                {
                    returnList = db.Database.SqlQuery<FYListViewModel>("select FiscalYearId, FiscalYearTitle from FiscalYearRecord where FiscalYearId='" + FyId + "'").ToList();
                }


            }
            return returnList;
        }

        public List<BerujuTypeViewModel> GetBerujuTypeList()
        {
            List<BerujuTypeViewModel> returnList = new List<BerujuTypeViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuTypeViewModel>("select BerujuTypeId,TypeName From BerujuType").ToList();

            }
            return returnList;
        }

        public List<KendriyaKaralayagtLaagatViewModel> KendriyaKaralayagtLaagat(int OfficeId, int FiscalYearId)
        {
            List<KendriyaKaralayagtLaagatViewModel> returnList = new List<KendriyaKaralayagtLaagatViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<KendriyaKaralayagtLaagatViewModel>("Rpt_KendriyaKaralayagtLaagat {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }


        public List<BerujuSampaReportModel> BerujuSampaJistReport(int OfficeId,int OfficeTypeId, int FiscalYearId)
        {
            List<BerujuSampaReportModel> returnList = new List<BerujuSampaReportModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuSampaReportModel>("Rpt_GetBerujuSampaJistReport {0},{1},{2}", OfficeId,OfficeTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }


        public List<BerujuSampaReportModel> BerujuSampaJistReportAdmin(int OfficeId, int OfficeTypeId, int FiscalYearId)
        {
            List<BerujuSampaReportModel> returnList = new List<BerujuSampaReportModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuSampaReportModel>("Rpt_GetBerujuSampaJistReportAdmin {0},{1},{2}", OfficeId, OfficeTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        // sp_FindExternalBerujuByAmount

        public List<BerujuFurcheutSampaReportModel> BerujuFurcheutToOfficeAndSampaJistReport(int OfficeId, int OfficeTypeId, int FiscalYearId)
        {
            List<BerujuFurcheutSampaReportModel> returnList = new List<BerujuFurcheutSampaReportModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuFurcheutSampaReportModel>("Rpt_GetBerujuFurcheutToOfficeAndSampaJistReport {0},{1},{2}", OfficeId, OfficeTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }
        

        public List<BerujuSampaReportModel> BerujuFurcheutJistReport(int OfficeId, int OfficeTypeId, int FiscalYearId)
        {
            List<BerujuSampaReportModel> returnList = new List<BerujuSampaReportModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuSampaReportModel>("Rpt_GetBerujuFurcheutJistReport {0},{1},{2}", OfficeId, OfficeTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<BerujuSampaReportModel> BerujuFurcheutJistReportAdmin(int OfficeId, int OfficeTypeId, int FiscalYearId)
        {
            List<BerujuSampaReportModel> returnList = new List<BerujuSampaReportModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuSampaReportModel>("Rpt_GetBerujuFurcheutJistReportAdmin {0},{1},{2}", OfficeId, OfficeTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }






        public List<KendriyaKaralayagtLaagatViewModel> ministry_Get803Reports(int OfficeId, int FiscalYearId)
        {
            List<KendriyaKaralayagtLaagatViewModel> returnList = new List<KendriyaKaralayagtLaagatViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<KendriyaKaralayagtLaagatViewModel>("ministry_Get803Reports {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<SamparikshadGausharaKhataVM> GetSamparikshadGausharaKhata(int OfficeId, int FiscalYearId, int KoshTypeId)
        {
            List<SamparikshadGausharaKhataVM> returnList = new List<SamparikshadGausharaKhataVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SamparikshadGausharaKhataVM>("GetSamparikshadGausharaKhata {0},{1},{2}", OfficeId, FiscalYearId, KoshTypeId).ToList();

            }
            return returnList;
        }

        public List<DashboardColumnChartViewModel> GetChartData(int OfficeId)
        {
            List<DashboardColumnChartViewModel> returnList = new List<DashboardColumnChartViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardColumnChartViewModel>("DashboardColumnChart {0}", OfficeId).ToList();

            }
            return returnList;
        }

        public List<DashboardColumnChartViewModel> DashboardColumnChartForMinistry(int OfficeId)
        {
            List<DashboardColumnChartViewModel> returnList = new List<DashboardColumnChartViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardColumnChartViewModel>("DashboardColumnChartForMinistry {0}", OfficeId).ToList();

            }
            return returnList;
        }
        public List<DashboardPieChart> DashboardPieChartForLocalLevel(int OfficeId)
        {
            List<DashboardPieChart> returnList = new List<DashboardPieChart>();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    returnList = db.Database.SqlQuery<DashboardPieChart>("DashboardPieChartForLocalLevel {0}", OfficeId).ToList();

                }
                catch (Exception)
                {

                    returnList = new List<DashboardPieChart>();
                }

            }
            return returnList;
        }


        public List<DashboardPieChart> DashboardPieChartForMinistry(int OfficeId)
        {
            List<DashboardPieChart> returnList = new List<DashboardPieChart>();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    returnList = db.Database.SqlQuery<DashboardPieChart>("DashboardPieChartForMinistry {0}", OfficeId).ToList();

                }
                catch (Exception)
                {

                    returnList = new List<DashboardPieChart>();
                }

            }
            return returnList;
        }

        public List<DashboardPieChart> DashboardPieChartForNirdeshanalaya(int OfficeId)
        {
            List<DashboardPieChart> returnList = new List<DashboardPieChart>();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    returnList = db.Database.SqlQuery<DashboardPieChart>("DashboardPieChartForMinistry {0}", OfficeId).ToList();

                }
                catch (Exception)
                {

                    returnList = new List<DashboardPieChart>();
                }

            }
            return returnList;
        }

        public List<DashboardPieChart> DashboardPieChartForDistrictUser(int OfficeId, int DistrictId)
        {
            List<DashboardPieChart> returnList = new List<DashboardPieChart>();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    returnList = db.Database.SqlQuery<DashboardPieChart>("DashboardPieChartForDistrict {0},{1}", OfficeId, DistrictId).ToList();

                }
                catch (Exception)
                {

                    returnList = new List<DashboardPieChart>();
                }

            }
            return returnList;
        }


        public List<DashboardkoshwiseTable> DashboardKoshwiseSumList(int OfficeId)
        {
            List<DashboardkoshwiseTable> returnList = new List<DashboardkoshwiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardkoshwiseTable>("DashboardKoshwiseSumList {0}", OfficeId).ToList();

            }
            return returnList;
        }


        public List<DashboardkoshwiseTable> DashboardKoshwiseCumulativeSumList(int OfficeId)
        {
            List<DashboardkoshwiseTable> returnList = new List<DashboardkoshwiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardkoshwiseTable>("DashboardKoshwiseCumulativeSumList {0}", OfficeId).ToList();

            }
            return returnList;
        }

        public DashboardOfficeCountVM GetOfficeCountByLoginOffice(int officeId)
        {
            using (var db = new BerujuEntities())
            {
                return db.Database
                         .SqlQuery<DashboardOfficeCountVM>(
                             "EXEC dbo.SP_GetOfficeCountByLoginOffice @OfficeId",
                             new SqlParameter("@OfficeId", officeId)
                         )
                         .FirstOrDefault() ?? new DashboardOfficeCountVM();
            }
        }



        public List<DashboardBerujuTypewiseTable> DashboardGetExternalBerujuSumByOfficeHierarchy(int OfficeId)
        {
            List<DashboardBerujuTypewiseTable> returnList = new List<DashboardBerujuTypewiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {

                //return db.Database.SqlQuery<DashboardBerujuTypewiseTable>(
                //    "EXEC dbo.Dashboard_GetExternalBerujuSumByOfficeHierarchy @OfficeId",
                //    new SqlParameter("@OfficeId", OfficeId)
                //).ToList();

              returnList = db.Database.SqlQuery<DashboardBerujuTypewiseTable>("Dashboard_GetExternalBerujuSumByOfficeHierarchy {0}", OfficeId).ToList();

            }
            return returnList;
        }


        public List<DashboardBerujuTypewiseTable> DashboardGetBerujuSumByOfficeOnly(int OfficeId)
        {
            List<DashboardBerujuTypewiseTable> returnList = new List<DashboardBerujuTypewiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {

                //return db.Database.SqlQuery<DashboardBerujuTypewiseTable>(
                //    "EXEC dbo.Dashboard_GetExternalBerujuSumByOfficeHierarchy @OfficeId",
                //    new SqlParameter("@OfficeId", OfficeId)
                //).ToList();

                returnList = db.Database.SqlQuery<DashboardBerujuTypewiseTable>("DashboardGetBerujuSumByOfficeOnly {0}", OfficeId).ToList();

            }
            return returnList;
        }

        public MakerDashboardViewModel GetMakerDashboardCounts()
        {
            using (var db = new BerujuEntities())
            {
                var result = db.Database.SqlQuery<MakerDashboardViewModel>(
                    "EXEC DashboardCountsForMaker"
                ).FirstOrDefault();

                return result ?? new MakerDashboardViewModel();
            }
        }

        public List<DashboardkoshwiseTable> DashboardKoshwiseSumListForMinistryUser(int OfficeId)
        {
            List<DashboardkoshwiseTable> returnList = new List<DashboardkoshwiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardkoshwiseTable>("DashboardKoshwiseSumListForMinistryUser {0}", OfficeId).ToList();

            }
            return returnList;
        }

        public List<DashboardkoshwiseTable> DashboardKoshwiseSumListForDistrictOnly(int OfficeId, int DistrictId)
        {
            List<DashboardkoshwiseTable> returnList = new List<DashboardkoshwiseTable>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardkoshwiseTable>("DashboardKoshwiseSumListForDistrict {0},{1}", OfficeId, DistrictId).ToList();

            }
            return returnList;
        }

        public List<DashboardOfficesGetSumBerujuTypeWiseViewModel> DashboardOfficesGetSumBerujuTypeWise(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<DashboardOfficesGetSumBerujuTypeWiseViewModel> returnList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                //updated on april 2 2023
                //returnList = db.Database.SqlQuery<DashboardOfficesGetSumBerujuTypeWiseViewModel>("DashboardOfficesGetSumBerujuTypeWise {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, MainofficeId).ToList();
                returnList = db.Database.SqlQuery<DashboardOfficesGetSumBerujuTypeWiseViewModel>("DashboardOfficesGetSumBerujuTypeWiseUpdated {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, MainofficeId).ToList();

            }
            return returnList;
        }

        public List<DashboardOfficesGetSumBerujuTypeWiseViewModel> DashboardBerujuTypeWiseForMinistry(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<DashboardOfficesGetSumBerujuTypeWiseViewModel> returnList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardOfficesGetSumBerujuTypeWiseViewModel>("DashboardBerujuTypeWiseForMinistry {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, MainofficeId).ToList();

            }
            return returnList;
        }

        public List<DashboardOfficesGetSumBerujuTypeWiseViewModel> DashboardBerujuTypeWiseForNirdesh(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<DashboardOfficesGetSumBerujuTypeWiseViewModel> returnList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardOfficesGetSumBerujuTypeWiseViewModel>("DashboardBerujuTypeWiseForNirdesh {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, MainofficeId).ToList();

            }
            return returnList;
        }

        public List<DashboardOfficesGetSumBerujuTypeWiseViewModel> DashboardOfficesGetSumBerujuTypeWiseForDistrict(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId, int DistrictId)
        {
            List<DashboardOfficesGetSumBerujuTypeWiseViewModel> returnList = new List<DashboardOfficesGetSumBerujuTypeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<DashboardOfficesGetSumBerujuTypeWiseViewModel>("DashboardOfficesGetSumBerujuTypeWiseForDistrict {0},{1},{2},{3},{4}", UserTypeId, FiscalYearId, ProvinceId, MainofficeId, DistrictId).ToList();

            }
            return returnList;
        }
        public List<AntimBerujuDetailsRptViewModel> SPRPT_GetRptKoshtypewise(int OfficeId, int FiscalYearId)
        {
            List<AntimBerujuDetailsRptViewModel> returnList = new List<AntimBerujuDetailsRptViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AntimBerujuDetailsRptViewModel>("SPRPT_GetRptKoshtypewise {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<AntimBerujuTowhomTypeWiseRptViewModel> SPRPT_GetFinalBerujuByToWhomType(int OfficeId, int FiscalYearId, int TowhomTypeId)
        {
            List<AntimBerujuTowhomTypeWiseRptViewModel> returnList = new List<AntimBerujuTowhomTypeWiseRptViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AntimBerujuTowhomTypeWiseRptViewModel>("SPRPT_GetFinalBerujuByToWhomType {0},{1},{2}", OfficeId, FiscalYearId, TowhomTypeId).ToList();

            }
            return returnList;
        }

        public List<AntimBerujuTowhomTypeWiseRptViewModel> SPRPT_GetFinalBerujuByToWhomTypeDetail(int OfficeId, int FiscalYearId, int TowhomTypeId)
        {
            List<AntimBerujuTowhomTypeWiseRptViewModel> returnList = new List<AntimBerujuTowhomTypeWiseRptViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AntimBerujuTowhomTypeWiseRptViewModel>("SPRPT_GetFinalBerujuByToWhomTypeDetail {0},{1},{2}", OfficeId, FiscalYearId, TowhomTypeId).ToList();

            }
            return returnList;
        }


        public List<AntimBerujuOfficeChiefWiseRptViewModel> SPRPT_GetFinalBerujuByOfficeChiefWise(int OfficeId, int FiscalYearId, int OfficeChiefId)
        {
            List<AntimBerujuOfficeChiefWiseRptViewModel> returnList = new List<AntimBerujuOfficeChiefWiseRptViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AntimBerujuOfficeChiefWiseRptViewModel>("SPRPT_GetFinalBerujuByOfficeChiefWise {0},{1},{2}", OfficeId, FiscalYearId, OfficeChiefId).ToList();

            }
            return returnList;
        }

        public List<AntimBerujuOfficeChiefWiseRptViewModel> SPRPT_GetFinalBerujuByOfficeFinancHeadWise(int OfficeId, int FiscalYearId, int OfficeChiefId)
        {
            List<AntimBerujuOfficeChiefWiseRptViewModel> returnList = new List<AntimBerujuOfficeChiefWiseRptViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AntimBerujuOfficeChiefWiseRptViewModel>("SPRPT_GetFinalBerujuByOfficeFinancHeadWise {0},{1},{2}", OfficeId, FiscalYearId, OfficeChiefId).ToList();

            }
            return returnList;
        }



        //        select isnull(sum(ReviesedVoucherAmount),0) as RevisedAmount from SamparishadDetail sd
        //right join FiscalYearRecord fd on sd.FiscalYearId=fd.FiscalYearId
        //group by fd.FiscalYearTitle
        //order by fd.FiscalYearTitle


        //select isnull(sum(sd.VoucharAmunt),0) as RevisedAmount from ExternalBeruju sd
        //right join FiscalYearRecord fd on sd.FiscalYearId=fd.FiscalYearId
        //group by fd.FiscalYearTitle
        //order by fd.FiscalYearTitle

        //select FiscalYearTitle From FiscalYearRecord
        //order by FiscalYearTitle
        //https://www.c-sharpcorner.com/article/asp-net-mvc5-google-charts-api-integration/

        public List<SaidantikCountOfficeWiseViewModel> SP_GetSaidantikBerujuCountByOfficeType(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<SaidantikCountOfficeWiseViewModel> returnList = new List<SaidantikCountOfficeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SaidantikCountOfficeWiseViewModel>("SP_GetSaidantikBerujuCountByOfficeType {0},{1},{2}", UserTypeId, FiscalYearId, ProvinceId).ToList();

            }
            return returnList;
        }


        public List<SamparikshadDetailByTypeVM> SP_GetSamparikshadDetailsForDashboard(int UserTypeId, int FiscalYearId)
        {
            List<SamparikshadDetailByTypeVM> returnList = new List<SamparikshadDetailByTypeVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SamparikshadDetailByTypeVM>("SP_GetSamparikshadDetailsForDashboard {0},{1}", UserTypeId, FiscalYearId).ToList();

            }
            return returnList;
        }
        public List<Admin_GetSaidaintikBerujuListByOfficeIdVM> Admin_GetSaidaintikBerujuListByOfficeId(int OfficeId, int FiscalYearId)
        {
            List<Admin_GetSaidaintikBerujuListByOfficeIdVM> returnList = new List<Admin_GetSaidaintikBerujuListByOfficeIdVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<Admin_GetSaidaintikBerujuListByOfficeIdVM>("Admin_GetSaidaintikBerujuListByOfficeId {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<SaidantikCountOfficeWiseViewModel> SP_GetSaidantikBerujuCountByOfficeTypeForDistrict(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId, int DistrictId)
        {
            List<SaidantikCountOfficeWiseViewModel> returnList = new List<SaidantikCountOfficeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SaidantikCountOfficeWiseViewModel>("SP_GetSaidantikBerujuCountByOfficeTypeForDistrict {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, DistrictId).ToList();

            }
            return returnList;
        }

        public List<SaidantikCountOfficeWiseViewModel> SP_GetBerujuNotDoneCountByOfficeType(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<SaidantikCountOfficeWiseViewModel> returnList = new List<SaidantikCountOfficeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SaidantikCountOfficeWiseViewModel>("SP_GetBerujuNotDoneCountByOfficeType {0},{1},{2}", UserTypeId, FiscalYearId, ProvinceId).ToList();

            }
            return returnList;
        }

        public List<IntExtSamparikshadCountVM> SP_GetSamparikshadCountByOfficeType(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<IntExtSamparikshadCountVM> returnList = new List<IntExtSamparikshadCountVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<IntExtSamparikshadCountVM>("SP_GetSamparikshadCountByOfficeType {0},{1},{2}", UserTypeId, FiscalYearId, ProvinceId).ToList();

            }
            return returnList;
        }

        public List<IntExtSamparikshadCountVM> SP_GetInternalSamparikshadCountByOfficeType(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<IntExtSamparikshadCountVM> returnList = new List<IntExtSamparikshadCountVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<IntExtSamparikshadCountVM>("SP_GetInternalSamparikshadCountByOfficeType {0},{1},{2}", UserTypeId, FiscalYearId, ProvinceId).ToList();

            }
            return returnList;
        }

        public List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM> Admin_GetInternalSamparikshadListByOfficeId(int OfficeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM> returnList = new List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<Admin_GetInternalExternalSamparikshadListByOfficeIdVM>("Admin_GetInternalSamparikshadListByOfficeId {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM> Admin_GetSamparikshadListByOfficeId(int OfficeId, int FiscalYearId, int ProvinceId, int MainofficeId)
        {
            List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM> returnList = new List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<Admin_GetInternalExternalSamparikshadListByOfficeIdVM>("Admin_GetSamparikshadListByOfficeId {0},{1}", OfficeId, FiscalYearId).ToList();

            }
            return returnList;
        }


        public List<Admin_BerujuNotDoneListByOfficeIdVM> Admin_BerujuNotDoneListByOfficeId(int OfficeID, int FiscalYearId)
        {
            List<Admin_BerujuNotDoneListByOfficeIdVM> returnList = new List<Admin_BerujuNotDoneListByOfficeIdVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<Admin_BerujuNotDoneListByOfficeIdVM>("Admin_BerujuNotDoneListByOfficeId {0},{1}", OfficeID, FiscalYearId).ToList();

            }
            return returnList;
        }

        public List<SaidantikCountOfficeWiseViewModel> SP_GetBerujuNotDoneCountByOfficeTypeForDistrict(int UserTypeId, int FiscalYearId, int ProvinceId, int MainofficeId, int DistrictId)
        {
            List<SaidantikCountOfficeWiseViewModel> returnList = new List<SaidantikCountOfficeWiseViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<SaidantikCountOfficeWiseViewModel>("SP_GetBerujuNotDoneCountByOfficeTypeForDistrict {0},{1},{2},{3}", UserTypeId, FiscalYearId, ProvinceId, DistrictId).ToList();

            }
            return returnList;
        }

        //public List<ExternalBerujuRptByTypeViewModel> Report_PopulateExternalBerujuByReportFilter(ReportVIewModel model)
        //{
        //    using (BerujuEntities db = new BerujuEntities())
        //    {
        //        try
        //        {
        //            var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
        //            var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
        //            var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
        //            var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId };
        //            var BerujuSubTitleChildIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleChildId", Value = model.BerujuSubTitleChildId };
        //            var BaushiNumberIdParam = new SqlParameter { ParameterName = "@BaushiNumberId", Value = model.BaushiNumberId };
        //            // Example for optional string/date
        //            // Optional string
        //            var PanNoParam = new SqlParameter
        //            {
        //                ParameterName = "@PanNo",
        //                Value = string.IsNullOrEmpty(model.PanNo) ? (object)DBNull.Value : model.PanNo
        //            };

        //            // Optional dates
        //            var DateFromParam = new SqlParameter
        //            {
        //                ParameterName = "@DateFrom",
        //                Value = model.DateFromSearch.HasValue ? (object)model.DateFromSearch.Value : DBNull.Value
        //            };

        //            var DateToParam = new SqlParameter
        //            {
        //                ParameterName = "@DateTo",
        //                Value = model.DateToSearch.HasValue ? (object)model.DateToSearch.Value : DBNull.Value
        //            };

        //            var PageNumberParam = new SqlParameter { ParameterName = "@PageNumber", Value = model.PageNumber };
        //            var PageSizeParam = new SqlParameter { ParameterName = "@PageSize", Value = model.PageSize };

        //            return db.Database.SqlQuery<ExternalBerujuRptByTypeViewModel>(
        //                "EXEC Report_PopulateExternalBerujuByReportFilter @OfficeId, @FiscalYearId, @BerujuTypeId, @BerujuSubTitleId, @BerujuSubTitleChildId, @BaushiNumberId, @PanNo, @DateFrom, @DateTo, @PageNumber, @PageSize",
        //                OfficeIdParam,
        //                FiscalYearIdParam,
        //                BerujuTypeIdParam,
        //                BerujuSubTitleIdParam,
        //                BerujuSubTitleChildIdParam,
        //                BaushiNumberIdParam,
        //                PanNoParam,
        //                DateFromParam,
        //                DateToParam,
        //                PageNumberParam,
        //                PageSizeParam
        //            ).ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            // log ex if needed
        //            return new List<ExternalBerujuRptByTypeViewModel>();
        //        }
        //    }
        //}


        public void ExportExternalBerujuToExcel(HttpResponseBase response, ReportVIewModel model)
        {
            string query = @"
        SELECT 
            EB.OfficeId,
            OFC.OfficeName,
            OFC.OfficeCode,
            EB.BerujuTypeId,
            EB.ExternalBerujuId,
            EB.BerujuNumber,
            EB.BerujuDetails,
            EB.VoucharAmunt BerujuAmount,
            BT.TypeName,
            FY.FiscalYearTitle,
            BST.SubTitle,
            BSCT.SubTitleChild,
            EB.BudgetSubTitle,
            CP.TItlle as KharchaSirsak,

            STUFF(
                (SELECT ',' + TWD.PanNumber
                 FROM ToWhomDetails TWD
                 WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
                 AND TWD.PanNumber IS NOT NULL
                 AND LTRIM(RTRIM(TWD.PanNumber)) <> ''
                 FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS PanNo,

            STUFF(
                (SELECT ',' + TWD.PersonName
                 FROM ToWhomDetails TWD
                 WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
                 AND TWD.PersonName IS NOT NULL
                 AND LTRIM(RTRIM(TWD.PersonName)) <> ''
                 FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS FirmName,

            STUFF(
                (SELECT ',' + OD.OfficeCode + '-' + OD.OfficeName
                 FROM ExternalBerujuTransfer EBT
                 INNER JOIN dbo.OfficeDetail OD ON OD.OfficeDetailId = EBT.ToOffice
                 WHERE EBT.ExternalBerujuId = EB.ExternalBerujuId
                 AND EBT.TransferStatus = 1
                 FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS MergedFromOfficeCode

        FROM ExternalBeruju EB
        INNER JOIN BerujuType BT ON EB.BerujuTypeId = BT.BerujuTypeId
        INNER JOIN dbo.OfficeDetail OFC ON EB.OfficeId = OFC.OfficeDetailId
        INNER JOIN FiscalYearRecord FY ON FY.FiscalYearId = EB.FiscalYearId
        INNER JOIN dbo.BerujuSubTitle BST ON BST.BerujuSubTitleId = EB.BerujuSubTitleId
        INNER JOIN dbo.ChaluPujigat CP on EB.ChaluOrPujigatTitleId = CP.ChaluPujigatId
        LEFT JOIN dbo.BerujuSubTitleChild BSCT ON BSCT.BerujuSubTitleChildId = EB.BerujuSubTitleChildId

        WHERE (@OfficeId IS NULL OR @OfficeId = 0 OR EB.OfficeId = @OfficeId)
        AND (@FiscalYearId IS NULL OR @FiscalYearId = 0 OR EB.FiscalYearId = @FiscalYearId)
        AND (@BerujuTypeId IS NULL OR @BerujuTypeId = 0 OR EB.BerujuTypeId = @BerujuTypeId)
        AND (@BerujuSubTitleId IS NULL OR @BerujuSubTitleId = 0 OR EB.BerujuSubTitleId = @BerujuSubTitleId)
        AND (@BerujuSubTitleChildId IS NULL OR @BerujuSubTitleChildId = 0 OR EB.BerujuSubTitleChildId = @BerujuSubTitleChildId)
        AND (
            @PanNo IS NULL OR LTRIM(RTRIM(@PanNo)) = ''
            OR EXISTS (
                SELECT 1 FROM ToWhomDetails TWD
                WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
                AND (
                    LTRIM(RTRIM(TWD.PanNumber)) LIKE LTRIM(RTRIM(@PanNo)) + '%'
                    OR LTRIM(RTRIM(TWD.PersonName)) LIKE LTRIM(RTRIM(@PanNo)) + '%'
                )
            )
        )

        ORDER BY EB.ExternalBerujuId DESC
    ";

    var parameters = new List<SqlParameter>
    {
        new SqlParameter("@OfficeId", (object)model.OfficeId ?? DBNull.Value),
        new SqlParameter("@FiscalYearId", (object)model.FiscalYearId ?? DBNull.Value),
        new SqlParameter("@BerujuTypeId", (object)model.BerujuTypeId ?? DBNull.Value),
        new SqlParameter("@BerujuSubTitleId", (object)model.BerujuSubTitleId ?? DBNull.Value),
        new SqlParameter("@BerujuSubTitleChildId", (object)model.BerujuSubTitleChildId ?? DBNull.Value),
        new SqlParameter("@PanNo", (object)model.PanNo ?? DBNull.Value)
    };

            var headers = new List<string>
    {
        "कार्यालयको नाम", "कार्यालय कोड", "बेरुजु दफा","ब.उ.शि.नं", "व्यहोरा", "रकम",
        "बेरुजु प्रकार","खर्च शिर्षक", "आ.ब.", "उप-प्रकार", "उप-उप प्रकार",
        "PAN", "व्यक्ति वा फर्मको नाम", "मर्ज भई आएको"
    };

            var fields = new List<string>
    {
        "OfficeName", "OfficeCode", "BerujuNumber", "BudgetSubTitle", "BerujuDetails", "BerujuAmount",
        "TypeName","KharchaSirsak", "FiscalYearTitle", "SubTitle", "SubTitleChild",
        "PanNo", "FirmName", "MergedFromOfficeCode"
    };

            ExcelStreamExporter.ExportToExcel(
                response,
                "BerujuReport.xlsx",
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                query,
                parameters,
                headers,
                fields
            );
        }


        public void ExportExternalBerujuHierarchyToExcel(HttpResponseBase response, ReportVIewModel model)
        {
            string query = @"

;WITH OfficeHierarchy AS
(
    SELECT 
        OfficeDetailId,
        MainOfficeId
    FROM OfficeDetail
    WHERE OfficeDetailId = @OfficeId

    UNION ALL

    SELECT 
        od.OfficeDetailId,
        od.MainOfficeId
    FROM OfficeDetail od
    INNER JOIN OfficeHierarchy oh
        ON od.MainOfficeId = oh.OfficeDetailId
)

SELECT 
    EB.OfficeId,
    OFC.OfficeName,
    OFC.OfficeCode,
    EB.BerujuTypeId,
    EB.ExternalBerujuId,
    EB.BerujuNumber,
    EB.BerujuDetails,

    EB.VoucharAmunt AS BerujuAmount,

    -- OFFICE TOTAL
    SUM(EB.VoucharAmunt) OVER(PARTITION BY EB.OfficeId) AS OfficeTotal,

    BT.TypeName,
    FY.FiscalYearTitle,
    BST.SubTitle,
    BSCT.SubTitleChild,

    STUFF(
        (SELECT ',' + TWD.PanNumber
         FROM ToWhomDetails TWD
         WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
         AND TWD.PanNumber IS NOT NULL
         AND LTRIM(RTRIM(TWD.PanNumber)) <> ''
         FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS PanNo,

    STUFF(
        (SELECT ',' + TWD.PersonName
         FROM ToWhomDetails TWD
         WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
         AND TWD.PersonName IS NOT NULL
         AND LTRIM(RTRIM(TWD.PersonName)) <> ''
         FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS FirmName,

    STUFF(
        (SELECT ',' + OD.OfficeCode + '-' + OD.OfficeName
         FROM ExternalBerujuTransfer EBT
         INNER JOIN dbo.OfficeDetail OD 
            ON OD.OfficeDetailId = EBT.ToOffice
         WHERE EBT.ExternalBerujuId = EB.ExternalBerujuId
         AND EBT.TransferStatus = 1
         FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS MergedFromOfficeCode

FROM ExternalBeruju EB
INNER JOIN BerujuType BT 
    ON EB.BerujuTypeId = BT.BerujuTypeId
INNER JOIN dbo.OfficeDetail OFC 
    ON EB.OfficeId = OFC.OfficeDetailId
INNER JOIN FiscalYearRecord FY 
    ON FY.FiscalYearId = EB.FiscalYearId
INNER JOIN dbo.BerujuSubTitle BST 
    ON BST.BerujuSubTitleId = EB.BerujuSubTitleId
LEFT JOIN dbo.BerujuSubTitleChild BSCT 
    ON BSCT.BerujuSubTitleChildId = EB.BerujuSubTitleChildId

WHERE 
    (@FiscalYearId IS NULL OR @FiscalYearId = 0 OR EB.FiscalYearId = @FiscalYearId)

    AND (@BerujuTypeId IS NULL OR @BerujuTypeId = 0 OR EB.BerujuTypeId = @BerujuTypeId)

    AND (@BerujuSubTitleId IS NULL OR @BerujuSubTitleId = 0 OR EB.BerujuSubTitleId = @BerujuSubTitleId)

    AND (@BerujuSubTitleChildId IS NULL OR @BerujuSubTitleChildId = 0 OR EB.BerujuSubTitleChildId = @BerujuSubTitleChildId)

    AND (
        (@OfficeId IS NULL OR @OfficeId = 0)
        OR (EB.OfficeId = @OfficeId)
        OR (
            EB.OfficeId IN (
                SELECT OfficeDetailId 
                FROM OfficeHierarchy
            )
        )
    )

    AND (
        @PanNo IS NULL 
        OR LTRIM(RTRIM(@PanNo)) = ''
        OR EXISTS (
            SELECT 1 
            FROM ToWhomDetails TWD
            WHERE TWD.InternalOrExternalId = EB.ExternalBerujuId
            AND (
                LTRIM(RTRIM(TWD.PanNumber)) 
                    LIKE LTRIM(RTRIM(@PanNo)) + '%'

                OR LTRIM(RTRIM(TWD.PersonName)) 
                    LIKE LTRIM(RTRIM(@PanNo)) + '%'
            )
        )
    )

ORDER BY EB.ExternalBerujuId DESC

";

            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@OfficeId", (object)model.OfficeId ?? DBNull.Value),
        new SqlParameter("@FiscalYearId", (object)model.FiscalYearId ?? DBNull.Value),
        new SqlParameter("@BerujuTypeId", (object)model.BerujuTypeId ?? DBNull.Value),
        new SqlParameter("@BerujuSubTitleId", (object)model.BerujuSubTitleId ?? DBNull.Value),
        new SqlParameter("@BerujuSubTitleChildId", (object)model.BerujuSubTitleChildId ?? DBNull.Value),
        new SqlParameter("@PanNo", (object)model.PanNo ?? DBNull.Value)
    };

            var headers = new List<string>
    {
        "कार्यालयको नाम", "कार्यालय कोड", "बेरुजु दफा", "व्यहोरा",  "कार्यालय कुल रकम", "रकम",
        "बेरुजु प्रकार", "आ.ब.", "उप-प्रकार", "उप-उप प्रकार",
        "PAN", "व्यक्ति वा फर्मको नाम", "मर्ज भई आएको"
    };

            var fields = new List<string>
    {
        "OfficeName", "OfficeCode", "BerujuNumber", "BerujuDetails", "OfficeTotal",  "BerujuAmount",
        "TypeName", "FiscalYearTitle", "SubTitle", "SubTitleChild",
        "PanNo", "FirmName", "MergedFromOfficeCode"
    };

            ExcelStreamExporter.ExportToExcel(
                response,
                "BerujuReport.xlsx",
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                query,
                parameters,
                headers,
                fields
            );
        }



        public (List<ExternalBerujuRptByTypeViewModel>, int) Report_PopulateExternalBerujuByReportFilter(ReportVIewModel model)
        {
            List<ExternalBerujuRptByTypeViewModel> list = new List<ExternalBerujuRptByTypeViewModel>();
            int totalRecords = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                var conn = db.Database.Connection;

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Report_PopulateExternalBerujuByReportFilter";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@OfficeId", model.OfficeId));
                    cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", model.OfficeTypeSearchId));
                    cmd.Parameters.Add(new SqlParameter("@MainOfficeId", model.MainOfficeId));
                    cmd.Parameters.Add(new SqlParameter("@FiscalYearId", model.FiscalYearId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuTypeId", model.BerujuTypeId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuSubTitleId", model.BerujuSubTitleId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuSubTitleChildId", model.BerujuSubTitleChildId));
                    cmd.Parameters.Add(new SqlParameter("@BaushiNumberId", model.BaushiNumberId));

                    cmd.Parameters.Add(new SqlParameter("@PanNo",
                        string.IsNullOrEmpty(model.PanNo) ? (object)DBNull.Value : model.PanNo));

                    cmd.Parameters.Add(new SqlParameter("@DateFrom",
                        model.DateFromSearch.HasValue ? (object)model.DateFromSearch.Value : DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@DateTo",
                        model.DateToSearch.HasValue ? (object)model.DateToSearch.Value : DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@PageNumber", model.PageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", model.PageSize));

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // FIRST RESULT SET (Paged Data)
                        while (reader.Read())
                        {
                            var item = new ExternalBerujuRptByTypeViewModel
                            {
                                OfficeId = reader["OfficeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OfficeId"]),
                                 OfficeName = reader["OfficeName"]?.ToString(),
                                OfficeCode = reader["OfficeCode"]?.ToString(),
                                BerujuTypeId = reader["BerujuTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BerujuTypeId"]),
                                ExternalBerujuId = reader["ExternalBerujuId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ExternalBerujuId"]),
                                BerujuNumber = reader["BerujuNumber"]?.ToString(),
                                BerujuAmount = reader["BerujuAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BerujuAmount"]),
                                TypeName = reader["TypeName"]?.ToString(),
                                FiscalYearTitle = reader["FiscalYearTitle"]?.ToString(),
                                SubTitle = reader["SubTitle"]?.ToString(),
                                SubTitleChild = reader["SubTitleChild"]?.ToString(),
                                PanNo = reader["PanNo"]?.ToString(),
                                FirmName = reader["FirmName"]?.ToString(),
                                MergedFromOfficeCode = reader["MergedFromOfficeCode"]?.ToString()
                            };

                            list.Add(item);
                        }

                        // SECOND RESULT SET (TotalRecords)
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                            }
                        }
                    }
                }
            }

            return (list, totalRecords);
        }



        public (List<ExternalBerujuRptByTypeViewModel>, int) FindExternalBerujuByAmount(ReportVIewModel model)
        {
            List<ExternalBerujuRptByTypeViewModel> list = new List<ExternalBerujuRptByTypeViewModel>();
            int totalRecords = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                var conn = db.Database.Connection;

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_FindExternalBerujuByAmount";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@OfficeId", model.OfficeId));
                    cmd.Parameters.Add(new SqlParameter("@Amount", model.BerujuAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", model.PageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", model.PageSize));

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // FIRST RESULT SET (Paged Data)
                        while (reader.Read())
                        {
                            var item = new ExternalBerujuRptByTypeViewModel
                            {
                                OfficeId = reader["OfficeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OfficeId"]),
                                OfficeName = reader["OfficeName"]?.ToString(),
                                OfficeCode = reader["OfficeCode"]?.ToString(),
                                BerujuTypeId = reader["BerujuTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BerujuTypeId"]),
                                ExternalBerujuId = reader["ExternalBerujuId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ExternalBerujuId"]),
                                BerujuNumber = reader["BerujuNumber"]?.ToString(),
                                BerujuAmount = reader["BerujuAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BerujuAmount"]),
                                FiscalYearTitle = reader["FiscalYearTitle"]?.ToString(),
                                PanNo = reader["PanNo"]?.ToString(),
                                FirmName = reader["FirmName"]?.ToString(),
                                Amount = reader["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalAmount"]),
                        

                            };

                            list.Add(item);
                        }

                        // SECOND RESULT SET (TotalRecords)
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                            }
                        }
                    }
                }
            }

            return (list, totalRecords);
        }














        public (List<ExternalBerujuRptByTypeViewModel>, int) Report_ExternalBeruju_Hierarchy_Final(ReportVIewModel model)
        {
            List<ExternalBerujuRptByTypeViewModel> list = new List<ExternalBerujuRptByTypeViewModel>();
            int totalRecords = 0;

            using (BerujuEntities db = new BerujuEntities())
            {
                var conn = db.Database.Connection;

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Report_ExternalBeruju_Hierarchy_Final";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@OfficeId", model.OfficeId));
                    cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", model.OfficeTypeSearchId));
                    cmd.Parameters.Add(new SqlParameter("@MainOfficeId", model.MainOfficeId));
                    cmd.Parameters.Add(new SqlParameter("@FiscalYearId", model.FiscalYearId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuTypeId", model.BerujuTypeId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuSubTitleId", model.BerujuSubTitleId));
                    cmd.Parameters.Add(new SqlParameter("@BerujuSubTitleChildId", model.BerujuSubTitleChildId));
                    cmd.Parameters.Add(new SqlParameter("@BaushiNumberId", model.BaushiNumberId));

                    cmd.Parameters.Add(new SqlParameter("@PanNo",
                        string.IsNullOrEmpty(model.PanNo) ? (object)DBNull.Value : model.PanNo));

                    cmd.Parameters.Add(new SqlParameter("@DateFrom",
                        model.DateFromSearch.HasValue ? (object)model.DateFromSearch.Value : DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@DateTo",
                        model.DateToSearch.HasValue ? (object)model.DateToSearch.Value : DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@PageNumber", model.PageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", model.PageSize));

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // FIRST RESULT SET (Paged Data)
                        while (reader.Read())
                        {
                            var item = new ExternalBerujuRptByTypeViewModel
                            {
                                OfficeId = reader["OfficeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OfficeId"]),
                                OfficeName = reader["OfficeName"]?.ToString(),
                                OfficeCode = reader["OfficeCode"]?.ToString(),
                                BerujuTypeId = reader["BerujuTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BerujuTypeId"]),
                                ExternalBerujuId = reader["ExternalBerujuId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ExternalBerujuId"]),
                                BerujuNumber = reader["BerujuNumber"]?.ToString(),
                                BerujuAmount = reader["BerujuAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BerujuAmount"]),
                                TypeName = reader["TypeName"]?.ToString(),
                                FiscalYearTitle = reader["FiscalYearTitle"]?.ToString(),
                                SubTitle = reader["SubTitle"]?.ToString(),
                                SubTitleChild = reader["SubTitleChild"]?.ToString(),
                                PanNo = reader["PanNo"]?.ToString(),
                                FirmName = reader["FirmName"]?.ToString(),
                                MergedFromOfficeCode = reader["MergedFromOfficeCode"]?.ToString(),
                              
                                OfficeTotal = reader["OfficeTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["OfficeTotal"]),
                            };

                            list.Add(item);
                        }


                        // SECOND RESULT SET (TotalRecords)
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                            }
                        }
                    }
                }
            }

            return (list, totalRecords);
        }

        public List<ExternalBerujuRptByTypeViewModel> Report_PopulateExternalBerujuByTypeID(int OfficeId, int FiscalYearId, int Baushinumber, int Berujutypeid, int Berujsubtypeid,  int Berujusubtypechildid)
        {
            List<ExternalBerujuRptByTypeViewModel> returnList = new List<ExternalBerujuRptByTypeViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    returnList = db.Database.SqlQuery<ExternalBerujuRptByTypeViewModel>("Report_PopulateExternalBerujuByTypeID {0},{1},{2},{3},{4},{5}", OfficeId, FiscalYearId, Berujutypeid,Berujsubtypeid,Berujusubtypechildid, Baushinumber).ToList();

                }
                catch (Exception)
                {

                    returnList = new List<ExternalBerujuRptByTypeViewModel>();
                }

            }
            return returnList;
        }
        public List<AnusuchiTwelveViewModel> spGetAnusuchiTwelve(int OfficeId, int KoshTypeId, int FiscalYearId)
        {
            List<AnusuchiTwelveViewModel> returnList = new List<AnusuchiTwelveViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AnusuchiTwelveViewModel>("spGetAnusuchiTwelve {0},{1},{2}", FiscalYearId, OfficeId, KoshTypeId).ToList();

            }
            return returnList;
        }

        public List<AnusuchiThirteenViewModel> spGetAnusuchiThirteen(int OfficeId, int KoshTypeId, int FiscalYearId)
        {
            List<AnusuchiThirteenViewModel> returnList = new List<AnusuchiThirteenViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AnusuchiThirteenViewModel>("spGetAnusuchiThirteen {0},{1},{2}", FiscalYearId, OfficeId, KoshTypeId).ToList();

            }
            return returnList;
        }
        public DateTime? GetFiscalyearEndDate(int FYID)
        {
            DateTime fyenddate = DateTime.Now;
            using (BerujuEntities db = new BerujuEntities())
            {
                fyenddate = db.Database.SqlQuery<DateTime>(@"select EndDate From FiscalYearRecord where FiscalYearId='" + FYID + "'").FirstOrDefault();
            }
            return fyenddate;
        }

        public DateTime? GetFiscalyearStartDate(int FYID)
        {
            DateTime startDate = DateTime.Now;
            using (BerujuEntities db = new BerujuEntities())
            {
                startDate = db.Database.SqlQuery<DateTime>(@"select StartFrom From FiscalYearRecord where FiscalYearId='" + FYID + "'").FirstOrDefault();
            }
            return startDate;
        }

        public DateTime? GetFirstSecondQuadStartEndDate(int FYID, int WhichQuadId, int StartOrEnd)
        {
            DateTime ReturnDate = DateTime.Now;
            using (BerujuEntities db = new BerujuEntities())
            {
                if (WhichQuadId == 1)
                {
                    if (StartOrEnd == 1)
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select FStartDate from QuadmesterDetails where FYid='" + FYID + "'").FirstOrDefault();
                    }
                    else
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select FEndDate from QuadmesterDetails where FYid = '" + FYID + "'").FirstOrDefault();
                    }
                }
                else if (WhichQuadId == 2)
                {
                    if (StartOrEnd == 1)
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select SStartDate from QuadmesterDetails where FYid='" + FYID + "'").FirstOrDefault();
                    }
                    else
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select SEndDate from QuadmesterDetails where FYid = '" + FYID + "'").FirstOrDefault();
                    }
                }
                else
                {
                    if (StartOrEnd == 1)
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select TStartDate from QuadmesterDetails where FYid='" + FYID + "'").FirstOrDefault();
                    }
                    else
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>(@"select TEndDate from QuadmesterDetails where FYid = '" + FYID + "'").FirstOrDefault();
                    }
                }



            }
            return ReturnDate;
        }

        public List<AnusuchiFourteenViewModel> spGetAnusuchiFourteen(int OfficeId, int KoshTypeId, int FiscalYearId)
        {
            DateTime? FYEndDate = GetFiscalyearEndDate(FiscalYearId);
            DateTime? FYStartDate = GetFiscalyearStartDate(FiscalYearId);

            DateTime? FirstquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 1, 1);
            DateTime? FirstquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 1, 2);

            DateTime? SecondquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 2, 1);
            DateTime? SecondquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 2, 2);

            DateTime? ThirdquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 3, 1);
            DateTime? ThirdquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 3, 2);

            List<AnusuchiFourteenViewModel> returnList = new List<AnusuchiFourteenViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<AnusuchiFourteenViewModel>("spGetAnusuchiFourteen {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", FiscalYearId, OfficeId, KoshTypeId, FYStartDate, FYEndDate, FirstquadStartDate, FirstquadEndDate, SecondquadStartDate, SecondquadEndDate, ThirdquadStartDate, ThirdquadEndDate).ToList();

            }
            return returnList;
        }

        public List<Anusuchi16ViewModel> spGetAnusuchiSixteenNew(int OfficeId, int KoshTypeId, int FiscalYearId)
        {
            DateTime? FYEndDate = GetFiscalyearEndDate(FiscalYearId);
            DateTime? FYStartDate = GetFiscalyearStartDate(FiscalYearId);

            DateTime? FirstquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 1, 1);
            DateTime? FirstquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 1, 2);

            DateTime? SecondquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 2, 1);
            DateTime? SecondquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 2, 2);

            DateTime? ThirdquadStartDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 3, 1);
            DateTime? ThirdquadEndDate = GetFirstSecondQuadStartEndDate(FiscalYearId, 3, 2);

            List<Anusuchi16ViewModel> returnList = new List<Anusuchi16ViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<Anusuchi16ViewModel>("spGetAnusuchiSixteenNew {0},{1},{2},{3},{4}", FiscalYearId, OfficeId, KoshTypeId, FYStartDate, FYEndDate).ToList();

            }
            return returnList;
        }


        public List<BerujuDetailsTillDateVM> SuperAdmin_TillDateBerujuDetails(int? FiscalYearId)
        {
            
            List<BerujuDetailsTillDateVM> returnList = new List<BerujuDetailsTillDateVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<BerujuDetailsTillDateVM>("SuperAdmin_TillDateBerujuDetails {0}", FiscalYearId).ToList();

            }
            return returnList;
        }







        public List<OfficeChiefsDetailsVM> sp_GetOfficeChiefDetails(int OfficeId, int FiscalYearId)
        {
            List<OfficeChiefsDetailsVM> returnList = new List<OfficeChiefsDetailsVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<OfficeChiefsDetailsVM>("sp_GetOfficeChiefDetails {0},{1}", FiscalYearId, OfficeId).ToList();

            }
            return returnList;
        }

        public List<MalepaPurnaPathVM> Report_GetMalepaPurnaPathDetails(int OfficeId)
        {
            List<MalepaPurnaPathVM> returnList = new List<MalepaPurnaPathVM>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<MalepaPurnaPathVM>("Report_GetMalepaPurnaPathDetails {0}", OfficeId).ToList();

            }
            return returnList;
        }


    }


}
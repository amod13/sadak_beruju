using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class DashboardViewModel
    {
        public int FiscalYearId { get; set; }
        public int UserTypeId { get; set; }
        public int CurrentLoginUserOfficeId { get; set; }
        public int OfficeTypeSearchId { get; set; }


        public int MininstrySearchId { get; set; }

        public int BivagSearchId { get; set; }

        public bool BerujuStatus { get; set; }

        public int NirdeshnalayaSearchId { get; set; }

        public int OfficeId { get; set; }

        public int KaryalayaSearchId { get; set; }

        public string UserEmail { get; set; }
        public int CurrentLoginUserDistrictId { get; set; }
        public int InternalOrExternalBerujuTypeId { get; set; }
        public int ViewBagFiscalYearId { get; set; }

        public List<SamparikshadDetailByTypeVM> SamparikshadDetailByTypeVMList { get; set; }
        public DashboardColumnChartViewModel ObjDashboardColumnChartViewModel { get; set; }

        public DashboardOfficeCountVM DashboardOfficeCountVM { get; set; }

        public MakerDashboardViewModel ObjDashboardMakerDashboardViewModel { get; set; }
        public List<DashboardColumnChartViewModel> DashboardColumnChartViewModelList { get; set; }
        public List<DashboardPieChart> DashboardPieChartList { get; set; }
        public List<DashboardkoshwiseTable> DashboardkoshwiseTableList { get; set; }
        public List<DashboardBerujuTypewiseTable> DashboardBerujuTypewiseTableList { get; set; }

        

        public List<DashboardOfficesGetSumBerujuTypeWiseViewModel> DashboardOfficesGetSumBerujuTypeWiseViewModelList { get; set; }
        public List<SaidantikCountOfficeWiseViewModel> SaidantikCountOfficeWiseViewModelList { get; set; }
        public List<Admin_GetSaidaintikBerujuListByOfficeIdVM> Admin_GetSaidaintikBerujuListByOfficeIdVMList { get; set; }
        public List<Admin_BerujuNotDoneListByOfficeIdVM> Admin_BerujuNotDoneListByOfficeIdVMList { get; set; }
        public List<IntExtSamparikshadCountVM> IntExtSamparikshadCountVMList { get; set; }
        public List<Admin_GetInternalExternalSamparikshadListByOfficeIdVM> Admin_GetInternalExternalSamparikshadListByOfficeIdVMList { get; set; }
    }


    public class DashboardOfficeCountVM
    {
        public int MinistryCount { get; set; }
        public int BivagCount { get; set; }
        public int NirdeshanCount { get; set; }
        public int OfficeCount { get; set; }
    }
    public class DashboardBerujuTypewiseTable
    {
        public int BerujuTypeId { get; set; }

        public string BerujuTypeNepali { get; set; }

        public string BerujuTypeEnglish { get; set; }

        public decimal TotalExternalBerujuAmount { get; set; }

        public decimal TotalSamparikshanAmount { get; set; }
    }


    public class DashboardColumnChartViewModel
    {
        public string FiscalYearTitle { get; set; }
        public decimal ExternalBerujuTotal { get; set; }
        public decimal SamparikshadTotal { get; set; }

    }

    public class DashboardPieChart
    {
        public string TypeName { get; set; }
        public decimal TotalAmount { get; set; }
        

    }

    public class DashboardkoshwiseTable
    {
        public string FiscalYearTitle { get; set; }
        public decimal Biniyojan { get; set; }
        public decimal Rajaswo { get; set; }
        public decimal Dharauti { get; set; }
        public decimal Anyakosh { get; set; }
        public decimal TotalSamparikshad { get; set; }

        public decimal TotalFurcheut { get; set; }
    }

    public class DashboardOfficesGetSumBerujuTypeWiseViewModel
    {
        public int OfficeDetailId { get; set; }
        public string OFficeName { get; set; }
        public string OfficeCode { get; set; }
        public decimal Niyamit { get; set; }
        public decimal AshulUpar { get; set; }
        public decimal Peski { get; set; }
        public decimal NiyamitTotalSamparikshad { get; set; }
        public decimal AshulUparTotalSamparikshad { get; set; }
        public decimal PeskiTotalSamparikshad { get; set; }
        public string FiscalYearId { get; set; }
    }

    public class SaidantikCountOfficeWiseViewModel
    {
        public string OFficeName { get; set; }
        public string OfficeCode { get; set; }
        public int Total { get; set; }
        public int OfficeDetailId { get; set; }
        public string FiscalYearTitle { get; set; }
    }

    public class Admin_GetSaidaintikBerujuListByOfficeIdVM
    {
        public string FiscalYearTitle { get; set; }
        public string BerujuDafaNumber { get; set; }
        public string BerujuShortDesc { get; set; }
        public string BerujuLongDesc { get; set; }
    }

    public class Admin_BerujuNotDoneListByOfficeIdVM
    {
        public string FiscalYearTitle { get; set; }
        public string UploadFileUrl { get; set; }
        public string NotDoneRemarks { get; set; }
        public int InternalOrExternal { get; set; }
    }

    public class IntExtSamparikshadCountVM
    {
        public int Total { get; set; }
        public string OFficeName { get; set; }
        public int OfficeDetailId { get; set; }
        public string OfficeCode { get; set; }
        public string FiscalYearTitle { get; set; }

    }

    public class Admin_GetInternalExternalSamparikshadListByOfficeIdVM
    {
        public string FiscalYearTitle { get; set; }
        public int? InternalBerujuId { get; set; }
        public int? ExternalBerujuId { get; set; }
        public string TypeName { get; set; }
        public int? InternalSamparishadId { get; set; }
        public int? SamparishadId { get; set; }
        public decimal? ReviesedVoucherAmount { get; set; }
        public DateTime? RevisedDate { get; set; }
        public string RevisedRemarks { get; set; }
        public string UploadFileDetails { get; set; }
        public int OfficeId { get; set; }
    }

    public class SamparikshadDetailByTypeVM
    {
        public decimal? TotalSamparikshadAmount { get; set; }

        public string FiscalYearTitle { get; set; }
        public string TypeName { get; set; }
    }


    public class MakerDashboardViewModel
    {
        public int TotalEntered { get; set; }
        public int TotalVerified { get; set; }
        public int EnteredToday { get; set; }
        public int VerifiedToday { get; set; }

        public int TotalBiniyojanBeruju { get; set; }

        public int TotalRajaswoBeuju { get; set; }

        public int TotalDharautiBeruju { get; set; }

        public int TotalSaidantikBeruju { get; set; }

        public int TotalAnyeKoshBeruju { get; set; }


        public int TotalBiniyojanBerujuVerified { get; set; }

        public int TotalRajaswoBeujuVerified { get; set; }

        public int TotalDharautiBerujuVerified { get; set; }

        public int TotalSaidantikBerujuVerified { get; set; }

        public int TotalAnyeKoshBerujuVerified { get; set; }

        public int EnteredTodayBiniyojan { get; set; }

        public int EnteredTodayRajaswo { get; set; }

        public int EnteredTodayDharauti { get; set; }

        public int EnteredTodaySaidantik { get; set; }

        public int EnteredTodayAnyeKosh { get; set; }


        public int VerifiedTodayBiniyojan { get; set; }

        public int VerifiedTodayDharauti { get; set; }

        public int VerifiedTodayRajaswo { get; set; }

        public int VerifiedTodayAnyeKosh { get; set; }
        public int VerifiedTodaySadantik { get; set; }


    }
}
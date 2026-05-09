using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class ReportVIewModel
    {
        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VDCMUNIdSearch { get; set; }
        public int DivisionHeadId { get; set; }
        public int DefaultProId { get; set; }
        public int KoshID { get; set; }
        public int BerujuTypeId { get; set; }

        public int BerujuSubTitleChildId { get; set; }

        public int BerujuSubTitleId { get; set; }
        public int FiscalYearId { get; set; }
        public int ToWhomTypeId { get; set; }
        public int SumamryOrDetailsId { get; set; }
        public int BaushiNumberId { get; set; }
        public int OfficeTypeSearchId { get; set; }
        [Required(ErrorMessage = @"कृपया कार्यालयको प्रकार छान्नुहोस")]
        public int OfficeTypeSearchIdForAdmin { get; set; }

        public int MininstrySearchId { get; set; }

        public int BivagSearchId { get; set; }


        public int NirdeshnalayaSearchId { get; set; }


        public int KaryalayaSearchId { get; set; }
        public int CurrentLoginUserTypeviewModel { get; set; }

        public int OfficeTypeForReportHeader { get; set; }
        public int OfficeIdForReportHeader { get; set; }
        public int CurrentLoginUserDistrictId { get; set; }//static code for district User

        public int CurrentLoginUserofficeTypeID { get; set; }//this is static for district users

        public int OfficeId { get; set; }

        public int MainOfficeId { get; set; }
        public int AayogAndOthers { get; set; }
        public int LocalLevelOfficeId { get; set; }

        public string PanNo { get; set; }
        public string DateFromStr { get; set; }
        public string DateToStr { get; set; }

        public decimal BerujuAmount { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;

        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }


        public DateTime? DateFromSearch { get; set; }
        public DateTime? DateToSearch { get; set; }


        public List<BerujuLagatKhataVM> BerujuLagatKhataVMList { get; set; }
        public List<FYListViewModel> FYListViewModellist { get; set; }
        public List<BerujuTypeViewModel> BerujuTypeViewModelList { get; set; }

        public List<KendriyaKaralayagtLaagatViewModel> KendriyaKaralayagtLaagatViewModelList { get; set; }
        public List<SamparikshadGausharaKhataVM> SamparikshadGausharaKhataVMList { get; set; }
        public SamparikshadGausharaKhataVM ObjSamparikshadGausharaKhataVM { get; set; }
        public List<AntimBerujuDetailsRptViewModel> AntimBerujuDetailsRptViewModelList { get; set; }
        public List<AntimBerujuTowhomTypeWiseRptViewModel> AntimBerujuTowhomTypeWiseRptViewModelList { get; set; }

        public List<AntimBerujuOfficeChiefWiseRptViewModel> AntimBerujuOfficeChiefWiseRptViewModelList { get; set; }
        public List<ExternalBerujuRptByTypeViewModel> ExternalBerujuRptByTypeViewModelList { get; set; }


        public List<AnusuchiTwelveViewModel> AnusuchiTwelveViewModelList { get; set; }
        public List<AnusuchiThirteenViewModel> AnusuchiThirteenViewModelList { get; set; }

        public List<AnusuchiFourteenViewModel> AnusuchiFourteenViewModelList { get; set; }

        public List<Anusuchi16ViewModel> Anusuchi16ViewModelList { get; set; }

        public List<OfficeChiefsDetailsVM> OfficeChiefsDetailsVMList { get; set; }

        public List<MalepaPurnaPathVM> MalepaPurnaPathVMList { get; set; }

        public List<BerujuDetailsTillDateVM> BerujuDetailsTillDateVMList { get; set; }


        public List<BerujuSampaReportModel> BerujuSampaReportVMList { get; set; }

        public List<BerujuFurcheutSampaReportModel> BerujuFurcheutSampaReportModelVMList { get; set; }

    }



    public class BerujuFurcheutSampaReportModel
    {
        // Office Info
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }

        // Beruju
        public decimal Kayam_Beruju { get; set; }

        // Samparikshan (Sampa)
        public decimal Sampa_Niyamit { get; set; }
        public decimal Sampa_Asul { get; set; }
        public decimal Sampa_Peski { get; set; }
        public decimal Sum_Sampa { get; set; }

        // Furcheut
        public decimal Furcheut_Anye_Amount { get; set; }
        public decimal Furcheut_Kumari_Amount { get; set; }
        public decimal Furcheut_Malepa_Amount { get; set; }
        public decimal Furcheut_Samiti_Amount { get; set; }
        public decimal Furcheut_Ministry_Amount { get; set; }
        public decimal Furcheut_Bivag_Amount { get; set; }
        public decimal Sum_Furcheut { get; set; }

        // Percentages
        public decimal Sampa_Percentage { get; set; }
        public decimal Furcheut_Percentage { get; set; }
    }


    public class BerujuSampaReportModel
    {
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }

        // ================= ALYA =================
        public decimal Alya_Niyamit { get; set; }
        public decimal Alya_Asul { get; set; }
        public decimal Alya_Peshki { get; set; }
        public decimal Alya_Total { get; set; }

        // ================= CURRENT =================
        public decimal Cur_Niyamit { get; set; }
        public decimal Cur_Asul { get; set; }
        public decimal Cur_Peshki { get; set; }
        public decimal Current_Total { get; set; }

        // ================= TOTAL =================
        public decimal Total_Niyamit { get; set; }
        public decimal Total_Asul { get; set; }
        public decimal Total_Peshki { get; set; }
        public decimal Grand_Total { get; set; }


        // ================= AUDIT bigat=================
        public decimal Audit_Niyamit_bigat_fy { get; set; }
        public decimal Audit_Asul_bigat_fy { get; set; }
        public decimal Audit_Peshki_bigat_fy { get; set; }

        public decimal total_audit_bigat_fy { get; set; }


        // ================= AUDIT bigat=================
        public decimal Audit_Niyamit_Chalu_fy { get; set; }
        public decimal Audit_Asul_chalu_fy { get; set; }
        public decimal Audit_Peshki_chalu_fy { get; set; }

        public decimal total_audit_chalu_fy { get; set; }



        // ================= AUDIT =================
        public decimal Audit_Niyamit_Total { get; set; }
        public decimal Audit_Asul_Total { get; set; }
        public decimal Audit_Peshki_Total { get; set; }

        public decimal Audit_Total   { get; set; }

        // ================= REMAINING =================
        public decimal Rem_Niyamit { get; set; }
        public decimal Rem_Asul { get; set; }
        public decimal Rem_Peshki { get; set; }
        public decimal Remaining_Total { get; set; }

        // ================= PERCENT =================
        public decimal Percentage { get; set; }
    }

    public class BerujuLagatKhataVM
    {
        public int ExternalBerujuId { get; set; }
        public int FiscalYearId { get; set; }
        public int BerujuTypeId { get; set; }
        public int BerujuDafaNumber { get; set; }
        public string VoucherNumber { get; set; }
        public string BerujuDetails { get; set; }
        public string BerujuSubCategory { get; set; }
        public decimal? TypeOne { get; set; }
        public decimal? TypeTwo { get; set; }
        public decimal? TypeThree { get; set; }
        public decimal TotalCurrentBeruju { get; set; }
        public DateTime SamparikshadDate { get; set; }
        public string SamparikshadBerujutTypeOne { get; set; }
        public string SamparikshadBerujutTypeTwo { get; set; }
        public string SamparikshadBerujutTypeThree { get; set; }
        public decimal SamparikshadBerujutTotal { get; set; }

        public string RemainingBerujutTypeOne { get; set; }
        public string RemainingBerujutTypeTwo { get; set; }
        public string RemainingBerujutTypeThree { get; set; }
        public decimal RemainingBerujutTotal { get; set; }
        public DateTime? RevisedDate { get; set; }

        public string FiscalYearTitle { get; set; }
        public string BerujuNumber { get; set; }
        public string BerujuSubTitle { get; set; }
        public decimal SamparikshadAmount { get; set; }
        public int SamparishadId { get; set; }
        public string BudgetSubTitle { get; set; }
    }

    public class FYListViewModel
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearTitle { get; set; }
    }

    public class BerujuTypeViewModel
    {
        public int BerujuTypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class KendriyaKaralayagtLaagatViewModel
    {
        public string FiscalYearTitle { get; set; }
        public int FiscalYearId { get; set; }
        public decimal? StypeOne { get; set; }
        public decimal? StypeTwo { get; set; }
        public decimal? StypeThree { get; set; }
        public decimal? RtypeOne { get; set; }
        public decimal? RtypeTwo { get; set; }
        public decimal? RtypeThree { get; set; }
        public decimal? TypeOne { get; set; }
        public decimal? TypeTwo { get; set; }
        public decimal? TypeThree { get; set; }
    }

    public class SamparikshadGausharaKhataVM
    {
        public string OFficeName { get; set; }
        public string SubTitleName { get; set; }
        public string LetterNumber { get; set; }
        public DateTime RevisedDate { get; set; }
        [NotMapped]
        public int FiscalYearId { get; set; }
        [NotMapped]
        public string RevisedDateStr { get; set; }
        public decimal? ReviesedVoucherAmount { get; set; }
        public int KoshTypeId { get; set; }
        public string BerujuNumber { get; set; }
        public string RequestedDateNep { get; set; }
        public decimal? ReqTotalAmount { get; set; }


    }

    public class AntimBerujuDetailsRptViewModel
    {
        public string FiscalYearTitle { get; set; }
        public int FiscalYearId { get; set; }
        public decimal Biniyojan { get; set; }
        public decimal Rajaswo { get; set; }
        public decimal Dharauti { get; set; }
        public decimal AnyaKosh { get; set; }
        public decimal TotalAmout { get; set; }
    }

    public class AntimBerujuTowhomTypeWiseRptViewModel
    {
        public string FiscalYearTitle { get; set; }
        public int FiscalYearId { get; set; }
        public decimal Biniyojan { get; set; }
        public decimal Rajaswo { get; set; }
        public decimal Dharauti { get; set; }
        public decimal AnyaKosh { get; set; }
        public decimal TotalAmout { get; set; }
        public string TypeName { get; set; }
        public int ToWhomID { get; set; }
        public string PersonName { get; set; }
        public int ToWhomDetailsId { get; set; }
    }

    public class AntimBerujuOfficeChiefWiseRptViewModel
    {
        public string FiscalYearTitle { get; set; }
        public int FiscalYearId { get; set; }
        public decimal Biniyojan { get; set; }
        public decimal Rajaswo { get; set; }
        public decimal Dharauti { get; set; }
        public decimal AnyaKosh { get; set; }
        public decimal TotalAmout { get; set; }
        public string EmpName { get; set; }
        public int OfficeManagerId { get; set; }
    }
    public class ExternalBerujuRptByTypeViewModel
    {
        public string SubTitleChild { get; set; }
        public string SubTitle { get; set; }
        public int BudgetSubTitleId { get; set; }
        public string SubTitleCode { get; set; }
        public int OfficeId { get; set; }
        public int BerujuTypeId { get; set; }
        public int ExternalBerujuId { get; set; }
        public string BerujuNumber { get; set; }

        public string PanNo { get; set; }

        public string MergedFromOfficeCode { get; set; }

        public string FirmName {  get; set; }

        public string OfficeName { get; set; }
        public string OfficeCode { get; set; }
        public decimal BerujuAmount { get; set; }


        public decimal Amount { get; set; }  // for individual amount person or firm
        public string TypeName { get; set; }
        public string FiscalYearTitle { get; set; }
        public string BerujuDetails { get; set; }

        public int TotalRecords { get; set; }

        public decimal? OfficeTotal { get; set; }
        public int rn_Office { get; set; }

    }
    public class AnusuchiTwelveViewModel
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearTitle { get; set; }
        public string SubTitleName { get; set; }
        public string BerujuNumber { get; set; }
        public string BerujuShorDesc { get; set; }
        public string LagatiSaidanktik { get; set; }
        public bool? WasMadeFinal { get; set; }
        public decimal? FinalInternalBerujuNiyamitAmount { get; set; }
        public decimal? FinalInternalBerujuAshuliAmount { get; set; }
        public decimal? FinalInternalBerujuPeskiAmount { get; set; }
        public decimal? FinalInternalBerujuTotal { get; set; }
        public decimal? NiyamitAmountBeforeExternalBerujuAmt { get; set; }
        public decimal? AshulAmountBeforeExternalBerujuAmt { get; set; }
        public decimal? PeskiAmountBeforeExternalBerujuAmt { get; set; }
        public decimal? TotalAmountBeforeExternalBerujuAmt { get; set; }

        public decimal? NiyamitAmountTransferFromInternalToExternal { get; set; }
        public decimal? AshulAmountTransferFromInternalToExternal { get; set; }
        public decimal? PeskiAmountTransferFromInternalToExternal { get; set; }
        public decimal? TotalAmountTransferFromInternalToExternal { get; set; }

        public string ShortDespInExternalBeruju { get; set; }
        public string Remarks { get; set; }

        public int InternalBerujuId { get; set; }
    }

    public class OfficeFilterResult
    {
        public int OfficeId { get; set; }
        public int OfficeTypeId { get; set; }

        public int MainOfficeId { get; set; } // 🔥 NEW
    }
    public class AnusuchiThirteenViewModel
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearTitle { get; set; }
        public string BudgetUpShirshak { get; set; }
        public string BerujuDafaNumber { get; set; }
        public decimal? FirstBerujuKayamAmount { get; set; }
        public decimal? SamparikshadAmountTillFY { get; set; }
        public decimal? NiyamitBeforeFY { get; set; }
        public decimal? AshulBeforeFY { get; set; }
        public decimal? PeskiBeforeFY { get; set; }
        public decimal? BeforeAmountFY { get; set; }
        public decimal? CurrentFYSamparikshadNiyamitAmount { get; set; }
        public decimal? CurrentFYSamparikshadAshulAmount { get; set; }
        public decimal? CurrentFYSamparikshadPeskiAmount { get; set; }
        public decimal? CurrentFYSamparikshadTotalAmount { get; set; }
        public decimal? CurrentRemainingNiyamitAmount { get; set; }
        public decimal? CurrentRemainingAshulAmount { get; set; }
        public decimal? CurrentRemainingPeskiAmount { get; set; }
        public decimal? CurrentRemainingTotalAmount { get; set; }
        public string ChalaniLetterNumber { get; set; }
        public decimal? BerujuAmount { get; set; }
        public string KaryalaPramukhName { get; set; }
        public string KaryalaLekhaPramukhName { get; set; }
        public string SamparikshadDate { get; set; }
        public string SamparikshadVoucharNumber { get; set; }
        public decimal? NagadAshuliAmount { get; set; }
        public decimal? LagatKatta { get; set; }
        public int ExternalBerujuId { get; set; }
        public string BerujuShorDesc { get; set; }





    }
    public class AnusuchiFourteenViewModel
    {
        public int OfficeCode { get; set; }
        public string OfficeNameWithCode { get; set; }
        public decimal? SamparikshadRemainNiyamitAmt { get; set; }
        public decimal? SamparikshadRemainAshuliAmt { get; set; }
        public decimal? SamparikshadRemainPeskiAmt { get; set; }
        public decimal? SamparikshadRemainTotalAmt { get; set; }

        public decimal? FirstTrimisterNiyamitAmt { get; set; }
        public decimal? FirstTrimisterAshuliAmt { get; set; }
        public decimal? FirstTrimisterPeskiAmt { get; set; }
        public decimal? FirstTrimisterTotalAmt { get; set; }
        public decimal? FirstTrimisterPercentage { get; set; }

        public decimal? SecondTrimisterNiyamitAmt { get; set; }
        public decimal? SecondTrimisterAshuliAmt { get; set; }
        public decimal? SecondTrimisterPeskiAmt { get; set; }
        public decimal? SecondTrimisterTotalAmt { get; set; }
        public decimal? SecondTrimisterPercentage { get; set; }

        public decimal? ThirdTrimisterNiyamitAmt { get; set; }
        public decimal? ThirdTrimisterAshuliAmt { get; set; }
        public decimal? ThirdTrimisterPeskiAmt { get; set; }
        public decimal? ThirdTrimisterTotalAmt { get; set; }
        public decimal? ThirdTrimisterPercentage { get; set; }


        public decimal? TotalyearlyNiyamitAmt { get; set; }
        public decimal? TotalyearlyAshuliAmt { get; set; }
        public decimal? TotalyearlyPeskiAmt { get; set; }
        public decimal? TotalyearlyTotalAmt { get; set; }
        public decimal? TotalyearlyPercentage { get; set; }



    }

    public class Anusuchi16ViewModel
    {


        public string OfficeNameAndCode { get; set; }
        public decimal? PeskiWithAlyaAmount { get; set; }
        public decimal? NiyamitWithAlyaAmount { get; set; }
        public decimal? AshuliWithAlyaAmount { get; set; }
        public decimal? PeskiSamparikshadThisYearAmount { get; set; }
        public decimal? NiyamitSamparikshadThisYearAmount { get; set; }
        public decimal? AshuliSamparikshadThisYearAmount { get; set; }

        public decimal? FromMalapaAmount { get; set; }
        public decimal? FromKumariChowkAmount { get; set; }
        public decimal? PeskiRemainingAmount { get; set; }
        public decimal? NiyamitRemainingAmount { get; set; }
        public decimal? AshuliRemainingAmount { get; set; }
        public decimal? NagadAshuliAmount { get; set; }
        public decimal? LagatKattaMalapaAmount { get; set; }
        public decimal? LagatKattaKumarichowkAmount { get; set; }
        public decimal? TotalLagatKattaAmount { get; set; }

    }

    public class OfficeChiefsDetailsVM
    {
        public string EmpName { get; set; }
        public string FromDuration { get; set; }
        public string ToDuration { get; set; }
        public int EmpType { get; set; }
        public int OfficeId { get; set; }
        public string EmpPost { get; set; }

    }

    public class MalepaPurnaPathVM
    {
        public string JVNUMBER { get; set; }
        public DateTime? VoucharDate { get; set; }
        public string BerujuShorDesc { get; set; }
        public string BerujuDetails { get; set; }
    }

    public class BerujuDetailsTillDateVM
    {
        public string OFficeName { get; set; }
        public decimal? TotalBerujuPY { get; set; }
        public decimal? TotalSamparikhadPY { get; set; }
        public decimal? TotalBerujuCY { get; set; }
        
    }


}
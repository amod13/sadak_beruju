using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2013.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class ExternalBeruju
    {
        public int ExternalBerujuId { get; set; }
        public int InternalBerujuId { get; set; }
        public int FiscalYearId { get; set; }
        public string BudgetSubTitle { get; set; }
        public string ExpenseTItle { get; set; }
        public string OfficeManagerName { get; set; }

        public string MergedFromOfficeCode { get; set; }
        public string OfficeManagerPost { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string AccountantName { get; set; }
        public DateTime AccountantFromDate { get; set; }
        public DateTime AccountantToDate { get; set; }
        public string JVNUMBER { get; set; }
        public DateTime VoucharDate { get; set; }
        public decimal VoucharAmunt { get; set; }
        public string BerujuDetails { get; set; }
        public string BerujuShorDesc { get; set; }
        public int BerujuTypeId { get; set; }
        public int ToWhomID { get; set; }
        public string ToWhomName { get; set; }
        public string AuditorName { get; set; }
        public string AuditorPost { get; set; }
        public int KoshTypeId { get; set; }
        public string BerujuNumber { get; set; }
        public bool BerujuStatus { get; set; }

        public string Remarks { get; set; }

        public int InternalOrExternalTypeId { get; set; }


        public HttpPostedFileBase SupportingDocFiles { get; set; }

        public string UploadedFileUrl { get; set; }
        
        public decimal? ReviesedVoucherAmount { get; set; }
        [NotMapped]
        public decimal? RemainingAmount { get; set; }
        [NotMapped]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public int OfficeId { get; set; }
        [NotMapped]
        public string FromDateStr { get; set; }
        [NotMapped]
        public string ToDateStr { get; set; }
        [NotMapped]
        public string AccountantFromDateStr { get; set; }
        [NotMapped]
        public string AccountantToDateStr { get; set; }
        [NotMapped]
        public string VoucharDateStr { get; set; }


        public int? OfficeManagerId { get; set; }
        public int? AccountantId { get; set; }
        public int? AuditorId { get; set; }
        public bool? IsSamparikshadDone { get; set; }
        public int? BerujuSubTitleId { get; set; }

        public int? BerujuSubTitleChildId { get; set; }

        [NotMapped]
        public int? SamparikshadReqMasterId { get; set; }
        [NotMapped]
        public decimal? RequestedTotalAmount { get; set; }

        public decimal? SamparikshadTotalAmount { get; set; }
        [NotMapped]
        public List<ExternalBeruju> ExternalBerujuList { get; set; }
        [NotMapped]
        public List<ExternalBeruju> ExternalBerujuListTopFive { get; set; }
        [NotMapped]
        public ExternalSamparikshadViewModel ObjExternalSamparikshadViewModel { get; set; }
        [NotMapped]
        public List<ExternalSamparikshadViewModel> ExternalSamparikshadViewModelList { get; set; }
        [NotMapped]
        public List<SamparikhadListViewModel> SamparikhadListViewModelList { get; set; }
        [NotMapped]
        public SamparikhadListViewModel ObjSamparikhadListViewModel { get; set; }

        [NotMapped]
        public List<SamparikhadListViewModelForReport> SamparikhadListViewModelForReportList { get; set; }

        [NotMapped]
        public List<SamparikhadRequestListViewModelForReport> SamparikhadRequestListViewModelForReportList { get; set; }

        [NotMapped]
        public List<SaidantikBeruju> SaidantikBerujuList { get; set; }


        public int? ChaluOrPujigatId { get; set; }
        public int? KoshTypeTitleListId { get; set; }

        public decimal? BerujuAmount { get; set; }
        public bool IsSaidantikBeruju { get; set; }
        //[NotMapped]
        //public bool IsSaidantikBerujuTrueFalse { get; set; }

        [NotMapped]
        public ToWhomDetailListVM ObjToWhomDetailListVM { get; set; }
        [NotMapped]
        public List<ToWhomDetailListVM> ToWhomDetailListVMList { get; set; }

        [NotMapped]
        public List<ExternalBerujuForSamparikshadVM> ExternalBerujuForSamparikshadVMList { get; set; }
        [NotMapped]
        public ExternalBerujuForSamparikshadVM ExternalBerujuForSamparikshadVMObj { get; set; }

        [NotMapped]
        public List<SamparikshadTowhomDetailVM> SamparikshadTowhomDetailVMListMain { get; set; }

        [NotMapped]
        public List<SamparikshadRequestTowhomDetailVM> SamparikshadRequestTowhomDetailVMMain { get; set; }

        [NotMapped]
        public SamparikshadReqMasterViewModel ObjSamparikshadReqMasterViewModel { get; set; }
        [NotMapped]
        public List<SamparikshadReqMasterViewModel> SamparikshadReqMasterViewModelList { get; set; }
        [NotMapped]
        public SamparikshadRequestMaterDetailVM ObjSamparikshadRequestMaterDetailVM { get; set; }


        [NotMapped]
        public List<GetsamparikshadrequesttowhomforletterViewModel> GetsamparikshadrequesttowhomforletterViewModelList { get; set; }

        [NotMapped]
        public List<ListBerujuForSamparikshadActionVM> ListBerujuForSamparikshadActionVMList { get; set; }
    }

    public class ExternalSamparikshadViewModel
    {
        public int SamparishadId { get; set; }
        public int ExternalBerujuId { get; set; }
        public int BerujuTypeId { get; set; }
        public decimal ReviesedVoucherAmount { get; set; }

        public DateTime RevisedDate { get; set; }
        public int OfficeId { get; set; }
        public int RevisedStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UploadFileDetails { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFileDetailsFileType { get; set; }

        public int ToWhomOfficeId { get; set; }
        public string LetterNumber { get; set; }
        public string RevisedRemarks { get; set; }
        public bool IsSamparikshadDone { get; set; }
        public int RequestStatus { get; set; }
        [NotMapped]
        public string RevisedDateStr { get; set; }

        [NotMapped]
        public SamparikshadTowhomDetailVM SamparikshadTowhomDetailVMObj { get; set; }
        [NotMapped]
        public List<SamparikshadTowhomDetailVM> SamparikshadTowhomDetailVMList { get; set; }
        //Static Code....
        public int? MalepaOrKumariChowkId { get; set; }

        public int SamparikshadReqMasterId { get; set; }



    }

    public class SamparikhadListViewModel
    {
        public int SamparishadId { get; set; }
        public int ExternalBerujuId { get; set; }
        public decimal ReviesedVoucherAmount { get; set; }
        public DateTime RevisedDate { get; set; }
        public string LetterNumber { get; set; }
        public int FiscalYearId { get; set; }
        public string BudgetSubTitle { get; set; }
        public string ExpenseTItle { get; set; }
        public string JVNUMBER { get; set; }
        public string BerujuNumber { get; set; }
        public decimal BerujuAmount { get; set; }

    }

    public class SamparikhadListViewModelForReport
    {
        public string FiscalYearTitle { get; set; }
        public string KoshTypeName { get; set; }
        public int ExternalBerujuId { get; set; }
        public decimal ReviesedVoucherAmount { get; set; }
        public string LetterNumber { get; set; }
        public string BerujuNumber { get; set; }
        public string JVNUMBER { get; set; }
        public string SubTitleName { get; set; }
        public int SamparishadId { get; set; }
        public decimal VoucharAmunt { get; set; }
       



    }


    public class SamparikhadRequestListViewModelForReport
    {

        public int SamparikshadReqOfficeId { get; set; }
        public int SamparikshadReqMasterId { get; set; }
        public int ExternalBerujuId { get; set; }
        public string JVNUMBER { get; set; }
        public string RequestedDateNep { get; set; }

        public int OfficeId { get; set; }
        public decimal RequestedAmount { get; set; }
        public string FiscalYearTitle { get; set; }
        public string KoshTypeName { get; set; }
        public string BerujuNumber { get; set; }

        public decimal BerujuAmount { get; set; }
        public string LetterNumber { get; set; }       
        public string SubTitleName { get; set; }

        public string RequestingOfficeName { get; set; }
       


    }

    public class ExternalBerujuForSamparikshadVM
    {
        public string KoshTypeName { get; set; }
        public string FiscalYearTitle { get; set; }
        public string BerujuNumber { get; set; }
        public decimal VoucharAmunt { get; set; }
        public DateTime VoucharDate { get; set; }
        public string JVNUMBER { get; set; }
        [NotMapped]
        public decimal RemainingAmount { get; set; }
        public string BerujuShorDesc { get; set; }

    }
    public class SamparikshadTowhomDetailVM
    {
        public int SMTowhomDetailId { get; set; }
        public int SamparikshadId { get; set; }
        public int ExternalBerujuId { get; set; }
        public int EBToWhomId { get; set; }

        [Required]
        public string PersonName { get; set; }
        public string PanNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobielNumber { get; set; }
        
        public decimal BerujuAmount { get; set; }
        public decimal? RevisedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? SamparikshadDate { get; set; }

        public decimal? IndividualAmount { get; set; }
        public int OfficeId { get; set; }
        public HttpPostedFileBase SupportingDocFiles { get; set; }

        public string UploadedFileUrl { get; set; }
    }

    public class SamparikshadDataIdModel
    {
        public int ExternalBerujuId { get; set; }
        public int SamparikshadReqMasterId { get; set; }

        public int SamparishadId { get; set; }
    }

    public class InternalSamparikshadTowhomDetailVM
    {
        public int InternalSMTowhomDetailId { get; set; }
        public int SamparikshadId { get; set; }
        public int InternalBerujuId { get; set; }
        public int IBToWhomId { get; set; }

        [Required]
        public string PersonName { get; set; }
        public string PanNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobielNumber { get; set; }

        public decimal BerujuAmount { get; set; }
        public decimal? RevisedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? SamparikshadDate { get; set; }

        public decimal? IndividualAmount { get; set; }
        public int OfficeId { get; set; }
    }

    public class SamparikshadRequestTowhomDetailVM
    {
        public int SMRequestTowhomDetailId { get; set; }
        public int SamparikshadId { get; set; }
        public int ExternalBerujuId { get; set; }
        public int EBToWhomId { get; set; }

        [Required]
        public string PersonName { get; set; }
        public string PanNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string MobielNumber { get; set; }
        public decimal BerujuAmount { get; set; }
        public decimal? RevisedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? SamparikshadDate { get; set; }
        public decimal? IndividualAmount { get; set; }
        public int OfficeId { get; set; }
    }


    public class InternalSamparikshadRequestTowhomDetailVM
    {
        public int InternalSMRequestTowhomDetailId { get; set; }
        public int SamparikshadId { get; set; }
        public int InternalBerujuId { get; set; }
        public int IBToWhomId { get; set; }

        [Required]
        public string PersonName { get; set; }
        public string PanNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string MobielNumber { get; set; }
        public decimal BerujuAmount { get; set; }
        public decimal? RevisedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? SamparikshadDate { get; set; }
        public decimal? IndividualAmount { get; set; }
        public int OfficeId { get; set; }
    }



    public class SamparikshadRequestMaterDetailVM
    {
        public string ToWhomMinistryName { get; set; }
        public string ToWhomDeptName { get; set; }
        public string ToWhomOfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public DateTime RequestedDateEng { get; set; }
        public string RequestedDateNep { get; set; }
        public string LetterNumber { get; set; }
        public int FYID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string Post { get; set; }
        public int OfficeId { get; set; }
        public string BerujuDafaNumber { get; set; }
        public string BerujuShortDes { get; set; }
        public int ToWhomofficeId { get; set; }
        public int ExternalBerujuId { get; set; }
        public string RemarksForRequest { get; set; }


    }

    public class GetsamparikshadrequesttowhomforletterViewModel
    {
        public string NameAndPost { get; set; }
        public decimal RevisedAmount { get; set; }
        public decimal BerujuAmount { get; set; }
    }

    public class ListBerujuForSamparikshadActionVM
    {
        public string FiscalYearTitleEng { get; set; }
        public int ExternalBerujuId { get; set; }
        public decimal? VoucharAmunt { get; set; }
        public string BudgetHead { get; set; }
        public string BerujuNumber { get; set; }
        public string JVNUMBER { get; set; }
        public int OfficeId { get; set; }

        public int KoshTypeId { get; set; }

        public int FiscalYearId { get; set; }
        public string ChaluOrPujigat { get; set; }
        public int SamparikshadReqMasterId { get; set; }

    }


    public class OfficeBerujuDTO
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int TotalBeruju { get; set; }

        public int TotalUnverified { get; set; }

        public int TotalVerified { get; set; }
    }


    public class BerujuCheckerReportFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int KoshTypeId { get; set; }
        public string BerujuNumber { get; set; }
        public int OfficeTypeSearchId { get; set; }

        public int MininstrySearchId { get; set; }

        public int BivagSearchId { get; set; }

        public bool BerujuStatus { get; set; }

        public int NirdeshnalayaSearchId { get; set; }


        public int KaryalayaSearchId { get; set; }

        public int TransferOfficeId { get; set; }

        public string CreatedBy { get; set; }

        public int? OfficeId { get; set; }            // single office selection
        public int? FiscalYearId { get; set; }        // dropdown selected fiscal year
        public string FiscalYearName { get; set; }    // optional, if you need FY text

        // Optional expansion:
        public int? MainOfficeId { get; set; }        // for parent office filtering
        public int? UserId { get; set; }              // if needed
        public int? Status { get; set; }              // pending/verified etc

        

        [NotMapped]
       public  List<OfficeBerujuDTO> BerujuList { get; set; }

        [NotMapped]
        public List<ExternalBeruju> ExternalBerujuList { get; set;}

        [NotMapped]
        public ExternalBeruju ExternalBeruju { get; set; }

    }


}
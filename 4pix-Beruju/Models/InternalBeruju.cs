using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class InternalBeruju
    {
        public int InternalBerujuId { get; set; }
        public int FiscalYearId { get; set; }
        public string BudgetSubTitle { get; set; }
        public string ExpenseTItle { get; set; }
        public string OfficeManagerName { get; set; }
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

        [NotMapped]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool WasMadeFinal { get; set; }
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
        public int? BerujuSubTitleId { get; set; }

        [NotMapped]
        public decimal? ReviesedVoucherAmount { get; set; }
        [NotMapped]
        public decimal? RemainingAmount { get; set; }


        public List<InternalBeruju> InternalBerujuList { get; set; }
        public List<InternalBeruju> InternalBerujuTopFiveList { get; set; }

        [NotMapped]
        public List<ManagerOrAuditorNameViewModel> ManagerOrAuditorNameViewModelList { get; set; }
        [NotMapped]
        public ManagerOrAuditorNameViewModel ObjManagerNameViewModel { get; set; }
        [NotMapped]
        public ManagerOrAuditorNameViewModel ObjAccountantNameViewModel { get; set; }
        [NotMapped]
        public ManagerOrAuditorNameViewModel ObjAuditorNameViewModel { get; set; }
        public int? ChaluOrPujigatId { get; set; }
        public int? KoshTypeTitleListId { get; set; }


        public decimal? BerujuAmount { get; set; }
        public bool? IsSaidantikBeruju { get; set; }
        //[NotMapped]
        //public bool IsSaidantikBerujuTrueFalse { get; set; }



        [NotMapped]
        public ToWhomDetailListVM ObjToWhomDetailListVM { get; set; }
        [NotMapped]
        public List<ToWhomDetailListVM> ToWhomDetailListVMList { get; set; }
        [NotMapped]
        public List<SaidantikBeruju> SaidantikBerujuList { get; set; }



        [NotMapped]
        public InternalBerujuForSamparikshadVM InternalBerujuForSamparikshadVMObj { get; set; }

        [NotMapped]
        public InternalSamparikshadReqMasterViewModel ObjSamparikshadReqMasterViewModel { get; set; }
        [NotMapped]
        public List<InternalSamparikshadReqMasterViewModel> SamparikshadReqMasterViewModelList { get; set; }
        [NotMapped]
        public List<InternalSamparikshadTowhomDetailVM> InternalSamparikshadTowhomDetailVMListMain { get; set; }
        [NotMapped]
        public InternalSamparikshadRequestMaterDetailVM ObjInternalSamparikshadRequestMaterDetailVM { get; set; }

        [NotMapped]
        public List<GetInternalsamparikshadrequesttowhomforletterViewModel> GetInternalsamparikshadrequesttowhomforletterViewModelList { get; set; }


        [NotMapped]
        public InternalSamparikshadViewModel ObjInternalSamparikshadViewModel { get; set; }
        [NotMapped]
        public List<InternalSamparikshadViewModel> ExternalSamparikshadViewModelList { get; set; }


    }


    public class InternalBerujuForSamparikshadVM
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

    public class ManagerOrAuditorNameViewModel
    {
        public int ManagerOrAuditorId { get; set; }
        public string ManagerOrAuditorName { get; set; }
        public DateTime FromDuration { get; set; }
        public DateTime ToDuration { get; set; }
        [NotMapped]
        public DateTime FromDateStr { get; set; }
        [NotMapped]
        public DateTime ToDateStr { get; set; }
        [NotMapped]
        public string AuditorPost { get; set; }

    }

    public class ToWhomDetailListVM
    {
        [Required]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "रकम लेख्नुहोस")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "अंक मिलेन ।.")]
        public decimal? AmountDetail { get; set; }
        public string PanNumber { get; set; }
        [Display(Name = "Mobile Number:")]
        //[Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobielNumber { get; set; }
        [NotMapped]
        public int MobileNumberInt { get; set; }
        [NotMapped]
        public string UploadedFileUrl { get; set; }

        public string VoucherNumber { get; set; }
        public string VoucherDate { get; set; }

    }

    public class SamparikshadReqMasterViewModel
    {
        public int SamparikshadReqMasterId { get; set; }
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
        [NotMapped]
        public int ExternalBerujuId { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFileDetailsFileType { get; set; }
        public string UploadedDoc { get; set; }
        [NotMapped]
        public string BerujuDafaNumber { get; set; }
        [NotMapped]
        public int ToWhomofficeId { get; set; }

  

        public string RemarksForRequest { get; set; }
        [NotMapped]
        public bool? IsSamparikshadDone { get; set; }//added new field

        [NotMapped]
        public int RequestToId { get; set; }

        [NotMapped]
        public string BerujuShortDescription { get; set; }
        public SamparikshadReqDetailViewModel ObjSamparikshadReqDetailViewModel { get; set; }
        public List<SamparikshadReqDetailViewModel> SamparikshadReqDetailViewModelList { get; set; }


        [NotMapped]
        public List<SamparikshadRequestTowhomDetailVM> SamparikshadRequestTowhomDetailVMMain { get; set; }


        [NotMapped]
        public List<SamparikshadTowhomDetailVM> SamparikshadTowhomDetailVMList { get; set; }//new code added here


    }


    public class InternalSamparikshadReqMasterViewModel
    {
        public int InternalSamparikshadReqMasterId { get; set; }
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
        [NotMapped]
        public int InternalBerujuId { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFileDetailsFileType { get; set; }
        public string UploadedDoc { get; set; }
        [NotMapped]
        public string BerujuDafaNumber { get; set; }
        [NotMapped]
        public int ToWhomofficeId { get; set; }

        [NotMapped]
        public string BerujuShortDescription { get; set; }
        public InternalSamparikshadReqDetailViewModel ObjInternalSamparikshadReqDetailViewModel { get; set; }
        public List<InternalSamparikshadReqDetailViewModel> InternalSamparikshadReqDetailViewModelList { get; set; }


        [NotMapped]
        public List<InternalSamparikshadRequestTowhomDetailVM> InternalSamparikshadRequestTowhomDetailVMMain { get; set; }


        [NotMapped]
        public List<InternalSamparikshadTowhomDetailVM> InternalSamparikshadTowhomDetailVMList { get; set; }//new code added here


    }



    public class SamparikshadReqDetailViewModel
    {
        public int SamparikshadReqDetailId { get; set; }
        public int MasterId { get; set; }
        public int InternalOrExteranlBerujuId { get; set; }
        public int InternalOrExternal { get; set; }
        public string BerujuDafaNumber { get; set; }
        public string BerujuShortDes { get; set; }
        public decimal BerujuAmount { get; set; }
    }

    public class InternalSamparikshadReqDetailViewModel
    {
        public int InternalSamparikshadReqDetailId { get; set; }
        public int MasterId { get; set; }
        public int InternalOrExteranlBerujuId { get; set; }
        public int InternalOrExternal { get; set; }
        public string BerujuDafaNumber { get; set; }
        public string BerujuShortDes { get; set; }
        public decimal BerujuAmount { get; set; }
    }
    public class InternalSamparikshadRequestMaterDetailVM
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


    }

    public class GetInternalsamparikshadrequesttowhomforletterViewModel
    {
        public string NameAndPost { get; set; }
        public decimal RevisedAmount { get; set; }
        public decimal BerujuAmount { get; set; }
    }

    public class InternalSamparikshadViewModel
    {
        public int InternalSamparishadId { get; set; }
        public int InternalBerujuId { get; set; }
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
        public int? MalepaOrKumariChowkId { get; set; }
        [NotMapped]
        public InternalSamparikshadTowhomDetailVM SamparikshadTowhomDetailVMObj { get; set; }
        [NotMapped]
        public List<InternalSamparikshadTowhomDetailVM> SamparikshadTowhomDetailVMList { get; set; }


    }









}
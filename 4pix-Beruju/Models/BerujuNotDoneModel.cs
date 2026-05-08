using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class BerujuNotDoneModel
    {
        public int BerujuNotDoneId { get; set; }
        public int OfficeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool NotDoneStatus { get; set; }
        public string UploadFileUrl { get; set; }
        public string NotDoneRemarks { get; set; }
        [Required]
        public int FiscalYearId { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFileDetailsFileType { get; set; }
        [NotMapped]
        public List<BerujuNotDoneModel> BerujuNotDoneModelList { get; set; }
        [NotMapped]
        public List<BerujuNotDoneModel> BerujuNotDoneModelListTopFive { get; set; }
        [NotMapped]
        public int InternalOrExternal { get; set; }

        public int? KoshTypeId { get; set; }
    }
}
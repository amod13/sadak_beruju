using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class SMSStatus
    {
        [Key]
        public int SMSStatusId { get; set; }

        public string MobileNumber { get; set; }

        public int? OfficeId { get; set; }

        public string ErrSuccessMessage { get; set; }

        public DateTime? InsertedDate { get; set; }

        public int? ExternalBerujuId { get; set; }

        public int? TowhomDetailsId { get; set; }

        public int? MaxLimitSMS { get; set; }

    }
}
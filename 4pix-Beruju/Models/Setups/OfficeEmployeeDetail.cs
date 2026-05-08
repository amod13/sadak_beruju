using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models.CustomValidation;

namespace _4pix_Beruju.Models.Setups
{
    public class OfficeEmployeeDetail
    {
        [Key]
        public int OfficeEmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Please enter position")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Please enter hire date")]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString =
            "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [HireDateValidation(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime HireDate { get; set; }
    }
}
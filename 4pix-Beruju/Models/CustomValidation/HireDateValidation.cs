using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.CustomValidation
{
    public class HireDateValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            if(dateTime<=DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult
                  ("Hire Date must be less than or equal to Today's Date.");
            }
            
        }
        //public override bool IsValid(object value)
        //{
        //    DateTime dateTime = Convert.ToDateTime(value);
        //    return dateTime <= DateTime.Now;
        //}
    }
}
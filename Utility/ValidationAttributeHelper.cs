using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bc.Web.Mvc.Utility
{
    public class RequiredDateTimeAttribute : ValidationAttribute
    {
        public RequiredDateTimeAttribute()
        { 

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (value is DateTime dateValue)
            {
                return dateValue > DateTime.MinValue;
            }

            return false;
        }
    }



    public class EcuadorPhoneNumberAttribute : ValidationAttribute
    {
        private static readonly Regex PhoneRegex = new Regex(@"^\+593\s\d{9}$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (phoneNumber == null || PhoneRegex.IsMatch(phoneNumber))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("El formato de celuar es  +593 999999999");
        }
    }
}

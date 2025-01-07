using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Group13_GoodRoots.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "The date must be valid and in the future";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("The date field is required");
            }

            if (!DateTime.TryParseExact(value.ToString(), "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
            {
                return new ValidationResult("The date must be in the format MM/YY");
            }

            if (expiryDate <= DateTime.Now)
            {
                return new ValidationResult("The date must be in the future");
            }

            return ValidationResult.Success;
        }
    }
}

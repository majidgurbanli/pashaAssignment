using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PashaVacancyProject.Logic.Validation
{
    public class AzerbaijaniPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Telefon nömrəsi mütləq doldurulmalıdır!");
            }

            var regex = new Regex(@"^\+994(?:50|51|55|70|77|\d{2})\d{7}$");
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult("Telefon nömrəsi +994xxyyyzzdd formatında olmalıdır!");
            }

            return ValidationResult.Success;
        }
    }
}

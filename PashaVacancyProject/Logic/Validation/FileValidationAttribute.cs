using System.ComponentModel.DataAnnotations;

namespace PashaVacancyProject.Logic.Validation
{
    public class FileValidationAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;
        private readonly string[] _allowedExtensions;

        public FileValidationAttribute(long maxFileSize, string[] allowedExtensions)
        {
            _maxFileSize = maxFileSize;
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return  new ValidationResult($"File boş ola bilməz");
            }

            // Check file size
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maksimum fayl ölçüsü: 5MB");
            }

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                return new ValidationResult("Qəbul olunan fayl formatları: PDF və ya DOCX.");
            }

            return ValidationResult.Success; // Validation passed
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Shared.Utils.Validators
{
    public class DecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly int _maxPrecision;
        private readonly bool _isRequired;

        public DecimalPrecisionAttribute(int maxPrecision, bool isRequired = true)
        {
            _maxPrecision = maxPrecision;
            _isRequired = isRequired;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (_isRequired && value == null)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            }

            if (value is decimal decimalValue)
            {
                var precision = GetPrecision(decimalValue);
                if (precision <= _maxPrecision)
                    return ValidationResult.Success;
            }
            else if (value == null)
            {
                // If the value is null and it's not required, consider it as valid.
                return ValidationResult.Success;
            }

            return new ValidationResult($"The {validationContext.DisplayName} field must be a decimal with a maximum precision of {_maxPrecision}.");
        }

        private static int GetPrecision(decimal value)
        {
            var parsingToString = value.ToString();
            return parsingToString.Length + 1;
        }
    }
}
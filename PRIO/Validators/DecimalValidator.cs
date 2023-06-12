using System.ComponentModel.DataAnnotations;

namespace PRIO.Validators
{
    public class DecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly int _maxPrecision;

        public DecimalPrecisionAttribute(int maxPrecision)
        {
            _maxPrecision = maxPrecision;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue)
            {
                var precision = GetPrecision(decimalValue);

                if (precision <= _maxPrecision)
                    return ValidationResult.Success;
            }

            return new ValidationResult($"The {validationContext.DisplayName} field must be a decimal with a maximum precision of {_maxPrecision}.");
        }
        private static int GetPrecision(decimal value)
        {
            var parsingToString = value.ToString();
            return parsingToString.Length + 1;
        }

        //private static int GetScale(decimal value)
        //{
        //    var bits = decimal.GetBits(value);
        //    var scale = (bits[3] >> 16) & 0x7F;
        //    return scale;
        //}

    }
}
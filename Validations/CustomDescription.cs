using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validations
{
    public class CustomDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string) value;
            int count = fieldValue.Length - fieldValue.Replace(" ", "").Length;
            if (fieldValue == null || count <= 5)
            {
                return new ValidationResult("the desscription must contain at least 5 words");
            }
            return ValidationResult.Success;
        }
    }
}

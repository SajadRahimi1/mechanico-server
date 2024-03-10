using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mechanico_Api.Validations;

public class PhoneNumberValidation:ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var regex = new Regex("^09[0-9]{9}$");
        var isValidNumber= regex.IsMatch(value?.ToString() ?? "");

        return isValidNumber ? ValidationResult.Success : new ValidationResult("شماره تلفن باید با 09 شروع شود و 11 رقم باشد");
    }
}
using System.ComponentModel.DataAnnotations;
using Mechanico_Api.Validations;

namespace Mechanico_Api.Dtos;

public class SendCodeDto
{
    [Required,PhoneNumberValidation]
    public string phoneNumber { get; set; }
}
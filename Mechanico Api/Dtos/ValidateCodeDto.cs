using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Dtos;

public sealed class ValidateCodeDto
{
    [Required(ErrorMessage = "لطفا کد را وارد کنید")]
    public string code { get; set; }

    [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید")]
    public string phoneNumber { get; set; }
}
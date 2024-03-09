using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Dtos;

public sealed class ValidateCodeDto
{
    [Required()] public string code { get; set; }

    [Required()] public string phoneNumber { get; set; }
}
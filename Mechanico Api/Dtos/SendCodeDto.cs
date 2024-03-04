using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Dtos;

public class SendCodeDto
{
    [Required]
    public string phoneNumber { get; set; }
}
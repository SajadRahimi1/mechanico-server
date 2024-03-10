using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Dtos;

public class LicenseImageDto
{
    [Required(ErrorMessage = "عکس را وارد کنید")]
    public IFormFile LicenseImage { get; set; }
}
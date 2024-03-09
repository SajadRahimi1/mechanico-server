using Mechanico_Api.Validations;

namespace Mechanico_Api.Dtos;

public class UpdateUserDto
{
    public string? Name { get; set; }
    
    
    public string? Email { get; set; }
    
    public IFormFile? Image { get; set; }
    
    public bool IsMale { get; set; }
}
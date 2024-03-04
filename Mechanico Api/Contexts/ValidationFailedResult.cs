using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mechanico_Api.Contexts;

public class ValidationFailedResult:ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary? modelState) : base(new ErrorResult(modelState))
    {
        
    }
}
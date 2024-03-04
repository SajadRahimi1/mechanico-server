using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mechanico_Api.Contexts;

public class ValidationFailedResult:ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary? modelState) : base(new Result
    {
        Errors = modelState?.Keys.SelectMany(key=>modelState[key].Errors.Select(e=>e.ErrorMessage)).ToList(),
        StatusCode = StatusCodes.Status400BadRequest
    })
    {
        StatusCode = 400;
    }
}
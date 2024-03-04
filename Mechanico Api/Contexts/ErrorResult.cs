using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mechanico_Api.Contexts;

public class ErrorResult
{
    public List<ValidationError> Errors { get; }
    public string? Message { get; }

    public ErrorResult(ModelStateDictionary modelState)
    {
        Message = "Validation Failed";
        Errors = modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(e => new ValidationError(key, e.ErrorMessage))).ToList();
    }
}

public class ValidationError
{
    public string? Field { get; }

    public string? Message { get; }

    public ValidationError(string? field, string? message)
    {
        Field = field;
        Message = message;
    }
}
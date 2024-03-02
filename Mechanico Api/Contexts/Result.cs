using Microsoft.AspNetCore.Mvc;

namespace Mechanico_Api.Contexts;

public class ActionResult:IActionResult
{
    public Result Result { get; set; }
    
    public async Task ExecuteResultAsync(ActionContext context)
    {

        var result = new ObjectResult(Result);
        await result.ExecuteResultAsync(context);
    }
}

public abstract class Result
{
    public int StatusCode { get; set; } = StatusCodes.Status200OK;

    public object? Data { get; set; }

    public abstract string? Token { set; }

    public abstract string? Message {  set; }
}
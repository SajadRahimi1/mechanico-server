using Microsoft.AspNetCore.Mvc;

namespace Mechanico_Api.Contexts;

public class ActionResult : IActionResult
{
    public Result Result;

    public ActionResult(Result result)
    {
        Result = result;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var result = new ObjectResult(Result);
        await result.ExecuteResultAsync(context);
    }
}

public class Result
{
    public int StatusCode { get; set; } = StatusCodes.Status200OK;

    public object? Data { get; set; }

    public string? Token { get; set; }

    public string? Message { get; set; }
}
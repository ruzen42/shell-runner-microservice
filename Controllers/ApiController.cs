using Microsoft.AspNetCore.Mvc;
using ShellRunner.Models;
using ShellRunner.Services;

namespace ShellRunner.Controllers;

[ApiController]
[Route("api/")]
public class ApiController(ILogger<ApiController> logger, IProcessRunner processRunner) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ExecuteCommand([FromQuery] QueryRequest models)
    {
        if (string.IsNullOrEmpty(models.Command))
            return BadRequest("Command cannot be empty");

        try
        {
            logger.LogInformation("Received command:\n {Context}", models.ToString());
            return Ok(await processRunner.RunCommand(models.Command));
        }
        catch (Exception e)
        {
            logger.LogError("Error: {EMessage}", e.Message);
            return Problem(new QueryResponse("", "Error with HTTP response: " + e.Message, 1).ToString());
        }
    }
}

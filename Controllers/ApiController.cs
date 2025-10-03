using Microsoft.AspNetCore.Mvc;
using ShellRunner.Models;
using ShellRunner.Services;

namespace ShellRunner.Controllers;

[ApiController]
[Route("api/")]
public class ApiController(ILogger<ApiController> logger, IProcessRunner processRunner) : ControllerBase
{
    [HttpPost] 
    public async Task<IActionResult> ExecuteCommand([FromBody] QueryRequest request)
    {
        if (string.IsNullOrEmpty(request.Command))
            return BadRequest("Command cannot be empty");

        if (request.Command.Length > 100) 
            return BadRequest("Command too long");

        try
        {
            logger.LogInformation("Received command execution request");
            var result = await processRunner.RunCommand(request.Command);
            
            if (result.ExitCode == 0)
            {
                logger.LogInformation("Command executed successfully");
                return Ok(result);
            }

            logger.LogWarning("Command execution failed with exit code: {ExitCode}", result.ExitCode);
            return BadRequest(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error executing command");
            return Problem(
                detail: $"Error executing command: {e.Message}",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }

}

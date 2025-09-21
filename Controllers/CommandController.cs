using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShellRunner.Models;

namespace ShellRunner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommandController(ILogger<CommandController> logger) : ControllerBase
{
    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteCommand([FromBody] CommandRequest request)
    {
        if (string.IsNullOrEmpty(request.Command))
            return BadRequest("Command cannot be empty");

        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{request.Command}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();
            
            logger.LogInformation("Received command:\n {Context}", request.ToString());
            var response = new CommandResponse(output, error, process.ExitCode);
            logger.LogInformation((response with { Output = output[..(output.Length/2)] }).ToString());
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError("Error: {EMessage}", e.Message);
            return Ok(new CommandResponse("", "Error with HTTP response: " + e.Message, 1));
        }
    }
}

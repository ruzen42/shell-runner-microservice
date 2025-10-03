using System.Diagnostics;
using ShellRunner.Models;

namespace ShellRunner.Services;

public class ProcessRunner(ILogger<ProcessRunner> logger) : IProcessRunner
{
    private static readonly string[] DangerousPatterns = ["&", "|", ";", "`", "$", ">", "<", "\\", "..", "//"];

    public async Task<QueryResponse> RunCommand(string command)
    {
        if (!IsCommandSafe(command))
        {
            logger.LogWarning("Attempted to execute unsafe command: {Command}", command);
            return new QueryResponse("", "Command is not allowed for security reasons", 1);
        }

        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{EscapeCommand(command)}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Directory.GetCurrentDirectory() 
                }
            };

            process.Start();
            
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            
            var response = new QueryResponse(output, error, process.ExitCode);
            
            logger.LogInformation("Command executed. Exit code: {ExitCode}, Output length: {OutputLength}, Error length: {ErrorLength}", 
                process.ExitCode, output.Length, error.Length);
                
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing command: {Command}", command);
            return new QueryResponse("", $"Process error: {ex.Message}", 1);
        }
    }

    private static bool IsCommandSafe(string command) => 
        !DangerousPatterns.Any(command.Contains) && !string.IsNullOrWhiteSpace(command);

    private static string EscapeCommand(string command)
    {
        return command.Replace("\"", "\\\"").Replace("$", "\\$");
    }
}
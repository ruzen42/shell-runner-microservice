namespace ShellRunner.Models;

public class CommandResponse
{
    public string? Output { get; set; }
    public required CommandRequest Context { get; set; }
    public string? Error { get; set; }
    public required int ExitCode { get; set; }
}
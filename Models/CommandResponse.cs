namespace ShellRunner.Models;

public class CommandResponse
{
    public string? Output { get; set; }
    public string? Error { get; set; }
    public required int ExitCode { get; init; }
}

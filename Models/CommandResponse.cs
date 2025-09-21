namespace ShellRunner.Models;

public record CommandResponse(string? Output, string? Error, int ExitCode);

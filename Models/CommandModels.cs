namespace ShellRunner.Models;

public record CommandModels(string Command);
public record CommandResponse(string? Output, string? Error, int ExitCode);

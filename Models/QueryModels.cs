namespace ShellRunner.Models;

public record QueryRequest(string Command);
public record QueryResponse(string? Output, string? Error, int ExitCode);

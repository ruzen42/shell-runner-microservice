namespace ShellRunner.Models;

public record CommandRequest
{
    public required string Command { get; set; }
    public override string ToString() => $"Command: {Command}\n userId: {UserId}\n from chat: {ChatId}";
}

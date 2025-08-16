namespace ShellRunner.Models;

public class CommandRequest
{
    public required string Command { get; set; }
    public override string ToString() => $"Command: {Command}\n userId: {UserId}\n from chat: {ChatId}";
}

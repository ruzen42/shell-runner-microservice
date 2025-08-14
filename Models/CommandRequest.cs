namespace ShellRunner.Models;

public class CommandRequest
{
    public required string Command { get; set; }
    public required long UserId { get; set; }
    public required long ChatId  { get; set; }

    public override string ToString() => $"Command: {Command}\n userId: {UserId}\n from chat: {ChatId}";
}
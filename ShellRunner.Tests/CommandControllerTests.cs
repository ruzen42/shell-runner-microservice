using Microsoft.AspNetCore.Mvc;
using Moq;
using ShellRunner.Controllers;
using ShellRunner.Models;
using Xunit;

namespace ShellRunner.ShellRunner.Tests;

public class CommandControllerTests
{
    private readonly Mock<ILogger<CommandController>> _mockLogger = new();
    [Fact]
    public void ExecuteCommand_ValidCommand_ReturnsOkWithOutput()
    {
        var controller = new CommandController(_mockLogger.Object);
        var request = new CommandRequest
        {
            Command = "echo Hello", 
            UserId = 3213212,
            ChatId = 323132
        };

        var result = controller.ExecuteCommand(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CommandResponse>(okResult.Value);
        Assert.Equal("Hello\n", response.Output); 
        Assert.Equal(0, response.ExitCode);
    }
    
    [Fact]
    public void ExecuteCommand_EmptyCommand_ReturnsBadRequest()
    {
        var controller = new CommandController(_mockLogger.Object);
        var request = new CommandRequest
        {
            Command = "", 
            UserId = 3213212,
            ChatId = 323132
        };

        var result = controller.ExecuteCommand(request);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Command cannot be empty", badRequestResult.Value);
    }
}
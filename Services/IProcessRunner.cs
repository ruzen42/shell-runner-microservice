using ShellRunner.Models;

namespace ShellRunner.Services;

public interface IProcessRunner
{
    Task<QueryResponse> RunCommand(string command);
}
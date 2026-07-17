using System;
namespace task17;
public static class ExceptionHandler
{
    private static Exception _ex;
    private static ICommand _command;
    private static int error_count;
    public static void Handle(ICommand command, Exception ex)
    {
        _ex = ex;
        _command = command;
        error_count ++;
        Console.WriteLine($"Command <<{command}>> error with exception {ex}");
        Console.WriteLine($"total error amount: {error_count}");
    }
    public static ICommand GetCommand() => _command;
    public static Exception GetException() => _ex;
    public static int GetErrorCount() => error_count;
    public static void Reset()
    {
        _command = null;
        _ex = null;
        error_count = 0;
    }
        
}
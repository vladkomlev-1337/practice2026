using System;
using System.Windows.Input;
namespace task17;
public class ExceptionHandler
{
    public virtual void Handle(Exception ex, ICommand command)
    {
        Console.WriteLine($"Exception: {ex}, command: {command}");
    }
}
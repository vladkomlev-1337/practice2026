namespace task17;
public class SoftStopCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Softstop executed");
    }
}
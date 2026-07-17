namespace task17;
public class HardStopCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Hardstop executed");
    }
}
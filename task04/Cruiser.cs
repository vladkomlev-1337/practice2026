using task04;
namespace task04;
public class Cruiser : ISpaceship
{
    public int Speed {get;} = 50;
    public int FirePower {get;} = 100;
    public void MoveForward()
    {
        Console.WriteLine("Cruiser moved forward");
    }
    public void Rotate(int angle)
    {
        Console.WriteLine($"Cruiser turned {angle} deg.");
    }
    public void Fire()
    {
        Console.WriteLine("Cruiser fired a shot!");
    }
    
}
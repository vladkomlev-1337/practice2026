using task04;
namespace task04;
public class Fighter : ISpaceship
{
    public int Speed {get;} = 100;
    public int FirePower {get;} = 30;
    public void MoveForward()
    {
       Console.WriteLine("Fighter moved forward");
    }
    public void Rotate(int angle)
    {
       Console.WriteLine($"Fighter turned {angle} deg.");
    }
    public void Fire()
    {
        Console.WriteLine("Fighter fired a shot!");
    }
}
using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }
    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(30, fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }
    [Fact]
    public void Fighter_ShouldBeWeakerThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }
    [Fact]
    public void Fighter_FunctionalCheck()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        fighter.Rotate(90);
        fighter.Fire();
    }
    [Fact]
    public void Cruiser_FunctionalCheck()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        cruiser.Rotate(90);
        cruiser.Fire();
    }
}
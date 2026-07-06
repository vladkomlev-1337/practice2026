using task11;
using Xunit;
namespace task11tests;

public class CalcTests
{
    [Fact]
    public void Addition_ReturnsCorrect()
    {
        ICalculator calc = new ClassGenerator().MakeCalc();
        int result = calc.Add(18,44);
        Assert.Equal(62, result);
    }
    [Fact]
    public void Substraction_ReturnsCorrect()
    {
        ICalculator calc = new ClassGenerator().MakeCalc();
        int result = calc.Minus(10,20);
        Assert.Equal(-10, result);
    }
    [Fact]
    public void Multiplication_ReturnsCorrect()
    {
        ICalculator calc = new ClassGenerator().MakeCalc();
        int result = calc.Mul(18,2);
        Assert.Equal(36, result);
    }
    [Fact]
    public void Division_ReturnsCorrect()
    {
        ICalculator calc = new ClassGenerator().MakeCalc();
        int result = calc.Div(36,3);
        Assert.Equal(12, result);
    }
}

using Xunit;
using task05;

public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }
    [Fact]
    public void GetAllProps()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var props = analyzer.GetProperties();
        Assert.Contains("Property", props);
    }
    [Fact]
    public void HasAttribute_FalseAndTrue()
    {
        var analyzertrue = new ClassAnalyzer(typeof(AttributedClass));
        var analyzerfalse = new ClassAnalyzer(typeof(TestClass));
        Assert.Equal(true, analyzertrue.HasAttribute<SerializableAttribute>());
        Assert.Equal(false, analyzerfalse.HasAttribute<SerializableAttribute>());
    }
    [Fact]
    public void GetMethodParams_ReturnCorrectAnswForMethod()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        Assert.Contains("Void", analyzer.GetMethodParams("Method"));
    }
}
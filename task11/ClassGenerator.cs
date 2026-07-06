using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.IO;
namespace task11;
public class ClassGenerator
{
    public ICalculator MakeCalc()
    {
        string code = @"
        using System;
        using task11;
        public class Calculator : ICalculator {
            public int Add(int a, int b) => a + b;
            public int Minus(int a, int b) => a - b;
            public int Mul(int a, int b) => a * b;
            public int Div(int a, int b) => a / b;
        }";
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        var libs = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location)
        };
        var compilator = CSharpCompilation.Create("Calc", new[] {syntaxTree}, libs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        using var ms = new MemoryStream();
        EmitResult result = compilator.Emit(ms);
        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());
        var type = assembly.GetType("Calculator");
        return (ICalculator)Activator.CreateInstance(type);
    }
}   
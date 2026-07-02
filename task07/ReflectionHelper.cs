using System.ComponentModel;
using System.Reflection;

namespace task07;
public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type) {
        var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();
        var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        var props = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        List<string> answ = [];
        if (displayName != null)
        {
            answ.Add(displayName.DisplayName);
        }
        else
        {
            answ.Add("null");
        }
        if (version != null)
        {
            answ.Add($"{version.Major}.{version.Minor}");
        }
        else
        {
            answ.Add("null");
        }
        if (methods.Count() != 0)
        {
            foreach (var method in methods) {
                var methoddispname = method.GetCustomAttribute<DisplayNameAttribute>();
                if (methoddispname != null)
                {
                    answ.Add($"{method.Name}:{methoddispname.DisplayName}");
                }
                else
                {
                    answ.Add($"{method.Name}:null");
                }
            }
        }
        else
        {
            answ.Add("class have no methods");
        }
        if (props.Count() != 0)
        {
            foreach (var prop in props)
            {
                var propdispname = prop.GetCustomAttribute<DisplayNameAttribute>();
                if (propdispname != null)
                {
                    answ.Add($"{prop.Name}:{propdispname.DisplayName}");
                }
                else
                {
                    answ.Add($"{prop.Name}:null");
                }
            }
        }
        else
        {
            answ.Add("class have no props");
        }
        foreach (var line in answ)
        {
            Console.WriteLine(line);
        }
    }
}
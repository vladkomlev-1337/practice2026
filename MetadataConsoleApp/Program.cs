using System;
using task07;
using System.Linq;
using System.Reflection;
class Program
{
    static void Main(string[] args)
    {
        string dllpath = args[0];
        Assembly assembly = Assembly.LoadFrom(dllpath);
        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
        Console.WriteLine("Classes:");
        foreach(Type type in types)
        {
            Console.WriteLine(type.Name);
            var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
            var version = type.GetCustomAttribute<VersionAttribute>();
            if (displayName != null) {
                Console.WriteLine($"disp. name: {displayName.DisplayName}");
            }
            else
            {
                Console.WriteLine("class have no display name attribute");
            }
            if (version != null)
            {
                Console.WriteLine($"ver:{version.Major}.{version.Minor}");
            }

            else
            {
                Console.WriteLine("class have no version attribute");
            }
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).Where(m=> !m.IsSpecialName && m.DeclaringType == type).ToList();
            if (methods.Count > 0)
            {
                Console.WriteLine("Methods:");
                foreach (var method in methods)
                {
                    Console.WriteLine(method.Name);
                    var paramss = method.GetParameters().Select(p=>$"{p.ParameterType.Name},{p.Name}").ToList();
                    Console.WriteLine(string.Join(",", paramss));
                }

            }
            else
            {
                Console.WriteLine("Class have no methods");
            }
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Count() > 0)
            {
                foreach (var constructor in constructors)
                {
                    var paramss = constructor.GetParameters().Select(c => $"{c.ParameterType.Name},{c.Name}").ToList();
                    Console.WriteLine(string.Join(",",paramss));
                }
            }
            else
            {
                Console.WriteLine("Class have no constructors");
            }
        }
    }
}
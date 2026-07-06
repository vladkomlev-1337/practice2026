using System.Reflection;
using Plugin;
using System.Linq;
class Program
{
    static void Main()
    {
        string currPath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net10.0");
        string[] ddls = Directory.GetFiles(currPath, "*.dll");
        
        List<(Type type, string Name, string[] Depends)> plugins = new();
        foreach (var dllfile in ddls)
        {
            Assembly assembly = Assembly.LoadFrom(dllfile);
            foreach(Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoadAttribute>() != null)
                {
                    if (type.GetCustomAttribute<PluginLoadAttribute>().Depends.Length == 0)
                    {
                        plugins.Add((type, type.Name, new string[0]));
                    }
                    else
                    {
                        plugins.Add((type, type.Name, type.GetCustomAttribute<PluginLoadAttribute>().Depends));
                    }
                    
                }
            }
        }
        List<string> loadorder = new List<string>();
        var remaining = plugins.ToList();
        foreach (var plugin in plugins.Where(p => p.Depends.Length == 0))
        {
            loadorder.Add(plugin.Name);
            remaining.Remove(plugin);
        }
        while (remaining.Count > 0)
        {
            var pluginwdependsInLoadoder = remaining.Where(p => p.Depends.All(d => loadorder.Contains(d))).ToList();
            foreach (var plugintoadd in pluginwdependsInLoadoder)
            {
                loadorder.Add(plugintoadd.Name);
                remaining.Remove(plugintoadd);
            }
        }
        foreach (string nameplugin in loadorder)
        {
            var plugin = plugins.First(p => p.Name == nameplugin);
            var obj = Activator.CreateInstance(plugin.type);
            plugin.type.GetMethod("Execute").Invoke(obj, null);
        }
        
    }
}
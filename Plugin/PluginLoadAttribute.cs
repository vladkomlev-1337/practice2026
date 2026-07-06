namespace Plugin;

public class PluginLoadAttribute : Attribute
{
    public string[] Depends {get;set;}   
    public PluginLoadAttribute()
    {
        Depends = new string[0];
    }
    public PluginLoadAttribute(string[] depends)
    {
        Depends = depends;
    }
} 

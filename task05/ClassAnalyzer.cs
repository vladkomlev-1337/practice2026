using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type), "cant be null");
        }
        _type = type;
        
    }
    public IEnumerable<string> GetPublicMethods()
    {
        return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(s => s.Name);
    }
    
    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var met = _type.GetMethod(methodname);
        if (met == null)
        {
            return Enumerable.Empty<string>();
        }
        string returntype = _type.GetMethod(methodname).ReturnType.Name;
        IEnumerable<string> names = _type.GetMethod(methodname).GetParameters().Select(s => s.Name);
        names = names.Append(returntype);
        return names;
    }

    public IEnumerable<string> GetAllFields()
    {
        return _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance).Select(s => s.Name);
    }
    public IEnumerable<string> GetProperties()
    {
        return _type.GetProperties().Select(s=> s.Name);       
    }
    public bool HasAttribute<T>() where T : Attribute
    {
       return _type.IsDefined(typeof(T),false);
    }
}
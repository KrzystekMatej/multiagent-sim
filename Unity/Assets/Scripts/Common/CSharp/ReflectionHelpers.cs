using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionHelpers
{
    public static List<T> CreateAllSubclasses<T>()
    {
        var baseType = typeof(T);
        var subclasses = Assembly.GetAssembly(baseType)
            .GetTypes()
            .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract);

        var instances = new List<T>();
        foreach (var subclass in subclasses)
        {
            if (subclass.GetConstructor(Type.EmptyTypes) != null)
            {
                instances.Add((T)Activator.CreateInstance(subclass)!);
            }
        }

        return instances;
    }
}

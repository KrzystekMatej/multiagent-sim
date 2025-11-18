using UnityEngine;
using System.Collections.Generic;
using System;

public class AgentContext : MonoBehaviour
{
    private Dictionary<Type, Component> cache = new();

    public T Get<T>(bool save = true) where T : Component
    {
        if (!cache.TryGetValue(typeof(T), out var component))
        {
            component = GetComponentInChildren<T>();
            if (component && save) cache[typeof(T)] = component;
        }
        return component as T;
    }

    void Awake() 
    {
        foreach (var component in GetComponentsInChildren<AgentComponent>())
        {
            cache[component.GetType()] = component;
            component.Initialize(this);
        }
    }
}
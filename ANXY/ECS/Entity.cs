using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ANXY.ECS;

/// <summary>
///     Represents any Gameobject and holds a list with its components as well as a position vector
/// </summary>
public sealed class Entity
{
    private readonly List<Component> _components = new();

    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    ///     Adds the given component to the Entity, calls its initialize() method. If already a
    ///     Component of the same type exists, it gets overwritten to ensure that each type of component
    ///     only exists once.
    /// </summary>
    /// <param name="component"></param>
    public void AddComponent(Component component)
    {
        component.Entity = this;
        _components.Add(component);
    }

    /// <summary>
    ///     Returns the first occurring Component of the type if the Entity has one attached
    /// </summary>
    /// <typeparam name="T">The type of Component to retrieve.</typeparam>
    /// <returns>T A Component of the matching type, otherwise null if no Component is found.</returns>
    public T GetComponent<T>() where T : Component
    {
        return (T)_components.FirstOrDefault(c => c.GetType() == typeof(T));
    }

    public List<T> GetComponents<T>() where T : Component
    {
        return _components
            .Where(component => component.GetType() == typeof(T))
            .Cast<T>()
            .ToList();
    }
}
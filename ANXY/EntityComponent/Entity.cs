using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ANXY.EntityComponent;

/// <summary>
///     Represents any Gameobject and holds a list with its components as well as a position vector
/// </summary>
public sealed class Entity
{
    public static int previousID;
    private readonly List<Component> _components = new();
    public int ID { get; set; } = previousID++;

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

        /* see why this is the same
        foreach (var component in _components)
            if (component.GetType() == typeof(T))
                return (T)component;
        return null;*/
    }

    public List<T> GetComponents<T>() where T : Component
    {
        return _components
            .Where(component => component.GetType() == typeof(T))
            .Cast<T>()
            .ToList();
    }

    /*
    /// <summary>
    ///     Removes the first Component of specified type, by call its Destroy()
    ///     method and then removing it from the List
    /// </summary>
    /// <param name="type">type of the Component to remove</param>
    public bool RemoveComponent(Component component)
    {
        if (_components.Remove(component))
        {
            component.Destroy();
            return true;
        }

        return false;
    }

    public void Update(GameTime gameTime)
    {
        foreach (var component in _components)
            if (component.IsActive)
                component.Update(gameTime);
    }

    public void Initialize()
    {
        foreach (var component in _components)
            if (component.IsActive)
                component.Initialize();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var component in _components)
            if (component.IsActive)
                component.Draw(gameTime, spriteBatch);
    }
    */
}
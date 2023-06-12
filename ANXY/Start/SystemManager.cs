using ANXY.ECS.Systems;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ANXY.Start;

/// <summary>
/// SystemManager holds all Systems that are added to the list via Register(). 
/// It is a helper class to easily go through all systems with its components and call each method.
/// If specific order is needed then use the GetSystemByType methode and then call the specific method.
/// Don't forget: to get the benefits of ECS call each component and don0t loop through each Entity.
/// </summary>
public sealed class SystemManager
{
    public static SystemManager Instance => _lazy.Value;
    private static readonly Lazy<SystemManager> _lazy = new(() => new SystemManager());
    private readonly List<ISystem> _systems = new();

    /// <summary>
    /// Adds the system to the list. 
    /// </summary>
    /// <param name="system">The System needs to implement ISystem interface</param>
    public void Register(ISystem system)
    {
        if (_systems.Contains(system)) { return; }
        _systems.Add(system);
    }

    /// <summary>
    /// Removes the system from the list
    /// </summary>
    /// <param name="system">The System needs to implement ISystem interface</param>
    /// <returns>true: system got removed; false: list didn't contain the system</returns>
    public bool Unregister(ISystem system)
    {
        if (_systems.Contains(system))
        {
            _systems.Remove(system);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Calls Initialize() in all systems.
    /// </summary>
    public void InitializeAll()
    {
        foreach (var system in _systems) system.Initialize();
    }

    /// <summary>
    /// Calls Update() in all systems.
    /// </summary>
    /// <param name="gameTime">current game time</param>
    public void UpdateAll(GameTime gameTime)
    {
        foreach (var system in _systems) system.Update(gameTime);
    }

    /// <summary>
    /// Calls Draw() in all systems
    /// </summary>
    /// <param name="gameTime">current game time</param>
    /// <param name="spriteBatch">spriteBatch of game class</param>
    public void DrawAll(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var system in _systems) system.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Returns the system of the specified component. Can be used to manipulate components
    /// in a particular order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISystem FindSystemByType<T>() where T : Component
    {
        return _systems.FirstOrDefault(s => s.GetType() == typeof(System<T>));
    }
}

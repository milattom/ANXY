﻿using ANXY.ECS.Components;
using ANXY.ECS.Systems;
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

    private SystemManager()
    {

    }

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
    /// Sets a new resolution for the camera
    /// </summary>
    /// <param name="resolution"></param>
    public void UpdateResolution(Vector2 resolution)
    {
        foreach (var _ in from system in _systems
                          where system is CameraSystem
                          select new { })
        {
            CameraSystem.SetResolution(resolution);
        }
    }

    /// <summary>
    /// Calls Draw() in all systems
    /// </summary>
    /// <param name="gameTime">current game time</param>
    /// <param name="spriteBatch">spriteBatch of game class</param>
    public void DrawAll(GameTime gameTime, SpriteBatch spriteBatch)
    {
        SpriteSystem.Instance.Draw(gameTime, spriteBatch);
        BackgroundSpriteSystem.Instance.Draw(gameTime, spriteBatch);
        
        PlayerSystem.Instance.Draw(gameTime, spriteBatch);
        PlayerSpriteSystem.Instance.Draw(gameTime, spriteBatch);
        DogSpriteSystem.Instance.Draw(gameTime, spriteBatch);

        ForegroundSpriteSystem.Instance.Draw(gameTime, spriteBatch);
        BoxColliderSystem.Instance.Draw(gameTime, spriteBatch);
        TextRendererSystem.Instance.Draw(gameTime, spriteBatch);
        CameraSystem.Instance.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Returns the system of the specified component. Can be used to manipulate components
    /// in a particular order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISystem FindSystemByType<T>() where T : Component
    {
        return _systems.FirstOrDefault(s => s.GetType().BaseType.GetGenericArguments().First() == typeof(T));
    }
}

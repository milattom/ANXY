﻿using ANXY.ECS.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static ANXY.ECS.Components.SingleSpriteRenderer;

namespace ANXY.ECS.Systems;

/// <summary>
/// System is used to hold the References of all Components of Type T.
/// This makes it possible to load them during runtime/usage more efficiently into the cache.
/// Instead of loading each Entity into the cache and looping through them to finaly get each
/// component, a system provides the possibility to load all the needed components of a type
/// such as Collider into the cache and loop right through them which results in better performance.
/// System implements the ISystem interface which is needed to do a Non Generic call on its methods.
/// </summary>
/// <typeparam name="T">Type of component</typeparam>
public class System<T> : ISystem where T : Component
{
    protected static readonly List<T> components = new();

    /// <summary>
    /// Adds a component to the system.
    /// </summary>
    /// <param name="component"></param>
    public void Register(T component)
    {
        components.Add(component);
    }

    public void Unregister(T component)
    {
        components.Remove(component);
    }

    /// <summary>
    /// Calls Initialize() in all components of this system.
    /// </summary>
    public void Initialize()
    {
        foreach (T component in components)
        {
            if (component.IsActive) component.Initialize();
        }
    }

    /// <summary>
    /// Calls Update() in all components of this system.
    /// </summary>
    /// <param name="gameTime">current game time</param>
    public void Update(GameTime gameTime)
    {
        foreach (T component in components)
        {
            if (component.IsActive) component.Update(gameTime);
        }
    }

    /// <summary>
    /// Calls Draw() in all components of this system.
    /// </summary>
    /// <param name="gameTime">current game time</param>
    /// <param name="spriteBatch">SpriteBatch of Game class</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (T component in components)
        {
            if (component.IsActive) component.Draw(gameTime, spriteBatch);
        }
    }

    /// <summary>
    /// Returns first component of the list
    /// </summary>
    /// <returns></returns>
    public Component GetFirstComponent()
    {
        return components[0];
    }
}

class PlayerSystem : System<Player>
{
    private static readonly Lazy<PlayerSystem> _lazy = new(() => new PlayerSystem());
    public static PlayerSystem Instance => _lazy.Value;
    private PlayerSystem()
    {
        SystemManager.Instance.Register(this);
    }

    /// <summary>
    /// Reset the player and destroy all other players
    /// </summary>
    internal void Reset()
    {
        var player = (Player)GetFirstComponent();
        player.Reset();

        var otherPlayerList = components
            .Where(c => c != player)
            .ToList();
        var otherPlayerEntityList = otherPlayerList
            .Select(c => c.Entity)
            .ToList();

        var otherComponentsPlayer = otherPlayerEntityList
            .SelectMany(e => e.GetComponents<Player>())
            .ToList();
        otherComponentsPlayer.ForEach(p => p.Destroy());
        var otherComponentPlayerSpriteRenderer = otherPlayerEntityList
            .SelectMany(e => e.GetComponents<PlayerSpriteRenderer>())
            .ToList();
        otherComponentPlayerSpriteRenderer.ForEach(s => s.Destroy());
        var otherComponentBoxCollider = otherPlayerEntityList
            .SelectMany(e => e.GetComponents<BoxCollider>())
            .ToList();
        otherComponentBoxCollider.ForEach(b => b.Destroy());
    }

    public static int GetPlayerCount()
    {
        return components.Count;
    }
}
class DogSpriteSystem : System<DogSpriteRenderer>
{
    private static readonly Lazy<DogSpriteSystem> _lazy = new(() => new DogSpriteSystem());
    public static DogSpriteSystem Instance => _lazy.Value;
    private DogSpriteSystem()
    {
        SystemManager.Instance.Register(this);
    }

    internal void Reset()
    {
        var dogSpriteRenderer = (DogSpriteRenderer)GetFirstComponent();
        dogSpriteRenderer.Reset();
    }
}

class PlayerSpriteSystem : System<PlayerSpriteRenderer>
{
    private static readonly Lazy<PlayerSpriteSystem> _lazy = new(() => new PlayerSpriteSystem());
    public static PlayerSpriteSystem Instance => _lazy.Value;
    private PlayerSpriteSystem()
    {
        SystemManager.Instance.Register(this);
    }
}
class SpriteSystem : System<SingleSpriteRenderer>
{
    private static readonly Lazy<SpriteSystem> _instance = new(() => new SpriteSystem());
    public static SpriteSystem Instance => _instance.Value;

    private SpriteSystem()
    {
        SystemManager.Instance.Register(this);
    }
}
class BackgroundSpriteSystem : System<BackgroundSpriteRenderer>
{
    private static readonly Lazy<BackgroundSpriteSystem> _instance = new(() => new BackgroundSpriteSystem());
    public static BackgroundSpriteSystem Instance => _instance.Value;

    private BackgroundSpriteSystem()
    {
        SystemManager.Instance.Register(this);
    }
}

class ForegroundSpriteSystem : System<ForegroundSpriteRenderer>
{
    private static readonly Lazy<ForegroundSpriteSystem> _instance = new(() => new ForegroundSpriteSystem());
    public static ForegroundSpriteSystem Instance => _instance.Value;

    private ForegroundSpriteSystem()
    {
        SystemManager.Instance.Register(this);
    }
}

class TextRendererSystem : System<TextRenderer>
{
    private static readonly Lazy<TextRendererSystem> _lazy = new(() => new TextRendererSystem());
    public static TextRendererSystem Instance => _lazy.Value;
    private TextRendererSystem()
    {
        SystemManager.Instance.Register(this);
    }
}
class CameraSystem : System<Camera>
{
    private static readonly Lazy<CameraSystem> _lazy = new(() => new CameraSystem());
    public static CameraSystem Instance => _lazy.Value;
    public static void SetResolution(Vector2 resolution)
    {
        foreach (Camera camera in components)
        {
            camera.SetResolution(resolution);
        }
    }
    private CameraSystem()
    {
        SystemManager.Instance.Register(this);
    }
}
class BackgroundSystem : System<Background>
{
    private static readonly Lazy<BackgroundSystem> _lazy = new(() => new BackgroundSystem());
    public static BackgroundSystem Instance => _lazy.Value;
    private BackgroundSystem()
    {
        SystemManager.Instance.Register(this);
    }
}

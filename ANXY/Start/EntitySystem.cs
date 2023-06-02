using ANXY.EntityComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.Start;

public sealed class EntitySystem
{
    ///Singleton Pattern
    private static readonly Lazy<EntitySystem> lazy = new(() => new EntitySystem());

    private readonly List<Entity> _gameEntities;

    /// <summary>
    ///     TODO
    /// </summary>
    private EntitySystem()
    {
        _gameEntities = new List<Entity>();
    }

    /// <summary>
    ///     Singleton Pattern return the only instance there is
    /// </summary>
    public static EntitySystem Instance => lazy.Value;

    /// <summary>
    ///     TODO
    /// </summary>
    public void AddEntity(Entity entity)
    {
        _gameEntities.Add(entity);
    }

    /// <summary>
    ///     TODO
    /// </summary>
    public bool RemoveEntity(Entity entity)
    {
        if (_gameEntities.Contains(entity))
        {
            _gameEntities.Remove(entity);
            return true;
        }

        return false;
    }

    public int GetNumberOfEntities()
    {
        return _gameEntities.Count;
    }

    /// <summary>
    ///     TODO
    /// </summary>
    internal void Clear()
    {
        _gameEntities.Clear();
    }

    /*
    /// <summary>
    ///     TODO
    /// </summary>
    internal void _UpdateEntities(GameTime gameTime)
    {
        foreach (var entity in _gameEntities) entity.Update(gameTime);
    }

    /// <summary>
    ///     TODO
    /// </summary>
    internal void _InitializeEntities()
    {
        foreach (var entity in _gameEntities) entity.Initialize();
    }

    /// <summary>
    ///     TODO
    /// </summary>
    internal void DrawEntities(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var entity in _gameEntities) entity.Draw(gameTime, spriteBatch);
    }

    */
    /// <summary>
    ///     TODO
    /// </summary>
    public List<Entity> FindEntityByType<T>() where T : Component
    {
        var FoundEntities = new List<Entity>();

        foreach (var e in _gameEntities)
            if (e.GetComponent<T>() != null)
                FoundEntities.Add(e);

        return FoundEntities;
    }
    
}
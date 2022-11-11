using System;
using System.Collections.Generic;
using ANXY.EntityComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.Start;

public sealed class EntityManager
{
    ///Singleton Pattern
    private static readonly Lazy<EntityManager> lazy = new(() => new EntityManager());

    private readonly List<Entity> _gameEntities;

    /// <summary>
    ///     TODO
    /// </summary>
    private EntityManager()
    {
        _gameEntities = new List<Entity>();
    }

    /// <summary>
    ///     Singleton Pattern return the only instance there is
    /// </summary>
    public static EntityManager Instance => lazy.Value;

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
        var i = 0;
        foreach (var VARIABLE in _gameEntities) i++;

        return i;
    }

    /// <summary>
    ///     TODO
    /// </summary>
    internal void Clear()
    {
        _gameEntities.Clear();
    }

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

    /// <summary>
    ///     TODO
    /// </summary>
    public List<T> FindEntityByType<T>(T entity)
    {
        var FoundEntities = new List<T>();

        return FoundEntities;
    }
}
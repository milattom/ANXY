using ANXY.ECS;
using ANXY.ECS.Components;
using System;
using System.Collections.Generic;

namespace ANXY.Start;

/// <summary>
/// TODO
/// </summary>
public class EntityManager
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
        return _gameEntities.Count;
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
    public List<Entity> FindEntitiesByType<T>() where T : Component
    {
        var FoundEntities = new List<Entity>();

        foreach (var e in _gameEntities)
            if (e.GetComponent<T>() != null)
                FoundEntities.Add(e);

        return FoundEntities;
    }

}
using System;
using System.Collections.Generic;
using ANXY.EntityComponent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace ANXY.Start
{
    public sealed class SystemManager<T,C> where T : ComponentSystem<C> where C : Component
    {
        private static readonly Lazy<SystemManager<T, C>> _lazy = new(() => new SystemManager<T, C>);

        public static SystemManager<T,C> Instance => _lazy.Value;

        private readonly List<T> _systems = new();

        public void AddSystem(T system)
        {
            _systems.Add(system);
        }

        /// <summary>
        ///     TODO
        /// </summary>
        public bool RemoveSystem(T system)
        {
            if(_systems.Contains(system))
            {
                _systems.Remove(system);
                return true;
            }

            return false;
        }

        public int GetNumberOfSystems()
        {
            return _systems.Count;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        internal void Clear()
        {
            _systems.Clear();
        }

        /// <summary>
        ///     TODO
        /// </summary>
        internal void _UpdateSystems(GameTime gameTime)
        {
            foreach(var system in _systems) system.Update(gameTime);
        }

        /// <summary>
        ///     TODO
        /// </summary>
        internal void InitializeSystems()
        {
            foreach(var system in _systems) system.Initialize();
        }

        /// <summary>
        ///     TODO
        /// </summary>
        internal void DrawSystems(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var system in _systems) system.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        ///     TODO
        /// </summary>
        public ComponentSystem<Component> FindSystemByType<T>() where T : Component
        {
            return _systems.FirstOrDefault(s => s.GetType() == typeof(ComponentSystem<T>));
        }
    }
}

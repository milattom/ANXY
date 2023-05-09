using System;
using System.Collections.Generic;
using ANXY.EntityComponent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace ANXY.Start
{
    public sealed class SystemManager
    {
        private static readonly Lazy<SystemManager> _lazy = new(() => new SystemManager());

        private readonly List<System<Component>> _systems;
        public static SystemManager Instance => _lazy.Value;

        public void AddSystem(System<Component> system)
        {
            _systems.Add(system);
        }

        /// <summary>
        ///     TODO
        /// </summary>
        public bool RemoveSystem(System<Component> system)
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
        internal void _InitializeEntities()
        {
            foreach(var system in _systems) system.Initialize();
        }

        /// <summary>
        ///     TODO
        /// </summary>
        internal void DrawEntities(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var system in _systems) system.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        ///     TODO
        /// </summary>
        public System<Component> FindSystemByType<T>() where T : Component
        {
            return _systems.FirstOrDefault(s => s.GetType() == typeof(System<T>));
        }
    }
}

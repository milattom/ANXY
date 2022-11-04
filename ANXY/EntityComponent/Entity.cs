using System;
using System.Collections.Generic;
using ANXY.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent
{
    /// <summary>
    /// Represents any Gameobject and holds a list with its components as well as a position vector
    /// </summary>
    public sealed class Entity
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public int ID { get; }
        private static int previousID = 0;
        private readonly List<Component> _components = new();

        public Entity()
        {
            ID = previousID++;
        }

        /// <summary>
        /// Adds the given component to the Entity, calls its initialize() method. If already a
        /// Component of the same type exists, it gets overwritten to ensure that each type of component
        /// only exists once.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            component.Entity = this;
            component.Initialize();
            if (GetComponent(component.GetType()) == null) { RemoveComponent(component.GetType()); }
            _components.Add(component);
        }

        /// <summary>
        /// Returns the first occurring Component of the type if the Entity has one attached
        /// </summary>
        /// <typeparam name="T">The type of Component to retrieve.</typeparam>
        /// <returns>T A Component of the matching type, otherwise null if no Component is found.</returns>
        public object GetComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the first occurring Component of the type if the Entity has one attached.
        /// </summary>
        /// <param name="type">The type of Component to retrieve.</param>
        /// <returns>A Component of the matching type, otherwise null if no Component is found.</returns>
        public Component GetComponent(Type type)
        {
            foreach (Component component in _components)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns all Components attached to the Entity
        /// </summary>
        /// <returns>List of all components.</returns>
        public List<Component> GetComponents()
        {
            return _components;
        }

        /// <summary>
        /// Removes the first Component of specified type, by call its Destroy()
        /// method and then removing it from the List
        /// </summary>
        /// <param name="type">type of the Component to remove</param>
        public void RemoveComponent(Type type)
        {
            Component foundComponent = null;
            foreach (Component component in _components)
            {
                if (component.GetType() == type)
                {
                    foundComponent = component;
                    component.Destroy();
                }
            }
            _components.Remove(foundComponent);
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}

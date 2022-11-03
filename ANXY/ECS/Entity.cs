using System;
using System.Collections.Generic;

namespace ANXY.ECS
{
    /// <summary>
    /// Represents any Gameobject and holds a list with its components
    /// </summary>
    public class Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private readonly List<Component> _components = new();

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.Entity = this;
            //component.init();
        }

        /// <summary>
        /// Returns the first occurring Component of the type if the Entity has one attached
        /// </summary>
        /// <typeparam name="T">The type of Component to retrieve.</typeparam>
        /// <returns>T A Component of the matching type, otherwise null if no Component is found.</returns>
        public T GetComponent<T>() where T : Component
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
        /// Returns all Components of the type if the Entity has one attached
        /// </summary>
        /// <typeparam name="T">The type of Component to retrieve.</typeparam>
        /// <returns>List of components of the matching type, otherwise null if no Component is found.</returns>
        public List<T> GetComponents<T>() where T : Component
        {
            List<T> returns = new();
            foreach (Component component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    returns.Add((T)component);
                }
            }
            return returns.Count >= 1 ? returns : null;
        }

        //public static void Instantiate(Entity entity)
        //{
        //    Scene.Add(entity);
        //}
        //
        //public static void Destroy(Entity entity)
        //{
        //    Scene.Remove(entity);
        //    foreach (var component in entity.components)
        //    {
        //        // destroy each component on this Entity
        //        component.SetForDeletion();
        //    }
        //}
    }
}

using ANXY.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ANXY.ECS.Systems
{
    /// <summary>
    /// allowed for the CPU to pull only the components it is processing. It allows in the main game loop to run
    /// types of components as: "ComponentType".Update(gameTime);
    /// </summary>
    /// <typeparam name="T">component type</typeparam>
    class BaseSystem<T> where T : Component
    {
        private static readonly List<T> Components = new List<T>();

        public static void Register(T component)
        {
            Components.Add(component);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (T component in Components)
            {
                component.Update(gameTime);
            }
        }

    }

    class TransformSystem : BaseSystem<Transform> { }

    //class SpriteSystem : BaseSystem<Sprite> { }
    //class ColliderSystem : BaseSystem<Collider> { }
}

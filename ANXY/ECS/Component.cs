using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ANXY.ECS
{
    /// <summary>
    /// Base Class. Components describe any data shared between Entities.
    /// Examples include Transform, Collider, Sprite, Script, etc.
    /// Every Component is a part of an Entity therefor it holds an Entity.
    /// The base Class also holds the Update(float gameTime) methods which can
    /// be overwritten by every component
    /// </summary>
    public class Component
    {
        public Entity Entity { get; set; }
        public virtual void Update(GameTime gameTime) { }
    }
}

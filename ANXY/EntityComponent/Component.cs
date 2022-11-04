using ANXY.EntityComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.ECS
{
    /// <summary>
    /// Abstract Class Component describes any data shared between Entities.
    /// Examples include Transform, Collider, Sprite, Script, etc.
    /// Every Component is a part of an Entity therefor it holds an Entity.
    /// The base Class also holds the Update(float gameTime) methods which can
    /// be overwritten by every component.
    /// Every Component needs to implement Initialize(), Update(...) and Destroy()
    /// </summary>
    public abstract class Component
    {
        public Entity Entity { get; set; }
        public bool IsActive { get; set; }
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
        public abstract void Destroy();
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ANXY.ECS.Components;

public abstract class Component
{
    public bool IsActive { get; set; } = true;
    public Entity Entity { get; set; }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    public abstract void Initialize();
    public abstract void Destroy();
}
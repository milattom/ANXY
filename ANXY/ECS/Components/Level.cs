using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.ECS.Components;

internal class Level : Component
{
    public Entity PlayerEntity { get; set; }

    public Level() { }

    public override void Update(GameTime gameTime)
    {
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
    public override void Initialize()
    {
    }
    public override void Destroy()
    {
    }
}

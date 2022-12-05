using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class SingleSpriteRenderer : Component
{
    public Texture2D Atlas { get; set; }

    //TODO public SpriteAnimation SpriteAnimation;

    /// <summary>
    /// </summary>
    public SingleSpriteRenderer(Texture2D atlas)
    {
        Atlas = atlas;
    }



    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Atlas, Entity.Position, Color.White);
    }

    public override void Initialize()
    {
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    // Methods

    /// <summary>
    ///     TODO
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
    }
}
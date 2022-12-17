using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class SingleSpriteRenderer : Component
{
    private readonly Texture2D _atlas;
    private readonly Rectangle _region;

    //TODO public SpriteAnimation SpriteAnimation;

    /// <summary>
    /// </summary>
    public SingleSpriteRenderer(Texture2D atlas) : this(atlas, atlas.Bounds)
    {
        //DO NOTHING, NOP
    }

    public SingleSpriteRenderer(Texture2D atlas, Rectangle region)
    {
        _atlas = atlas;
        _region = region;
    }


    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_atlas, Entity.Position /*TODO - Camera.Position*/, _region, Color.White);
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
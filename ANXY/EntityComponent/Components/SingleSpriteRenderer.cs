using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// SingleSpriteRenderer renders a single image to the screen
/// </summary>
public class SingleSpriteRenderer : Component
{
    private readonly Texture2D _atlas;
    private readonly Rectangle _region;

    //TODO public SpriteAnimation SpriteAnimation;

    /// <summary>
    /// SingleSpriteRenderer Class Constructor
    /// </summary>
    /// <param name="atlas"></param>
    public SingleSpriteRenderer(Texture2D atlas) : this(atlas, atlas.Bounds)
    {
        //DO NOTHING, NOP
    }

    /// <summary>
    /// SingleSpriteRenderer Class Constructor
    /// </summary>
    /// <param name="atlas">Sprite atlas</param>
    /// <param name="region">defines which region of the atlas the to be rendered image is in.</param>
    public SingleSpriteRenderer(Texture2D atlas, Rectangle region)
    {
        _atlas = atlas;
        _region = region;
    }

    /// <summary>
    /// TODO implement Update
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
    }

    /// <summary>
    /// prints the desired part of the sprite to the screen at position of the parent Entity.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_atlas, Entity.Position /*TODO - Camera.Position*/, _region, Color.White);
    }

    /// <summary>
    /// TODO Initialize
    /// </summary>
    public override void Initialize()
    {
    }

    /// <summary>
    /// TODO implement Destroy
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void Destroy()
    {
        throw new NotImplementedException();
    }
}
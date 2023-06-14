using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ANXY.ECS.Systems;

namespace ANXY.ECS.Components;

/// <summary>
/// SingleSpriteRenderer renders a single image to the screen
/// </summary>
public class SingleSpriteRenderer : Component
{
    private readonly Texture2D _atlas;
    private readonly Rectangle _region;

    /// <summary>
    /// For SingleSpriterenderer which uses the whole atlas.
    /// Registers .this in the system.
    /// </summary>
    /// <param name="atlas"></param>
    public SingleSpriteRenderer(Texture2D atlas) : this(atlas, atlas.Bounds)
    {
        SpriteSystem.Instance.Register(this);
    }

    /// <summary>
    /// For SingleSpriterenderer which uses a part of an atlas.
    /// Registers .this in the system.
    /// </summary>
    /// <param name="atlas">Sprite atlas</param>
    /// <param name="region">defines which region of the atlas the to be rendered image is in.</param>
    public SingleSpriteRenderer(Texture2D atlas, Rectangle region)
    {
        _atlas = atlas;
        _region = region;
        SpriteSystem.Instance.Register(this);
    }

    /// <summary>
    /// prints the desired part of the sprite to the screen at position of the parent Entity.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_atlas, Entity.Position - Camera.ActiveCamera.DrawOffset, _region, Color.White);
    }
}
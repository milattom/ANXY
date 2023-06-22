using ANXY.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Color = Microsoft.Xna.Framework.Color;

namespace ANXY.ECS.Components;
/// <summary>
/// TextRenderer renders text on the screen.
/// </summary>
internal class TextRenderer : Component
{
    private readonly SpriteFont _font;
    public String _text;
    private readonly Vector2 _position;
    private readonly Color _color;
    /// <summary>
    /// Taking in the font, text, position and color to render the text on the screen.
    /// </summary>
    /// <param name="font"></param>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public TextRenderer(SpriteFont font, String text, Vector2 position, Color color)
    {
        _font = font;
        _text = text;
        _position = position;
        _color = color;
        TextRendererSystem.Instance.Register(this);
    }
    /// <summary>
    /// Draw loop for the text.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, _text, _position, _color);
    }
}

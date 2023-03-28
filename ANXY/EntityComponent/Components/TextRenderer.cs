using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace ANXY.EntityComponent.Components
{
    internal class TextRenderer : Component
    {
        private readonly SpriteFont _font;
        public String _text;
        private readonly Pointer _textPointer;
        private readonly Vector2 _position;
        private readonly Color _color;

        public TextRenderer(SpriteFont font, String text, Vector2 position, Color color)
        {
            _font = font;
            _text = text;
            _position = position;
            _color = color;
        }

        public TextRenderer(SpriteFont font, Pointer textPointer, Vector2 position, Color color)
        {
            _font = font;
            _textPointer = textPointer;
            _position = position;
            _color = color;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        { 
            spriteBatch.DrawString(_font,_text, _position, _color);
        }

        public override void Initialize()
        {
        }

        public override void Destroy()
        {

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

namespace ANXY.EntityComponent.Components
{
    internal class FpsCounterComponent : Component
    {
        private readonly String fps = "FPS: ";
        public String fpsText { get; private set; }
        private float fpsValue;
        private StringBuilder stringBuilder = new StringBuilder();

        ///Singleton Pattern
        private static readonly Lazy<FpsCounterComponent> lazy = new(() => new FpsCounterComponent());

        public static FpsCounterComponent Instance => lazy.Value;

        public override void Update(GameTime gameTime)
        {
            fpsValue = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
            stringBuilder.Clear();
            stringBuilder.Append(fps);
            stringBuilder.Append(fpsValue.ToString());
            fpsText = stringBuilder.ToString();

            var textRenderer = Entity.GetComponent<TextRenderer>();
            textRenderer._text = fpsText;
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
}

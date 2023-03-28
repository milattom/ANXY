using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components
{
    internal class FpsCounter : Component
    {
        private readonly String fps = "FPS: ";
        public String fpsText { get; private set; }
        private float fpsValue;
        private StringBuilder stringBuilder = new StringBuilder();

        ///Singleton Pattern
        private static readonly Lazy<FpsCounter> lazy = new(() => new FpsCounter());

        public static FpsCounter Instance => lazy.Value;

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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ANXY.EntityComponent.Components
{
    internal class UI : Component
    {
        private float fps = 0;
        public UI()
        {
            UISystem.Instance.Register(this);
        }
        public override void Update(GameTime gameTime)
        {
            var fps = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(spriteFont, fps, new Vector2(1, 1), Color.Green);
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.ECS.Components
{
    internal class SpriteAnimation : Component
    {
        private List<SpriteAnimationFrame> _frame;
        public float PlaybackProgress { get; private set; }

        public SpriteAnimation()
        {
            _frame = new List<SpriteAnimationFrame>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public void AddFrame(Texture2D sprite, GameTime gameTime)
        {

        }

        public void Play()
        {

        }

        public void Stop()
        {

        }
    }
}

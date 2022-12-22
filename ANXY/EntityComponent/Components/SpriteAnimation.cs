using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components
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

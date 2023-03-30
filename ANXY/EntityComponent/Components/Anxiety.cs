using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO implement Anxiety with Anxiety Score
    /// </summary>
    public class Anxiety : Component
    {
        public double AnxietyScore { get; set; }

        /// <summary>
        /// TODO implement Anxiety Score. Starting Score, Death/Game Over Score. Steps per time, increase of Steps-Length
        /// </summary>
        public Anxiety()
        {

        }

        /// <summary>
        /// TODO implement Update
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO implement Draw
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO implement Initialize
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO implement Destory
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}

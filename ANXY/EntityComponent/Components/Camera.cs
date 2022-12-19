using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO implement Camera
    /// Camera will shift over the Level and shift everything accordingly. Player needed as Parameter.
    /// </summary>
    public class Camera : Component
    {
        public Player Player { get; set; }

        /// <summary>
        /// TODO implement Update
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// TODO implement Draw
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// TODO implement initialize
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// TODO implement Destroy
        /// </summary>
        public override void Destroy()
        {
        }
    }
}

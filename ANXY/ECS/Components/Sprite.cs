using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.ECS.Components
{
    /// <summary>
    /// Sprite Component makes it possible for an Entity to hold its Sprite as a component.
    /// 
    /// Serves the Entity-Component design.
    /// </summary>
    public class Sprite : Component
    {
        //default values
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color TintColor { get; set; } = Color.White;
        public Single Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;

        public Vector2 Scale { get; set; } = Vector2.One;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public Single layerDepth = 0f;


        /// <summary>
        /// With Sprite you can make a sprite Object with the texture in the Parameter.
        /// Per default, It retrieves the position vector from its Entity by default.
        /// </summary>
        /// <param name="texture">Texture You want to use for Spritesheet.</param>
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        // TODO: should be done in logic and not in here later on?
        /// <summary>
        /// Draws the sprite in the current batch at the position of its Entity, with the
        /// default tint Color.White. If which will be rendered. If it is not intended
        /// to use the whole texture of this sprite, use the optional sourceRectangle parameter.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle? sourceRectangle)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Draw(Texture, Entity.GetComponent<Transform>().Position, sourceRectangle, TintColor, Rotation, Origin, Scale, Effects, layerDepth);
        }

        /// <summary>
        /// Gets the new position from the entity
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(float gameTime) {
            // We'd like to do something like this:
            Transform transform = Entity.GetComponent<Transform>();
            Position = transform.Position;
            Rotation = transform.Rotation;
            layerDepth = transform.LayerDepth;
            Scale = transform.Scale;
            //GameEngine.DrawSprite(Texture, Position); // assume the fictitious GameEngine class
        }
    }
}

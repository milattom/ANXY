using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANXY
{
    public class BasicSprite
    {
        // animation frames
        public List<Rectangle> frames;
        public int currentFrame;
        private int totalFrames;
        // animation times
        private int currentUpdate = 0;
        public double updatesPerFrame = 7;
        //public double updatesPerFrame = 60;
        // sprite info
        public Texture2D spriteSheet;
        public float scalar = 4f;
        // sprite effects
        public SpriteEffects spriteEffects;
        // location info

        // offsets
        public List<Vector2> offsets = null;

        public BasicSprite(
            Texture2D givenSpriteSheet,
            List<Rectangle> givenFrames,
            List<Vector2> offsets = null,
            SpriteEffects givenSpriteEffects = SpriteEffects.None
        )
        {
            spriteEffects = SpriteEffects.None;
            spriteSheet = givenSpriteSheet;
            frames = givenFrames;
            currentFrame = 0;
            totalFrames = frames.Count();
            this.offsets = offsets;
            this.spriteEffects = givenSpriteEffects;
        }

        public BasicSprite(Texture2D givenSpriteSheet, List<Rectangle> givenFrames)
        {
            spriteEffects = SpriteEffects.None;
            spriteSheet = givenSpriteSheet;
            frames = givenFrames;
            currentFrame = 0;
            totalFrames = frames.Count();
        }

        public BasicSprite(Texture2D givenSpriteSheet, List<Rectangle> givenFrames, int givenUpdatesPerFrame)
        {
            spriteEffects = SpriteEffects.None;
            spriteSheet = givenSpriteSheet;
            frames = givenFrames;
            currentFrame = 0;
            totalFrames = frames.Count();
            updatesPerFrame = givenUpdatesPerFrame;
        }

        public BasicSprite(Texture2D givenSpriteSheet, List<Rectangle> givenFrames, SpriteEffects givenSpriteEffects)
        {
            spriteSheet = givenSpriteSheet;
            frames = givenFrames;
            currentFrame = 0;
            totalFrames = frames.Count();
            spriteEffects = givenSpriteEffects;
        }

        public void Update(GameTime gameTime, Boolean animate)
        {
            if (animate)
            {
                // update changes only when updates per frame is equal
                currentUpdate++;
                if (currentUpdate == updatesPerFrame)
                {
                    currentUpdate = 0;

                    currentFrame++;
                    if (currentFrame == totalFrames)
                        currentFrame = 0;
                }
            }
        }
    }
}
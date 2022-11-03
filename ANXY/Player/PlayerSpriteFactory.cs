using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ANXY.Player
{
    public class PlayerSpriteFactory
    {
        private Texture2D PlayerSpriteSheet;
        private readonly PlayerSpriteFrames playerSpriteFrames = new PlayerSpriteFrames();

        public static PlayerSpriteFactory Instance { get; } = new PlayerSpriteFactory();

        public Task LoadAllTextures(ContentManager content)
        {
            PlayerSpriteSheet = content.Load<Texture2D>("Player");
            return Task.CompletedTask;
        }

        public BasicSprite CreateLinkWalkingRight()
        {
            List<Rectangle> frames = new List<Rectangle>
            {
                playerSpriteFrames.frames["PlayerWalkingRightFrame1"],
                playerSpriteFrames.frames["PlayerWalkingRightFrame2"],
                playerSpriteFrames.frames["PlayerWalkingRightFrame3"],
                playerSpriteFrames.frames["PlayerWalkingRightFrame4"],
                playerSpriteFrames.frames["PlayerWalkingRightFrame5"],
                playerSpriteFrames.frames["PlayerWalkingRightFrame6"]
            };
            return new BasicSprite(PlayerSpriteSheet, frames);
        }
        public BasicSprite CreateLinkWalkingLeft()
        {
            List<Rectangle> frames = new List<Rectangle>
            {
                playerSpriteFrames.frames["PlayerWalkingLeftFrame1"],
                playerSpriteFrames.frames["PlayerWalkingLeftFrame2"],
                playerSpriteFrames.frames["PlayerWalkingLeftFrame3"],
                playerSpriteFrames.frames["PlayerWalkingLeftFrame4"],
                playerSpriteFrames.frames["PlayerWalkingLeftFrame5"],
                playerSpriteFrames.frames["PlayerWalkingLeftFrame6"]
            };
            return new BasicSprite(PlayerSpriteSheet, frames, SpriteEffects.FlipHorizontally);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace ANXY.Player
{
    public class PlayerSpriteFrames
    {
        public Dictionary<string, Rectangle> frames;

        public PlayerSpriteFrames()
        {
            frames = new Dictionary<string, Rectangle>();
            //Player walking
            frames.Add("PlayerWalkingRightFrame1", new Rectangle(35, 11, 15, 16));
            frames.Add("PlayerWalkingRightFrame2", new Rectangle(52, 12, 14, 15));
            frames.Add("PlayerWalkingRightFrame3", new Rectangle(35, 11, 15, 16));
            frames.Add("PlayerWalkingLeftFrame1", new Rectangle(71, 11, 12, 16));
            frames.Add("PlayerWalkingLeftFrame2", new Rectangle(88, 11, 12, 16));
            frames.Add("PlayerWalkingLeftFrame3", new Rectangle(88, 11, 12, 16));
            //Player jumping
            frames.Add("PlayerJumpingRightFrame1", new Rectangle(35, 11, 15, 16));
            frames.Add("PlayerJumpingRightFrame2", new Rectangle(52, 12, 14, 15));
        }
    }
}

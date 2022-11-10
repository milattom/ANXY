using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class ScoreTracker : Component
{
    public int Time { get; set; }
    public int Score { get; set; }

    // TODO public File HighScore;


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
}
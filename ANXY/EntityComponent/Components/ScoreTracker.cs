using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// TODO implement ScoreTracker to track HighScore, personal Score, etc
/// Maybe track time as score?
/// </summary>
public class ScoreTracker : Component
{
    public int Time { get; set; }
    public int Score { get; set; }

    // TODO public File HighScore;

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
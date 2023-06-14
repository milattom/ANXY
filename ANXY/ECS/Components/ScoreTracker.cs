using Microsoft.Xna.Framework;

namespace ANXY.ECS.Components;

/// <summary>
/// TODO implement ScoreTracker to track HighScore, personal Score, etc
/// Maybe track time as score?
/// </summary>
public class ScoreTracker : Component
{
    public int Time { get; set; }
    public int Score { get; set; }

    // TODO public File HighScore;
    public override void Update(GameTime gameTime)
    {
        //
    }
}
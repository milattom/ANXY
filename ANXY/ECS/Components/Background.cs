using ANXY.ECS.Systems;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.ECS.Components;  

/// <summary>
/// Background manager. Shifts the Background when needed
/// </summary>
public class Background : Component
{
    public int WindowHeight { get; }
    public int WindowWidth { get; }
    private Player _player;
    private float _minDistance;

    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="windowWidth">Current Windows size, Width</param>
    /// <param name="windowHeight">Current Windows size, Height</param>
    public Background(int windowWidth, int windowHeight)
    {
        this.WindowWidth = windowWidth;
        this.WindowHeight = windowHeight;
        BackgroundSystem.Instance.Register(this);
    }

    /// <summary>
    /// Update called multiple times every Frame (Update Cycle).
    ///
    /// Implementation of "Parallax Background Shifting"
    /// Move the Background when the Player Character is about to leave the Screen. Player Speed is set to zero, Background speed is reversed Player Speed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
        var currentPosition = WindowWidth - _player.Entity.Position.X;
        if(currentPosition < _minDistance)
        {
            //shift background
        }
    }

    /// <summary>
    /// Initialize Player so we later know how far from the Edges the Player is positioned.
    /// </summary>
    public override void Initialize()
    {
        _player = (Player)SystemManager.Instance.FindSystemByType<Player>().GetFirstComponent();
    }
}
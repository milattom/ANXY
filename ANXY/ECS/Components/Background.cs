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
}
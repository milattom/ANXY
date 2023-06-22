using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ANXY.ECS.Components;
/// <summary>
/// Base class for every component to ensure they implement Update(), Draw() and Initialize()
/// </summary>
public class Component
{
    public bool IsActive { get; set; } = true;
    public Entity Entity { get; set; }
    /// <summary>
    /// Is called once every frame
    /// </summary>
    /// <param name="gameTime"></param>
    public virtual void Update(GameTime gameTime) {}
    /// <summary>
    /// Is called once every frame to draw the spriteBatch given by parameter
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    /// <summary>
    /// Is called before the first frame
    /// </summary>
    public virtual void Initialize() {}
    /// <summary>
    /// Used to destroy a components
    /// </summary>
    public virtual void Destroy() {}
}
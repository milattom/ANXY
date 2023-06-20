using ANXY.ECS.Systems;
using Microsoft.Xna.Framework;

namespace ANXY.ECS.Components;

/// <summary>
/// Camera that follows the player and shifts over the Level in respect to the set bounderies.
/// </summary>
public class Camera : Component
{
    public static Camera ActiveCamera { get; private set; }
    public Vector2 DrawOffset { get; private set; }

    private readonly Player _player;
    private Vector2 _resolution;

    /// <summary>
    /// sets the player, the dimensions of the window and its boundaries
    /// </summary>
    /// <param name="player">player component</param>
    /// <param name="windowDimensions">dimension of the window</param>
    public Camera(Player player, Vector2 windowDimensions)
    {
        _player = player;

        _resolution = windowDimensions;
        CameraSystem.Instance.Register(this);
    }


    /// <summary>
    /// Updates the camera's position.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        var ClampedEntityPosition = Vector2.Clamp(
            Entity.Position,
            _player.Entity.Position - new Vector2(0.25f, 0.15f) * _resolution,
            _player.Entity.Position + new Vector2(0.25f, 0.15f) * _resolution);

        Entity.Position = ClampedEntityPosition;
        DrawOffset = Entity.Position - 0.5f * _resolution;
    }

    /// <summary>
    /// Sets .this to ActiveCamera and the camera position to the player.
    /// </summary>
    public override void Initialize()
    {
        ActiveCamera = this;

        Entity.Position = _player.Entity.Position;
    }

    /// <summary>
    /// Sets camera position back to player.
    /// </summary>
    public void Reset()
    {
        Entity.Position = _player.Entity.Position;
    }

    /// <summary>
    /// Sets the resolution of the camera.
    /// </summary>
    /// <param name="resolution"> resolution as hight x width</param>
    public void SetResolution(Vector2 resolution)
    {
        _resolution = resolution;
    }
}
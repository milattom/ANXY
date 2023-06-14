using ANXY.ECS.Systems;
using Microsoft.Xna.Framework;

namespace ANXY.ECS.Components;

/// <summary>
/// Camera that follows the player and shifts over the Level in respect to the set bounderies.
/// </summary>
public class Camera : Component
{
    public static Camera ActiveCamera { get; private set; }
    private Player _player;
    public Vector2 _windowDimensions;
    public Vector2 DrawOffset { get; private set; }
    public readonly Vector2 _minPosition;
    public readonly Vector2 _maxPosition;

    /// <summary>
    /// sets the player, the dimensions of the window and its boundaries
    /// </summary>
    /// <param name="player">player component</param>
    /// <param name="windowDimensions">dimension of the window</param>
    /// <param name="minPosition">minimum boundery</param>
    /// <param name="maxPosition">maximum boundery</param>
    public Camera(Player player, Vector2 windowDimensions, Vector2 minPosition, Vector2 maxPosition)
    {
        _player = player;

        _windowDimensions = windowDimensions;

        _minPosition = minPosition;
        _maxPosition = maxPosition;
        CameraSystem.Instance.Register(this);
    }


    /// <summary>
    /// Updates the camera's position.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        var ClampedEntityPosition = Entity.Position;
        ClampedEntityPosition = Vector2.Clamp(ClampedEntityPosition, _player.Entity.Position - new Vector2(0.25f, 0.15f) * _windowDimensions,
            _player.Entity.Position + new Vector2(0.25f, 0.15f) * _windowDimensions);

        Entity.Position = Vector2.Clamp(ClampedEntityPosition, _minPosition, _maxPosition);
        DrawOffset = Entity.Position - 0.5f * _windowDimensions;
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
}
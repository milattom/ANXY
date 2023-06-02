using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO implement Camera
    /// Camera will shift over the Level and shift everything accordingly. _player needed as Parameter.
    /// </summary>
    public class Camera : Component
    {
        public static Camera ActiveCamera { get; private set; }
        private Player _player;
        private readonly Vector2 _windowDimensions;
        public Vector2 DrawOffset { get; private set; }
        public readonly Vector2 _minPosition;
        public readonly Vector2 _maxPosition;

        public Camera(Player player, Vector2 windowDimensions, Vector2 minPosition, Vector2 maxPosition)
        {
            _player = player;

            _windowDimensions = windowDimensions;

            _minPosition = minPosition;
            _maxPosition = maxPosition;
            CameraSystem.Instance.Register(this);
        }


        /// <summary>
        /// TODO implement Update
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
        /// TODO implement Draw
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// TODO implement initialize
        /// </summary>
        public override void Initialize()
        {
            ActiveCamera = this;

        }

        /// <summary>
        /// TODO implement Destroy
        /// </summary>
        public override void Destroy()
        {
        }
    }
}
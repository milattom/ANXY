using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ANXY.ECS.Systems;
using ANXY.Start;

namespace ANXY.ECS.Components;

/// <summary>
/// PlayerSpriteRenderer renders the image of the Player. It switches between different frames depending on the current movement type, direction, etc.
/// </summary>
public class PlayerSpriteRenderer : Component
{
    public readonly Rectangle StartPlayerRectangle = new(0, 0, 33, 70);
    private const int XOffsetRectangle = 33;
    private const int _numberOfFrames = 7;
    private const int _millisecondsPerFrame = 100; //the smaller the faster animation. 42ms ~= 24fps
    private Player _player;
    private Rectangle CurrentPlayerRectangle;
    private SpriteEffects _spriteEffect;
    private Texture2D PlayerAtlas { get; }
    private bool _gamePaused = false;

    /// <summary>
    /// set the playerAtlas with all Player Movement Frames
    /// </summary>
    public PlayerSpriteRenderer(Texture2D playerAtlas)
    {
        PlayerAtlas = playerAtlas;
        CurrentPlayerRectangle = StartPlayerRectangle;
        PlayerSpriteSystem.Instance.Register(this);
        ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
    }

    /// <summary>
    /// Update the Player Frame to the current Frame. shifts to the next Animation frame if needed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
        if (_gamePaused)
        {
            return;
        }
        if (_player.InputDirection.X > 0)
        {
            _spriteEffect = SpriteEffects.None;
        }
        else if (_player.InputDirection.X < 0)
        {
            _spriteEffect = SpriteEffects.FlipHorizontally;
        }
        UpdateAnimation(gameTime);
    }

    /// <summary>
    /// Drawing the chosen Player Animation Frame at the Player Position
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var drawRectangle = new Rectangle((int)(Entity.Position.X - Camera.ActiveCamera.DrawOffset.X), (int)(Entity.Position.Y - Camera.ActiveCamera.DrawOffset.Y), 33, 70);

        spriteBatch.Draw(PlayerAtlas, drawRectangle, CurrentPlayerRectangle, Color.White, 0f, new Vector2(0, 0),
            _spriteEffect, 0f);
    }

    /// <summary>
    /// Update the Animation frame to the next frame.
    /// </summary>
    private void UpdateAnimation(GameTime gameTime)
    {
        var currentAnimationTime = gameTime.TotalGameTime.TotalMilliseconds % (_millisecondsPerFrame * _numberOfFrames);
        var currentFrame = (int)(currentAnimationTime / _millisecondsPerFrame);

        if (_player.Velocity.X == 0 && !_player.MidAir)
        {
            currentFrame = 0;
        }

        if (_player.MidAir)
        {
            currentFrame += _numberOfFrames;
        }
        CurrentPlayerRectangle.X = XOffsetRectangle * currentFrame;
    }

    internal void SetPlayerComponent()
    {
        _player = Entity.GetComponent<Player>();
    }

    private void OnGamePausedChanged(bool gamePaused)
    {
        _gamePaused = gamePaused;
    }
}
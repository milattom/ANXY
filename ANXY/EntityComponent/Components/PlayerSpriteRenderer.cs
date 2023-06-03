using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// PlayerSpriteRenderer renders the image of the Player. It switches between different frames depending on the current movement type, direction, etc.
/// </summary>
public class PlayerSpriteRenderer : Component
{
    private const int XOffsetRectangle = 33;
    private const int _numberOfFrames = 7;
    public readonly Rectangle StartPlayerRectangle = new(0, 0, 33, 70);
    private const int _millisecondsPerFrame = 100; //the smaller the faster animation. 42ms ~= 24fps
    private Player _player;
    private Rectangle CurrentPlayerRectangle;
    private SpriteEffects _spriteEffect;
    private Texture2D PlayerAtlas { get; }

    /// <summary>
    ///     set the playerAtlas with all Player Movement Frames
    /// </summary>
    public PlayerSpriteRenderer(Texture2D playerAtlas)
    {
        PlayerAtlas = playerAtlas;
        CurrentPlayerRectangle = StartPlayerRectangle;
        PlayerSpriteSystem.Instance.Register(this);
    }

    /// <summary>
    /// Update the Player Frame to the current Frame. shifts to the next Animation frame if needed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
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
    /// Get the parent Player Entity.
    /// Set the _outputRectangle, where to draw the Animation Frame.
    /// </summary>
    public override void Initialize()
    {
        _player = Entity.GetComponent<Player>();
    }

    /// <summary>
    /// TODO implement destroy
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO future ideas, changing the walking direction here.
    /// </summary>
    public void WalkingLeft()
    {
    }

    /// <summary>
    /// TODO set the walking Direction here
    /// </summary>
    public void WalkingRight()
    {
    }

    /// <summary>
    /// TODO set the jumping animation here
    /// </summary>
    public void Jumping()
    {
    }

    /// <summary>
    /// Update the Animation frame to the next frame.
    /// </summary>
    private void UpdateAnimation(GameTime gameTime)
    {
        var currentAnimationTime = gameTime.TotalGameTime.TotalMilliseconds % (_millisecondsPerFrame * _numberOfFrames);
        var currentFrame = (int)(currentAnimationTime / _millisecondsPerFrame);
        if (_player.Velocity.X == 0)
        {
            currentFrame = 0;
        }
        CurrentPlayerRectangle.X = XOffsetRectangle * currentFrame;
    }
}
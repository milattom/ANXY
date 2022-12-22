using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// PlayerSpriteRenderer renders the image of the Player. It switches between different frames depending on the current movement type, direction, etc.
/// </summary>
public class PlayerSpriteRenderer : Component
{
    public const int XOffsetRectangle = 33;
    private readonly int _numberOfFrames = 7;
    public readonly Rectangle StartPlayerRectangle = new(0, 0, 33, 70);
    private int _currentFrame;
    private readonly int _millisecondsPerFrame = 40; //the smaller the faster animation
    private Rectangle _outputRectangle;
    private Player _player;
    private int _timeSinceLastFrame;
    public Rectangle CurrentPlayerRectangle;
    private SpriteEffects spriteEffect;
    public Texture2D PlayerAtlas { get; }

    /// <summary>
    ///     set the playerAtlas with all Player Movement Frames
    /// </summary>
    public PlayerSpriteRenderer(Texture2D playerAtlas)
    {
        PlayerAtlas = playerAtlas;
        CurrentPlayerRectangle = StartPlayerRectangle;
    }

    /// <summary>
    /// Update the Player Frame to the current Frame. shifts to the next Animation frame if needed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
        //Animation Update
        _outputRectangle.X = (int)Entity.Position.X;
        _outputRectangle.Y = (int)Entity.Position.Y;
        _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
        if (_timeSinceLastFrame > _millisecondsPerFrame) _updateAnimation();
    }

    /// <summary>
    /// Drawing the chosen Player Animation Frame at the Player Position
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(PlayerAtlas, _outputRectangle, CurrentPlayerRectangle, Color.White, 0f, new Vector2(0, 0),
            spriteEffect, 0f);
    }

    /// <summary>
    /// Get the parent Player Entity.
    /// Set the _outputRectangle, where to draw the Animation Frame.
    /// </summary>
    public override void Initialize()
    {
        _player = Entity.GetComponent<Player>();
        _outputRectangle = new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 33, 70);
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
    private void _updateAnimation()
    {
        _timeSinceLastFrame = 0;
        if (_currentFrame < _numberOfFrames && _player.InputDirection.X > 0)
        {
            CurrentPlayerRectangle.X = XOffsetRectangle * _currentFrame;
            spriteEffect = SpriteEffects.None;
            _currentFrame++;
        }
        else if (_currentFrame < _numberOfFrames && _player.InputDirection.X < 0)
        {
            CurrentPlayerRectangle.X = XOffsetRectangle * _currentFrame;
            spriteEffect = SpriteEffects.FlipHorizontally;
            _currentFrame++;
        }
        else
        {
            CurrentPlayerRectangle.X = 0;
            _currentFrame = 0;
        }
    }
}
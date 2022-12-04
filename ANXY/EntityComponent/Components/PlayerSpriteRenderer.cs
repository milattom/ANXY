using System;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class PlayerSpriteRenderer : Component
{
    public const int XOffsetRectangle = 33;
    public readonly Rectangle StartPlayerRectangle = new(0, 0, 33, 70);
    public Rectangle CurrentPlayerRectangle;
    private int _currentFrame;
    private Rectangle _outputRectangle;
    private readonly int _numberOfFrames = 7;
    private int _timeSinceLastFrame = 0;
    private int _millisecondsPerFrame = 40; //the smaller the faster animation
    private Player _player;
    private SpriteEffects spriteEffect;


    /// <summary>
    ///     TODO
    /// </summary>
    public PlayerSpriteRenderer(Texture2D playerAtlas)
    {
        PlayerAtlas = playerAtlas;
        CurrentPlayerRectangle = StartPlayerRectangle;
    }

    public Texture2D PlayerAtlas { get; }


    public override void Update(GameTime gameTime)
    {
        //Animation Update
        _outputRectangle.X = (int) Entity.Position.X;
        _outputRectangle.Y = (int) Entity.Position.Y;
        _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
        if (_timeSinceLastFrame > _millisecondsPerFrame) _updateAnimation();

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(PlayerAtlas, _outputRectangle, CurrentPlayerRectangle, Color.White,0f,new Vector2(0,0), spriteEffect, 0f);
    }

    public override void Initialize()
    {
        _player = Entity.GetComponent<Player>();
        _outputRectangle = new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 33, 70);
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    // Methods

    /// <summary>
    ///     TODO
    /// </summary>
    public void WalkingLeft()
    {
    }

    /// <summary>
    ///     TODO
    ///     walking left but flipped
    /// </summary>
    public void WalkingRight()
    {
    }

    /// <summary>
    ///     TODO
    /// </summary>
    public void Jumping()
    {
    }

    private void _updateAnimation()
    {
        _timeSinceLastFrame = 0;
        if (_currentFrame < _numberOfFrames && _player.Velocity.X > 0)
        {
            CurrentPlayerRectangle.X = XOffsetRectangle * _currentFrame;
            spriteEffect = SpriteEffects.None;
            _currentFrame++;
        }
        else if (_currentFrame < _numberOfFrames && _player.Velocity.X < 0)
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
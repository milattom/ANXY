using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ANXY.ECS.Systems;
using ANXY.Start;
using ANXY.UI;
using System;

namespace ANXY.ECS.Components;

/// <summary>
/// PlayerSpriteRenderer renders the image of the Player. It switches between different frames depending on the current movement type, direction, etc.
/// </summary>
public class DogSpriteRenderer : Component
{
    public readonly Rectangle StartDogRectangle = new(0, 38, 50, 40);
    private const int XOffsetRectangle = 50;
    private const int _numberOfFrames = 4;
    private const int _millisecondsPerFrame = 200; //the smaller the faster animation. 42ms ~= 24fps
    private Rectangle CurrentDogRectangle;
    private SpriteEffects _spriteEffect;
    private Texture2D DogAtlas { get; }
    private bool _gamePaused = false;
    private bool _dogPause = true;

    /// <summary>
    /// set the playerAtlas with all Player Movement Frames
    /// </summary>
    public DogSpriteRenderer(Texture2D dogAtlas)
    {
        DogAtlas = dogAtlas;
        CurrentDogRectangle = StartDogRectangle;
        DogSpriteSystem.Instance.Register(this);
        ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
        PlayerSystem.Instance.GetFirstComponent().Entity.GetComponent<Player>().EndReached += OnEndReached;
    }

    /// <summary>
    /// Update the Player Frame to the current Frame. shifts to the next Animation frame if needed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
        if (_dogPause)
        {
            return;
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
        var drawRectangle = new Rectangle((int)(Entity.Position.X - Camera.ActiveCamera.DrawOffset.X), (int)(Entity.Position.Y - Camera.ActiveCamera.DrawOffset.Y), 50, 40);

        spriteBatch.Draw(DogAtlas, drawRectangle, CurrentDogRectangle, Color.White, 0f, new Vector2(0, 0),
            _spriteEffect, 0f);
    }

    /// <summary>
    /// Update the Animation frame to the next frame.
    /// </summary>
    private void UpdateAnimation(GameTime gameTime)
    {
        var currentAnimationTime = gameTime.TotalGameTime.TotalMilliseconds % (_millisecondsPerFrame * _numberOfFrames);
        var currentFrame = (int)(currentAnimationTime / _millisecondsPerFrame);

        CurrentDogRectangle.X = XOffsetRectangle * currentFrame;
    }

    private void OnGamePausedChanged(bool gamePaused)
    {
        _gamePaused = gamePaused;
    }

    private void OnEndReached()
    {
        _dogPause = false;
    }

    public void Reset()
    {
        _dogPause = true;
    }

    public override void Destroy()
    {
        DogSpriteSystem.Instance.Unregister(this);
        ANXYGame.Instance.GamePausedChanged -= OnGamePausedChanged;
    }
}
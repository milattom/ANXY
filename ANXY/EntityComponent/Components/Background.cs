using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// Background manager. Shifts the Background when needed
/// </summary>
public class Background : Component
{
    private Player _playerComponent;
    private Vector2 _screenScrollingDirection;
    private float _screenScrollingSpeed;
    public int WindowHeight { get; }
    public int WindowWidth { get; }
    public Entity PlayerEntity { get; set; }

    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="windowWidth">Current Windows size, Width</param>
    /// <param name="windowHeight">Current Windows size, Height</param>
    public Background(int windowWidth, int windowHeight)
    {
        this.WindowWidth = windowWidth;
        this.WindowHeight = windowHeight;
    }

    /// <summary>
    /// Update called multiple times every Frame (Update Cycle).
    ///
    /// Implementation of "Parallax Background Shifting"
    /// Move the Background when the Player Character is about to leave the Screen. Player Speed is set to zero, Background speed is reversed Player Speed.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        _screenScrollingDirection = Vector2.Zero;
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) _screenScrollingDirection = new Vector2(-1, 0);
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) _screenScrollingDirection = new Vector2(1, 0);

        if (_playerComponent.Velocity.X == 0 && _playerComponent.Inputdirection.X != 0)
            _screenScrollingSpeed = Math.Abs(_playerComponent.ScrollSpeed);
        else
            _screenScrollingSpeed = 0;


        Entity.Position += _screenScrollingDirection * _screenScrollingSpeed *
                           (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    /// <summary>
    /// Draw called once every Frame.
    /// </summary>
    /// <param name="gameTime">gameTime</param>
    /// <param name="spriteBatch">the Sprite Batch of the Background</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    /// <summary>
    /// Initialize Player so we later know how far from the Edges the Player is positioned.
    /// </summary>
    public override void Initialize()
    {
        _playerComponent = PlayerEntity.GetComponent<Player>();
    }

    /// <summary>
    /// TODO implement Destroy
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void Destroy()
    {
        throw new NotImplementedException();
    }
}
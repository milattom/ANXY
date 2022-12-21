using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// Player is the Main Game Character
/// </summary>
public class Player : Component
{
    public Vector2 Velocity { get; private set; } = Vector2.Zero;
    public float ScrollSpeed { get; private set; } = 0;
    public PlayerState State { get; private set; } = PlayerState.Idle;
    public Vector2 Inputdirection { get; private set; } = Vector2.Zero;

    //TODO remove GroundLevel or reduce it to Window Bottom Edge when Level is fully implemented in Tiled.
    private const int GroundLevel = 800;
    private const float Gravity = 15;
    private const float WalkForce = 200;
    private const float JumpForce = 550;

    private bool isAlive = true;
    private PlayerInputController playerInputController;

    private int windowHeight;

    private readonly int windowWidth;

    /// <summary>
    /// Player Class Constructor
    /// </summary>
    /// <param name="windowWidth">Current Window size, Width</param>
    /// <param name="windowHeight">Current Window size, Height</param>
    public Player(int windowWidth, int windowHeight)
    {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
    }

    /* TODO maybe implement later. Ideas for now
    public bool Crouch()
    {
        return true;
    }
    public bool Dash()
    {
        return true;
    }
    */

    /// <summary>
    /// TODO implement Initialize
    /// </summary>
    public override void Initialize()
    {
    }

    /// <summary>
    /// TODO implement Destroy
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update, called multiple times per Frame. (Update Cycle)
    /// TODO remove input from here and move it to PlayerInputController
    /// - checks input, moves the player accordingly.
    /// - creates gravity and checks for collisions.
    /// TODO remove screen constraints for background movement to camera
    /// - updates position of Player Entity
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        //input
        var boxCollider = Entity.GetComponent<BoxCollider>();
        var state = Keyboard.GetState();
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var acceleration = new Vector2(WalkForce, Gravity);
        Inputdirection = Vector2.Zero;

        if (boxCollider.Colliding)
        {
            switch (boxCollider.CollidingEdge)
            {
                case BoxCollider.Edge.Top:
                    Velocity = new Vector2(Velocity.X, 0);
                    Inputdirection = new Vector2(Inputdirection.X, 1); //gravity
                    break;
                case BoxCollider.Edge.Left:
                    if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) Inputdirection = new Vector2( 1, Inputdirection.Y);
                    break;
                case BoxCollider.Edge.Right:
                    if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) Inputdirection = new Vector2( -1, Inputdirection.Y);
                    break;
                case BoxCollider.Edge.Bottom:
                    Velocity = new Vector2(Velocity.X, 0); //stop gravity
                    if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) Inputdirection = new Vector2( 1, Inputdirection.Y);
                    if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) Inputdirection = new Vector2( -1, Inputdirection.Y);
                    if (state.IsKeyDown(Keys.Space))
                    {
                        Inputdirection = new Vector2( Inputdirection.X, -JumpForce/Gravity);
                    }
                    break;
            }
        }
        else
        {
            Inputdirection = new Vector2(0, 1); //gravity
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) Inputdirection = new Vector2( 1, Inputdirection.Y);
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) Inputdirection = new Vector2( -1, Inputdirection.Y);
        }


        //velocity update
        var xVelocity = Inputdirection.X * acceleration.X;
        var yVelocity = Velocity.Y + (Inputdirection.Y * acceleration.Y);
        Velocity = new Vector2(xVelocity, yVelocity);
        ScrollSpeed = Velocity.X;

        //ScreenConstraintUpdate
        if ((Inputdirection.X > 0 && Entity.Position.X >= windowWidth * 3.0 / 4.0)
            || (Inputdirection.X < 0 && Entity.Position.X <= windowWidth * 1.0 / 4.0))
            Velocity *= new Vector2(0, 1);

        //position update
        Entity.Position += Velocity * dt;
    }

    /// <summary>
    /// Draw
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    /// <summary>
    /// TODO implementation idea for DoubleJump
    /// set DoubleJump to true or set Player State to Double jump. only one Double Jump Possible, not infinite or multiple.
    /// </summary>
    /// <returns>true if jumped.</returns>
    public bool DoubleJump()
    {
        return true;
    }

    /// <summary>
    /// TODO idea for future for cancelling jump and immediately starting to drop. No more Jumping or Double jumping. Set Player State to either Drop or Falling
    /// </summary>
    /// <returns>true if dropping possible and started</returns>
    public bool Drop()
    {
        return true;
    }

    /// <summary>
    /// TODO idea for future of player health or death. Will die when anxiety score is too high. Game Over. Try Again.
    /// </summary>
    /// <returns>true when dying possible.</returns>
    public bool Die()
    {
        return true;
    }

    /// <summary>
    /// PlayerState describes in what movement state the Player currently is
    /// </summary>
    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        DoubleJumping,
        Ducking,
        Falling
    }
}
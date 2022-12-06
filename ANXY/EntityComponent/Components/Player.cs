using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ANXY.EntityComponent.Components;

public class Player : Component
{
    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        DoubleJumping,
        Ducking,
        Falling
    }
    public Vector2 Velocity { get; private set; } = Vector2.Zero;
    public PlayerState State { get; private set; } = PlayerState.Idle;
    public Vector2 Inputdirection { get; private set; } = Vector2.Zero;

    private const int GroundLevel = 800;
    private const float Gravity = 15;
    private const float WalkForce = 200;
    private const float JumpForce = 550;

    private bool isAlive = true;
    private PlayerInputController playerInputController;
    private int windowHeight;

    private readonly int windowWidth;


    /// <summary>
    ///     TODO
    /// </summary>
    public Player(int windowWidth, int windowHeight)
    {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
    }

    /* TODO implement later
    public bool Crouch()
    {
        return true;
    }
    public bool Dash()
    {
        return true;
    }
    */

    public override void Initialize()
    {
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public override void Update(GameTime gameTime)
    {
        //input
        var colliding = Entity.GetComponent<BoxCollider>().isColliding;
        var state = Keyboard.GetState();
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var acceleration = new Vector2(WalkForce, Gravity);
        Inputdirection = Vector2.Zero;

        //on ground
        if(Entity.Position.Y >= GroundLevel || colliding)
        {
            colliding = true;
            Velocity = new Vector2(Velocity.X, 0);
        }
        //free fall
        else Inputdirection = new Vector2(0, 1);

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) Inputdirection = new Vector2( 1, Inputdirection.Y);
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) Inputdirection = new Vector2( -1, Inputdirection.Y);
        if (colliding && state.IsKeyDown(Keys.Space)) Inputdirection = new Vector2( Inputdirection.X, -JumpForce/Gravity);

        //velocity update
        var xVelocity = Inputdirection.X * acceleration.X;
        var yVelocity = Velocity.Y + (Inputdirection.Y * acceleration.Y);
        Velocity = new Vector2(xVelocity, yVelocity);

        //ScreenConstraintUpdate
        if ((Inputdirection.X > 0 && Entity.Position.X >= windowWidth * 3.0 / 4.0)
            || (Inputdirection.X < 0 && Entity.Position.X <= windowWidth * 1.0 / 4.0))
            Velocity *= new Vector2(0, 1);

        //position update
        Entity.Position += Velocity * dt;
    }

    /// <summary>
    ///     TODO
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public bool DoubleJump()
    {
        return true;
    }

    public bool Drop()
    {
        return true;
    }

    public bool Die()
    {
        return true;
    }
}
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

    private const int GroundLevel = 400;
    private const float Gravity = 10;
    private const float WalkForce = 200;
    private const float JumpForce = 500;

    private float _jumpedHeight;
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

    public float WalkingInXVelocity { get; private set; }

    public Vector2 Velocity = Vector2.Zero;
    public PlayerState MovementState { get; private set; }

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
        MovementState = PlayerState.Falling;
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    // Methods

    public override void Update(GameTime gameTime)
    {
        //input
        var state = Keyboard.GetState();
        var inputDirection = Vector2.Zero;
        var colliding = false;
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var acceleration = new Vector2(WalkForce, Gravity);

        //on ground
        if(Entity.Position.Y >= GroundLevel)
        {
            colliding = true;
            Velocity.Y = 0;
            Entity.Position = new Vector2(Entity.Position.X, GroundLevel);
        }
        //free fall
        else
        {
            inputDirection.Y = 1;
        }

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) inputDirection.X = 1;
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) inputDirection.X = -1;
        if (colliding && state.IsKeyDown(Keys.Space)) inputDirection.Y = -JumpForce/Gravity;

        //velocity update
        Velocity.X =  inputDirection.X * acceleration.X;
        Velocity.Y += inputDirection.Y * Gravity;

        //Direction update
        WalkingInXVelocity = Velocity.X;

        //ScreenConstraintUpdate
        if ((WalkingInXVelocity > 0 && Entity.Position.X >= windowWidth * 3.0 / 4.0)
            || (WalkingInXVelocity < 0 && Entity.Position.X <= windowWidth * 1.0 / 4.0))
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
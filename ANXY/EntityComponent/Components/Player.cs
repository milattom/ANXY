using System;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ANXY.EntityComponent.Components;

public class Player : Component
{
    public BoxCollider BoxCollider;

    private bool isAlive = true;
    private PlayerInputController playerInputController;

    public Vector2 CurrentVelocity { get; private set; }
    private PlayerState State { get; }
    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        DoubleJumping,
        Ducking,
        Falling
    }

    public bool WalkingRight = true;

    private const float WalkingSpeed = 300;


    /// <summary>
    ///     TODO
    /// </summary>
    public Player()
    {

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

    // Methods

    public override void Update(GameTime gameTime)
    {
        //input
        KeyboardState state = Keyboard.GetState();
        Vector2 inputDirection = Vector2.Zero;
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
        {
            inputDirection += new Vector2(1, 0);
        }
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
        {
            inputDirection += new Vector2( -1,0);
        }
        if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
        {
            inputDirection += new Vector2(0, -1);
        }
        if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
        {
            inputDirection += new Vector2(0, 1);
        }

        //velocity update
        CurrentVelocity = inputDirection * WalkingSpeed;

        //Direction update
        WalkingRight = CurrentVelocity.X >= 0;

        //position update
        Entity.Position += CurrentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

    }

    /// <summary>
    ///     TODO
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }

    public bool Jump()
    {
        return true;
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

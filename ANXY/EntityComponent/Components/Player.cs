﻿using System;
using System.Diagnostics;
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
    private readonly int _GroundLevel = 400;
    private readonly int _JumpHeight = 150;
    private float JumpedHeight = 0;

    public Vector2 CurrentVelocity { get; private set; }
    public PlayerState MovementState { get; private set; }
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

    private Vector2 MovementSpeed = new Vector2(200,1000);


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
        MovementState = PlayerState.Idle;
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
        Vector2 jumpingDirection = Vector2.Zero;

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
        {
            inputDirection += new Vector2(1, 0);
        }
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
        {
            inputDirection += new Vector2( -1,0);
        }
        if (state.IsKeyDown(Keys.Space) && MovementState != PlayerState.Falling)
        {
            MovementState = PlayerState.Jumping;
            inputDirection += Jump(gameTime);
        }


        /*TODO remove
        if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
        {
            inputDirection += new Vector2(0, -1);
        }
        if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
        {
            inputDirection += new Vector2(0, 1);
        }*/

        //velocity update
        CurrentVelocity = inputDirection * MovementSpeed;

        if (Entity.Position.Y < _GroundLevel)
        {
            MovementState = PlayerState.Idle;
            CurrentVelocity += applyGravity();
        }

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

    public Vector2 Jump(GameTime gameTime)
    {
        if (MovementState == PlayerState.Idle || MovementState == PlayerState.Running)
        {
            MovementState = PlayerState.Jumping;
        }

        if (MovementState == PlayerState.Jumping)
        {
            JumpedHeight += MovementSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (JumpedHeight >= _JumpHeight)
            {
                JumpedHeight = 0;
                MovementState = PlayerState.Falling;
            }
            else
            {
                return new Vector2(0, -1);
            }
        }
        return new Vector2(0, 0);
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

    private Vector2 applyGravity()
    {
        if (Entity.Position.Y < _GroundLevel && MovementState != PlayerState.Jumping)
        {
            return new Vector2(0, 250);
        }
        else
        {
            MovementState = PlayerState.Idle;
            return new Vector2(0, 0);
        }
    }


}

using System;
using System.Diagnostics;
using System.Numerics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ANXY.EntityComponent.Components;

public class Player : Component
{
    public BoxCollider BoxCollider;

    private bool isAlive = true;
    private PlayerInputController playerInputController;

    private readonly int _GroundLevel = 400;
    private static float _gravity = 9;
    private static float _walkForce = 200;
    private static float _jumpforce = 350;
    private Vector2 _inputDirection = Vector2.Zero;

    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Acceleration = new Vector2(_walkForce, _gravity);

    public bool colliding;

    public bool WalkingRight = true;

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
        KeyboardState state = Keyboard.GetState();
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //on ground
        if(Entity.Position.Y > _GroundLevel)
        {
            colliding = true;
            _inputDirection = Vector2.Zero;
        }
        //free fall
        else
        {
            colliding = false;
            _inputDirection.Y += 1;
        }

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
        {
            _inputDirection.X = 1;
        }
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
        {
            _inputDirection.X = -1;
        }
        if (colliding && state.IsKeyDown(Keys.Space))
        {
            _inputDirection.Y = -_jumpforce/_gravity;
        }

        //Direction update
        WalkingRight = Velocity.X >= 0;
        //velocity & position update
        Velocity.X =  _inputDirection.X * Acceleration.X;
        Velocity.Y = _inputDirection.Y * _gravity;
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

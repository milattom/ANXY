using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public Vector2 InputDirection { get; private set; } = Vector2.Zero;

    //TODO remove GroundLevel or reduce it to Window Bottom Edge when Level is fully implemented in Tiled.
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
        InputDirection = Vector2.Zero;


        if (boxCollider.Colliding)
        {
            var edgeCases = new List<(BoxCollider.Edge, Vector2)>(boxCollider.CollidingEdges);

            var edges = new List<BoxCollider.Edge>();
            foreach (var e in edgeCases)
            {
                edges.Add(e.Item1);
            }
            var lastCase = boxCollider.CollidingEdges.Last();

            foreach (var edgeCase in edgeCases)
            {
                var edge = edgeCase.Item1;
                var pos = edgeCase.Item2;
                switch (edge)
                {
                    case BoxCollider.Edge.Top:
                        Velocity = new Vector2(Velocity.X, 0);
                        InputDirection = new Vector2(InputDirection.X, 1); //gravity
                        Entity.Position = new Vector2(Entity.Position.X, pos.Y);
                        break;
                    case BoxCollider.Edge.Left:
                        Entity.Position = new Vector2(pos.X, Entity.Position.Y);
                        if(!edges.Contains(BoxCollider.Edge.Bottom)) InputDirection = new Vector2(0, 1); //gravity}
                        break;
                    case BoxCollider.Edge.Right:
                        Entity.Position = new Vector2(pos.X-boxCollider.Dimensions.X, Entity.Position.Y);
                        if(!edges.Contains(BoxCollider.Edge.Bottom)) InputDirection = new Vector2(0, 1); //gravity}
                        break;
                    case BoxCollider.Edge.Bottom:
                        Entity.Position = new Vector2(Entity.Position.X, pos.Y-boxCollider.Dimensions.Y);
                        Velocity = new Vector2(Velocity.X, 0); //stop gravity
                        if (state.IsKeyDown(Keys.Space))
                        {
                            InputDirection = new Vector2( InputDirection.X, -JumpForce/Gravity);
                        }
                        break;
                }
                boxCollider.CollidingEdges.Clear();
            }
        }
        else
        {
            InputDirection = new Vector2(0, 1); //gravity
        }
        boxCollider.Colliding = false;

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) InputDirection = new Vector2( 1, InputDirection.Y);
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) InputDirection = new Vector2( -1, InputDirection.Y);

        //velocity update
        var xVelocity = InputDirection.X * acceleration.X;
        var yVelocity = Velocity.Y + (InputDirection.Y * acceleration.Y);
        Velocity = new Vector2(xVelocity, yVelocity);
        ScrollSpeed = Velocity.X;

        //ScreenConstraintUpdate
        if ((InputDirection.X > 0 && Entity.Position.X >= windowWidth * 3.0 / 4.0)
            || (InputDirection.X < 0 && Entity.Position.X <= windowWidth * 1.0 / 4.0))
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
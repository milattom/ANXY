using ANXY.Start;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using static ANXY.EntityComponent.Components.BoxCollider;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// Player is the Main Game Character
/// </summary>
public class Player : Component
{
    public Vector2 Velocity => _velocity;
    private Vector2 _velocity = Vector2.Zero;
    public PlayerState State { get; private set; } = PlayerState.Idle;
    public Vector2 InputDirection { get; private set; } = Vector2.Zero;

    //TODO remove GroundLevel or reduce it to Window Bottom Edge when Level is fully implemented in Tiled.
    private const float Gravity = 350;
    private const float JumpVelocity = 300;
    private bool _midAir = true;

    private const float MaxWalkSpeed = 300;
    private const float WalkAcceleration = 150;
    private const float FloorFriction = 25;

    private bool _isAlive = true;

    public Player()
    {
        PlayerSystem.Instance.Register(this);
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
        ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
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
    /// - updates position of Player Entity
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        //keyboard input
        InputDirection = Vector2.Zero;

        if (PlayerInput.Instance.IsWalkingRight())
            InputDirection += new Vector2(1, 0);

        if (PlayerInput.Instance.IsWalkingLeft())
            InputDirection += new Vector2(-1, 0);

        //velocity update
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var acceleration = new Vector2(WalkAcceleration * InputDirection.X, Gravity);
        _velocity += acceleration * dt;
        if (PlayerInput.Instance.IsJumping() && !_midAir)
        {
            _velocity.Y = -JumpVelocity;
            _midAir = true;
        }

        _velocity.X = InputDirection.X * WalkAcceleration;

        //position update
        Entity.Position += _velocity * dt;

        //collisions
        HandleCollisions();
    }

    private void OnGamePausedChanged(bool gamePaused)
    {
        IsActive = !gamePaused;
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void HandleCollisions()
    {
        var playerBoxCollider = Entity.GetComponent<BoxCollider>();
        var colliders = BoxColliderSystem.Instance.GetCollisions(playerBoxCollider);

        var leftRightColliders = colliders.Where(col =>
        {
            var detectedEdge = BoxColliderSystem.DetectEdge(playerBoxCollider, col);
            return detectedEdge == Edge.Left || detectedEdge == Edge.Right;
        });
        foreach (var collider in leftRightColliders)
        {
            if (BoxColliderSystem.Instance.IsColliding(playerBoxCollider, collider))
            {
                ActOnCollider(collider);
            }
        }

        var restColliders = BoxColliderSystem.Instance.GetCollisions(playerBoxCollider);

        if (restColliders.Count == 0)
        {
            _midAir = true;
        }

        foreach (var collider in restColliders)
        {
            if (BoxColliderSystem.Instance.IsColliding(playerBoxCollider, collider))
            {
                ActOnCollider(collider);
            }
        }
    }

    private void ActOnCollider(BoxCollider collider)
    {
        var playerBoxCollider = Entity.GetComponent<BoxCollider>();
        var detetedEdge = BoxColliderSystem.DetectEdge(playerBoxCollider, collider);
        var edgePosition = collider.GetCollisionPosition(detetedEdge);
        switch (detetedEdge)
        {
            case BoxCollider.Edge.Top:
                if (Velocity.Y >= 0)
                {
                    _midAir = false;
                    _velocity = new Vector2(Velocity.X, 0);
                }

                Entity.Position = new Vector2(Entity.Position.X, edgePosition - playerBoxCollider.Dimensions.Y - playerBoxCollider.Offset.Y);
                break;
            case BoxCollider.Edge.Bottom:
                _velocity = new Vector2(Velocity.X,
                    Math.Clamp(Velocity.Y, 1, float.PositiveInfinity)); //stop gravity
                Entity.Position = new Vector2(Entity.Position.X, edgePosition - playerBoxCollider.Offset.Y);
                break;
            case BoxCollider.Edge.Left:
                _velocity = new Vector2(Math.Clamp(Velocity.X, float.NegativeInfinity, 0), Velocity.Y);
                Entity.Position = new Vector2(edgePosition - playerBoxCollider.Dimensions.X - playerBoxCollider.Offset.X, Entity.Position.Y);
                break;
            case BoxCollider.Edge.Right:
                _velocity = new Vector2(Math.Clamp(Velocity.X, 0, float.PositiveInfinity), Velocity.Y);
                Entity.Position = new Vector2(edgePosition - playerBoxCollider.Offset.X, Entity.Position.Y);
                break;
            default:
                throw new ArgumentOutOfRangeException("Collision happened but no edge case");
        }
    }

    // ########################################################## future TO DO section #####################################################################
    /// <summary>
    /// Draw
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

    /// <summary>
    /// TODO implementation idea for DoubleJump
    /// set DoubleJump to true or set Player State to Double jump. only one Double Jump Possible, not infinite or multiple.
    /// </summary>
    /// <returns>true if jumped.</returns>
    public bool DoubleJump()
    {
        return true;
    }

    public void Reset()
    {
        _velocity = Vector2.Zero;
        Entity.Position = new Vector2(1200,540);
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
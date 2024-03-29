﻿using ANXY.ECS.Systems;
using ANXY.Start;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using static ANXY.ECS.Components.BoxCollider;

namespace ANXY.ECS.Components;

/// <summary>
/// Player is the Main Game Character
/// </summary>
public class Player : Component
{
    public Vector2 Velocity => _velocity;
    private Vector2 _velocity = Vector2.Zero;
    public Vector2 InputDirection { get; private set; } = Vector2.Zero;
    public event Action EndReached;

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

    private const float Gravity = 350;
    private const float JumpVelocity = 310;
    public bool MidAir { get; private set; } = true;

    private const float WalkAcceleration = 150;

    /// <summary>
    /// Registers the player in the system
    /// </summary>
    public Player()
    {
        PlayerSystem.Instance.Register(this);
    }

    /// <summary>
    /// Sets the Event GamePausedChanged
    /// </summary>
    public override void Initialize()
    {
        ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
    }

    public override void Destroy()
    {
        PlayerSystem.Instance.Unregister(this);
    }

    /// <summary>
    /// Update, called multiple times per Frame. (Update Cycle)
    /// - checks input, moves the player accordingly.
    /// - creates gravity and checks for collisions.
    /// - updates position of Player Entity
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        //keyboard input
        InputDirection = Vector2.Zero;

        if (PlayerInput.Instance.IsWalkingRight)
            InputDirection += new Vector2(1, 0);

        if (PlayerInput.Instance.IsWalkingLeft)
            InputDirection += new Vector2(-1, 0);

        //velocity update
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var acceleration = new Vector2(WalkAcceleration * InputDirection.X, Gravity);
        _velocity += acceleration * dt;
        if (PlayerInput.Instance.IsJumping && !MidAir)
        {
            _velocity.Y = -JumpVelocity;
            MidAir = true;
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
    /// Handles Collisions with other Entities
    /// </summary>
    private void HandleCollisions()
    {
        var playerBoxCollider = Entity.GetComponent<BoxCollider>();
        var colliders = BoxColliderSystem.GetCollisions(playerBoxCollider);

        if (colliders.Any(c => c.LayerMask.Equals("End") ))
        {
            EndReached?.Invoke();
        }

        var leftRightColliders = colliders.Where(col =>
        {
            var detectedEdge = BoxColliderSystem.DetectEdge(playerBoxCollider, col);
            return detectedEdge == Edge.Left || detectedEdge == Edge.Right;
        });
        foreach (var collider in leftRightColliders.Where(collider => BoxColliderSystem.IsColliding(playerBoxCollider, collider)))
        {
            ActOnCollider(collider);
        }

        var restColliders = BoxColliderSystem.GetCollisions(playerBoxCollider);

        if (restColliders.Count == 0)
        {
            MidAir = true;
        }

        foreach (var collider in restColliders.Where(collider => BoxColliderSystem.IsColliding(playerBoxCollider, collider)))
        {
            ActOnCollider(collider);
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
                    MidAir = false;
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
                throw new ArgumentException("Collision happened but no edge case");
        }
    }

    public void Reset()
    {
        _velocity = Vector2.Zero;
        Entity.Position = ANXYGame.Instance.SpawnPosition;
    }
}
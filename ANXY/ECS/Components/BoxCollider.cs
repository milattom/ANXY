using ANXY.ECS.Systems;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.ECS.Components;

/// <summary>
/// Represents a Collision box. Holds the pivot, center, Dimensions, Layermask and a boolean which indicates if
/// it is colliding with another BoxCollider (set by the system). If a collision happens, Colliding is set to true
/// and a List with all edges from other colliders this box collides with.
/// </summary>
public class BoxCollider : Component
{
    public bool DebugEnabled { get; set; } = false;
    public Vector2 Pivot => Entity.Position + Offset;
    public Vector2 Center => Pivot + Dimensions / 2;
    public Vector2 Dimensions { get; }
    public Vector2 Offset { get; set; }
    public string LayerMask { get; }
    /// <summary>
    /// List of other boxes edges which are colliding with .this and its correspondent edge position
    /// as a vector. The position of the edge serves as an orientation where the collision happened on
    /// the map and where the box should be moved next.
    /// after the collision happens.
    /// </summary>
    public List<(Edge, Vector2)> CollidingEdges { get; set; } = new List<(Edge, Vector2)>();
    private readonly Color _activeColor = Color.Green;
    private readonly Color _inactiveColor = Color.Blue;
    private Color _highlightColor;
    private Texture2D _recTexture;

    /// <summary>
    /// enum to describe an Edge at collision
    /// </summary>
    public enum Edge
    {
        Bottom,
        Top,
        Left,
        Right
    }

    /// <summary>
    /// Constructor takes a rectangle with the dimensions of the bounding box
    /// and the x and y values for its offset. The dimensions + offset will be the edges.
    /// The layerMask will be needed to have a more complex collision system.
    /// </summary>
    /// <param name="rectangle"></param>
    /// <param name="layerMask"></param>
    public BoxCollider(Rectangle rectangle, string layerMask)
    {
        Dimensions = new Vector2(rectangle.Width, rectangle.Height);
        Offset = new Vector2(rectangle.X, rectangle.Y);
        LayerMask = layerMask;
        BoxColliderSystem.Instance.Register(this);
    }

    /// <summary>
    /// Returns the edge of this box which collided with the other (usually player).
    /// It is the opposite edge, so if the as parameter given edge = bottom
    /// (means the player is standing on it) it will return the position vector
    /// of the top edge from this box.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns>Position vector of the edge that was touched</returns>
    /// <exception cref="ArgumentException"></exception>
    public float GetCollisionPosition(Edge edge)
    {
        if (DebugEnabled) Highlight();
        return edge switch
        {
            Edge.Top => Pivot.Y,
            Edge.Bottom => Pivot.Y + Dimensions.Y,
            Edge.Left => Pivot.X,
            Edge.Right => Pivot.X + Dimensions.X,
            _ => throw new ArgumentException("No edge to get position from!")
        };
    }

    /// <summary>
    /// Gets called each frame and checks if Debug is enabled to dehighlight the collision boxes.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        if (DebugEnabled) Dehighlight(); //Debug
    }

    /// <summary>
    /// Gets called by EntityManager, sets the highlightColor for debugging
    /// </summary>
    public override void Initialize()
    {
        _highlightColor = _activeColor;
    }

    // -------------------------------------------------------- Debugging ------------------------------------------------------------------

    /// <inheritdoc />
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!DebugEnabled) return;
        var rect = new Rectangle(
            (int)(Pivot.X - Camera.ActiveCamera.DrawOffset.X),
            (int)(Pivot.Y - Camera.ActiveCamera.DrawOffset.Y),
            (int)Dimensions.X,
            (int)Dimensions.Y
            );
        spriteBatch.Draw(_recTexture, rect, _highlightColor);
    }
    public void SetRectangleTexture(Texture2D texture)
    {
        _recTexture = texture;
    }

    public void Highlight()
    {
        _highlightColor = _activeColor;
    }
    public void Dehighlight()
    {
        _highlightColor = _inactiveColor;
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

/// <summary>
/// Represents a Collision box. Holds the pivot, center, Dimensions, Layermask and a boolean which indicates if
/// it is colliding with another BoxCollider (set by the system). If a collision happens, Colliding is set to true
/// and a List with all edges from other colliders this box collides with.
/// </summary>
public class BoxCollider : Component
{
    public bool DebugMode;
    public Vector2 Pivot { get; set; }
    public Vector2 Center { get; set; }
    public Vector2 Dimensions { get; }
    public Vector2 Offset { get; }
    public string LayerMask { get; }
    public bool Colliding { get; set; } = false;
    public Edge CollidingEdge { get; set; }
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
    public Vector2 GetCollisionPosition(Edge edge)
    {
        switch (edge)
        {
            case Edge.Bottom: //returns the top
                return new Vector2(Pivot.X, Pivot.Y);
            case Edge.Top: //returns the bottom 
                return new Vector2(Pivot.X, Pivot.Y + Dimensions.Y);
            case Edge.Right: //returns the left
                return new Vector2(Pivot.X, Pivot.Y);
            case Edge.Left: // returns the right
                return new Vector2(Pivot.X+Dimensions.X, Pivot.Y+Dimensions.Y);
            default:
                throw new ArgumentException("No edge to get position from!");
        }
    }

    /// <inheritdoc />
    public override void Update(GameTime gameTime)
    {
        UpDatePivotAndCenter();
        Dehighlight(); //Debug
        if (Colliding)
        {
            Highlight();
            //Colliding = false;
        }
    }

    /// <summary>
    /// Gets called by EntityManager, sets the highlightColor (debug) and the Pivot point
    /// </summary>
    public override void Initialize()
    {
        _highlightColor = _activeColor;
        Pivot = Entity.Position + Offset;
    }

    /// <summary>
    /// Should be called when this component isn't active anymore.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets the pivot point with the position Coordinates of its Entity and the Offset.
    /// Sets the center point by adding half the dimensions to the pivot point (upper left)
    /// </summary>
    private void UpDatePivotAndCenter()
    {
        Pivot = Entity.Position + Offset;
        Center = Pivot + Dimensions/2;
    }

    // -------------------------------------------------------- Debugging ------------------------------------------------------------------
    
    /// <inheritdoc />
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!DebugMode) return;
        var rect = new Rectangle((int)Pivot.X, (int)Pivot.Y, (int)Dimensions.X, (int)Dimensions.Y);
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
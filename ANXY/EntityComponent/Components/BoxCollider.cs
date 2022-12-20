using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class BoxCollider : Component
{
    public bool DebugMode;
    public Vector2 Pivot { get; set; }
    public Vector2 Center { get; set; }
    public Vector2 Dimensions { get; }
    public Vector2 Offset { get; }
    public string LayerMask { get; }
    public bool Colliding { get; set; }

    public Edge CollidingEdge { get; private set; }
    private readonly Color _activeColor = Color.Green;
    private readonly Color _inactiveColor = Color.Blue;
    private Color _highlightColor;
    private Texture2D _recTexture;

    public enum Edge
    {
        None,
        Bottom,
        Top,
        Left,
        Right
    }

    public BoxCollider(Rectangle rectangle, string layerMask)
    {
        Dimensions = new Vector2(rectangle.Width, rectangle.Height);
        Offset = new Vector2(rectangle.X, rectangle.Y);
        LayerMask = layerMask;
    }

    public BoxCollider(Vector2 dimensions, string layerMask)
    {
        Dimensions = dimensions - Offset;
        LayerMask = layerMask;
    }

    public override void Update(GameTime gameTime)
    {
        SetPivotAndCenter();
    }

    public override void Initialize()
    {
        _highlightColor = _activeColor;
        Pivot = Entity.Position + Offset;
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public bool IsColliding(BoxCollider other)
    {
        var dx = (Center.X - other.Center.X);
        var dy = (Center.Y - other.Center.Y);
        var d = new Vector2(Math.Abs(dx), Math.Abs(dy));

        var dxMax = (Dimensions.X + other.Dimensions.X) / 2;
        var dyMax = (Dimensions.Y + other.Dimensions.Y) / 2;
        var dMax = new Vector2(dxMax, dyMax);

        if (Compare(d, dMax) < 0)
        {
            EdgeDetection(dx, dy, dMax);
            return true;
        }
        CollidingEdge = Edge.None;
        return false;
    }

    private void EdgeDetection(float dx, float dy, Vector2 dMax)
    {
        var crossWidth = dMax.X * dy;
        var crossHeight = dMax.Y * dx;
        if (crossWidth < crossHeight) 
        {
            CollidingEdge = (crossWidth < (-crossHeight)) ? Edge.Bottom : Edge.Left;
        }
        else
        {
            CollidingEdge = (crossWidth <-(crossHeight)) ? Edge.Right : Edge.Top;
        }
    }

    /// <summary>
    /// Sets the pivot point with the position Coordinates of its Entity and the Offset.
    /// Sets the center point by adding half the dimensions to the pivot point (upper left)
    /// </summary>
    private void SetPivotAndCenter()
    {
        Pivot = Entity.Position + Offset;
        Center = Pivot + Dimensions/2;
    }

    /// <summary>
    /// Compares two vectors by element.
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns>
    ///     -1 if x_1 < x_2 && y_1 < y_2
    ///     0 if x_1 == x_2 && y_1 == y_2
    ///     1 if x_1 > x_2 && y_1 > y_2
    /// </returns>
    private int Compare(Vector2 v1, Vector2 v2)
    {
        if (v1.X <= v2.X 
            && v1.Y <= v2.Y) return -1;
        if (v1.X > v2.X 
            && v1.Y > v2.Y) return 1;
        return 0;
    }

    // -------------------------------------------------------- Debugging ------------------------------------------------------------------
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!DebugMode) return;
        var rect = new Rectangle((int)Pivot.X, (int)Pivot.Y, (int)Dimensions.X, (int)Dimensions.Y);
        switch (CollidingEdge)
        {
            case Edge.None:
                break;
            case Edge.Bottom:
                rect = new Rectangle((int)Pivot.X, (int)Pivot.Y+(int)Dimensions.X-1, (int)Dimensions.X, 1);
                break;
            case Edge.Top:
                rect = new Rectangle((int)Pivot.X, (int)Pivot.Y, (int)Dimensions.X, 1);
                break;
            case Edge.Left:
                rect = new Rectangle((int)Pivot.X, (int)Pivot.Y, 1, (int)Dimensions.Y);
                break;
            case Edge.Right:
                rect = new Rectangle((int)Pivot.X+(int)Dimensions.X-1, (int)Pivot.Y, 1, (int)Dimensions.Y);
                break;
        }
        spriteBatch.Draw(_recTexture, rect, _highlightColor);

    }
    public void setRectangleTexture(Texture2D texture)
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
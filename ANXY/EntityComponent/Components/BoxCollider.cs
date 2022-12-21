using System;
using System.Runtime.ConstrainedExecution;
using System.Text;
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
    public bool Colliding { get; set; } = false;

    public Edge CollidingEdge { get; set; }
    private readonly Color _activeColor = Color.Green;
    private readonly Color _inactiveColor = Color.Blue;
    private Color _highlightColor;
    private Texture2D _recTexture;

    public enum Edge
    {
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
        //Debug
        Dehighlight();
        if (Colliding)
        {
            Highlight();
        }
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

    /// <summary>
    /// Sets the pivot point with the position Coordinates of its Entity and the Offset.
    /// Sets the center point by adding half the dimensions to the pivot point (upper left)
    /// </summary>
    private void SetPivotAndCenter()
    {
        Pivot = Entity.Position + Offset;
        Center = Pivot + Dimensions/2;
    }

    // -------------------------------------------------------- Debugging ------------------------------------------------------------------
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
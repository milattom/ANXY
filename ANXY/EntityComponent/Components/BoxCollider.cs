using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class BoxCollider : Component
{
    public bool DebugMode;
    public Vector2 Pivot;
    public Vector2 Dimensions;
    public Vector2 Offset;
    public readonly string LayerMask;
    public bool isColliding;

    private BBox _bBox;
    private readonly Color _activeColor = Color.Green;
    private readonly Color _inactiveColor = Color.Blue;
    private Color _highlightColor;
    private Texture2D _recTexture;

    public struct BBox
    {
        public float minX, maxX, minY, maxY;
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
        Pivot = Entity.Position + Offset;
        _bBox.minX = Pivot.X;
        _bBox.maxX = Pivot.X + Dimensions.X;
        _bBox.minY = Pivot.Y;
        _bBox.maxY = Pivot.Y + Dimensions.Y;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!DebugMode) return;
        spriteBatch.Draw(_recTexture, new Rectangle((int)Pivot.X, (int)Pivot.Y, (int)Dimensions.X, (int)Dimensions.Y), _highlightColor);

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
        return _bBox.maxX >= other._bBox.minX
               && _bBox.minX <= other._bBox.maxX
               && _bBox.maxY >= other._bBox.minY
               && _bBox.minY <= other._bBox.maxY;
    }

    public void setRectangleTexture(Texture2D texture)
    {
        _recTexture = texture;
    }

    //Debugging
    public void Highlight()
    {
        _highlightColor = _activeColor;
    }
    public void Dehighlight()
    {
        _highlightColor = _inactiveColor;
    }
}
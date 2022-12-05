using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    private readonly Texture2D _filling;
    private BBox _bBox;

    public struct BBox
    {
        public float minX, maxX, minY, maxY;
    }

    public BoxCollider(Texture2D fillTexture2D, string layerMask)
    {
        LayerMask = layerMask;
        _filling = fillTexture2D;
    }

    public override void Update(GameTime gameTime)
    {
        throw new NotImplementedException();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!DebugMode) return;
        spriteBatch.Begin();
        spriteBatch.Draw(_filling, new Rectangle((int)Pivot.X, (int)Pivot.Y, (int)Dimensions.X, (int)Dimensions.Y), Color.Green);
        spriteBatch.End();
    }

    public override void Initialize()
    {
        Pivot = Entity.Position + Offset;
        if (LayerMask.Equals("Layer"))
        {
            var rectangle = (Entity.GetComponent<PlayerSpriteRenderer>().CurrentPlayerRectangle);
            Dimensions = new Vector2(rectangle.Width, rectangle.Height) - Offset;
        }
        else
        {
            var rectangle = (Entity.GetComponent<SingleSpriteRenderer>().SingleSpriteRectangle);
            Dimensions = new Vector2(rectangle.Width, rectangle.Height) - Offset;
        }

        _bBox.minX = Pivot.X;
        _bBox.maxX = Pivot.X + Dimensions.X;
        _bBox.minY = Pivot.Y;
        _bBox.maxY = Pivot.Y + Dimensions.Y;
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public Boolean IsColliding(BoxCollider other)
    {
        return _bBox.maxX >= other._bBox.minX && _bBox.minX <= other._bBox.maxX
            && _bBox.maxY >= other._bBox.minY && _bBox.minY <= other._bBox.maxY;
    }
}
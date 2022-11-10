using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class BoxCollider : Component
{
    // Class Fields
    public bool DebugMode = false;
    public Vector2 Dimensions;
    public Vector2 Offset;
    

    public override void Update(GameTime gameTime)
    {
        throw new NotImplementedException();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        throw new NotImplementedException();
    }

    public override void Initialize()
    {
        throw new NotImplementedException();
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }


    /// <summary>
    ///     TODO
    /// </summary>
    /// <returns></returns>
    public List<BoxCollider> GetAllCollisions()
    {
        var collisions = new List<BoxCollider>();
        return collisions;
    }
}
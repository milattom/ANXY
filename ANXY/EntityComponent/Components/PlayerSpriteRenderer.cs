using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.EntityComponent.Components;

public class PlayerSpriteRenderer : Component
{
    public Texture2D PlayerAtlas { get; private set; }


    /// <summary>
    ///     TODO
    /// </summary>
    public PlayerSpriteRenderer(Texture2D playerAtlas)
    {
        PlayerAtlas = playerAtlas;
    }


    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(PlayerAtlas, Entity.Position, Color.White);
    }

    public override void Initialize()
    {
        throw new NotImplementedException();
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    // Methods

    /// <summary>
    ///     TODO
    /// </summary>
    public void WalkingLeft()
    {
    }

    /// <summary>
    ///     TODO
    ///     walking left but flipped
    /// </summary>
    public void WalkingRight()
    {
    }

    /// <summary>
    ///     TODO
    /// </summary>
    public void Jumping()
    {
    }
}
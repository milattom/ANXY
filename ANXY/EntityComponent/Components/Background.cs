using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ANXY.EntityComponent.Components;

public class Background : Component
{
    private Vector2 ScreenScrollingDirection;
    private int windowHeight;
    private int windowWidth;

    public Background(int windowWidth, int windowHeight)
    {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
    }

    public Entity playerEntity { get; set; }

    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        ScreenScrollingDirection = Vector2.Zero;
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) ScreenScrollingDirection += new Vector2(-1, 0);
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) ScreenScrollingDirection += new Vector2(1, 0);

        Entity.Position += ScreenScrollingDirection;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public override void Initialize()
    {
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }
}
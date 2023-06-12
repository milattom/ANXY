﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.ECS.Systems;

/// <summary>
/// Provides the methods singatures Initialize(), Update(GameTime gameTime) and Draw(GameTime gameTime, SpriteBatch spriteBatch) which are necessary for ComponentSystems
/// </summary>
public interface ISystem
{
    public void Initialize();
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
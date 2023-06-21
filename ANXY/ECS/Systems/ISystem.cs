using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ANXY.ECS.Systems;

/// <summary>
/// Provides the methods singatures Initialize(), Update(GameTime gameTime) and Draw(GameTime gameTime, SpriteBatch spriteBatch) which are necessary for ComponentSystems
/// </summary>S
public interface ISystem
{
    public void Initialize();
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    public Component GetFirstComponent();
}

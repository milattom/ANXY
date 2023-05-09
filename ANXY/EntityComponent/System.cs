using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ANXY.EntityComponent
{
    public class System<T> where T : Component
    {
        protected static List<T> components = new();

        public void Register(T component)
        {
            components.Add(component);
        }
    
        public void Initialize()
        {
            foreach (T component in components)
            {
                component.Initialize();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (T component in components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (T component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }
    }

    class PlayerSystem : System<Player> { }
    class PlayerSpriteSystem : System<PlayerSpriteRenderer> { }
    class SpriteSystem : System<SingleSpriteRenderer> { }
    class CameraSystem : System<Camera> { }
    class FpsSystem : System<FpsCounter> { }
    class TextRendererSystem : System<TextRenderer> { }
    class UISystem : System<UI> { }
}

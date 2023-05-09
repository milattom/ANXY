using ANXY.EntityComponent.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.EntityComponent
{
    public class ComponentSystem<T> where T : Component
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

    class PlayerSystem : ComponentSystem<Player> 
        {
        private static readonly Lazy<PlayerSystem> _lazy = new(() => new PlayerSystem());
        public static PlayerSystem Instance => _lazy.Value;
        }
    class PlayerSpriteSystem : ComponentSystem<PlayerSpriteRenderer>
        {
        private static readonly Lazy<PlayerSpriteSystem> _lazy = new(() => new PlayerSpriteSystem());
        public static PlayerSpriteSystem Instance => _lazy.Value;
        }
    class SpriteSystem : ComponentSystem<SingleSpriteRenderer>
        {
        private static readonly Lazy<SpriteSystem> _lazy = new(() => new SpriteSystem());
        public static SpriteSystem Instance => _lazy.Value;
        }
    class CameraSystem : ComponentSystem<Camera>
        {
        private static readonly Lazy<CameraSystem> _lazy = new(() => new CameraSystem());
        public static CameraSystem Instance => _lazy.Value;
        }
    class FpsSystem : ComponentSystem<FpsCounter>
        {
        private static readonly Lazy<FpsSystem> _lazy = new(() => new FpsSystem());
        public static FpsSystem Instance => _lazy.Value;
        }
    class TextRendererSystem : ComponentSystem<TextRenderer>
        {
        private static readonly Lazy<TextRendererSystem> _lazy = new(() => new TextRendererSystem());
        public static TextRendererSystem Instance => _lazy.Value;

        }
    class UISystem : ComponentSystem<UI> //not used
        {
        private static readonly Lazy<UISystem> _lazy = new(() => new UISystem());
        public static UISystem Instance => _lazy.Value;

        }
    class BackgroundSystem : ComponentSystem<Background> 
        {
        private static readonly Lazy<BackgroundSystem> _lazy = new(() => new BackgroundSystem());
        public static BackgroundSystem Instance => _lazy.Value;
        }
}

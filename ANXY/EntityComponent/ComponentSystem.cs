using ANXY.EntityComponent.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.EntityComponent
{
    /// <summary>
    /// The ComponentSystem is used to hold the References of all Components of Type T.
    /// This makes it possible to load them during runtime/usage more efficiently into the cache.
    /// Instead of loading each Entity into the cache and looping through them to finaly get each
    /// component, a system provides the possibility to load all the needed components of a type
    /// such as Collider into the cache and loop right through them which results in better performance.
    /// The ComponentSystem implements the ISystem interface which is needed to do a Non Generic call on its methods.
    /// </summary>
    /// <typeparam name="T">Type of component</typeparam>
    public class ComponentSystem<T> : ISystem where T : Component
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
                if (component.IsActive) component.Initialize();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (T component in components)
            {
                if (component.IsActive) component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (T component in components)
            {
                if (component.IsActive) component.Draw(gameTime, spriteBatch);
            }
        }
    }

    class PlayerSystem : ComponentSystem<Player> 
        {
        private static readonly Lazy<PlayerSystem> _lazy = new(() => new PlayerSystem());
        public static PlayerSystem Instance => _lazy.Value;
        private PlayerSystem() 
        {
            SystemManager.Instance.Register(this);
        }
        }
    class PlayerSpriteSystem : ComponentSystem<PlayerSpriteRenderer>
        {
        private static readonly Lazy<PlayerSpriteSystem> _lazy = new(() => new PlayerSpriteSystem());
        public static PlayerSpriteSystem Instance => _lazy.Value;
        private PlayerSpriteSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class SpriteSystem : ComponentSystem<SingleSpriteRenderer>
        {
        private static readonly Lazy<SpriteSystem> _lazy = new(() => new SpriteSystem());
        public static SpriteSystem Instance => _lazy.Value;
        private SpriteSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class TextRendererSystem : ComponentSystem<TextRenderer>
        {
        private static readonly Lazy<TextRendererSystem> _lazy = new(() => new TextRendererSystem());
        public static TextRendererSystem Instance => _lazy.Value;
        private TextRendererSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class UISystem : ComponentSystem<UI> //not used
        {
        private static readonly Lazy<UISystem> _lazy = new(() => new UISystem());
        public static UISystem Instance => _lazy.Value; 
        private UISystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class CameraSystem : ComponentSystem<Camera>
        {
        private static readonly Lazy<CameraSystem> _lazy = new(() => new CameraSystem());
        public static CameraSystem Instance => _lazy.Value; 
        private CameraSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class FpsSystem : ComponentSystem<FpsCounter>
        {
        private static readonly Lazy<FpsSystem> _lazy = new(() => new FpsSystem());
        public static FpsSystem Instance => _lazy.Value;
        private FpsSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
    class BackgroundSystem : ComponentSystem<Background> 
        {
        private static readonly Lazy<BackgroundSystem> _lazy = new(() => new BackgroundSystem());
        public static BackgroundSystem Instance => _lazy.Value; 
        private BackgroundSystem()
        {
            SystemManager.Instance.Register(this);
        }
        }
}

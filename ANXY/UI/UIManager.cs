using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    internal class UIManager
    {
        public event Action PauseToggeled;

        private PauseMenu _pauseMenu;
        private float _fps;
        private Desktop _desktop;

        ///Singleton Pattern
        private static readonly Lazy<UIManager> lazy = new(() => new UIManager());


        /// <summary>
        ///     Singleton Pattern return the only instance there is
        /// </summary>
        public static UIManager Instance => lazy.Value;
        private UIManager()
        {
            _desktop = new Desktop();
            _pauseMenu = new PauseMenu();
            _pauseMenu.ResumePressed += OnResumeBtnPressed;
            _desktop.Root = _pauseMenu;
            _pauseMenu.Visible = false;
            PlayerInputController.Instance.GamePausedChanged += OnGamePausedChanged;
        }

        public void UpdateFPS(GameTime gameTime)
        {
            _fps = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            _desktop.Render();
        }

        public void OnResumeBtnPressed()
        {
            _pauseMenu.Visible = false;
            PauseToggeled?.Invoke();
        }

        public void OnGamePausedChanged(bool gamePaused)
        {
            _pauseMenu.Visible = !_pauseMenu.Visible;
        }
    }
}

using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace ANXY.UI
{
    internal class UIManager
    {
        private PauseMenu _pauseMenu;
        private float _fps = 0;
        private Desktop _desktop;

        public void UpdateFPS(GameTime gameTime)
        {
            _fps = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            _desktop.Render();
        }

        public UIManager()
        {
            _desktop = new Desktop();
            _pauseMenu = new PauseMenu();
            _pauseMenu.ResumePressed += OnResumeBtnPressed;
            _desktop.Root = _pauseMenu;
            _pauseMenu.Visible = false;
            PlayerInputController.Instance.GamePausedChanged += OnGamePausedChanged;
        }

        public void OnResumeBtnPressed()
        {
            _pauseMenu.Visible = false;
        }

        public void OnGamePausedChanged(bool gamePaused)
        {
            _pauseMenu.Visible = gamePaused;
        }
    }
}

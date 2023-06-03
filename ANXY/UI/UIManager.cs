using ANXY.EntityComponent.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    internal class UIManager
    {
        private PauseMenu _pauseMenu;
        private ControlsMenu _controlsMenu;
        private FpsOverlay _fpsOverlay;
        private Credits _credits;
        private Desktop _desktop;

        private float _fps;
        private bool _showFps = false;

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
            _pauseMenu.ResetGamePressed += OnResetGameBtnPressed;
            _pauseMenu.ControlsPressed += OnControlsBtnPressed;
            _pauseMenu.CreditsPressed += OnCreditsBtnPressed;
            _pauseMenu.ExitGamePressed += OnExitGamePressed;

            _controlsMenu = new ControlsMenu();
            _controlsMenu.ReturnPressed += OnReturnClicked;
            _controlsMenu.LoadDefaultsPressed += OnLoadDefaultsClicked;
            _controlsMenu.SaveChangesPressed += OnSaveChangesClicked;

            _desktop.Root = _pauseMenu;
            _desktop.Root.Visible = false;
            ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
            PlayerInput.Instance.ShowFpsKeyPressed += OnShowFpsKeyPressed;

            _fpsOverlay = new FpsOverlay();

            _credits = new Credits();
            _credits.ReturnPressed += OnReturnClicked;
        }

        public void UpdateFPS(GameTime gameTime)
        {
            if (_desktop.Root == _fpsOverlay && _desktop.Root.Visible)
            {
                _fps = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
                _fpsOverlay.FpsValue = _fps;
                _fpsOverlay.Update();
            }
        }

        public void Draw()
        {
            _desktop.Render();
        }


        private void OnResumeBtnPressed()
        {
            ANXYGame.Instance.SetGamePaused(false);
        }

        private void OnResetGameBtnPressed()
        {
            var player = EntitySystem.Instance.FindEntitiesByType<Player>()[0].GetComponent<Player>();
            player.Reset();
            var camera = EntitySystem.Instance.FindEntitiesByType<Camera>()[0].GetComponent<Camera>();
            camera.Reset();
            ANXYGame.Instance.SetGamePaused(false);
        }
        private void OnControlsBtnPressed()
        {
            _controlsMenu.LoadButtonLayout();
            _desktop.Root = _controlsMenu;
        }

        private void OnCreditsBtnPressed()
        {
            _desktop.Root = _credits;
        }

        private void OnGamePausedChanged(bool gamePaused)
        {
            if (gamePaused)
            {
                _desktop.Root = _pauseMenu;
                _desktop.Root.Visible = true;
            }
            else
            {
                _desktop.Root = _fpsOverlay;
                _desktop.Root.Visible = _showFps;
            }
        }

        private void OnReturnClicked()
        {
            PlayerInput.Instance.LoadUserSettings();
            _desktop.Root = _pauseMenu;
        }

        private void OnLoadDefaultsClicked()
        {
            PlayerInput.Instance.ResetToDefaults();
        }

        private void OnSaveChangesClicked()
        {
            PlayerInput.Instance.Save();
        }
        private void OnExitGamePressed()
        {
            Environment.Exit(0);
        }

        private void OnShowFpsKeyPressed()
        {
            if (ANXYGame.Instance.GamePaused)
            {
                return;
            }

            _showFps = !_showFps;
            _desktop.Root = _fpsOverlay;
            _desktop.Root.Visible = _showFps;
        }
    }
}

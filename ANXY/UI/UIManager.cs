﻿using ANXY.EntityComponent.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    /// <summary>
    ///    Manages the UI elements. This class is a singleton.
    /// </summary>
    internal class UIManager
    {
        // The different UI overlays or UI screens.
        private readonly Desktop _desktop;
        private readonly FpsOverlay _fpsOverlay;
        private readonly PauseMenu _pauseMenu;
        private readonly ControlsMenu _controlsMenu;
        private readonly Credits _credits;

        private bool _showFps = false;

        // Singleton Pattern.
        private static readonly Lazy<UIManager> lazy = new(() => new UIManager());
        public static UIManager Instance => lazy.Value;

        /// <summary>
        ///    Singleton Pattern: Private constructor to prevent instantiation.
        /// </summary>
        private UIManager()
        {
            // Pause Menu
            _pauseMenu = new PauseMenu();
            _pauseMenu.ResumePressed += OnResumeBtnPressed;
            _pauseMenu.ResetGamePressed += OnResetGameBtnPressed;
            _pauseMenu.ControlsPressed += OnControlsBtnPressed;
            _pauseMenu.CreditsPressed += OnCreditsBtnPressed;
            _pauseMenu.ExitGamePressed += OnExitGamePressed;

            // Controls Menu
            _controlsMenu = new ControlsMenu();
            _controlsMenu.ReturnPressed += OnReturnClicked;
            _controlsMenu.LoadDefaultsPressed += OnLoadDefaultsClicked;
            _controlsMenu.SaveChangesPressed += OnSaveChangesClicked;

            // FPS Overlay
            _fpsOverlay = new FpsOverlay();

            // Credits
            _credits = new Credits();
            _credits.ReturnPressed += OnReturnClicked;

            // Desktop is the root of UI
            _desktop = new Desktop();
            _desktop.Root = _pauseMenu;
            _desktop.Root.Visible = false;

            // Event handlers
            ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
            PlayerInput.Instance.ShowFpsKeyPressed += OnShowFpsKeyPressed;
        }

        /// <summary>
        ///    Calculates the current FPS rate and update the FPS overlay.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateFPS(GameTime gameTime)
        {
            if (_desktop.Root == _fpsOverlay && _desktop.Root.Visible)
            {
                _fpsOverlay.Update(gameTime);
            }
        }

        /// <summary>
        ///    Draws the UI. UI root needs to be set to the desired UI elements before calling this method.
        /// </summary>
        public void Draw()
        {
            _desktop.Render();
        }

        // Event handlers.
        /// <summary>
        ///     Resumes the game when the resume button is pressed.
        /// </summary>
        private void OnResumeBtnPressed()
        {
            ANXYGame.Instance.SetGamePaused(false);
        }

        /// <summary>
        ///     Restarts the game from the beginning.
        /// </summary>
        private void OnResetGameBtnPressed()
        {
            var player = EntitySystem.Instance.FindEntitiesByType<Player>()[0].GetComponent<Player>();
            player.Reset();
            var camera = EntitySystem.Instance.FindEntitiesByType<Camera>()[0].GetComponent<Camera>();
            camera.Reset();
            ANXYGame.Instance.SetGamePaused(false);
        }

        /// <summary>
        ///     Shows the controls menu.
        /// </summary>
        private void OnControlsBtnPressed()
        {
            _controlsMenu.LoadButtonLayout();
            _desktop.Root = _controlsMenu;
        }

        /// <summary>
        ///     Shows the credits of the game.
        /// </summary>
        private void OnCreditsBtnPressed()
        {
            _desktop.Root = _credits;
        }

        /// <summary>
        ///     Shows the pause menu on game pause.
        /// </summary>
        /// <param name="gamePaused"></param>
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

        /// <summary>
        ///     Resumes the game.
        /// </summary>
        private void OnReturnClicked()
        {
            _desktop.Root = _pauseMenu;
        }

        /// <summary>
        ///     Loads default keyboard input settings.
        /// </summary>
        private void OnLoadDefaultsClicked()
        {
            PlayerInput.Instance.ResetToDefaults();
        }

        /// <summary>
        ///     Saves the current keyboard input settings from the controls menu.
        /// </summary>
        private void OnSaveChangesClicked()
        {
            PlayerInput.Instance.Save();
        }

        /// <summary>
        ///     Stops the game and exits the application.
        /// </summary>
        private void OnExitGamePressed()
        {
            Environment.Exit(0);
        }

        /// <summary>
        ///     Toggles the FPS overlay.
        /// </summary>
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
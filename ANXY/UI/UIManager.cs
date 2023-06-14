using ANXY.ECS.Components;
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
        private readonly InGameOverlay _inGameOverlay;
        private readonly PauseMenu _pauseMenu;
        private readonly ControlsMenu _controlsMenu;
        private readonly Credits _credits;

        private bool _showFps = false;
        public bool _showWelcomeAndTutorial { get; private set; } = true;

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

            //InGameOverlay
            _inGameOverlay = new InGameOverlay();

            // Credits
            _credits = new Credits();
            _credits.ReturnPressed += OnReturnClicked;


            // Desktop is the root of UI
            _desktop = new Desktop();
            _desktop.Root = _inGameOverlay;
            _desktop.Root.Visible = true;

            // Event handlers
            ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
            PlayerInput.Instance.ShowFpsKeyPressed += OnShowFpsKeyPressed;
            PlayerInput.Instance.MovementKeyPressed += OnStartMoving;
        }

        public void Update(GameTime gameTime)
        {
            UpdateFPS(gameTime);
        }

        /// <summary>
        ///    Calculates the current FPS rate and update the FPS overlay.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateFPS(GameTime gameTime)
        {
            if (_desktop.Root == _inGameOverlay)
            {
                _inGameOverlay.Update(gameTime);
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
            var player = (Player)SystemManager.Instance.FindSystemByType<Player>().GetComponent();
            //var player = EntityManager.Instance.FindEntitiesByType<Player>()[0].GetComponent<Player>();
            player.Reset();
            var camera = (Camera)SystemManager.Instance.FindSystemByType<Camera>().GetComponent();
            //var camera = EntityManager.Instance.FindEntitiesByType<Camera>()[0].GetComponent<Camera>();
            camera.Reset();

            _inGameOverlay.Reset();
            _showWelcomeAndTutorial = true;
            PlayerInput.Instance.MovementKeyPressed += OnStartMoving;
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
            }
            else
            {
                _desktop.Root = _inGameOverlay;
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
            _inGameOverlay.ShowFps(_showFps);
            _inGameOverlay.ResetFpsUI();
        }

        private void OnStartMoving()
        {
            _showWelcomeAndTutorial = false;
            _inGameOverlay.ShowWelcomeAndTutorial(_showWelcomeAndTutorial);

            PlayerInput.Instance.MovementKeyPressed -= OnStartMoving;
        }
    }
}

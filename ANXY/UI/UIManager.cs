using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.IO;
using System.Text;

namespace ANXY.UI
{
    internal class UIManager
    {
        public event Action PauseToggled;

        private PauseMenu _pauseMenu;
        private ControlsMenu _controlsMenu;
        private FpsOverlay _fpsOverlay;
        private Credits _credits;
        private float _fps;
        private Desktop _desktop;
        private double startTrackingTime = double.MinValue;
        private bool writeFpsValueFile = false;
        StringBuilder fpsStringValuesBuilder;

        private LastUiState _lastUiState = LastUiState.PauseMenu;

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
            _pauseMenu.ControlsPressed += OnControlsBtnPressed;
            _pauseMenu.CreditsPressed += OnCreditsBtnPressed;
            _pauseMenu.ExitGamePressed += OnExitGamePressed;

            _controlsMenu = new ControlsMenu();
            _controlsMenu.ReturnPressed += OnReturnClicked;
            _controlsMenu.LoadDefaultsPressed += OnLoadDefaultsClicked;
            _controlsMenu.SaveChangesPressed += OnSaveChangesClicked;

            _desktop.Root = _pauseMenu;
            _desktop.Root.Visible = false;
            PlayerInputController.Instance.GamePausedChanged += OnGamePausedChanged;
            PlayerInputController.Instance.ShowFpsKeyPressed += OnShowFpsKeyPressed;

            _fpsOverlay = new FpsOverlay();

            _credits = new Credits();
            _credits.ReturnPressed += OnReturnClicked;

            fpsStringValuesBuilder = new StringBuilder();
            OnShowFpsKeyPressed();
        }

        public void UpdateFPS(GameTime gameTime)
        {
            if (_desktop.Root == _fpsOverlay && _desktop.Root.Visible)
            {
                writeFpsValueFile = true;
                var totalSeconds = gameTime.TotalGameTime.TotalSeconds;
                if (startTrackingTime == double.MinValue)
                {
                    startTrackingTime = totalSeconds;
                }
                _fps =  1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
                _fpsOverlay.FpsValue = _fps;
                _fpsOverlay.Update();

                var time = totalSeconds - startTrackingTime;
                var roundedTime = Math.Round(time / (1 / 3.0)) * (1 / 3.0);
                fpsStringValuesBuilder.AppendLine(roundedTime.ToString() + "," + time.ToString() + "," + _fps.ToString());
            }
        }

        public void writeFpsFile()
        {
            if (writeFpsValueFile)
            {
                writeFpsValueFile = false;
                //write fps value file
                var FpsValuesCsvPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                FpsValuesCsvPath = Path.Combine(FpsValuesCsvPath, "ANXY");
                FpsValuesCsvPath = Path.Combine(FpsValuesCsvPath, "FpsValues.csv");
                File.WriteAllText(FpsValuesCsvPath, "rounded Time,Time,FPS\n" + fpsStringValuesBuilder.ToString());
            }
        }

        public void Draw()
        {
            _desktop.Render();
        }

        private void SwitchUiViews()
        {
            if (_lastUiState == LastUiState.FpsOverlay && _desktop.Root == _fpsOverlay)
            {
                _desktop.Root = _pauseMenu;
                _desktop.Root.Visible = true;
            }
            else if (_lastUiState == LastUiState.FpsOverlay && _desktop.Root != _fpsOverlay)
            {
                _desktop.Root = _fpsOverlay;
                _desktop.Root.Visible = true;
            }
            else
            {
                _desktop.Root = _pauseMenu;
                _desktop.Root.Visible = !_desktop.Root.Visible;
            }
        }

        public void OnResumeBtnPressed()
        {
            SwitchUiViews();
            PauseToggled?.Invoke();
        }

        public void OnControlsBtnPressed()
        {
            _controlsMenu.LoadButtonLayout();
            _desktop.Root = _controlsMenu;
        }

        public void OnCreditsBtnPressed()
        {
            _desktop.Root = _credits;
        }

        public void OnGamePausedChanged(bool gamePaused)
        {
            SwitchUiViews();
        }

        public void OnReturnClicked()
        {
            PlayerInputController.Instance.LoadUserSettings();
            _desktop.Root = _pauseMenu;
        }

        public void OnLoadDefaultsClicked()
        {
            PlayerInputController.Instance.ResetToDefaults();
        }

        public void OnSaveChangesClicked()
        {
            PlayerInputController.Instance.Save();
        }
        private void OnExitGamePressed()
        {
            writeFpsFile();
            Environment.Exit(0);
        }

        private void OnShowFpsKeyPressed()
        {
            if (_lastUiState == LastUiState.FpsOverlay)
            {
                _desktop.Root = _pauseMenu;
                _lastUiState = LastUiState.PauseMenu;
            }
            else if (!_desktop.Root.Visible)
            {
                _desktop.Root = _fpsOverlay;
                _lastUiState = LastUiState.FpsOverlay;
            }
        }
    }

    public enum LastUiState
    {
        PauseMenu,
        FpsOverlay
    }
}

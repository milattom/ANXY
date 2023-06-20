using ANXY.ECS.Components;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ANXY.UI
{
    internal class InGameOverlay : Grid
    {
        private float _fpsValue = -1;
        private float _maxFpsValue = float.MinValue;
        private float _minFpsValue = float.MaxValue;
        private float _lastFpsTextUpdate = 0.0f;
        private float _lastMinMaxFpsTextUpdate = 0.0f;
        private readonly NumberFormatInfo _nfi;
        private readonly Player _player;

        //UI Elements
        private Label _lblCurrentFps;
        private Label _lblMaxFps;
        private Label _lblMinFps;
        private VerticalStackPanel _uiFPS;
        private Label _lblDebugPlayerLocation;
        private Label lblDebugPlayerMidair;
        private Label lblDebugToggleHitboxesControls;
        private VerticalStackPanel uiDebug;
        private float _lastDebugTextUpdate = 0.0f;
        private readonly float _debugTextUpdateTime = 1 / 3.0f;
        private VerticalStackPanel _uiWelcomeAndTutorial;
        private Label _lblMoveTutorial;
        private Label _lblJumpTutorial;
        private Label _lblMenuTutorial;
        private Label _lblFastestTime;
        private VerticalStackPanel _uiFastestTime;

        //StopWatch elements
        private double _stopWatchTime;
        private Label _lblStopWatch;
        private readonly double _currentFpsRefreshTime = 1 / 3.0;
        private readonly double _minMaxFpsRefreshTime = 2;

        private readonly StringBuilder _stopWatchStringBuilder;

        internal InGameOverlay()
        {
            BuildUI();
            _stopWatchStringBuilder = new StringBuilder();
            _nfi = (NumberFormatInfo)
            CultureInfo.InvariantCulture.NumberFormat.Clone();
            _nfi.NumberGroupSeparator = "'";
            _player = (Player)SystemManager.Instance.FindSystemByType<Player>().GetFirstComponent();
        }

        private void BuildUI()
        {
            //FPS Overlay
            _lblCurrentFps = new Label
            {
                Text = "Current FPS:"
            };
            _lblMinFps = new Label
            {
                Text = "Min:"
            };
            _lblMaxFps = new Label
            {
                Text = "Max:"
            };
            var lblFpsRefreshExplanation = new Label
            {
                Text = "FPS refreshing every " + string.Format("{0:0.00}", _currentFpsRefreshTime) + "s",
                Padding = new Thickness(0, 7, 0, 0)
            };
            var lblMinMaxFpsRefreshExplanation = new Label
            {
                Text = "Min/Max refreshing every " + string.Format("{0:0}", _minMaxFpsRefreshTime) + "s"
            };
            _uiFPS = new VerticalStackPanel
            {
                Spacing = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Padding = new Thickness(5),
                Background = new SolidBrush("#000000DD")
            };
            _uiFPS.Widgets.Add(_lblCurrentFps);
            _uiFPS.Widgets.Add(_lblMinFps);
            _uiFPS.Widgets.Add(_lblMaxFps);
            _uiFPS.Widgets.Add(lblFpsRefreshExplanation);
            _uiFPS.Widgets.Add(lblMinMaxFpsRefreshExplanation);
            _uiFPS.Visible = false;

            //Debug Overlay
            var lblDebugTitle = new Label
            {
                Text = "Debuggin Mode",
                Padding = new Thickness(0, 0, 0, 7),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            _lblDebugPlayerLocation = new Label
            {
                Text = "X/Y: 12.34 / 56.78",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            lblDebugPlayerMidair = new Label
            {
                Text = "Midair: false",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            lblDebugToggleHitboxesControls = new Label
            {
                Text = "Spawn new Players with " + PlayerInput.Instance.InputSettings.Debug.SpawnNewPlayer,
                Padding = new Thickness(0, 7, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            uiDebug = new VerticalStackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Padding = new Thickness(5),
                GridColumn = 1,
                Background = new SolidBrush("#FF0000DD")
            };
            uiDebug.Widgets.Add(lblDebugTitle);
            uiDebug.Widgets.Add(_lblDebugPlayerLocation);
            uiDebug.Widgets.Add(lblDebugPlayerMidair);
            uiDebug.Widgets.Add(lblDebugToggleHitboxesControls);
            uiDebug.Visible = false;

            //StopWatch Overlay
            _lblStopWatch = new Label
            {
                Text = "0.00",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            var lblFastestTimeDescription = new Label
            {
                Text = "Fastest time:",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            _lblFastestTime = new Label
            {
                Text = "12:34:56.78",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            _uiFastestTime = new VerticalStackPanel
            {
                Spacing = 2,
                Padding = new Thickness(5)
            };
            _uiFastestTime.Widgets.Add(lblFastestTimeDescription);
            _uiFastestTime.Widgets.Add(_lblFastestTime);
            _uiFastestTime.Visible = false;
            var uiStopWatch = new VerticalStackPanel
            {
                Spacing = 4,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Padding = new Thickness(5),
                GridColumn = 2,
                Background = new SolidBrush("#000000AA")
            };
            uiStopWatch.Widgets.Add(_lblStopWatch);
            uiStopWatch.Widgets.Add(_uiFastestTime);

            //Welcome and Tutorial Overlay
            var lblAnxy = new Label
            {
                Text = "ANXY",
                Padding = new Thickness(0, 0, 0, 4),
                HorizontalAlignment = HorizontalAlignment.Center,
                Scale = new Vector2(2, 2)
            };
            var lblWelcome = new Label
            {
                Text = "Welcome to",
                Padding = new Thickness(0, 0, 0, 3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            var lblFiller = new Label();
            _lblMoveTutorial = new Label
            {
                Text = "Move with " + PlayerInput.Instance.InputSettings.Movement.Left + " & " + PlayerInput.Instance.InputSettings.Movement.Right,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            _lblJumpTutorial = new Label
            {
                Text = "Jump with " + PlayerInput.Instance.InputSettings.Movement.Jump,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            _lblMenuTutorial = new Label
            {
                Text = "Open the Menu with " + PlayerInput.Instance.InputSettings.General.Menu,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            _uiWelcomeAndTutorial = new VerticalStackPanel
            {
                Spacing = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(10, 10, 10, 10),
                GridColumn = 1,
                GridRow = 1,
                Background = new SolidBrush("#0000FFAA")
            };
            _uiWelcomeAndTutorial.Widgets.Add(lblWelcome);
            _uiWelcomeAndTutorial.Widgets.Add(lblAnxy);
            _uiWelcomeAndTutorial.Widgets.Add(lblFiller);
            _uiWelcomeAndTutorial.Widgets.Add(_lblMoveTutorial);
            _uiWelcomeAndTutorial.Widgets.Add(_lblJumpTutorial);
            _uiWelcomeAndTutorial.Widgets.Add(_lblMenuTutorial);

            //Anxiety Meter Overlay
            var lblAnxietyMeterDescription = new Label
            {
                Text = "Anxiety meter:"
            };
            var lblAnxietyMeter = new Label
            {
                Text = "#########################--------------------------------------------------------" +
    "-------------------"
            };
            var uiAnxietyMeter = new HorizontalStackPanel
            {
                Spacing = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Padding = new Thickness(5),
                GridRow = 2,
                GridColumnSpan = 3,
                Background = new SolidBrush("#000000AA")
            };
            uiAnxietyMeter.Widgets.Add(lblAnxietyMeterDescription);
            uiAnxietyMeter.Widgets.Add(lblAnxietyMeter);

            //Assemble the In Game Overlay and set Properties
            Padding = new Thickness(15, 15, 15, 5);
            Widgets.Add(_uiFPS);
            Widgets.Add(uiDebug);
            Widgets.Add(uiStopWatch);
            Widgets.Add(_uiWelcomeAndTutorial);
            Widgets.Add(uiAnxietyMeter);
        }

        public void Update(GameTime gameTime)
        {
            UpdateFPS(gameTime);
            UpdateStopWatch(gameTime);
            UpdateDebug(gameTime);
        }

        private void UpdateFPS(GameTime gameTime)
        {
            if (ANXYGame.Instance.GamePaused ||!_uiFPS.Visible)
            {
                return;
            }

            var gameTimeElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _lastFpsTextUpdate += gameTimeElapsedSeconds;
            _lastMinMaxFpsTextUpdate += gameTimeElapsedSeconds;

            _fpsValue = 1.0f / gameTimeElapsedSeconds;

            if (_fpsValue > _maxFpsValue)
            {
                _maxFpsValue = _fpsValue;
            }
            else if (_fpsValue < _minFpsValue)
            {
                _minFpsValue = _fpsValue;
            }

            if (_lastFpsTextUpdate >= _currentFpsRefreshTime)
            {
                _lblCurrentFps.Text = "Current FPS: " + _fpsValue.ToString("n", _nfi);
                _lastFpsTextUpdate = 0;
            }

            if (_lastMinMaxFpsTextUpdate >= _minMaxFpsRefreshTime)
            {
                _lblMinFps.Text = "Min: " + _minFpsValue.ToString("n", _nfi);
                _lblMaxFps.Text = "Max: " + _maxFpsValue.ToString("n", _nfi);
                _maxFpsValue = float.MinValue;
                _minFpsValue = float.MaxValue;
                _lastMinMaxFpsTextUpdate = 0;
            }
        }

        public void UpdateStopWatch(GameTime gameTime)
        {
            if (ANXYGame.Instance.GamePaused || _uiWelcomeAndTutorial.Visible)
            {
                return;
            }

            var gameTimeElapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            _stopWatchTime += gameTimeElapsedSeconds;

            TimeSpan timeSpan = TimeSpan.FromSeconds(_stopWatchTime);

            if (timeSpan.Hours > 0)
            {
                _stopWatchStringBuilder.Append(string.Format("{0:00}:", timeSpan.Hours));
            }

            if (timeSpan.Minutes > 0 || timeSpan.Hours > 0)
            {
                _stopWatchStringBuilder.Append(string.Format("{0:00}:", timeSpan.Minutes));
                _stopWatchStringBuilder.Append(string.Format("{0:00}.{1:00}", timeSpan.Seconds, timeSpan.Milliseconds / 10));
            }
            else
            {
                _stopWatchStringBuilder.Append(string.Format("{0:0}.{1:00}", timeSpan.Seconds, timeSpan.Milliseconds / 10));
            }


            _lblStopWatch.Text = _stopWatchStringBuilder.ToString();
            _stopWatchStringBuilder.Clear();
        }

        private void UpdateDebug(GameTime gameTime)
        {
            var gameTimeElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _lastDebugTextUpdate += gameTimeElapsedSeconds;

            lblDebugToggleHitboxesControls.Text = "Spawn new Players with " + PlayerInput.Instance.InputSettings.Debug.SpawnNewPlayer;

            if (ANXYGame.Instance.GamePaused || !uiDebug.Visible || _lastDebugTextUpdate < _debugTextUpdateTime)
                return;
            _lastDebugTextUpdate = 0.0f;
            _lblDebugPlayerLocation.Text = "X/Y: " + _player.Entity.Position.X.ToString("n", _nfi) + " / " + _player.Entity.Position.Y.ToString("n", _nfi);
            lblDebugPlayerMidair.Text = "Midair: " + _player.MidAir.ToString();
        }

        public void Reset()
        {
            ResetStopWatch();
            ResetTutorial();
            ResetFpsUI();
        }

        private void ResetStopWatch()
        {
            _stopWatchTime = 0;
            _lblStopWatch.Text = string.Format("{0:0.00}", _stopWatchTime);
        }
        private void ResetTutorial()
        {
            RewriteTutorialText();
            _uiWelcomeAndTutorial.Visible = true;
        }
        public void RewriteTutorialText()
        {
            _lblMoveTutorial.Text = "Move with " + PlayerInput.Instance.InputSettings.Movement.Left + " & " + PlayerInput.Instance.InputSettings.Movement.Right;
            _lblJumpTutorial.Text = "Jump with " + PlayerInput.Instance.InputSettings.Movement.Jump;
            _lblMenuTutorial.Text = "Open the Menu with " + PlayerInput.Instance.InputSettings.General.Menu;
        }
        public void ResetFpsUI()
        {
            _lblCurrentFps.Text = "Current FPS:";
            _lblMinFps.Text = "Min: ";
            _lblMaxFps.Text = "Max: ";
            _fpsValue = -1;
            _maxFpsValue = float.MinValue;
            _minFpsValue = float.MaxValue;
            _lastFpsTextUpdate = 0.0f;
            _lastMinMaxFpsTextUpdate = 0.0f;
        }

        public void ShowFps(bool show)
        {
            _uiFPS.Visible = show;
        }

        public void ShowDebug(bool show)
        {
            uiDebug.Visible = show;
        }

        public void ShowWelcomeAndTutorial(bool show)
        {
            _uiWelcomeAndTutorial.Visible = show;
        }
    }
}

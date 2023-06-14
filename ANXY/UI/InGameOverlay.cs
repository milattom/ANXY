using ANXY.Start;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Globalization;
using System.Text;

namespace ANXY.UI
{
    internal class InGameOverlay : Grid
    {
        private float FpsValue = -1;
        private float MaxFpsValue = float.MinValue;
        private float MinFpsValue = float.MaxValue;
        private float lastFpsTextUpdate = 0.0f;
        private float lastMinMaxFpsTextUpdate = 0.0f;
        private readonly NumberFormatInfo nfi;


        //UI Elements
        private Label lblCurrentFps;
        private Label lblMaxFps;
        private Label lblMinFps;
        private VerticalStackPanel uiFPS;
        private VerticalStackPanel uiDebug;
        private VerticalStackPanel uiWelcomeAndTutorial;
        private Label lblFastestTime;
        private VerticalStackPanel uiFastestTime;

        //StopWatch elements
        private double StopWatchTime;
        private Label _lblStopWatch;
        private readonly double _currentFpsRefreshTime = 1 / 3.0;
        private readonly double _minMaxFpsRefreshTime = 2;

        private readonly StringBuilder _stopWatchStringBuilder;

        internal InGameOverlay()
        {
            BuildUI();
            _stopWatchStringBuilder = new StringBuilder();
            nfi = (NumberFormatInfo)
            CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = "'";
        }

        private void BuildUI()
        {
            //FPS Overlay
            lblCurrentFps = new Label
            {
                Text = "Current FPS:"
            };
            lblMinFps = new Label
            {
                Text = "Min:"
            };
            lblMaxFps = new Label
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
            uiFPS = new VerticalStackPanel
            {
                Spacing = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Padding = new Thickness(5),
                Background = new SolidBrush("#000000DD")
            };
            uiFPS.Widgets.Add(lblCurrentFps);
            uiFPS.Widgets.Add(lblMinFps);
            uiFPS.Widgets.Add(lblMaxFps);
            uiFPS.Widgets.Add(lblFpsRefreshExplanation);
            uiFPS.Widgets.Add(lblMinMaxFpsRefreshExplanation);
            uiFPS.Visible = false;

            //Debug Overlay
            var lblDebugTitle = new Label
            {
                Text = "Debuggin Mode",
                Padding = new Thickness(0, 0, 0, 7),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblDebugPlayerLocation = new Label
            {
                Text = "XY: 12.34 / 56.78",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblDebugPlayerFacingDirection = new Label
            {
                Text = "Facing: Right",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblDebugPlayerMidair = new Label
            {
                Text = "Midair: false",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblDebugToggleHitboxesControls = new Label
            {
                Text = "Toggle Hitboxes with F6",
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
            uiDebug.Widgets.Add(lblDebugPlayerLocation);
            uiDebug.Widgets.Add(lblDebugPlayerFacingDirection);
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
            lblFastestTime = new Label
            {
                Text = "12:34:56.78",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            uiFastestTime = new VerticalStackPanel
            {
                Spacing = 2,
                Padding = new Thickness(5)
            };
            uiFastestTime.Widgets.Add(lblFastestTimeDescription);
            uiFastestTime.Widgets.Add(lblFastestTime);
            uiFastestTime.Visible = false;
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
            uiStopWatch.Widgets.Add(uiFastestTime);

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
            var lblMoveTutorial = new Label
            {
                Text = "Move with A & D",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblJumpTutorial = new Label
            {
                Text = "Jump with Space",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var lblMenuTutorial = new Label
            {
                Text = "Open the Menu with ESC",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            uiWelcomeAndTutorial = new VerticalStackPanel
            {
                Spacing = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(10, 10, 10, 10),
                GridColumn = 1,
                GridRow = 1,
                Background = new SolidBrush("#0000FFAA")
            };
            uiWelcomeAndTutorial.Widgets.Add(lblWelcome);
            uiWelcomeAndTutorial.Widgets.Add(lblAnxy);
            uiWelcomeAndTutorial.Widgets.Add(lblFiller);
            uiWelcomeAndTutorial.Widgets.Add(lblMoveTutorial);
            uiWelcomeAndTutorial.Widgets.Add(lblJumpTutorial);
            uiWelcomeAndTutorial.Widgets.Add(lblMenuTutorial);

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
            Widgets.Add(uiFPS);
            Widgets.Add(uiDebug);
            Widgets.Add(uiStopWatch);
            Widgets.Add(uiWelcomeAndTutorial);
            Widgets.Add(uiAnxietyMeter);
        }

        public void Update(GameTime gameTime)
        {
            UpdateFPS(gameTime);
            UpdateStopWatch(gameTime);
        }

        private void UpdateFPS(GameTime gameTime)
        {
            if (!uiFPS.Visible)
            {
                return;
            }

            var gameTimeElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            lastFpsTextUpdate += gameTimeElapsedSeconds;
            lastMinMaxFpsTextUpdate += gameTimeElapsedSeconds;

            FpsValue = 1.0f / gameTimeElapsedSeconds;

            if (FpsValue > MaxFpsValue)
            {
                MaxFpsValue = FpsValue;
            }
            else if (FpsValue < MinFpsValue)
            {
                MinFpsValue = FpsValue;
            }

            if (lastFpsTextUpdate >= _currentFpsRefreshTime)
            {
                lblCurrentFps.Text = "Current FPS: " + FpsValue.ToString("n", nfi);
                lastFpsTextUpdate = 0;
            }

            if (lastMinMaxFpsTextUpdate >= _minMaxFpsRefreshTime)
            {
                lblMinFps.Text = "Min: " + MinFpsValue.ToString("n", nfi);
                lblMaxFps.Text = "Max: " + MaxFpsValue.ToString("n", nfi);
                MaxFpsValue = float.MinValue;
                MinFpsValue = float.MaxValue;
                lastMinMaxFpsTextUpdate = 0;
            }
        }

        public void UpdateStopWatch(GameTime gameTime)
        {
            if (ANXYGame.Instance.GamePaused || uiWelcomeAndTutorial.Visible)
            {
                return;
            }

            var gameTimeElapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            StopWatchTime += gameTimeElapsedSeconds;

            TimeSpan timeSpan = TimeSpan.FromSeconds(StopWatchTime);

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

        public void Reset()
        {
            ResetStopWatch();
            uiWelcomeAndTutorial.Visible = true;
            ResetFpsUI();
        }

        private void ResetStopWatch()
        {
            StopWatchTime = 0;
            _lblStopWatch.Text = string.Format("{0:0.00}", StopWatchTime);
        }

        public void ResetFpsUI()
        {
            lblCurrentFps.Text = "Current FPS:";
            lblMinFps.Text = "Min: ";
            lblMaxFps.Text = "Max: ";
            FpsValue = -1;
            MaxFpsValue = float.MinValue;
            MinFpsValue = float.MaxValue;
            lastFpsTextUpdate = 0.0f;
            lastMinMaxFpsTextUpdate = 0.0f;
        }

        public void ShowFps(bool show)
        {
            uiFPS.Visible = show;
        }

        public void ShowWelcomeAndTutorial(bool show)
        {
            uiWelcomeAndTutorial.Visible = show;
        }
    }
}

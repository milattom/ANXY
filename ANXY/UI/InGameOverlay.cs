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
        public float FpsValue = -1;
        public float MaxFpsValue = float.MinValue;
        public float MinFpsValue = float.MaxValue;
        private float lastFpsTextUpdate = 0.0f;
        private float lastMinMaxFpsTextUpdate = 0.0f;
        private readonly NumberFormatInfo nfi;


        //UI Elements
        Label lblCurrentFps;
        Label lblMaxFps;
        Label lblMinFps;
        VerticalStackPanel uiFPS;
        VerticalStackPanel uiDebug;
        VerticalStackPanel uiWelcomeAndTutorial;
        Label lblFastestTime;
        VerticalStackPanel uiFastestTime;

        //StopWatch elements
        private double StopWatchTime;
        private Label _lblStopWatch;
        private double _currentFpsRefreshTime = 1 / 3.0;
        private double _minMaxFpsRefreshTime = 2;

        private StringBuilder _stopWatchStringBuilder;

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
            lblCurrentFps = new Label();
            lblCurrentFps.Text = "Current FPS:";

            lblMinFps = new Label();
            lblMinFps.Text = "Min:";

            lblMaxFps = new Label();
            lblMaxFps.Text = "Max:";

            var lblFpsRefreshExplanation = new Label();
            lblFpsRefreshExplanation.Text = "FPS refreshing every " + string.Format("{0:0.00}", _currentFpsRefreshTime) + "s";
            lblFpsRefreshExplanation.Padding = new Thickness(0, 7, 0, 0);

            var lblMinMaxFpsRefreshExplanation = new Label();
            lblMinMaxFpsRefreshExplanation.Text = "Min/Max refreshing every " + string.Format("{0:0}", _minMaxFpsRefreshTime) + "s";

            uiFPS = new VerticalStackPanel();
            uiFPS.Spacing = 2;
            uiFPS.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Left;
            uiFPS.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Top;
            uiFPS.Padding = new Thickness(5);
            uiFPS.Background = new SolidBrush("#000000DD");
            uiFPS.Widgets.Add(lblCurrentFps);
            uiFPS.Widgets.Add(lblMinFps);
            uiFPS.Widgets.Add(lblMaxFps);
            uiFPS.Widgets.Add(lblFpsRefreshExplanation);
            uiFPS.Widgets.Add(lblMinMaxFpsRefreshExplanation);
            uiFPS.Visible = false;

            var lblDebugTitle = new Label();
            lblDebugTitle.Text = "Debuggin Mode";
            lblDebugTitle.Padding = new Thickness(0, 0, 0, 7);
            lblDebugTitle.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var lblDebugPlayerLocation = new Label();
            lblDebugPlayerLocation.Text = "XY: 12.34 / 56.78";
            lblDebugPlayerLocation.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var lblDebugPlayerFacingDirection = new Label();
            lblDebugPlayerFacingDirection.Text = "Facing: Right";
            lblDebugPlayerFacingDirection.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var lblDebugPlayerMidair = new Label();
            lblDebugPlayerMidair.Text = "Midair: false";
            lblDebugPlayerMidair.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var lblDebugToggleHitboxesControls = new Label();
            lblDebugToggleHitboxesControls.Text = "Toggle Hitboxes with F6";
            lblDebugToggleHitboxesControls.Padding = new Thickness(0, 7, 0, 0);
            lblDebugToggleHitboxesControls.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            uiDebug = new VerticalStackPanel();
            uiDebug.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            uiDebug.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Top;
            uiDebug.Padding = new Thickness(5);
            uiDebug.GridColumn = 1;
            uiDebug.Background = new SolidBrush("#FF0000DD");
            uiDebug.Widgets.Add(lblDebugTitle);
            uiDebug.Widgets.Add(lblDebugPlayerLocation);
            uiDebug.Widgets.Add(lblDebugPlayerFacingDirection);
            uiDebug.Widgets.Add(lblDebugPlayerMidair);
            uiDebug.Widgets.Add(lblDebugToggleHitboxesControls);
            uiDebug.Visible = false;

            _lblStopWatch = new Label();
            _lblStopWatch.Text = "0.00";
            _lblStopWatch.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;

            var lblFastestTimeDescription = new Label();
            lblFastestTimeDescription.Text = "Fastest time:";
            lblFastestTimeDescription.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;

            lblFastestTime = new Label();
            lblFastestTime.Text = "12:34:56.78";
            lblFastestTime.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;

            uiFastestTime = new VerticalStackPanel();
            uiFastestTime.Spacing = 2;
            uiFastestTime.Padding = new Thickness(5);
            uiFastestTime.Widgets.Add(lblFastestTimeDescription);
            uiFastestTime.Widgets.Add(lblFastestTime);
            uiFastestTime.Visible = false;

            var uiStopWatch = new VerticalStackPanel();
            uiStopWatch.Spacing = 4;
            uiStopWatch.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;
            uiStopWatch.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Top;
            uiStopWatch.Padding = new Thickness(5);
            uiStopWatch.GridColumn = 2;
            uiStopWatch.Background = new SolidBrush("#000000AA");
            uiStopWatch.Widgets.Add(_lblStopWatch);
            uiStopWatch.Widgets.Add(uiFastestTime);

            var label15 = new Label();
            label15.Text = "ANXY";
            label15.Padding = new Thickness(0, 0, 0, 4);
            label15.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            label15.Scale = new Vector2(2, 2);

            var label16 = new Label();
            label16.Text = "Welcome to";
            label16.Padding = new Thickness(0, 0, 0, 3);
            label16.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            label16.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;

            var label17 = new Label();

            var label18 = new Label();
            label18.Text = "Move with A & D";
            label18.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var label19 = new Label();
            label19.Text = "Jump with Space";
            label19.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var label20 = new Label();
            label20.Text = "Open the Menu with ESC";
            label20.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            uiWelcomeAndTutorial = new VerticalStackPanel();
            uiWelcomeAndTutorial.Spacing = 2;
            uiWelcomeAndTutorial.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            uiWelcomeAndTutorial.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            uiWelcomeAndTutorial.Padding = new Thickness(10, 10, 10, 10);
            uiWelcomeAndTutorial.GridColumn = 1;
            uiWelcomeAndTutorial.GridRow = 1;
            uiWelcomeAndTutorial.Background = new SolidBrush("#0000FFAA");
            uiWelcomeAndTutorial.Widgets.Add(label16);
            uiWelcomeAndTutorial.Widgets.Add(label15);
            uiWelcomeAndTutorial.Widgets.Add(label17);
            uiWelcomeAndTutorial.Widgets.Add(label18);
            uiWelcomeAndTutorial.Widgets.Add(label19);
            uiWelcomeAndTutorial.Widgets.Add(label20);

            var label21 = new Label();
            label21.Text = "Anxiety meter:";

            var lblAnxietyMeter = new Label();
            lblAnxietyMeter.Text = "#########################--------------------------------------------------------" +
    "-------------------";

            var uiAnxietyMeter = new HorizontalStackPanel();
            uiAnxietyMeter.Spacing = 2;
            uiAnxietyMeter.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            uiAnxietyMeter.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Bottom;
            uiAnxietyMeter.Padding = new Thickness(5);
            uiAnxietyMeter.GridRow = 2;
            uiAnxietyMeter.GridColumnSpan = 3;
            uiAnxietyMeter.Background = new SolidBrush("#000000AA");
            uiAnxietyMeter.Widgets.Add(label21);
            uiAnxietyMeter.Widgets.Add(lblAnxietyMeter);


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

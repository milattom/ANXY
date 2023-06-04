using ANXY.Start;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using System;
using System.Text;

namespace ANXY.UI
{
    internal class GameOverlay : VerticalStackPanel
    {
        private const string FPS_TEXT = "FPS: ";
        private const string CURRENT_MAX_FPS = "max: ";
        private const string CURRENT_MIN_FPS = "min: ";
        public float FpsValue = -1;
        public float MaxFpsValue = float.MinValue;
        public float MinFpsValue = float.MaxValue;
        private readonly Label _lblCurrentFps;
        private readonly Label _lblMinMaxTimeStepExplanation;
        private readonly Label _lblFpsTimeStepExplanation;
        private readonly Label _lblMaxFps;
        private readonly Label _lblMinFps;
        private float lastFpsTextUpdate = 0.0f;
        private float lastMinMaxFpsTextUpdate = 0.0f;

        //StopWatch elements
        private double StopWatchTime;
        private Label _lblStopWatch;

        private StringBuilder _stopWatchStringBuilder;

        public GameOverlay()
        {
            _stopWatchStringBuilder = new StringBuilder();

            _lblFpsTimeStepExplanation = new Label();
            _lblFpsTimeStepExplanation.Text = "fps refresh all 0.33s";
            _lblFpsTimeStepExplanation.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _lblFpsTimeStepExplanation.Id = "lblExplanation";

            _lblCurrentFps = new Label();
            _lblCurrentFps.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);
            _lblCurrentFps.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _lblCurrentFps.Id = "lblCurrentFps";

            _lblMinMaxTimeStepExplanation = new Label();
            _lblMinMaxTimeStepExplanation.Text = "min/max refresh all 2s";
            _lblMinMaxTimeStepExplanation.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _lblMinMaxTimeStepExplanation.Id = "lblExplanation";

            _lblMaxFps = new Label();
            _lblMaxFps.Text = CURRENT_MAX_FPS;
            _lblMaxFps.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _lblMaxFps.Id = "lblMaxFps";

            _lblMinFps = new Label();
            _lblMinFps.Text = CURRENT_MIN_FPS;
            _lblMinFps.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _lblMinFps.Id = "lblMinFps";

            //StopWatch
            _lblStopWatch = new Label();


            Padding = new Thickness(15);
            Spacing = 5;
            Widgets.Add(_lblStopWatch);
        }

        public void Update(GameTime gameTime)
        {
            UpdateStopWatch(gameTime);

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

            if (lastFpsTextUpdate >= 0.33)
            {
                _lblCurrentFps.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);
                lastFpsTextUpdate = 0;
            }

            if (lastMinMaxFpsTextUpdate >= 2)
            {
                _lblMaxFps.Text = CURRENT_MAX_FPS + string.Format("{0:0.00}", MaxFpsValue);
                _lblMinFps.Text = CURRENT_MIN_FPS + string.Format("{0:0.00}", MinFpsValue);
                MaxFpsValue = float.MinValue;
                MinFpsValue = float.MaxValue;
                lastMinMaxFpsTextUpdate = 0;
            }
        }


        public void UpdateStopWatch(GameTime gameTime)
        {
            if (ANXYGame.Instance.GamePaused)
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


            _lblStopWatch.Text = "Time: " + _stopWatchStringBuilder.ToString();
            _stopWatchStringBuilder.Clear();
        }

        public void ResetStopWatch()
        {
            StopWatchTime = 0;
        }

        public void ShowFps(bool show)
        {
            if (show)
            {
                Widgets.Add(_lblCurrentFps);
                Widgets.Add(_lblMaxFps);
                Widgets.Add(_lblMinFps);
                Widgets.Add(_lblFpsTimeStepExplanation);
                Widgets.Add(_lblMinMaxTimeStepExplanation);
            }
            else
            {
                Widgets.Remove(_lblCurrentFps);
                Widgets.Remove(_lblMaxFps);
                Widgets.Remove(_lblMinFps);
                Widgets.Remove(_lblFpsTimeStepExplanation);
                Widgets.Remove(_lblMinMaxTimeStepExplanation);
            }
        }
    }
}

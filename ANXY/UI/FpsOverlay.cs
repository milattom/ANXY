using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace ANXY.UI
{
    internal class FpsOverlay : VerticalStackPanel
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

        public FpsOverlay()
        {
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
            //_lblMinFps.SetStyle =
            _lblMinFps.Id = "lblMinFps";

            Padding = new Thickness(15);
            Spacing = 5;
            Widgets.Add(_lblCurrentFps);
            Widgets.Add(_lblMaxFps);
            Widgets.Add(_lblMinFps);
            Widgets.Add(_lblFpsTimeStepExplanation);
            Widgets.Add(_lblMinMaxTimeStepExplanation);
        }

        public void Update(GameTime gameTime)
        {
            float newMilliseconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            lastFpsTextUpdate += newMilliseconds;
            lastMinMaxFpsTextUpdate += newMilliseconds;

            FpsValue = 1.0f / newMilliseconds;
            
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
    }
}

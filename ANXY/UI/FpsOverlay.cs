using FontStashSharp.RichText;
using Myra.Graphics2D.UI;

namespace ANXY.UI
{
    internal class FpsOverlay : Panel
    {
        private const string FPS_TEXT = "FPS: ";
        public float FpsValue = 0.0f;
        private Label _fpsLabel;

        public FpsOverlay()
        {
            _fpsLabel = new Label();
            _fpsLabel.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);
            _fpsLabel.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _fpsLabel.Id = "txtFpsCoutner";

            Widgets.Add(_fpsLabel);
        }

        public void Update()
        {
            _fpsLabel.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);
        }
    }
}

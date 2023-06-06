using FontStashSharp.RichText;
using Myra.Graphics2D.UI;
using System.IO;
using System;
using System.Text;
using Microsoft.Xna.Framework;

namespace ANXY.UI
{
    internal class FpsOverlay : Panel
    {
        private const string FPS_TEXT = "FPS: ";
        public double FpsValue = 0;
        public double gameTime = 0;
        public double startTrackingTime = double.MinValue;
        private Label _fpsLabel;
        private bool writeFpsValueFile = false;
        StringBuilder fpsStringValuesBuilder;

        public FpsOverlay()
        {
            _fpsLabel = new Label();
            _fpsLabel.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);
            _fpsLabel.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
            _fpsLabel.Id = "txtFpsCoutner";

            Widgets.Add(_fpsLabel);
            fpsStringValuesBuilder = new StringBuilder();
        }

        public void Update()
        {
            _fpsLabel.Text = FPS_TEXT + string.Format("{0:0.00}", FpsValue);

            if (startTrackingTime == double.MinValue)
            {
                startTrackingTime = gameTime;
            }

            writeFpsValueFile = true;

            var time = gameTime - startTrackingTime;
            var roundedTime = Math.Round(time / (1/3.0)) * (1/3.0);
            fpsStringValuesBuilder.AppendLine(roundedTime.ToString() + "," + time.ToString() + "," + FpsValue.ToString());
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
    }
}

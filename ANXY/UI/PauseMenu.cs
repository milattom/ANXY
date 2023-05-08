using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    internal class PauseMenu : VerticalStackPanel
    {
        public event Action ResumePressed;
        public PauseMenu()
        {
            var textButton0 = new TextButton();
            textButton0.Text = "Resume Game";
            textButton0.Padding = new Thickness(10);
            textButton0.Click += OnResumeClicked;

            var textButton1 = new TextButton();
            textButton1.Text = "Reset/New Game";
            textButton1.Padding = new Thickness(10);

            var textButton2 = new TextButton();
            textButton2.Text = "Controls";
            textButton2.Padding = new Thickness(10);

            var textButton3 = new TextButton();
            textButton3.Text = "Credits";
            textButton3.Padding = new Thickness(10);

            var textButton4 = new TextButton();
            textButton4.Text = "Exit Game";
            textButton4.Padding = new Thickness(10);

            Spacing = 10;
            HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            Padding = new Thickness(15);
            Background = new SolidBrush("#FE3930FF");
            Widgets.Add(textButton0);
            Widgets.Add(textButton1);
            Widgets.Add(textButton2);
            Widgets.Add(textButton3);
            Widgets.Add(textButton4);
        }

        private void OnResumeClicked(object sender, EventArgs e)
        {
            ResumePressed?.Invoke();
        }
    }
}

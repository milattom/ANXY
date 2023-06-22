using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using System;
using SolidBrush = Myra.Graphics2D.Brushes.SolidBrush;

namespace ANXY.UI
{
    internal class PauseMenu : VerticalStackPanel
    {
        public event Action ResumePressed;
        public event Action ResetGamePressed;
        public event Action ControlsPressed;
        public event Action CreditsPressed;
        public event Action ExitGamePressed;
        public PauseMenu()
        {
            var lblTitle = new TextBox
            {
                Text = "PAUSE",
                TextColor = ColorStorage.CreateColor(255, 255, 255, 255),
                Readonly = true,
                TextVerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 50,
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                Scale = new Vector2(2, 2),
                Background = new SolidBrush("#00000000")
            };

            var btnResumeGame = new TextButton
            {
                Text = "Resume Game",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            btnResumeGame.Click += OnResumeClicked;


            var btnRestartGame = new TextButton
            {
                Text = "Restart Game",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            btnRestartGame.Click += OnResetGameClicked;

            var btnControls = new TextButton
            {
                Text = "Controls",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            btnControls.Click += OnControlsClicked;

            var btnCredits = new TextButton
            {
                Text = "Credits",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            btnCredits.Click += OnCreditsClicked;

            var btnExitGame = new TextButton
            {
                Text = "Exit Game",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            btnExitGame.Click += OnExitGameClicked;


            Spacing = 20;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            MinWidth = 500;
            Padding = new Thickness(50);
            Background = new SolidBrush("#0000FFAA");
            Widgets.Add(lblTitle);
            Widgets.Add(btnResumeGame);
            Widgets.Add(btnRestartGame);
            Widgets.Add(btnControls);
            Widgets.Add(btnCredits);
            Widgets.Add(btnExitGame);
        }

        private void OnExitGameClicked(object sender, EventArgs e)
        {
            ExitGamePressed?.Invoke();
        }

        private void OnCreditsClicked(object sender, EventArgs e)
        {
            CreditsPressed?.Invoke();
        }

        private void OnResetGameClicked(object sender, EventArgs e)
        {
            ResetGamePressed?.Invoke();
        }

        private void OnResumeClicked(object sender, EventArgs e)
        {
            ResumePressed?.Invoke();
        }

        private void OnControlsClicked(object sender, EventArgs e)
        {
            ControlsPressed?.Invoke();
        }
    }
}

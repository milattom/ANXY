﻿using Cyotek.Drawing.BitmapFont;
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
        public PauseMenu()
        {
            var font = new BitmapFont();
            font.FontSize = 50;
            font.Bold = true;
            font.FamilyName = "Arial";


            var textBox1 = new TextBox();
            textBox1.Text = "PAUSE";
            textBox1.TextColor = ColorStorage.CreateColor(255, 255, 255, 255);
            textBox1.Readonly = true;
            textBox1.TextVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            textBox1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            textBox1.Height = 50;
            textBox1.Padding = new Thickness(10);
            textBox1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            textBox1.Scale = new Vector2(2, 2);
            textBox1.Background = new SolidBrush("#00000000");

            var textButton1 = new TextButton();
            textButton1.Text = "Resume Game";
            textButton1.Padding = new Thickness(10);
            textButton1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
            textButton1.Click += OnResumeClicked;


            var textButton2 = new TextButton();
            textButton2.Text = "Reset / New Game";
            textButton2.Padding = new Thickness(10);
            textButton2.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;

            var textButton3 = new TextButton();
            textButton3.Text = "Controls";
            textButton3.Padding = new Thickness(10);
            textButton3.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;

            var textButton4 = new TextButton();
            textButton4.Text = "Credits";
            textButton4.Padding = new Thickness(10);
            textButton4.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;

            var textButton5 = new TextButton();
            textButton5.Text = "Exit Game";
            textButton5.Padding = new Thickness(10);
            textButton5.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;


            Spacing = 20;
            HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            MinWidth = 500;
            Padding = new Thickness(50);
            Background = new SolidBrush("#0000FF44");
            Widgets.Add(textBox1);
            Widgets.Add(textButton1);
            Widgets.Add(textButton2);
            Widgets.Add(textButton3);
            Widgets.Add(textButton4);
            Widgets.Add(textButton5);
        }

        private void OnResumeClicked(object sender, EventArgs e)
        {
            ResumePressed?.Invoke();
        }
    }
}

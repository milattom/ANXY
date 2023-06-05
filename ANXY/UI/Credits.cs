using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    internal class Credits : Grid
    {
        public event Action ReturnPressed;
        public TextButton btnReturn;

        public Credits()
        {
            var label1 = new Label();
            label1.Text = "ANXY";
            label1.TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center;
            label1.HorizontalAlignment = HorizontalAlignment.Center;
            label1.GridColumnSpan = 2;
            label1.Scale = new Microsoft.Xna.Framework.Vector2(2, 2);

            var label2 = new Label();
            label2.Text = "Game Developers";
            label2.GridRow = 3;

            var label3 = new Label();
            label3.Text = "Dominic Akeret";
            label3.GridColumn = 1;
            label3.GridRow = 3;

            var label4 = new Label();
            label4.Text = "Tomas Stefan Milata";
            label4.GridColumn = 1;
            label4.GridRow = 4;

            var label5 = new Label();
            label5.Text = "Game Publisher";
            label5.GridRow = 6;

            var label6 = new Label();
            label6.Text = "mayfly studios";
            label6.GridColumn = 1;
            label6.GridRow = 6;

            var label7 = new Label();
            label7.Text = "Contributors";
            label7.GridRow = 8;

            var label8 = new Label();
            label8.Text = "Andreas Meier (Professor)";
            label8.GridColumn = 1;
            label8.GridRow = 8;

            var label9 = new Label();
            label9.Text = "pixlhero (Game Testing)";
            label9.GridColumn = 1;
            label9.GridRow = 9;

            var label10 = new Label();
            label10.Text = "Art and Graphics";
            label10.GridRow = 11;

            var label11 = new Label();
            label11.Text = "Dominic Akeret";
            label11.GridColumn = 1;
            label11.GridRow = 11;

            var label12 = new Label();
            label12.Text = "Pixel Frog (Tileset)";
            label12.GridColumn = 1;
            label12.GridRow = 12;

            var label13 = new Label();
            label13.Text = "Daniel Ap (Background)";
            label13.GridColumn = 1;
            label13.GridRow = 13;

            var label14 = new Label();
            label14.Text = "GameEngine";
            label14.GridRow = 15;

            var label15 = new Label();
            label15.Text = "ANXY Game Engine";
            label15.GridColumn = 1;
            label15.GridRow = 15;

            var label16 = new Label();
            label16.Text = "Framework";
            label16.GridRow = 16;

            var label17 = new Label();
            label17.Text = "MonoGame";
            label17.GridColumn = 1;
            label17.GridRow = 16;

            var label18 = new Label();
            label18.Text = ".NET";
            label18.GridColumn = 1;
            label18.GridRow = 17;

            var label19 = new Label();
            label19.Text = "Tools";
            label19.GridRow = 19;

            var label20 = new Label();
            label20.Text = "Myra";
            label20.GridColumn = 1;
            label20.GridRow = 19;

            var label21 = new Label();
            label21.Text = "Tiled";
            label21.GridColumn = 1;
            label21.GridRow = 20;

            var label22 = new Label();
            label22.Text = "Visual Studio";
            label22.GridColumn = 1;
            label22.GridRow = 21;

            var lblLegalStuff = new Label();
            lblLegalStuff.Text = "Legal Notice";
            lblLegalStuff.GridRow = 24;

            var lblCreativeCommons = new Label();
            lblCreativeCommons.Text = "CC BY-NC-ND";
            lblCreativeCommons.GridColumn = 1;
            lblCreativeCommons.GridRow = 24;

            var lblVersion = new Label();
            lblVersion.Text = "Version";
            lblVersion.GridRow = 23;

            var lblVersionNr = new Label();
            lblVersionNr.Text = "v0.1.8-alpha";
            lblVersionNr.GridColumn = 1;
            lblVersionNr.GridRow = 23;

            var label25 = new Label();
            label25.Text = "Contact Information";
            label25.GridRow = 25;

            var label26 = new Label();
            label26.Text = "GitHub.com/milattom/ANXY";
            label26.GridColumn = 1;
            label26.GridRow = 25;

            btnReturn = new TextButton();
            btnReturn.Text = "Return";
            btnReturn.GridRow = 27;
            btnReturn.Id = "btnReturn";
            btnReturn.GridRowSpan = 2;
            btnReturn.HorizontalAlignment = HorizontalAlignment.Left;
            btnReturn.VerticalAlignment = VerticalAlignment.Center;
            btnReturn.MinWidth = 100;
            btnReturn.Padding = new Thickness(10);
            btnReturn.Click += OnReturnClicked;

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            MinWidth = 550;
            Margin = new Thickness(10);
            Padding = new Thickness(10, 30, 10, 10);
            Background = new SolidBrush("#0000FFAA");
            Widgets.Add(label1);
            Widgets.Add(label2);
            Widgets.Add(label3);
            Widgets.Add(label4);
            Widgets.Add(label5);
            Widgets.Add(label6);
            Widgets.Add(label7);
            Widgets.Add(label8);
            Widgets.Add(label9);
            Widgets.Add(label10);
            Widgets.Add(label11);
            Widgets.Add(label12);
            Widgets.Add(label13);
            Widgets.Add(label14);
            Widgets.Add(label15);
            Widgets.Add(label16);
            Widgets.Add(label17);
            Widgets.Add(label18);
            Widgets.Add(label19);
            Widgets.Add(label20);
            Widgets.Add(label21);
            Widgets.Add(label22);
            Widgets.Add(lblVersion);
            Widgets.Add(lblVersionNr);
            Widgets.Add(lblLegalStuff);
            Widgets.Add(lblCreativeCommons);
            Widgets.Add(label25);
            Widgets.Add(label26);
            Widgets.Add(btnReturn);
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            ReturnPressed?.Invoke();
        }
    }
}

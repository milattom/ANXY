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
            var lblAnxyName = new Label
            {
                Text = "ANXY",
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                GridColumnSpan = 2,
                Scale = new Microsoft.Xna.Framework.Vector2(2, 2)
            };

            var lblGameDevs = new Label
            {
                Text = "Game Developers",
                GridRow = 3
            };

            var lblDominic = new Label
            {
                Text = "Dominic Akeret",
                GridColumn = 1,
                GridRow = 3
            };

            var lblTomas = new Label
            {
                Text = "Tomas Stefan Milata",
                GridColumn = 1,
                GridRow = 4
            };

            var lblGamePublisher = new Label
            {
                Text = "Game Publisher",
                GridRow = 6
            };

            var lblMayflyStudios = new Label
            {
                Text = "mayfly studios",
                GridColumn = 1,
                GridRow = 6
            };

            var lblContributors = new Label
            {
                Text = "Contributors",
                GridRow = 8
            };

            var lblAndreas = new Label
            {
                Text = "Andreas Meier (Professor)",
                GridColumn = 1,
                GridRow = 8
            };

            var lblPixlhero = new Label
            {
                Text = "pixlhero (Game Testing)",
                GridColumn = 1,
                GridRow = 9
            };

            var lblArtDesign = new Label
            {
                Text = "Art and Graphics",
                GridRow = 11
            };

            var lblArtDominic = new Label
            {
                Text = "Dominic Akeret",
                GridColumn = 1,
                GridRow = 11
            };

            var lblPixelFrog = new Label
            {
                Text = "Pixel Frog (Tileset)",
                GridColumn = 1,
                GridRow = 12
            };

            var lblDanielAp = new Label
            {
                Text = "Daniel Ap (Background)",
                GridColumn = 1,
                GridRow = 13
            };

            var lblGameEngine = new Label
            {
                Text = "GameEngine",
                GridRow = 15
            };

            var lblAnxyGameEngine = new Label
            {
                Text = "ANXY Game Engine",
                GridColumn = 1,
                GridRow = 15
            };

            var lblFramework = new Label
            {
                Text = "Framework",
                GridRow = 16
            };

            var lblMonogame = new Label
            {
                Text = "MonoGame",
                GridColumn = 1,
                GridRow = 16
            };

            var lblDotNet = new Label
            {
                Text = ".NET",
                GridColumn = 1,
                GridRow = 17
            };

            var lblTools = new Label
            {
                Text = "Tools",
                GridRow = 19
            };

            var lblMyra = new Label
            {
                Text = "Myra",
                GridColumn = 1,
                GridRow = 19
            };

            var lblTiled = new Label
            {
                Text = "Tiled",
                GridColumn = 1,
                GridRow = 20
            };

            var lblVisualStudio = new Label
            {
                Text = "Visual Studio",
                GridColumn = 1,
                GridRow = 21
            };

            var lblLegalStuff = new Label
            {
                Text = "Legal Notice",
                GridRow = 24
            };

            var lblCreativeCommons = new Label
            {
                Text = "CC BY-NC-ND",
                GridColumn = 1,
                GridRow = 24
            };

            var lblVersion = new Label
            {
                Text = "Version",
                GridRow = 23
            };

            var lblVersionNr = new Label
            {
                Text = "v0.1.9-alpha",
                GridColumn = 1,
                GridRow = 23
            };

            var lblContactInformation = new Label
            {
                Text = "Contact Information",
                GridRow = 25
            };

            var lblGitHub = new Label
            {
                Text = "GitHub.com/milattom/ANXY",
                GridColumn = 1,
                GridRow = 25
            };

            btnReturn = new TextButton
            {
                Text = "Return",
                GridRow = 27,
                Id = "btnReturn",
                GridRowSpan = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                MinWidth = 100,
                Padding = new Thickness(10)
            };
            btnReturn.Click += OnReturnClicked;

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            MinWidth = 550;
            Margin = new Thickness(10);
            Padding = new Thickness(10, 30, 10, 10);
            Background = new SolidBrush("#0000FFAA");
            Widgets.Add(lblAnxyName);
            Widgets.Add(lblGameDevs);
            Widgets.Add(lblDominic);
            Widgets.Add(lblTomas);
            Widgets.Add(lblGamePublisher);
            Widgets.Add(lblMayflyStudios);
            Widgets.Add(lblContributors);
            Widgets.Add(lblAndreas);
            Widgets.Add(lblPixlhero);
            Widgets.Add(lblArtDesign);
            Widgets.Add(lblArtDominic);
            Widgets.Add(lblPixelFrog);
            Widgets.Add(lblDanielAp);
            Widgets.Add(lblGameEngine);
            Widgets.Add(lblAnxyGameEngine);
            Widgets.Add(lblFramework);
            Widgets.Add(lblMonogame);
            Widgets.Add(lblDotNet);
            Widgets.Add(lblTools);
            Widgets.Add(lblMyra);
            Widgets.Add(lblTiled);
            Widgets.Add(lblVisualStudio);
            Widgets.Add(lblVersion);
            Widgets.Add(lblVersionNr);
            Widgets.Add(lblLegalStuff);
            Widgets.Add(lblCreativeCommons);
            Widgets.Add(lblContactInformation);
            Widgets.Add(lblGitHub);
            Widgets.Add(btnReturn);
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            ReturnPressed?.Invoke();
        }
    }
}

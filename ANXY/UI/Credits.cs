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
            var lblAnxyName = new Label();
            lblAnxyName.Text = "ANXY";
            lblAnxyName.TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center;
            lblAnxyName.HorizontalAlignment = HorizontalAlignment.Center;
            lblAnxyName.GridColumnSpan = 2;
            lblAnxyName.Scale = new Microsoft.Xna.Framework.Vector2(2, 2);

            var lblGameDevs = new Label();
            lblGameDevs.Text = "Game Developers";
            lblGameDevs.GridRow = 3;

            var lblDominic = new Label();
            lblDominic.Text = "Dominic Akeret";
            lblDominic.GridColumn = 1;
            lblDominic.GridRow = 3;

            var lblTomas = new Label();
            lblTomas.Text = "Tomas Stefan Milata";
            lblTomas.GridColumn = 1;
            lblTomas.GridRow = 4;

            var lblGamePublisher = new Label();
            lblGamePublisher.Text = "Game Publisher";
            lblGamePublisher.GridRow = 6;

            var lblMayflyStudios = new Label();
            lblMayflyStudios.Text = "mayfly studios";
            lblMayflyStudios.GridColumn = 1;
            lblMayflyStudios.GridRow = 6;

            var lblContributors = new Label();
            lblContributors.Text = "Contributors";
            lblContributors.GridRow = 8;

            var lblAndreas = new Label();
            lblAndreas.Text = "Andreas Meier (Professor)";
            lblAndreas.GridColumn = 1;
            lblAndreas.GridRow = 8;

            var lblPixlhero = new Label();
            lblPixlhero.Text = "pixlhero (Game Testing)";
            lblPixlhero.GridColumn = 1;
            lblPixlhero.GridRow = 9;

            var lblArtDesign = new Label();
            lblArtDesign.Text = "Art and Graphics";
            lblArtDesign.GridRow = 11;

            var lblArtDominic = new Label();
            lblArtDominic.Text = "Dominic Akeret";
            lblArtDominic.GridColumn = 1;
            lblArtDominic.GridRow = 11;

            var lblPixelFrog = new Label();
            lblPixelFrog.Text = "Pixel Frog (Tileset)";
            lblPixelFrog.GridColumn = 1;
            lblPixelFrog.GridRow = 12;

            var lblDanielAp = new Label();
            lblDanielAp.Text = "Daniel Ap (Background)";
            lblDanielAp.GridColumn = 1;
            lblDanielAp.GridRow = 13;

            var lblGameEngine = new Label();
            lblGameEngine.Text = "GameEngine";
            lblGameEngine.GridRow = 15;

            var lblAnxyGameEngine = new Label();
            lblAnxyGameEngine.Text = "ANXY Game Engine";
            lblAnxyGameEngine.GridColumn = 1;
            lblAnxyGameEngine.GridRow = 15;

            var lblFramework = new Label();
            lblFramework.Text = "Framework";
            lblFramework.GridRow = 16;

            var lblMonogame = new Label();
            lblMonogame.Text = "MonoGame";
            lblMonogame.GridColumn = 1;
            lblMonogame.GridRow = 16;

            var lblDotNet = new Label();
            lblDotNet.Text = ".NET";
            lblDotNet.GridColumn = 1;
            lblDotNet.GridRow = 17;

            var lblTools = new Label();
            lblTools.Text = "Tools";
            lblTools.GridRow = 19;

            var lblMyra = new Label();
            lblMyra.Text = "Myra";
            lblMyra.GridColumn = 1;
            lblMyra.GridRow = 19;

            var lblTiled = new Label();
            lblTiled.Text = "Tiled";
            lblTiled.GridColumn = 1;
            lblTiled.GridRow = 20;

            var lblVisualStudio = new Label();
            lblVisualStudio.Text = "Visual Studio";
            lblVisualStudio.GridColumn = 1;
            lblVisualStudio.GridRow = 21;

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

            var lblContactInformation = new Label();
            lblContactInformation.Text = "Contact Information";
            lblContactInformation.GridRow = 25;

            var lblGitHub = new Label();
            lblGitHub.Text = "GitHub.com/milattom/ANXY";
            lblGitHub.GridColumn = 1;
            lblGitHub.GridRow = 25;

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

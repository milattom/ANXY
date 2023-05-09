using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI
{
    internal class ControlsMenu : Grid
    {
        public event Action ReturnPressed;
        public event Action LoadDefaultsPressed;
        public event Action SaveChangesPressed;
        public ControlsMenu()
        {
            var label1 = new Label();
            label1.Text = "Main Key";
            label1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label1.GridColumn = 2;

            var label2 = new Label();
            label2.Text = "Alternate Key";
            label2.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label2.GridColumn = 3;

            var label3 = new Label();
            label3.Text = "Movement";
            label3.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label3.GridRow = 1;

            var label4 = new Label();
            label4.Text = "Move Left";
            label4.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label4.GridColumn = 1;
            label4.GridRow = 2;

            var btnMoveLeft = new TextButton();
            btnMoveLeft.Text = "A";
            btnMoveLeft.MinWidth = 100;
            btnMoveLeft.Padding = new Thickness(10);
            btnMoveLeft.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveLeft.GridColumn = 2;
            btnMoveLeft.GridRow = 2;
            btnMoveLeft.Id = "btnMoveLeft";

            var btnMoveLeftAlternate = new TextButton();
            btnMoveLeftAlternate.Text = "Left";
            btnMoveLeftAlternate.MinWidth = 100;
            btnMoveLeftAlternate.Padding = new Thickness(10);
            btnMoveLeftAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveLeftAlternate.GridColumn = 3;
            btnMoveLeftAlternate.GridRow = 2;
            btnMoveLeftAlternate.Id = "btnMoveLeftAlternate";

            var label5 = new Label();
            label5.Text = "Move Right";
            label5.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label5.GridColumn = 1;
            label5.GridRow = 3;

            var btnMoveRight = new TextButton();
            btnMoveRight.Text = "D";
            btnMoveRight.MinWidth = 100;
            btnMoveRight.Padding = new Thickness(10);
            btnMoveRight.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveRight.GridColumn = 2;
            btnMoveRight.GridRow = 3;
            btnMoveRight.Id = "btnMoveRight";

            var btnMoveRightAlternate = new TextButton();
            btnMoveRightAlternate.Text = "Right";
            btnMoveRightAlternate.MinWidth = 100;
            btnMoveRightAlternate.Padding = new Thickness(10);
            btnMoveRightAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveRightAlternate.GridColumn = 3;
            btnMoveRightAlternate.GridRow = 3;
            btnMoveRightAlternate.Id = "btnMoveRightAlternate";

            var label6 = new Label();
            label6.Text = "Jump";
            label6.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label6.GridColumn = 1;
            label6.GridRow = 4;

            var btnJump = new TextButton();
            btnJump.Text = "Space";
            btnJump.MinWidth = 100;
            btnJump.Padding = new Thickness(10);
            btnJump.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnJump.GridColumn = 2;
            btnJump.GridRow = 4;
            btnJump.Id = "btnJump";

            var btnJumpAlternate = new TextButton();
            btnJumpAlternate.Text = "W";
            btnJumpAlternate.MinWidth = 100;
            btnJumpAlternate.Padding = new Thickness(10);
            btnJumpAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnJumpAlternate.GridColumn = 3;
            btnJumpAlternate.GridRow = 4;
            btnJumpAlternate.Id = "btnJumpAlternate";

            var label7 = new Label();
            label7.Text = "General";
            label7.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label7.GridRow = 5;

            var label8 = new Label();
            label8.Text = "Show FPS";
            label8.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label8.GridColumn = 1;
            label8.GridRow = 6;

            var btnShowFps = new TextButton();
            btnShowFps.Text = "F1";
            btnShowFps.MinWidth = 100;
            btnShowFps.Padding = new Thickness(10);
            btnShowFps.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnShowFps.GridColumn = 2;
            btnShowFps.GridRow = 6;
            btnShowFps.Id = "btnShowFps";

            var btnShowFpsAlternate = new TextButton();
            btnShowFpsAlternate.MinWidth = 100;
            btnShowFpsAlternate.Padding = new Thickness(10);
            btnShowFpsAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnShowFpsAlternate.GridColumn = 3;
            btnShowFpsAlternate.GridRow = 6;
            btnShowFpsAlternate.Id = "btnShowFpsAlternate";

            var label9 = new Label();
            label9.Text = "V Sync (FPS cap)";
            label9.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label9.GridColumn = 1;
            label9.GridRow = 7;

            var btnCapFPS = new TextButton();
            btnCapFPS.Text = "F4";
            btnCapFPS.MinWidth = 100;
            btnCapFPS.Padding = new Thickness(10);
            btnCapFPS.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnCapFPS.GridColumn = 2;
            btnCapFPS.GridRow = 7;
            btnCapFPS.Id = "btnCapFPS";

            var btnCapFPSAlternate = new TextButton();
            btnCapFPSAlternate.MinWidth = 100;
            btnCapFPSAlternate.Padding = new Thickness(10);
            btnCapFPSAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnCapFPSAlternate.GridColumn = 3;
            btnCapFPSAlternate.GridRow = 7;
            btnCapFPSAlternate.Id = "btnCapFPSAlternate";

            var btnReturnFromControls = new TextButton();
            btnReturnFromControls.Text = "Return";
            btnReturnFromControls.MinWidth = 100;
            btnReturnFromControls.Padding = new Thickness(10);
            btnReturnFromControls.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnReturnFromControls.GridRow = 9;
            btnReturnFromControls.Id = "btnReturnFromControls";
            btnReturnFromControls.Click += OnReturnClicked;

            var btnLoadDefaults = new TextButton();
            btnLoadDefaults.Text = "Load Defaults";
            btnLoadDefaults.MinWidth = 100;
            btnLoadDefaults.Padding = new Thickness(10);
            btnLoadDefaults.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnLoadDefaults.GridColumn = 2;
            btnLoadDefaults.GridRow = 9;
            btnLoadDefaults.Id = "btnLoadDefaults";
            btnLoadDefaults.Click += OnLoadDefaultsClicked;

            var btnSaveChanges = new TextButton();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.MinWidth = 100;
            btnSaveChanges.Padding = new Thickness(10);
            btnSaveChanges.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnSaveChanges.GridColumn = 3;
            btnSaveChanges.GridRow = 9;
            btnSaveChanges.Id = "btnSaveChanges";
            btnSaveChanges.Click += OnSaveChangesClicked;


            ColumnSpacing = 10;
            RowSpacing = 5;
            ColumnsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            Margin = new Thickness(10);
            Padding = new Thickness(10);
            Background = new SolidBrush("#0000FF22");
            Widgets.Add(label1);
            Widgets.Add(label2);
            Widgets.Add(label3);
            Widgets.Add(label4);
            Widgets.Add(btnMoveLeft);
            Widgets.Add(btnMoveLeftAlternate);
            Widgets.Add(label5);
            Widgets.Add(btnMoveRight);
            Widgets.Add(btnMoveRightAlternate);
            Widgets.Add(label6);
            Widgets.Add(btnJump);
            Widgets.Add(btnJumpAlternate);
            Widgets.Add(label7);
            Widgets.Add(label8);
            Widgets.Add(btnShowFps);
            Widgets.Add(btnShowFpsAlternate);
            Widgets.Add(label9);
            Widgets.Add(btnCapFPS);
            Widgets.Add(btnCapFPSAlternate);
            Widgets.Add(btnReturnFromControls);
            Widgets.Add(btnLoadDefaults);
            Widgets.Add(btnSaveChanges);
        }

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            SaveChangesPressed?.Invoke();
        }

        private void OnLoadDefaultsClicked(object sender, EventArgs e)
        {
            LoadDefaultsPressed?.Invoke();
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            ReturnPressed?.Invoke();
        }
    }

}

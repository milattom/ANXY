using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
            label1.Text = "Key";
            label1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label1.GridColumn = 2;

            var label3 = new Label();
            label3.Text = "Movement";
            label3.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label3.GridRow = 1;

            lblMultipleIdenticalKeys = new Label();
            lblMultipleIdenticalKeys.Text = "Multiple identical Keys";
            lblMultipleIdenticalKeys.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            lblMultipleIdenticalKeys.HorizontalAlignment = HorizontalAlignment.Center;
            lblMultipleIdenticalKeys.Left = -40;
            lblMultipleIdenticalKeys.GridColumn = 2;
            lblMultipleIdenticalKeys.GridRow = 1;
            lblMultipleIdenticalKeys.TextColor = Color.Red;
            lblMultipleIdenticalKeys.Id = "lblMultipleIdenticalKeys";

            var label4 = new Label();
            label4.Text = "Move Left";
            label4.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label4.GridColumn = 1;
            label4.GridRow = 2;

            btnMoveLeft = new TextButton();
            btnMoveLeft.Text = "A";
            btnMoveLeft.MinWidth = 100;
            btnMoveLeft.Padding = new Thickness(10);
            btnMoveLeft.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveLeft.GridColumn = 2;
            btnMoveLeft.GridRow = 2;
            btnMoveLeft.Id = "btnMovementLeft";
            btnMoveLeft.Click += OnInputButtonClicked;

            var label5 = new Label();
            label5.Text = "Move Right";
            label5.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label5.GridColumn = 1;
            label5.GridRow = 3;

            btnMoveRight = new TextButton();
            btnMoveRight.Text = "D";
            btnMoveRight.MinWidth = 100;
            btnMoveRight.Padding = new Thickness(10);
            btnMoveRight.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMoveRight.GridColumn = 2;
            btnMoveRight.GridRow = 3;
            btnMoveRight.Id = "btnMoveRight";
            btnMoveRight.Click += OnInputButtonClicked;

            var label6 = new Label();
            label6.Text = "Jump";
            label6.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label6.GridColumn = 1;
            label6.GridRow = 4;

            btnJump = new TextButton();
            btnJump.Text = "Space";
            btnJump.MinWidth = 100;
            btnJump.Padding = new Thickness(10);
            btnJump.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnJump.GridColumn = 2;
            btnJump.GridRow = 4;
            btnJump.Id = "btnJump";
            btnJump.Click += OnInputButtonClicked;

            var label7 = new Label();
            label7.Text = "General";
            label7.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label7.GridRow = 5;

            lblWaitingForKeyPress = new Label();
            lblWaitingForKeyPress.Text = "Waiting for Key Press";
            lblWaitingForKeyPress.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            lblWaitingForKeyPress.HorizontalAlignment = HorizontalAlignment.Center;
            lblWaitingForKeyPress.Left = -40;
            lblWaitingForKeyPress.GridColumn = 2;
            lblWaitingForKeyPress.GridRow = 5;
            lblWaitingForKeyPress.TextColor = Color.Red;
            lblWaitingForKeyPress.Id = "lblWaitingForKeyPress";

            var label9 = new Label();
            label9.Text = "Menu";
            label9.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label9.GridColumn = 1;
            label9.GridRow = 6;

            btnMenu = new TextButton();
            btnMenu.Text = "Menu Key TESTING";
            btnMenu.MinWidth = 100;
            btnMenu.Padding = new Thickness(10);
            btnMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnMenu.GridColumn = 2;
            btnMenu.GridRow = 6;
            btnMenu.Id = "btnMenu";
            btnMenu.Click += OnInputButtonClicked;

            var label10 = new Label();
            label10.Text = "Show FPS";
            label10.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label10.GridColumn = 1;
            label10.GridRow = 7;

            btnShowFps = new TextButton();
            btnShowFps.Text = "F1";
            btnShowFps.MinWidth = 100;
            btnShowFps.Padding = new Thickness(10);
            btnShowFps.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnShowFps.GridColumn = 2;
            btnShowFps.GridRow = 7;
            btnShowFps.Id = "btnShowFps";
            btnShowFps.Click += OnInputButtonClicked;

            var label11 = new Label();
            label11.Text = "V Sync (FPS cap)";
            label11.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            label11.GridColumn = 1;
            label11.GridRow = 8;

            btnCapFps = new TextButton();
            btnCapFps.Text = "F4";
            btnCapFps.MinWidth = 100;
            btnCapFps.Padding = new Thickness(10);
            btnCapFps.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnCapFps.GridColumn = 2;
            btnCapFps.GridRow = 8;
            btnCapFps.Id = "btnCapFps";
            btnCapFps.Click += OnInputButtonClicked;

            btnReturnFromControls = new TextButton();
            btnReturnFromControls.Text = "Return";
            btnReturnFromControls.MinWidth = 100;
            btnReturnFromControls.Padding = new Thickness(10);
            btnReturnFromControls.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnReturnFromControls.GridRow = 10;
            btnReturnFromControls.Id = "btnReturnFromControls";
            btnReturnFromControls.Click += OnReturnClicked;

            btnLoadDefaults = new TextButton();
            btnLoadDefaults.Text = "Load Defaults";
            btnLoadDefaults.MinWidth = 100;
            btnLoadDefaults.Padding = new Thickness(10);
            btnLoadDefaults.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnLoadDefaults.HorizontalAlignment = HorizontalAlignment.Right;
            btnLoadDefaults.GridColumn = 1;
            btnLoadDefaults.GridRow = 10;
            btnLoadDefaults.Id = "btnLoadDefaults";
            btnLoadDefaults.Click += OnLoadDefaultsClicked;

            btnSaveChanges = new TextButton();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.MinWidth = 100;
            btnSaveChanges.Padding = new Thickness(10);
            btnSaveChanges.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            btnSaveChanges.HorizontalAlignment = HorizontalAlignment.Left;
            btnSaveChanges.GridColumn = 2;
            btnSaveChanges.GridRow = 10;
            btnSaveChanges.Id = "btnSaveChanges";
            btnSaveChanges.Click += OnSaveChangesClicked;

            ColumnSpacing = 10;
            RowSpacing = 5;
            MinWidth = 550;
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
            Widgets.Add(label3);
            Widgets.Add(label4);
            Widgets.Add(btnMoveLeft);
            Widgets.Add(label5);
            Widgets.Add(btnMoveRight);
            Widgets.Add(label6);
            Widgets.Add(btnJump);
            Widgets.Add(label7);
            Widgets.Add(label9);
            Widgets.Add(btnMenu);
            Widgets.Add(label10);
            Widgets.Add(btnShowFps);
            Widgets.Add(label11);
            Widgets.Add(btnCapFps);
            Widgets.Add(btnReturnFromControls);
            Widgets.Add(btnLoadDefaults);
            Widgets.Add(btnSaveChanges);
        }

        public TextButton btnMoveLeft;
        public TextButton btnMoveRight;
        public TextButton btnJump;
        public TextButton btnMenu;
        public TextButton btnShowFps;
        public TextButton btnCapFps;
        public TextButton btnReturnFromControls;
        public TextButton btnLoadDefaults;
        public TextButton btnSaveChanges;
        public Label lblWaitingForKeyPress;
        public Label lblMultipleIdenticalKeys;
        private TextButton oldSenderTextButton;

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (!CheckMultiple())
            {
                SaveChangesPressed?.Invoke();
            }
        }

        private void OnLoadDefaultsClicked(object sender, EventArgs e)
        {
            LoadDefaultsPressed?.Invoke();
            LoadButtonLayout();
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            ReturnPressed?.Invoke();
        }

        private void OnInputButtonClicked(object sender, EventArgs e)
        {
            TextButton senderTextButton = sender as TextButton;
            if (oldSenderTextButton != null && senderTextButton.Id == oldSenderTextButton.Id && Widgets.Contains(lblWaitingForKeyPress))
            {
                Widgets.Remove(lblWaitingForKeyPress);
                PlayerInputController.Instance.AnyKeyPress -= OnAnyKeyPress;
                return;
            }
            else if (!Widgets.Contains(lblWaitingForKeyPress))
            {
                Widgets.Add(lblWaitingForKeyPress);
            }
            oldSenderTextButton = senderTextButton;

            PlayerInputController.Instance.AnyKeyPress += OnAnyKeyPress;
        }
        private void OnAnyKeyPress(Keys key)
        {
            oldSenderTextButton.Text = key.ToString();
            Widgets.Remove(lblWaitingForKeyPress);
            PlayerInputController.Instance.AnyKeyPress -= OnAnyKeyPress;
            CheckMultiple();
        }

        private bool CheckMultiple()
        {
            var result = false;
            Widgets.Remove(lblMultipleIdenticalKeys);
            btnSaveChanges.TextColor = Color.White;

            Dictionary<string, int> dict = new();
            foreach (TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                if (!dict.ContainsKey(txtBtn.Text))
                {
                    dict.Add(txtBtn.Text, 1);
                }
                else
                {
                    dict[txtBtn.Text]++;
                    result = true;
                }
            }

            foreach (TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                dict.TryGetValue(txtBtn.Text, out var i);
                if (i > 1)
                {
                    txtBtn.TextColor = Color.Red;
                }
                else
                {
                    txtBtn.TextColor = Color.White;
                }
            }

            if (result)
            {
                Widgets.Add(lblMultipleIdenticalKeys);
                btnSaveChanges.TextColor = Color.Red;
            }

            return result;
        }
        public void LoadButtonLayout()
        {
            var inputSettings = PlayerInputController.Instance.inputSettings;

            foreach (TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                if (txtBtn.Id == null)
                    continue;
                switch (txtBtn.Id)
                {
                    case "btnMovementLeft":
                        txtBtn.Text = inputSettings.Movement.Left.ToString();
                        break;
                    case "btnMovementRight":
                        txtBtn.Text = inputSettings.Movement.Right.ToString();
                        break;
                    case "btnJump":
                        txtBtn.Text = inputSettings.Movement.Jump.ToString();
                        break;
                    case "btnMenu":
                        txtBtn.Text = inputSettings.Menu.Key.ToString();
                        break;
                    case "btnShowFps":
                        txtBtn.Text = inputSettings.ShowFps.Key.ToString();
                        break;
                    case "btnCapFps":
                        txtBtn.Text = inputSettings.CapFps.Key.ToString();
                        break;
                    default:
                        continue;
                }
            }
            CheckMultiple();
        }
    }

}

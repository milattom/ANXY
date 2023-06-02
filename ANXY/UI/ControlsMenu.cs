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

        public TextButton btnMovementLeft;
        public TextButton btnMovementRight;
        public TextButton btnMovementJump;
        public TextButton btnMenu;
        public TextButton btnShowFps;
        public TextButton btnCapFps;
        public TextButton btnReturnFromControls;
        public TextButton btnLoadDefaults;
        public TextButton btnSaveChanges;
        public Label lblWaitingForKeyPress;
        public Label lblMultipleIdenticalKeys;
        private TextButton oldSenderTextButton;

        public ControlsMenu()
        {
            var lblKey = new Label();
            lblKey.Text = "Key";
            lblKey.VerticalAlignment = VerticalAlignment.Center;
            lblKey.GridColumn = 2;

            var lblMovement = new Label();
            lblMovement.Text = "Movement";
            lblMovement.VerticalAlignment = VerticalAlignment.Center;
            lblMovement.GridRow = 1;

            lblMultipleIdenticalKeys = new Label();
            lblMultipleIdenticalKeys.Text = "Multiple identical Keys";
            lblMultipleIdenticalKeys.VerticalAlignment = VerticalAlignment.Center;
            lblMultipleIdenticalKeys.HorizontalAlignment = HorizontalAlignment.Center;
            lblMultipleIdenticalKeys.Left = -40;
            lblMultipleIdenticalKeys.GridColumn = 2;
            lblMultipleIdenticalKeys.GridRow = 1;
            lblMultipleIdenticalKeys.TextColor = Color.Red;
            lblMultipleIdenticalKeys.Id = "lblMultipleIdenticalKeys";

            var lblMoveLeft = new Label();
            lblMoveLeft.Text = "Move Left";
            lblMoveLeft.VerticalAlignment = VerticalAlignment.Center;
            lblMoveLeft.GridColumn = 1;
            lblMoveLeft.GridRow = 2;

            btnMovementLeft = new TextButton();
            btnMovementLeft.Text = "A";
            btnMovementLeft.MinWidth = 100;
            btnMovementLeft.Padding = new Thickness(10);
            btnMovementLeft.VerticalAlignment = VerticalAlignment.Center;
            btnMovementLeft.GridColumn = 2;
            btnMovementLeft.GridRow = 2;
            btnMovementLeft.Id = "btnMovementLeft";
            btnMovementLeft.Click += OnInputButtonClicked;

            var lblMoveRight = new Label();
            lblMoveRight.Text = "Move Right";
            lblMoveRight.VerticalAlignment = VerticalAlignment.Center;
            lblMoveRight.GridColumn = 1;
            lblMoveRight.GridRow = 3;

            btnMovementRight = new TextButton();
            btnMovementRight.Text = "D";
            btnMovementRight.MinWidth = 100;
            btnMovementRight.Padding = new Thickness(10);
            btnMovementRight.VerticalAlignment = VerticalAlignment.Center;
            btnMovementRight.GridColumn = 2;
            btnMovementRight.GridRow = 3;
            btnMovementRight.Id = "btnMovementRight";
            btnMovementRight.Click += OnInputButtonClicked;

            var lblJump = new Label();
            lblJump.Text = "Jump";
            lblJump.VerticalAlignment = VerticalAlignment.Center;
            lblJump.GridColumn = 1;
            lblJump.GridRow = 4;

            btnMovementJump = new TextButton();
            btnMovementJump.Text = "Space";
            btnMovementJump.MinWidth = 100;
            btnMovementJump.Padding = new Thickness(10);
            btnMovementJump.VerticalAlignment = VerticalAlignment.Center;
            btnMovementJump.GridColumn = 2;
            btnMovementJump.GridRow = 4;
            btnMovementJump.Id = "btnMovementJump";
            btnMovementJump.Click += OnInputButtonClicked;

            var lblGeneral = new Label();
            lblGeneral.Text = "General";
            lblGeneral.VerticalAlignment = VerticalAlignment.Center;
            lblGeneral.GridRow = 5;

            lblWaitingForKeyPress = new Label();
            lblWaitingForKeyPress.Text = "Waiting for Key Press";
            lblWaitingForKeyPress.VerticalAlignment = VerticalAlignment.Center;
            lblWaitingForKeyPress.HorizontalAlignment = HorizontalAlignment.Center;
            lblWaitingForKeyPress.Left = -40;
            lblWaitingForKeyPress.GridColumn = 2;
            lblWaitingForKeyPress.GridRow = 5;
            lblWaitingForKeyPress.TextColor = Color.Red;
            lblWaitingForKeyPress.Id = "lblWaitingForKeyPress";

            var lblMenu = new Label();
            lblMenu.Text = "Menu";
            lblMenu.VerticalAlignment = VerticalAlignment.Center;
            lblMenu.GridColumn = 1;
            lblMenu.GridRow = 6;

            btnMenu = new TextButton();
            btnMenu.Text = "Menu Key TESTING";
            btnMenu.MinWidth = 100;
            btnMenu.Padding = new Thickness(10);
            btnMenu.VerticalAlignment = VerticalAlignment.Center;
            btnMenu.GridColumn = 2;
            btnMenu.GridRow = 6;
            btnMenu.Id = "btnMenu";
            btnMenu.Click += OnInputButtonClicked;

            var lblShowFps = new Label();
            lblShowFps.Text = "Show FPS";
            lblShowFps.VerticalAlignment = VerticalAlignment.Center;
            lblShowFps.GridColumn = 1;
            lblShowFps.GridRow = 7;

            btnShowFps = new TextButton();
            btnShowFps.Text = "F1";
            btnShowFps.MinWidth = 100;
            btnShowFps.Padding = new Thickness(10);
            btnShowFps.VerticalAlignment = VerticalAlignment.Center;
            btnShowFps.GridColumn = 2;
            btnShowFps.GridRow = 7;
            btnShowFps.Id = "btnShowFps";
            btnShowFps.Click += OnInputButtonClicked;

            var lblFpsCap = new Label();
            lblFpsCap.Text = "V Sync (FPS cap)";
            lblFpsCap.VerticalAlignment = VerticalAlignment.Center;
            lblFpsCap.GridColumn = 1;
            lblFpsCap.GridRow = 8;

            btnCapFps = new TextButton();
            btnCapFps.Text = "F4";
            btnCapFps.MinWidth = 100;
            btnCapFps.Padding = new Thickness(10);
            btnCapFps.VerticalAlignment = VerticalAlignment.Center;
            btnCapFps.GridColumn = 2;
            btnCapFps.GridRow = 8;
            btnCapFps.Id = "btnCapFps";
            btnCapFps.Click += OnInputButtonClicked;

            btnReturnFromControls = new TextButton();
            btnReturnFromControls.Text = "Return";
            btnReturnFromControls.MinWidth = 100;
            btnReturnFromControls.Padding = new Thickness(10);
            btnReturnFromControls.VerticalAlignment = VerticalAlignment.Center;
            btnReturnFromControls.GridRow = 10;
            btnReturnFromControls.Id = "btnReturnFromControls";
            btnReturnFromControls.Click += OnReturnClicked;

            btnLoadDefaults = new TextButton();
            btnLoadDefaults.Text = "Load Defaults";
            btnLoadDefaults.MinWidth = 100;
            btnLoadDefaults.Padding = new Thickness(10);
            btnLoadDefaults.VerticalAlignment = VerticalAlignment.Center;
            btnLoadDefaults.HorizontalAlignment = HorizontalAlignment.Right;
            btnLoadDefaults.GridColumn = 1;
            btnLoadDefaults.GridRow = 10;
            btnLoadDefaults.Id = "btnLoadDefaults";
            btnLoadDefaults.Click += OnLoadDefaultsClicked;

            btnSaveChanges = new TextButton();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.MinWidth = 100;
            btnSaveChanges.Padding = new Thickness(10);
            btnSaveChanges.VerticalAlignment = VerticalAlignment.Center;
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
                Type = ProportionType.Auto,
            });
            RowsProportions.Add(new Proportion
            {
                Type = ProportionType.Auto,
            });
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(10);
            Padding = new Thickness(10);
            Background = new SolidBrush("#0000FF22");
            Widgets.Add(lblKey);
            Widgets.Add(lblMovement);
            Widgets.Add(lblMoveLeft);
            Widgets.Add(btnMovementLeft);
            Widgets.Add(lblMoveRight);
            Widgets.Add(btnMovementRight);
            Widgets.Add(lblJump);
            Widgets.Add(btnMovementJump);
            Widgets.Add(lblGeneral);
            Widgets.Add(lblMenu);
            Widgets.Add(btnMenu);
            Widgets.Add(lblShowFps);
            Widgets.Add(btnShowFps);
            Widgets.Add(lblFpsCap);
            Widgets.Add(btnCapFps);
            Widgets.Add(btnReturnFromControls);
            Widgets.Add(btnLoadDefaults);
            Widgets.Add(btnSaveChanges);
        }

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (!CheckMultiple())
            {
                var inputSettings = new PlayerInput.InputSettings();
                inputSettings.Movement = new PlayerInput.MovementSettings();
                inputSettings.Movement.Left = btnMovementLeft.Text;
                inputSettings.Movement.Right = btnMovementRight.Text;
                inputSettings.Movement.Jump = btnMovementJump.Text;
                inputSettings.Menu = new PlayerInput.KeySetting();
                inputSettings.Menu.Key = btnMenu.Text;
                inputSettings.ShowFps = new PlayerInput.KeySetting();
                inputSettings.ShowFps.Key = btnShowFps.Text;
                inputSettings.CapFps = new PlayerInput.KeySetting();
                inputSettings.CapFps.Key = btnCapFps.Text;

                PlayerInput.Instance.SetInputSettings(inputSettings);

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
                PlayerInput.Instance.AnyKeyPress -= OnAnyKeyPress;
                return;
            }
            else if (!Widgets.Contains(lblWaitingForKeyPress))
            {
                Widgets.Add(lblWaitingForKeyPress);
            }
            oldSenderTextButton = senderTextButton;

            PlayerInput.Instance.AnyKeyPress += OnAnyKeyPress;
        }
        private void OnAnyKeyPress(Keys key)
        {
            oldSenderTextButton.Text = key.ToString();
            Widgets.Remove(lblWaitingForKeyPress);
            PlayerInput.Instance.AnyKeyPress -= OnAnyKeyPress;
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
            var inputSettings = PlayerInput.Instance.inputSettings;

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
                    case "btnMovementJump":
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

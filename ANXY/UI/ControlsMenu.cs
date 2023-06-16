using ANXY.Start;
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

        private readonly TextButton _btnMovementLeft;
        private readonly TextButton _btnMovementRight;
        private readonly TextButton _btnMovementJump;
        private readonly TextButton _btnMenu;
        private readonly TextButton __btnShowFps;
        private readonly TextButton _btnCapFps;
        private readonly TextButton _btnFullscreen;
        private readonly TextButton _btnReturnFromControls;
        private readonly TextButton _btnLoadDefaults;
        private readonly TextButton _btnSaveChanges;
        private readonly Label _lblWaitingForKeyPress;
        private readonly Label _lblMultipleIdenticalKeys;
        private TextButton _oldSenderTextButton;

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

            _lblMultipleIdenticalKeys = new Label();
            _lblMultipleIdenticalKeys.Text = "Multiple identical Keys";
            _lblMultipleIdenticalKeys.VerticalAlignment = VerticalAlignment.Center;
            _lblMultipleIdenticalKeys.HorizontalAlignment = HorizontalAlignment.Center;
            _lblMultipleIdenticalKeys.Left = -40;
            _lblMultipleIdenticalKeys.GridColumn = 2;
            _lblMultipleIdenticalKeys.GridRow = 1;
            _lblMultipleIdenticalKeys.TextColor = Color.Red;
            _lblMultipleIdenticalKeys.Id = "lblMultipleIdenticalKeys";

            var lblMoveLeft = new Label();
            lblMoveLeft.Text = "Move Left";
            lblMoveLeft.VerticalAlignment = VerticalAlignment.Center;
            lblMoveLeft.GridColumn = 1;
            lblMoveLeft.GridRow = 2;

            _btnMovementLeft = new TextButton();
            _btnMovementLeft.Text = "A";
            _btnMovementLeft.MinWidth = 100;
            _btnMovementLeft.Padding = new Thickness(10);
            _btnMovementLeft.VerticalAlignment = VerticalAlignment.Center;
            _btnMovementLeft.GridColumn = 2;
            _btnMovementLeft.GridRow = 2;
            _btnMovementLeft.Id = "btnMovementLeft";
            _btnMovementLeft.Click += OnInputButtonClicked;

            var lblMoveRight = new Label();
            lblMoveRight.Text = "Move Right";
            lblMoveRight.VerticalAlignment = VerticalAlignment.Center;
            lblMoveRight.GridColumn = 1;
            lblMoveRight.GridRow = 3;

            _btnMovementRight = new TextButton();
            _btnMovementRight.Text = "D";
            _btnMovementRight.MinWidth = 100;
            _btnMovementRight.Padding = new Thickness(10);
            _btnMovementRight.VerticalAlignment = VerticalAlignment.Center;
            _btnMovementRight.GridColumn = 2;
            _btnMovementRight.GridRow = 3;
            _btnMovementRight.Id = "btnMovementRight";
            _btnMovementRight.Click += OnInputButtonClicked;

            var lblJump = new Label();
            lblJump.Text = "Jump";
            lblJump.VerticalAlignment = VerticalAlignment.Center;
            lblJump.GridColumn = 1;
            lblJump.GridRow = 4;

            _btnMovementJump = new TextButton();
            _btnMovementJump.Text = "Space";
            _btnMovementJump.MinWidth = 100;
            _btnMovementJump.Padding = new Thickness(10);
            _btnMovementJump.VerticalAlignment = VerticalAlignment.Center;
            _btnMovementJump.GridColumn = 2;
            _btnMovementJump.GridRow = 4;
            _btnMovementJump.Id = "btnMovementJump";
            _btnMovementJump.Click += OnInputButtonClicked;

            var lblGeneral = new Label();
            lblGeneral.Text = "General";
            lblGeneral.VerticalAlignment = VerticalAlignment.Center;
            lblGeneral.GridRow = 5;

            _lblWaitingForKeyPress = new Label();
            _lblWaitingForKeyPress.Text = "Waiting for Key Press";
            _lblWaitingForKeyPress.VerticalAlignment = VerticalAlignment.Center;
            _lblWaitingForKeyPress.HorizontalAlignment = HorizontalAlignment.Center;
            _lblWaitingForKeyPress.Left = -40;
            _lblWaitingForKeyPress.GridColumn = 2;
            _lblWaitingForKeyPress.GridRow = 5;
            _lblWaitingForKeyPress.TextColor = Color.Red;
            _lblWaitingForKeyPress.Id = "lblWaitingForKeyPress";

            var lblMenu = new Label();
            lblMenu.Text = "Menu";
            lblMenu.VerticalAlignment = VerticalAlignment.Center;
            lblMenu.GridColumn = 1;
            lblMenu.GridRow = 6;

            _btnMenu = new TextButton();
            _btnMenu.Text = "Menu Key TESTING";
            _btnMenu.MinWidth = 100;
            _btnMenu.Padding = new Thickness(10);
            _btnMenu.VerticalAlignment = VerticalAlignment.Center;
            _btnMenu.GridColumn = 2;
            _btnMenu.GridRow = 6;
            _btnMenu.Id = "btnMenu";
            _btnMenu.Click += OnInputButtonClicked;

            var lblShowFps = new Label();
            lblShowFps.Text = "Show FPS";
            lblShowFps.VerticalAlignment = VerticalAlignment.Center;
            lblShowFps.GridColumn = 1;
            lblShowFps.GridRow = 7;

            __btnShowFps = new TextButton();
            __btnShowFps.Text = "F1";
            __btnShowFps.MinWidth = 100;
            __btnShowFps.Padding = new Thickness(10);
            __btnShowFps.VerticalAlignment = VerticalAlignment.Center;
            __btnShowFps.GridColumn = 2;
            __btnShowFps.GridRow = 7;
            __btnShowFps.Id = "btnShowFps";
            __btnShowFps.Click += OnInputButtonClicked;

            var lblFpsCap = new Label();
            lblFpsCap.Text = "V Sync (FPS cap)";
            lblFpsCap.VerticalAlignment = VerticalAlignment.Center;
            lblFpsCap.GridColumn = 1;
            lblFpsCap.GridRow = 8;

            _btnCapFps = new TextButton();
            _btnCapFps.Text = "F4";
            _btnCapFps.MinWidth = 100;
            _btnCapFps.Padding = new Thickness(10);
            _btnCapFps.VerticalAlignment = VerticalAlignment.Center;
            _btnCapFps.GridColumn = 2;
            _btnCapFps.GridRow = 8;
            _btnCapFps.Id = "btnCapFps";
            _btnCapFps.Click += OnInputButtonClicked;

            var lblFullscreen = new Label();
            lblFullscreen.Text = "Fullscreen on/off";
            lblFullscreen.VerticalAlignment = VerticalAlignment.Center;
            lblFullscreen.GridColumn = 1;
            lblFullscreen.GridRow = 9;

            _btnFullscreen = new TextButton();
            _btnFullscreen.Text = "F11";
            _btnFullscreen.MinWidth = 100;
            _btnFullscreen.Padding = new Thickness(10);
            _btnFullscreen.VerticalAlignment = VerticalAlignment.Center;
            _btnFullscreen.GridColumn = 2;
            _btnFullscreen.GridRow = 9;
            _btnFullscreen.Id = "btnFullscreen";
            _btnFullscreen.Click += OnInputButtonClicked;

            _btnReturnFromControls = new TextButton();
            _btnReturnFromControls.Text = "Return";
            _btnReturnFromControls.MinWidth = 100;
            _btnReturnFromControls.Padding = new Thickness(10);
            _btnReturnFromControls.VerticalAlignment = VerticalAlignment.Center;
            _btnReturnFromControls.GridRow = 11;
            _btnReturnFromControls.Id = "btnReturnFromControls";
            _btnReturnFromControls.Click += OnReturnClicked;

            _btnLoadDefaults = new TextButton();
            _btnLoadDefaults.Text = "Load Defaults";
            _btnLoadDefaults.MinWidth = 100;
            _btnLoadDefaults.Padding = new Thickness(10);
            _btnLoadDefaults.VerticalAlignment = VerticalAlignment.Center;
            _btnLoadDefaults.HorizontalAlignment = HorizontalAlignment.Right;
            _btnLoadDefaults.GridColumn = 1;
            _btnLoadDefaults.GridRow = 11;
            _btnLoadDefaults.Id = "btnLoadDefaults";
            _btnLoadDefaults.Click += OnLoadDefaultsClicked;

            _btnSaveChanges = new TextButton();
            _btnSaveChanges.Text = "Save Changes";
            _btnSaveChanges.MinWidth = 100;
            _btnSaveChanges.Padding = new Thickness(10);
            _btnSaveChanges.VerticalAlignment = VerticalAlignment.Center;
            _btnSaveChanges.HorizontalAlignment = HorizontalAlignment.Left;
            _btnSaveChanges.GridColumn = 2;
            _btnSaveChanges.GridRow = 11;
            _btnSaveChanges.Id = "btnSaveChanges";
            _btnSaveChanges.Click += OnSaveChangesClicked;

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
            Background = new SolidBrush("#0000FFAA");
            Widgets.Add(lblKey);
            Widgets.Add(lblMovement);
            Widgets.Add(lblMoveLeft);
            Widgets.Add(_btnMovementLeft);
            Widgets.Add(lblMoveRight);
            Widgets.Add(_btnMovementRight);
            Widgets.Add(lblJump);
            Widgets.Add(_btnMovementJump);
            Widgets.Add(lblGeneral);
            Widgets.Add(lblMenu);
            Widgets.Add(_btnMenu);
            Widgets.Add(lblShowFps);
            Widgets.Add(__btnShowFps);
            Widgets.Add(lblFpsCap);
            Widgets.Add(_btnCapFps);
            Widgets.Add(lblFullscreen);
            Widgets.Add(_btnFullscreen);
            Widgets.Add(_btnReturnFromControls);
            Widgets.Add(_btnLoadDefaults);
            Widgets.Add(_btnSaveChanges);
        }

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if(!CheckMultiple())
            {
                var inputSettings = new PlayerInput.InputSettings();
                inputSettings.Movement = new PlayerInput.MovementSettings();
                inputSettings.Movement.Left = _btnMovementLeft.Text;
                inputSettings.Movement.Right = _btnMovementRight.Text;
                inputSettings.Movement.Jump = _btnMovementJump.Text;
                inputSettings.Menu = new PlayerInput.KeySetting();
                inputSettings.Menu.Key = _btnMenu.Text;
                inputSettings.ShowFps = new PlayerInput.KeySetting();
                inputSettings.ShowFps.Key = __btnShowFps.Text;
                inputSettings.CapFps = new PlayerInput.KeySetting();
                inputSettings.CapFps.Key = _btnCapFps.Text;
                inputSettings.Fullscreen = new PlayerInput.KeySetting();
                inputSettings.Fullscreen.Key = _btnFullscreen.Text;

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
            if(_oldSenderTextButton != null && senderTextButton.Id == _oldSenderTextButton.Id && Widgets.Contains(_lblWaitingForKeyPress))
            {
                Widgets.Remove(_lblWaitingForKeyPress);
                PlayerInput.Instance.AnyKeyPressed -= OnAnyKeyPress;
                return;
            }
            else if(!Widgets.Contains(_lblWaitingForKeyPress))
            {
                Widgets.Add(_lblWaitingForKeyPress);
            }
            _oldSenderTextButton = senderTextButton;

            PlayerInput.Instance.AnyKeyPressed += OnAnyKeyPress;
        }
        private void OnAnyKeyPress(Keys key)
        {
            _oldSenderTextButton.Text = key.ToString();
            Widgets.Remove(_lblWaitingForKeyPress);
            PlayerInput.Instance.AnyKeyPressed -= OnAnyKeyPress;
            CheckMultiple();
        }

        private bool CheckMultiple()
        {
            var result = false;
            Widgets.Remove(_lblMultipleIdenticalKeys);
            _btnSaveChanges.TextColor = Color.White;

            Dictionary<string, int> dict = new();
            foreach(TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                if(!dict.ContainsKey(txtBtn.Text))
                {
                    dict.Add(txtBtn.Text, 1);
                }
                else
                {
                    dict[txtBtn.Text]++;
                    result = true;
                }
            }

            foreach(TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                dict.TryGetValue(txtBtn.Text, out var i);
                if(i > 1)
                {
                    txtBtn.TextColor = Color.Red;
                }
                else
                {
                    txtBtn.TextColor = Color.White;
                }
            }

            if(result)
            {
                Widgets.Add(_lblMultipleIdenticalKeys);
                _btnSaveChanges.TextColor = Color.Red;
            }

            return result;
        }
        public void LoadButtonLayout()
        {
            var inputSettings = PlayerInput.Instance.inputSettings;

            foreach(TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                if(txtBtn.Id == null)
                    continue;
                switch(txtBtn.Id)
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

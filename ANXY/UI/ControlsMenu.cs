using ANXY.ECS.Components;
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
        public TextButton btnFullscreen;
        public TextButton btnDebugToggle;
        public TextButton btnReturnFromControls;
        public TextButton btnLoadDefaults;
        public TextButton btnSaveChanges;
        public Label lblWaitingForKeyPress;
        public Label lblMultipleIdenticalKeys;
        public Label lblDontForgetToSaveChanges;
        private TextButton oldSenderTextButton;
        internal ControlsMenu()
        {
            //Row 0: Title
            var lblTitle = new Label();
            lblTitle.Text = "Controls";
            lblTitle.Padding = new Thickness(-50, 0, 0, 0);
            lblTitle.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            lblTitle.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Bottom;
            lblTitle.GridColumn = 0;
            lblTitle.GridColumnSpan = 3;
            lblTitle.GridRow = 0;
            lblTitle.Scale = new Vector2(2, 2);

            //Row 1: General
            var lblGeneral = new Label();
            lblGeneral.Text = "General";
            lblGeneral.VerticalAlignment = VerticalAlignment.Bottom;
            lblGeneral.GridRow = 1;
            lblGeneral.Padding = new Thickness(50, 0, 0, 0);
            lblGeneral.GridColumnSpan = 2;

            lblWaitingForKeyPress = new Label();
            lblWaitingForKeyPress.Text = "Waiting for Key Press";
            lblWaitingForKeyPress.VerticalAlignment = VerticalAlignment.Center;
            lblWaitingForKeyPress.HorizontalAlignment = HorizontalAlignment.Right;
            lblWaitingForKeyPress.GridColumn = 2;
            lblWaitingForKeyPress.GridRow = 1;
            lblWaitingForKeyPress.TextColor = Color.Red;
            lblWaitingForKeyPress.Id = "lblWaitingForKeyPress";
            lblWaitingForKeyPress.Visible = false;

            //Row 2
            var lblMenu = new Label();
            lblMenu.Text = "Menu";
            lblMenu.VerticalAlignment = VerticalAlignment.Center;
            lblMenu.GridColumn = 1;
            lblMenu.GridRow = 2;

            btnMenu = new TextButton();
            btnMenu.Text = "Esc";
            btnMenu.MinWidth = 100;
            btnMenu.Padding = new Thickness(10, 5, 10, 5);
            btnMenu.VerticalAlignment = VerticalAlignment.Center;
            btnMenu.HorizontalAlignment = HorizontalAlignment.Left;
            btnMenu.GridColumn = 2;
            btnMenu.GridRow = 2;
            btnMenu.Id = "btnMenu";
            btnMenu.Click += OnInputButtonClicked;

            //Row 3
            var lblFullscreen = new Label();
            lblFullscreen.Text = "Fullscreen on/off";
            lblFullscreen.VerticalAlignment = VerticalAlignment.Center;
            lblFullscreen.GridColumn = 1;
            lblFullscreen.GridRow = 3;

            btnFullscreen = new TextButton();
            btnFullscreen.Text = "F11";
            btnFullscreen.MinWidth = 100;
            btnFullscreen.Padding = new Thickness(10, 5, 10, 5);
            btnFullscreen.VerticalAlignment = VerticalAlignment.Center;
            btnFullscreen.HorizontalAlignment = HorizontalAlignment.Left;
            btnFullscreen.GridColumn = 2;
            btnFullscreen.GridRow = 3;
            btnFullscreen.Id = "btnFullscreen";
            btnFullscreen.Click += OnInputButtonClicked;

            //Row 4: Movement
            var lblMovement = new Label();
            lblMovement.Text = "Movement";
            lblMovement.VerticalAlignment = VerticalAlignment.Bottom;
            lblMovement.GridRow = 4;
            lblMovement.Padding = new Thickness(50, 0, 0, 0);
            lblMovement.GridColumnSpan = 2;

            //Row 5
            var lblMoveLeft = new Label();
            lblMoveLeft.Text = "Move Left";
            lblMoveLeft.VerticalAlignment = VerticalAlignment.Center;
            lblMoveLeft.GridColumn = 1;
            lblMoveLeft.GridRow = 5;

            btnMovementLeft = new TextButton();
            btnMovementLeft.Text = "A";
            btnMovementLeft.MinWidth = 100;
            btnMovementLeft.Padding = new Thickness(10, 5, 10, 5);
            btnMovementLeft.VerticalAlignment = VerticalAlignment.Center;
            btnMovementLeft.HorizontalAlignment = HorizontalAlignment.Left;
            btnMovementLeft.GridColumn = 2;
            btnMovementLeft.GridRow = 5;
            btnMovementLeft.Id = "btnMovementLeft";
            btnMovementLeft.Click += OnInputButtonClicked;

            //Row 6
            var lblMoveRight = new Label();
            lblMoveRight.Text = "Move Right";
            lblMoveRight.VerticalAlignment = VerticalAlignment.Center;
            lblMoveRight.GridColumn = 1;
            lblMoveRight.GridRow = 6;

            btnMovementRight = new TextButton();
            btnMovementRight.Text = "D";
            btnMovementRight.MinWidth = 100;
            btnMovementRight.Padding = new Thickness(10, 5, 10, 5);
            btnMovementRight.VerticalAlignment = VerticalAlignment.Center;
            btnMovementRight.HorizontalAlignment = HorizontalAlignment.Left;
            btnMovementRight.GridColumn = 2;
            btnMovementRight.GridRow = 6;
            btnMovementRight.Id = "btnMovementRight";
            btnMovementRight.Click += OnInputButtonClicked;

            //Row 7
            var lblJump = new Label();
            lblJump.Text = "Jump";
            lblJump.VerticalAlignment = VerticalAlignment.Center;
            lblJump.GridColumn = 1;
            lblJump.GridRow = 7;

            btnMovementJump = new TextButton();
            btnMovementJump.Text = "Space";
            btnMovementJump.MinWidth = 100;
            btnMovementJump.Padding = new Thickness(10, 5, 10, 5);
            btnMovementJump.VerticalAlignment = VerticalAlignment.Center;
            btnMovementJump.HorizontalAlignment = HorizontalAlignment.Left;
            btnMovementJump.GridColumn = 2;
            btnMovementJump.GridRow = 7;
            btnMovementJump.Id = "btnMovementJump";
            btnMovementJump.Click += OnInputButtonClicked;

            //Row 8: FPS
            var lblFps = new Label();
            lblFps.Text = "FPS";
            lblFps.VerticalAlignment = VerticalAlignment.Bottom;
            lblFps.GridRow = 8;
            lblFps.Padding = new Thickness(50, 0, 0, 0);
            lblFps.GridColumnSpan = 2;

            //Row 9
            var lblShowFps = new Label();
            lblShowFps.Text = "Show FPS";
            lblShowFps.VerticalAlignment = VerticalAlignment.Center;
            lblShowFps.GridColumn = 1;
            lblShowFps.GridRow = 9;

            btnShowFps = new TextButton();
            btnShowFps.Text = "F1";
            btnShowFps.MinWidth = 100;
            btnShowFps.Padding = new Thickness(10, 5, 10, 5);
            btnShowFps.VerticalAlignment = VerticalAlignment.Center;
            btnShowFps.HorizontalAlignment = HorizontalAlignment.Left;
            btnShowFps.GridColumn = 2;
            btnShowFps.GridRow = 9;
            btnShowFps.Id = "btnShowFps";
            btnShowFps.Click += OnInputButtonClicked;

            //Row 10
            var lblFpsCap = new Label();
            lblFpsCap.Text = "V Sync (FPS cap)";
            lblFpsCap.VerticalAlignment = VerticalAlignment.Center;
            lblFpsCap.GridColumn = 1;
            lblFpsCap.GridRow = 10;

            btnCapFps = new TextButton();
            btnCapFps.Text = "F4";
            btnCapFps.MinWidth = 100;
            btnCapFps.Padding = new Thickness(10, 5, 10, 5);
            btnCapFps.VerticalAlignment = VerticalAlignment.Center;
            btnCapFps.HorizontalAlignment = HorizontalAlignment.Left;
            btnCapFps.GridColumn = 2;
            btnCapFps.GridRow = 10;
            btnCapFps.Id = "btnCapFps";
            btnCapFps.Click += OnInputButtonClicked;

            //Row 11: Debug
            var lblDebugSection = new Label();
            lblDebugSection.Text = "Debug";
            lblDebugSection.VerticalAlignment = VerticalAlignment.Bottom;
            lblDebugSection.GridRow = 11;
            lblDebugSection.Padding = new Thickness(50, 0, 0, 0);
            lblDebugSection.GridColumnSpan = 2;

            //Row 12
            var lblDebugToggle = new Label();
            lblDebugToggle.Text = "Enter/Leave Debug Mode";
            lblDebugToggle.VerticalAlignment = VerticalAlignment.Center;
            lblDebugToggle.GridColumn = 1;
            lblDebugToggle.GridRow = 12;

            btnDebugToggle = new TextButton();
            btnDebugToggle.Text = "F5";
            btnDebugToggle.MinWidth = 100;
            btnDebugToggle.Padding = new Thickness(10, 5, 10, 5);
            btnDebugToggle.VerticalAlignment = VerticalAlignment.Center;
            btnDebugToggle.HorizontalAlignment = HorizontalAlignment.Left;
            btnDebugToggle.GridColumn = 2;
            btnDebugToggle.GridRow = 12;
            btnDebugToggle.Id = "btnDebugToggle";
            btnDebugToggle.Click += OnInputButtonClicked;

            //Row 13: Error/Hint Messages
            lblMultipleIdenticalKeys = new Label();
            lblMultipleIdenticalKeys.Text = "Multiple identical Keys";
            lblMultipleIdenticalKeys.VerticalAlignment = VerticalAlignment.Center;
            lblMultipleIdenticalKeys.HorizontalAlignment = HorizontalAlignment.Right;
            lblMultipleIdenticalKeys.Left = -40;
            lblMultipleIdenticalKeys.GridColumn = 0;
            lblMultipleIdenticalKeys.GridColumnSpan = 3;
            lblMultipleIdenticalKeys.GridRow = 13;
            lblMultipleIdenticalKeys.TextColor = Color.Red;
            lblMultipleIdenticalKeys.Id = "lblMultipleIdenticalKeys";

            lblDontForgetToSaveChanges = new Label();
            lblDontForgetToSaveChanges.Text = "Don't forget to save changes";
            lblDontForgetToSaveChanges.VerticalAlignment = VerticalAlignment.Center;
            lblDontForgetToSaveChanges.HorizontalAlignment = HorizontalAlignment.Right;
            lblDontForgetToSaveChanges.Left = -40;
            lblDontForgetToSaveChanges.GridColumn = 0;
            lblDontForgetToSaveChanges.GridColumnSpan = 3;
            lblDontForgetToSaveChanges.GridRow = 13;
            lblDontForgetToSaveChanges.TextColor = Color.Red;
            lblDontForgetToSaveChanges.Id = "lblDontForgetToSaveChanges";
            lblDontForgetToSaveChanges.Visible = false;

            //Row 14: Return/Reset/Save
            btnReturnFromControls = new TextButton();
            btnReturnFromControls.Text = "Return";
            btnReturnFromControls.MinWidth = 100;
            btnReturnFromControls.Padding = new Thickness(10);
            btnReturnFromControls.VerticalAlignment = VerticalAlignment.Center;
            btnReturnFromControls.GridRow = 14;
            btnReturnFromControls.Id = "btnReturnFromControls";
            btnReturnFromControls.Click += OnReturnClicked;

            btnLoadDefaults = new TextButton();
            btnLoadDefaults.Text = "Reset to Defaults";
            btnLoadDefaults.MinWidth = 100;
            btnLoadDefaults.Padding = new Thickness(10);
            btnLoadDefaults.VerticalAlignment = VerticalAlignment.Center;
            btnLoadDefaults.HorizontalAlignment = HorizontalAlignment.Right;
            btnLoadDefaults.GridColumn = 1;
            btnLoadDefaults.GridRow = 14;
            btnLoadDefaults.Id = "btnLoadDefaults";
            btnLoadDefaults.Click += OnLoadDefaultsClicked;

            btnSaveChanges = new TextButton();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.MinWidth = 100;
            btnSaveChanges.Padding = new Thickness(10);
            btnSaveChanges.VerticalAlignment = VerticalAlignment.Center;
            btnSaveChanges.HorizontalAlignment = HorizontalAlignment.Left;
            btnSaveChanges.GridColumn = 2;
            btnSaveChanges.GridRow = 14;
            btnSaveChanges.Id = "btnSaveChanges";
            btnSaveChanges.Click += OnSaveChangesClicked;

            //Controls UI Properties
            ColumnSpacing = 10;
            RowSpacing = -5;
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
            Padding = new Thickness(15, 10, -50, 10);
            Background = new SolidBrush("#0000FFAA");

            //Add Widgets
            Widgets.Add(lblTitle);
            Widgets.Add(lblGeneral);
            Widgets.Add(lblWaitingForKeyPress);
            Widgets.Add(lblMenu);
            Widgets.Add(btnMenu);
            Widgets.Add(lblFullscreen);
            Widgets.Add(btnFullscreen);
            Widgets.Add(lblMovement);
            Widgets.Add(lblMoveLeft);
            Widgets.Add(btnMovementLeft);
            Widgets.Add(lblMoveRight);
            Widgets.Add(btnMovementRight);
            Widgets.Add(lblJump);
            Widgets.Add(btnMovementJump);
            Widgets.Add(lblFps);
            Widgets.Add(lblShowFps);
            Widgets.Add(btnShowFps);
            Widgets.Add(lblFpsCap);
            Widgets.Add(btnCapFps);
            Widgets.Add(lblDebugSection);
            Widgets.Add(lblDebugToggle);
            Widgets.Add(btnDebugToggle);
            Widgets.Add(lblMultipleIdenticalKeys);
            Widgets.Add(lblDontForgetToSaveChanges);
            Widgets.Add(btnReturnFromControls);
            Widgets.Add(btnLoadDefaults);
            Widgets.Add(btnSaveChanges);
        }

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (!HasMultiple())
            {
                var inputSettings = new PlayerInput.InputKeyStrings();
                inputSettings.Debug.Toggle = btnDebugToggle.Text;
                inputSettings.Fps.Cap = btnCapFps.Text;
                inputSettings.Fps.ToggleShow = btnShowFps.Text;
                inputSettings.General.Fullscreen = btnFullscreen.Text;
                inputSettings.General.Menu = btnMenu.Text;
                inputSettings.Movement.Jump = btnMovementJump.Text;
                inputSettings.Movement.Left = btnMovementLeft.Text;
                inputSettings.Movement.Right = btnMovementRight.Text;

                lblDontForgetToSaveChanges.Visible = false;

                PlayerInput.Instance.SetInputSettings(inputSettings);

                SaveChangesPressed?.Invoke();
            }
        }

        private void OnLoadDefaultsClicked(object sender, EventArgs e)
        {
            lblDontForgetToSaveChanges.Visible = false;
            btnSaveChanges.TextColor = Color.White;
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
            lblWaitingForKeyPress.Visible = true;
            PlayerInput.Instance.DisableMenuKey();
            oldSenderTextButton = senderTextButton;

            PlayerInput.Instance.AnyKeyPressed += OnAnyKeyPress;
        }
        private void OnAnyKeyPress(Keys key)
        {
            if (!oldSenderTextButton.Text.Equals(key.ToString()))
            {
                oldSenderTextButton.Text = key.ToString();
                lblDontForgetToSaveChanges.Visible = true;
            }
            lblWaitingForKeyPress.Visible = false;
            PlayerInput.Instance.AnyKeyPressed -= OnAnyKeyPress;
            if (!HasMultiple())
            {
                CheckChanges();
            }

        }

        private bool HasMultiple()
        {
            var hasMultiple = false;
            lblMultipleIdenticalKeys.Visible = false;
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
                    hasMultiple = true;
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

            if (hasMultiple)
            {
                lblMultipleIdenticalKeys.Visible = true;
                lblDontForgetToSaveChanges.Visible = false;
                btnSaveChanges.TextColor = Color.Red;
                btnSaveChanges.Enabled = false;
            }
            else
            {
                btnSaveChanges.Enabled = true;
            }

            return hasMultiple;
        }

        private void CheckChanges()
        {
            var noChangesMade = true;

            var inputSettings = PlayerInput.Instance.inputSettings;
            foreach (TextButton txtBtn in Widgets.OfType<TextButton>())
            {
                if (txtBtn.Id == null)
                    continue;
                switch (txtBtn.Id)
                {
                    case "btnDebugToggle":
                        if (!inputSettings.Debug.Toggle.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnCapFps":
                        if (!inputSettings.Fps.Cap.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnShowFps":
                        if (!inputSettings.Fps.ToggleShow.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnFullscreen":
                        if (!inputSettings.General.Fullscreen.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnMenu":
                        if (!inputSettings.General.Menu.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnMovementLeft":
                        if (!inputSettings.Movement.Left.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnMovementRight":
                        if (!inputSettings.Movement.Right.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    case "btnMovementJump":
                        if (!inputSettings.Movement.Jump.Equals(txtBtn.Text))
                            noChangesMade = false;
                        break;
                    default:
                        continue;
                }
                if (!noChangesMade)
                {
                    break;
                }
            }

            if (noChangesMade)
            {
                lblDontForgetToSaveChanges.Visible = false;
                btnSaveChanges.TextColor = Color.White;

            }
            else
            {
                lblDontForgetToSaveChanges.Visible = true;
                btnSaveChanges.TextColor = Color.Green;
            }
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
                    case "btnDebugToggle":
                        txtBtn.Text = inputSettings.Debug.Toggle.ToString();
                        break;
                    case "btnCapFps":
                        txtBtn.Text = inputSettings.Fps.Cap.ToString();
                        break;
                    case "btnShowFps":
                        txtBtn.Text = inputSettings.Fps.ToggleShow.ToString();
                        break;
                    case "btnFullscreen":
                        txtBtn.Text = inputSettings.General.Fullscreen.ToString();
                        break;
                    case "btnMenu":
                        txtBtn.Text = inputSettings.General.Menu.ToString();
                        break;
                    case "btnMovementLeft":
                        txtBtn.Text = inputSettings.Movement.Left.ToString();
                        break;
                    case "btnMovementRight":
                        txtBtn.Text = inputSettings.Movement.Right.ToString();
                        break;
                    case "btnMovementJump":
                        txtBtn.Text = inputSettings.Movement.Jump.ToString();
                        break;
                    default:
                        continue;
                }
            }
            if (HasMultiple())
            {
                CheckChanges();
            }
        }
    }
}

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
            var lblTitle = new Label
            {
                Text = "Controls",
                Padding = new Thickness(-35, 5, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                GridColumn = 0,
                GridColumnSpan = 3,
                GridRow = 0,
                Scale = new Vector2(2, 2)
            };

            //Row 1: General
            var lblGeneral = new Label
            {
                Text = "General",
                VerticalAlignment = VerticalAlignment.Bottom,
                GridRow = 1,
                Padding = new Thickness(50, 0, 0, 0),
                GridColumnSpan = 2
            };

            lblWaitingForKeyPress = new Label
            {
                Text = "Waiting for Key Press",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                GridColumn = 2,
                GridRow = 1,
                TextColor = Color.Red,
                Id = "lblWaitingForKeyPress",
                Visible = false
            };

            //Row 2
            var lblMenu = new Label
            {
                Text = "Menu",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 2
            };

            btnMenu = new TextButton
            {
                Text = "Esc",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 2,
                Id = "btnMenu"
            };
            btnMenu.Click += OnInputButtonClicked;

            //Row 3
            var lblFullscreen = new Label
            {
                Text = "Fullscreen on/off",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 3
            };

            btnFullscreen = new TextButton
            {
                Text = "F11",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 3,
                Id = "btnFullscreen"
            };
            btnFullscreen.Click += OnInputButtonClicked;

            //Row 4: Movement
            var lblMovement = new Label
            {
                Text = "Movement",
                VerticalAlignment = VerticalAlignment.Bottom,
                GridRow = 4,
                Padding = new Thickness(50, 0, 0, 0),
                GridColumnSpan = 2
            };

            //Row 5
            var lblMoveLeft = new Label
            {
                Text = "Move Left",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 5
            };

            btnMovementLeft = new TextButton
            {
                Text = "A",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 5,
                Id = "btnMovementLeft"
            };
            btnMovementLeft.Click += OnInputButtonClicked;

            //Row 6
            var lblMoveRight = new Label
            {
                Text = "Move Right",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 6
            };

            btnMovementRight = new TextButton
            {
                Text = "D",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 6,
                Id = "btnMovementRight"
            };
            btnMovementRight.Click += OnInputButtonClicked;

            //Row 7
            var lblJump = new Label
            {
                Text = "Jump",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 7
            };

            btnMovementJump = new TextButton
            {
                Text = "Space",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 7,
                Id = "btnMovementJump"
            };
            btnMovementJump.Click += OnInputButtonClicked;

            //Row 8: FPS
            var lblFps = new Label
            {
                Text = "FPS",
                VerticalAlignment = VerticalAlignment.Bottom,
                GridRow = 8,
                Padding = new Thickness(50, 0, 0, 0),
                GridColumnSpan = 2
            };

            //Row 9
            var lblShowFps = new Label
            {
                Text = "Show FPS",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 9
            };

            btnShowFps = new TextButton
            {
                Text = "F1",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 9,
                Id = "btnShowFps"
            };
            btnShowFps.Click += OnInputButtonClicked;

            //Row 10
            var lblFpsCap = new Label
            {
                Text = "V Sync (FPS cap)",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 10
            };

            btnCapFps = new TextButton
            {
                Text = "F4",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 10,
                Id = "btnCapFps"
            };
            btnCapFps.Click += OnInputButtonClicked;

            //Row 11: Debug
            var lblDebugSection = new Label
            {
                Text = "Debug",
                VerticalAlignment = VerticalAlignment.Bottom,
                GridRow = 11,
                Padding = new Thickness(50, 0, 0, 0),
                GridColumnSpan = 2
            };

            //Row 12
            var lblDebugToggle = new Label
            {
                Text = "Enter/Leave Debug Mode",
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 12
            };

            btnDebugToggle = new TextButton
            {
                Text = "F5",
                MinWidth = 100,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 12,
                Id = "btnDebugToggle"
            };
            btnDebugToggle.Click += OnInputButtonClicked;

            //Row 13: Error/Hint Messages
            lblMultipleIdenticalKeys = new Label
            {
                Text = "Multiple identical Keys",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Left = -40,
                GridColumn = 0,
                GridColumnSpan = 3,
                GridRow = 13,
                TextColor = Color.Red,
                Id = "lblMultipleIdenticalKeys"
            };

            lblDontForgetToSaveChanges = new Label
            {
                Text = "Don't forget to save changes",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Left = -40,
                GridColumn = 0,
                GridColumnSpan = 3,
                GridRow = 13,
                TextColor = Color.Red,
                Id = "lblDontForgetToSaveChanges",
                Visible = false
            };

            //Row 14: Return/Reset/Save
            btnReturnFromControls = new TextButton
            {
                Text = "Return",
                MinWidth = 100,
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                GridRow = 14,
                Id = "btnReturnFromControls"
            };
            btnReturnFromControls.Click += OnReturnClicked;

            btnLoadDefaults = new TextButton
            {
                Text = "Reset to Defaults",
                MinWidth = 100,
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                GridColumn = 1,
                GridRow = 14,
                Id = "btnLoadDefaults"
            };
            btnLoadDefaults.Click += OnLoadDefaultsClicked;

            btnSaveChanges = new TextButton
            {
                Text = "Save Changes",
                MinWidth = 100,
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                GridColumn = 2,
                GridRow = 14,
                Id = "btnSaveChanges"
            };
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

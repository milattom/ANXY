﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO Player Input Controller
    /// Checking for Keyboard input and setting everything accordingly. Every Class needing to check the Keyboard input must check it through this Class.
    /// </summary>
    public class PlayerInputController : Component
    {
        public event Action ShowFpsKeyPressed;
        public event Action LimitFpsKeyPressed;
        public event Action<bool> GamePausedChanged;
        public event Action<Keys> AnyKeyPress;

        public bool GamePaused { get; private set; }

        public class InputSettings
        {
            public MovementSettings Movement { get; set; }
            public KeySetting Menu { get; set; }
            public KeySetting ShowFps { get; set; }
            public KeySetting CapFps { get; set; }
        }

        public class MovementSettings
        {
            public string Left { get; set; }
            public string Right { get; set; }
            public string Jump { get; set; }
        }

        public class KeySetting
        {
            public string Key { get; set; }
        }

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        public InputSettings inputSettings { get; private set; }
        private Keys leftKey, rightKey, jumpKey, menuKey, showFpsKey, limitFpsKey;
        private String userValuePath;
        private String defaultValuePath;

        private PlayerInputController() { }

        ///Singleton Pattern
        private static readonly Lazy<PlayerInputController> lazy = new(() => new PlayerInputController());

        /// <summary>
        ///     Singleton Pattern return the only instance there is
        /// </summary>
        public static PlayerInputController Instance => lazy.Value;

        public override void Update(GameTime gameTime)
        {
            if (currentKeyboardState.GetPressedKeys().Length > 0 && !lastKeyboardState.IsKeyDown(currentKeyboardState.GetPressedKeys()[0]))
            {
                // Raise the key press event
                KeyPressed(currentKeyboardState.GetPressedKeys()[0]);
            }

            if (IsLimitFpsKeyPressed())
            {
                LimitFpsKeyPressed?.Invoke();
            }

            if (IsMenuKeyPressed)
            {
                GamePaused = !GamePaused;
                GamePausedChanged?.Invoke(GamePaused);
            }

            if (IsShowFpsKeyPressed())
            {
                ShowFpsKeyPressed?.Invoke();
            }
        }

        public void SetCurrentState()
        {
            currentKeyboardState = Keyboard.GetState();
        }

        public void SetLastState()
        {
            lastKeyboardState = currentKeyboardState;
        }

        private void KeyPressed(Keys key)
        {
            AnyKeyPress?.Invoke(key);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public override void Initialize()
        {
            var assemblyLocation = Assembly.GetEntryAssembly().Location;
            var contentRootPath = Path.GetDirectoryName(assemblyLocation);

            if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
            {
                contentRootPath = Path.Combine(contentRootPath, "..", "Resources");
            }

            userValuePath = Path.Combine(contentRootPath, "Content", "InputUserValues.json");
            defaultValuePath = Path.Combine(contentRootPath, "Content", "InputDefaults.json");


            if (File.Exists(userValuePath))
            {
                Load(userValuePath);
                UpdateKeys();
            }
            else
            {
                Load(defaultValuePath);
                UpdateKeys();
            }
        }

        public override void Destroy() { }

        public void setInputSettings(InputSettings inputSettings)
        {
            this.inputSettings = inputSettings;
        }

        public void LoadUserSettings()
        {
            Load(userValuePath);
        }

        private void Load(string fileName)
        {
            string json = File.ReadAllText(fileName);
            inputSettings = JsonConvert.DeserializeObject<InputSettings>(json);
            UpdateKeys();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(inputSettings, Formatting.Indented);
            File.WriteAllText(userValuePath, json);
            UpdateKeys();
        }

        public void ResetToDefaults()
        {
            Load(defaultValuePath);
            UpdateKeys();
        }

        private void UpdateKeys()
        {
            // Convert the MovementSettings keys
            Enum.TryParse(inputSettings.Movement.Left, out leftKey);
            Enum.TryParse(inputSettings.Movement.Right, out rightKey);
            Enum.TryParse(inputSettings.Movement.Jump, out jumpKey);
            Enum.TryParse(inputSettings.Menu.Key, out menuKey);
            Enum.TryParse(inputSettings.ShowFps.Key, out showFpsKey);
            Enum.TryParse(inputSettings.CapFps.Key, out limitFpsKey);
        }

        public bool IsWalkingRight()
        {
            return currentKeyboardState.IsKeyDown(rightKey);
        }

        public bool IsWalkingLeft()
        {
            return currentKeyboardState.IsKeyDown(leftKey);
        }

        public bool IsJumping()
        {
            return currentKeyboardState.IsKeyDown(jumpKey);
        }

        public bool IsMenuKeyPressed => IsKeyPressed(menuKey);

        public bool IsShowFpsKeyPressed()
        {
            return IsKeyPressed(showFpsKey);
        }

        public bool IsLimitFpsKeyPressed()
        {
            return IsKeyPressed(limitFpsKey);
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
        }
    }
}

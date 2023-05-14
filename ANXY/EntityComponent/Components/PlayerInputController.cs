using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO Player Input Controller
    /// Checking for Keyboard input and setting everything accordingly. Every Class needing to check the Keyboard input must check it through this Class.
    /// </summary>
    public class PlayerInputController : Component
    {
        public class InputSettings
        {
            public MovementSettings Movement { get; set; }
            public KeySetting Jump { get; set; }
            public KeySetting Menu { get; set; }
            public KeySetting ShowFps { get; set; }
            public KeySetting CapFps { get; set; }
        }

        public class MovementSettings
        {
            public string Up { get; set; }
            public string Down { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
        }

        public class KeySetting
        {
            public string Key { get; set; }
        }

        private KeyboardState currentKeyboardState;
        private InputSettings inputSettings;
        private Keys upKey, downKey, leftKey, rightKey, jumpKey, menuKey, showFpsKey, capFpsKey;


        /*
        private String userValuePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contents\\Resources\\Content\\InputUserValues.json");
        private String defaultValuePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contents\\Resources\\Content\\InputDefaults.json");*/
        private Dictionary<Keys, bool> lastKeyState;


        ///Singleton Pattern
        private static readonly Lazy<PlayerInputController> lazy = new(() => new PlayerInputController());
        private PlayerInputController() { Initialize(); }

        /// <summary>
        ///     Singleton Pattern return the only instance there is
        /// </summary>
        public static PlayerInputController Instance => lazy.Value;

        public override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            if (IsMenuKeyPressed())
            {
                EntitySystem.Instance.FindEntityByType<Player>()[0].GetComponent<Player>().IsActive =
                    !EntitySystem.Instance.FindEntityByType<Player>()[0].GetComponent<Player>().IsActive;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void Initialize()
        {
            // Access the content files
            string assemblyLocation = Assembly.GetEntryAssembly().Location;
            string contentRootPath = Path.GetDirectoryName(assemblyLocation);

            if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
            {
                contentRootPath = Path.Combine(contentRootPath, "..", "Resources");
            }

            string filePath = Path.Combine(contentRootPath,"Content","InputUserValues.json");

            /*
            // Get the base directory path of the application
            string baseDirectory = AppContext.BaseDirectory;

            // Access a file within the app bundle
            string filePath = Path.Combine(baseDirectory.ToString());
            if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS()) 
            {
                filePath = Path.Combine(filePath, "Contents", "Resources"); 
            }
            filePath = Path.Combine(filePath, "Content", "InputUserValues.json");

            
            // Read the contents of the file*/
            Load(filePath);
            UpdateKeys();
            

            /*
            if (File.Exists(userValuePath))
            {
                Load(userValuePath);
                UpdateKeys();
            }
            else
            {
                Load(defaultValuePath);
                UpdateKeys();
            }*/

            lastKeyState = new Dictionary<Keys, bool>();
            Keys[] keys = { leftKey, rightKey, jumpKey, menuKey, showFpsKey, capFpsKey };
            foreach (var key in keys)
            {
                lastKeyState.Add(key, false);
            }
            Debug.WriteLine("Hello");
        }

        public override void Destroy()
        {
        }

        public void Load(string fileName)
        {
            //var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string json = File.ReadAllText(fileName);
            inputSettings = JsonConvert.DeserializeObject<InputSettings>(json);
            UpdateKeys();
        }

        public void Save()
        {
        }

        public void ResetToDefaults()
        {
        }

        private void UpdateKeys()
        {
            // Convert the MovementSettings keys
            Enum.TryParse(inputSettings.Movement.Up, out upKey);
            Enum.TryParse(inputSettings.Movement.Down, out downKey);
            Enum.TryParse(inputSettings.Movement.Left, out leftKey);
            Enum.TryParse(inputSettings.Movement.Right, out rightKey);
            Enum.TryParse(inputSettings.Jump.Key, out jumpKey);
            Enum.TryParse(inputSettings.Menu.Key, out menuKey);
            Enum.TryParse(inputSettings.ShowFps.Key, out showFpsKey);
            Enum.TryParse(inputSettings.CapFps.Key, out capFpsKey);
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

        public bool IsMenuKeyPressed()
        {
            return IsKeyPressed(menuKey);
        }

        public bool IsShowFpsKeyPressed()
        {
            return IsKeyPressed(showFpsKey);
        }

        public bool IsCapFpsKeyPressed()
        {
            return IsKeyPressed(capFpsKey);
        }

        private bool IsKeyPressed(Keys key)
        {
            var currentState = currentKeyboardState.IsKeyDown(key);

            var oldState = false;
            lastKeyState.TryGetValue(key, out oldState);
            if (currentState && !oldState)
            {
                lastKeyState[key] = currentState;
                return true;
            }
            lastKeyState[key] = currentState;
            return false;
        }
    }
}

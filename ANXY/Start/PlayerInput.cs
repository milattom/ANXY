using ANXY.ECS.Components;
using ANXY.ECS.Systems;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ANXY.Start;

/// <summary>
/// TODO Player Input Controller
/// Checking for Keyboard input and setting everything accordingly. Every Class needing to check the Keyboard input must check it through this Class.
/// </summary>
public class PlayerInput
{
    public event Action ShowFpsKeyPressed;
    public event Action LimitFpsKeyPressed;
    public event Action FullscreenKeyPressed;
    public event Action<Keys> AnyKeyPressed;
    public event Action MovementKeyPressed;

    public class InputSettings
    {
        public MovementSettings Movement { get; set; }
        public KeySetting Menu { get; set; }
        public KeySetting ShowFps { get; set; }
        public KeySetting CapFps { get; set; }
        public KeySetting Fullscreen { get; set; }
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
    private Keys leftKey, rightKey, jumpKey, menuKey, showFpsKey, limitFpsKey, fullscreenKey;
    private List<Keys> keys = new List<Keys>();
    private string userValuePath;
    private string defaultValuePath;

    ///Singleton Pattern
    private static readonly Lazy<PlayerInput> lazy = new(() => new PlayerInput());

    /// <summary>
    ///     Singleton Pattern return the only instance there is
    /// </summary>
    public static PlayerInput Instance => lazy.Value;

    public void Initialize()
    // Access the content files
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;

        string contentRootPath = Path.GetDirectoryName(assemblyLocation);
        string tempLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        Debug.WriteLine("\nThis is temp: " + tempLocation + "\n");

        string tempFolderANXY = Path.Combine(tempLocation, "ANXY");
        Directory.CreateDirectory(tempFolderANXY);
        string tempFilePath = Path.Combine(tempFolderANXY, "InputUserValues.json");
        if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
        {
            contentRootPath = Path.Combine(contentRootPath, "..", "Resources");
        }

        string assemblyFilePath = Path.Combine(contentRootPath, "Content", "InputDefaults.json");

        if (!File.Exists(tempFilePath))
        {
            File.Copy(assemblyFilePath, tempFilePath);
        }
        userValuePath = tempFilePath;
        defaultValuePath = assemblyFilePath;

        if (File.Exists(userValuePath))
        {
            Load(userValuePath);
        }
        else
        {
            ResetToDefaults();
        }
    }

    public void Update(GameTime gameTime)
    {
        SetCurrentState();

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
            ANXYGame.Instance.SetGamePaused(!ANXYGame.Instance.GamePaused);
        }

        if (IsShowFpsKeyPressed())
        {
            ShowFpsKeyPressed?.Invoke();
        }

        if (IsFullscreenPressed())
        {
            FullscreenKeyPressed?.Invoke();
        }

        if (UIManager.Instance.ShowWelcomeAndTutorial)
        {
            if (IsMovementKeyPressed())
            {
                MovementKeyPressed?.Invoke();
            }
        }

        SetLastState();
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
        AnyKeyPressed?.Invoke(key);
    }

    private bool IsMovementKeyPressed()
    {
        return IsWalkingLeft() || IsWalkingRight() || IsJumping();
    }

    private void Load(string fileName)
    {
        string json = File.ReadAllText(fileName);
        inputSettings = JsonConvert.DeserializeObject<InputSettings>(json);
        try
        {
            UpdateKeys();
        }
        catch
        {
            ResetToDefaults();
        }
    }

    public void SetInputSettings(InputSettings inputSettings)
    {
        this.inputSettings = inputSettings;
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(inputSettings, Formatting.Indented);
        File.WriteAllText(userValuePath, json);
        UpdateKeys();
    }

    public void ResetToDefaults()
    {
        File.Delete(userValuePath);
        File.Copy(defaultValuePath, userValuePath);
        Load(userValuePath);
    }

    private void UpdateKeys()
    {
        // Convert the MovementSettings keys
        keys.Clear();
        Enum.TryParse(inputSettings.Movement.Left, out leftKey);
        keys.Add(leftKey);
        Enum.TryParse(inputSettings.Movement.Right, out rightKey);
        keys.Add(rightKey);
        Enum.TryParse(inputSettings.Movement.Jump, out jumpKey);
        keys.Add(jumpKey);
        Enum.TryParse(inputSettings.Menu.Key, out menuKey);
        keys.Add(menuKey);
        Enum.TryParse(inputSettings.ShowFps.Key, out showFpsKey);
        keys.Add(showFpsKey);
        Enum.TryParse(inputSettings.CapFps.Key, out limitFpsKey);
        keys.Add(limitFpsKey);
        Enum.TryParse(inputSettings.Fullscreen.Key, out fullscreenKey);
        keys.Add(fullscreenKey);
        if (keys.Contains(Keys.None))
        {
            throw new Exception("One or more keys are not set in the InputUserValues.json file");
        }
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

    public bool IsFullscreenPressed()
    {
        return IsKeyPressed(fullscreenKey);
    }

    private bool IsKeyPressed(Keys key)
    {
        return currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
    }
}
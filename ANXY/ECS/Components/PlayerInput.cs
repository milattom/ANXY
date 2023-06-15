using ANXY.ECS.Systems;
using ANXY.Start;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ANXY.ECS.Components;

/// <summary>
/// TODO Player Input Controller
/// Checking for Keyboard input and setting everything accordingly. Every Class needing to check the Keyboard input must check it through this Class.
/// </summary>
public class PlayerInput : Component
{
    public event Action DebugToggleKeyPressed;
    public event Action FpsCapKeyPressed;
    public event Action FpsToggleShowKeyPressed;
    public event Action FullscreenKeyPressed;
    public event Action AnyMovementKeyPressed;
    public event Action<Keys> AnyKeyPressed;

    public class InputKeyStrings
    {
        public DebugKeys Debug { get; set; } = new DebugKeys();
        public FpsKeys Fps { get; set; } = new FpsKeys();
        public GeneralKeys General { get; set; } = new GeneralKeys();
        public MovementKeys Movement { get; set; } = new MovementKeys();
    }
    public class DebugKeys
    {
        public string Toggle;
    }
    public class FpsKeys
    {
        public string Cap;
        public string ToggleShow;
    }
    public class GeneralKeys
    {
        public string Fullscreen;
        public string Menu;
    }
    public class MovementKeys
    {
        public string Jump;
        public string Left;
        public string Right;
    }

    private KeyboardState currentKeyboardState;
    private KeyboardState lastKeyboardState;
    public InputKeyStrings InputSettings { get; private set; }
    private Keys fpsCapKey, fpsToggleShowKey, debugKey, fullscreenKey, menuKey, movementJumpKey, movementLeftKey, movementRightKey;
    private String userValuePath;
    private String defaultValuePath;
    private bool IsMenuKeyDisabled = false;

    private PlayerInput()
    {
        PlayerInputSystem.Instance.Register(this);
    }

    ///Singleton Pattern
    private static readonly Lazy<PlayerInput> lazy = new(() => new PlayerInput());

    /// <summary>
    ///     Singleton Pattern return the only instance there is
    /// </summary>
    public static PlayerInput Instance => lazy.Value;

    public override void Update(GameTime gameTime)
    {
        SetCurrentState();

        if (currentKeyboardState.GetPressedKeys().Length > 0 && !lastKeyboardState.IsKeyDown(currentKeyboardState.GetPressedKeys()[0]))
        {
            KeyPressed(currentKeyboardState.GetPressedKeys()[0]);
        }
        if (WasDebugToggleKeyJustPressed)
            DebugToggleKeyPressed?.Invoke();
        if (WasFpsCapKeyJustPressed)
            FpsCapKeyPressed?.Invoke();
        if (WasFpsToggleShowKeyJustPressed)
            FpsToggleShowKeyPressed?.Invoke();
        if (WasFullscreenKeyJustPressed)
            FullscreenKeyPressed?.Invoke();
        if (WasMenuKeyJustPressed())
            ANXYGame.Instance.SetGamePaused(!ANXYGame.Instance.GamePaused);

        if (IsMenuKeyDisabled && !UIManager.Instance.WaitingForKeyPress())
        {
            EnableMenuKey();
        }

        if (UIManager.Instance.ShowWelcomeAndTutorial && IsMovementKeyPressed())
        {
            AnyMovementKeyPressed?.Invoke();
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
        return IsWalkingRight || IsWalkingLeft || IsJumping;
    }

    public override void Initialize()
    // Access the content files
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;

        string contentRootPath = Path.GetDirectoryName(assemblyLocation);
        string tempLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

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

    private void Load(string fileName)
    {
        string json = File.ReadAllText(fileName);
        InputSettings = JsonConvert.DeserializeObject<InputKeyStrings>(json);
        try
        {
            UpdateKeys();
        }
        catch (Exception)
        {
            ResetToDefaults();
        }
    }

    public void SetInputSettings(InputKeyStrings inputSettings)
    {
        this.InputSettings = inputSettings;
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(InputSettings, Formatting.Indented);
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
        try
        {
            fpsCapKey = Enum.Parse<Keys>(InputSettings.Fps.Cap);
            fpsToggleShowKey = Enum.Parse<Keys>(InputSettings.Fps.ToggleShow);
            debugKey = Enum.Parse<Keys>(InputSettings.Debug.Toggle);
            fullscreenKey = Enum.Parse<Keys>(InputSettings.General.Fullscreen);
            menuKey = Enum.Parse<Keys>(InputSettings.General.Menu);
            movementJumpKey = Enum.Parse<Keys>(InputSettings.Movement.Jump);
            movementLeftKey = Enum.Parse<Keys>(InputSettings.Movement.Left);
            movementRightKey = Enum.Parse<Keys>(InputSettings.Movement.Right);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            throw new ApplicationException("Failed to update keys.", e);
        }
    }

// check input keys
    public bool WasDebugToggleKeyJustPressed => WasKeyJustPressed(debugKey);
    public bool WasFpsCapKeyJustPressed => WasKeyJustPressed(fpsCapKey);
    public bool WasFpsToggleShowKeyJustPressed => WasKeyJustPressed(fpsToggleShowKey);
    public bool WasFullscreenKeyJustPressed => WasKeyJustPressed(fullscreenKey);
    public bool WasMenuKeyJustPressed()
    {
        if (IsMenuKeyDisabled)
        {
            return false;
        }
        return WasKeyJustPressed(menuKey);
    }
    public bool IsWalkingRight => currentKeyboardState.IsKeyDown(movementRightKey);
    public bool IsWalkingLeft => currentKeyboardState.IsKeyDown(movementLeftKey);
    public bool IsJumping => currentKeyboardState.IsKeyDown(movementJumpKey);
    private bool WasKeyJustPressed(Keys key)
    {
        return currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
    }

    public void EnableMenuKey()
    {
        IsMenuKeyDisabled = false;
    }

    public void DisableMenuKey()
    {
        IsMenuKeyDisabled = true;
    }
}
using ANXY.ECS.Components;
using ANXY.ECS.Systems;
using ANXY.Start;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;

namespace ANXY.UI;

/// <summary>
///    Manages the UI elements. This class is a singleton.
/// </summary>
public class UIManager
{
    // The different UI overlays or UI screens.
    private readonly Desktop _desktop;
    private readonly InGameOverlay _inGameOverlay;
    private readonly PauseMenu _pauseMenu;
    private readonly ControlsMenu _controlsMenu;
    private readonly Credits _credits;

    private bool _showFps = false;
    private bool _showDebug = false;
    public bool ShowWelcomeAndTutorial { get; private set; } = true;

    // Singleton Pattern.
    private static readonly Lazy<UIManager> lazy = new(() => new UIManager());
    public static UIManager Instance => lazy.Value;

    /// <summary>
    ///    Singleton Pattern: Private constructor to prevent instantiation.
    /// </summary>
    private UIManager()
    {
        // Pause Menu
        _pauseMenu = new PauseMenu();
        _pauseMenu.ResumePressed += OnResumeBtnPressed;
        _pauseMenu.ResetGamePressed += OnResetGameBtnPressed;
        _pauseMenu.ControlsPressed += OnControlsBtnPressed;
        _pauseMenu.CreditsPressed += OnCreditsBtnPressed;
        _pauseMenu.ExitGamePressed += OnExitGamePressed;

        // Controls Menu
        _controlsMenu = new ControlsMenu();
        _controlsMenu.ReturnPressed += OnReturnClicked;
        _controlsMenu.LoadDefaultsPressed += OnLoadDefaultsClicked;
        _controlsMenu.SaveChangesPressed += OnSaveChangesClicked;

        //InGameOverlay
        _inGameOverlay = new InGameOverlay();
        _inGameOverlay.BtnResetGame.Click += OnResetGameBtnPressed;
        _inGameOverlay.BtnEndGame.Click += OnExitGamePressed;

        // Credits
        _credits = new Credits();
        _credits.ReturnPressed += OnReturnClicked;


        // Desktop is the root of UI
        _desktop = new Desktop
        {
            Root = _inGameOverlay
        };
        _desktop.Root.Visible = true;

        // Event handlers
        ANXYGame.Instance.GamePausedChanged += OnGamePausedChanged;
        PlayerInput.Instance.FpsToggleShowKeyPressed += OnFpsToggleShowKeyPressed;
        PlayerInput.Instance.DebugToggleKeyPressed += OnDebugToggleKeyPressed;
        PlayerInput.Instance.AnyMovementKeyPressed += OnStartMoving;
        PlayerSystem.Instance.GetFirstComponent().Entity.GetComponent<Player>().EndReached += OnEndReached;
    }

    public void Update(GameTime gameTime)
    {
        UpdateFPS(gameTime);
    }

    /// <summary>
    ///    Calculates the current FPS rate and update the FPS overlay.
    /// </summary>
    /// <param name="gameTime"></param>
    public void UpdateFPS(GameTime gameTime)
    {
        if (_desktop.Root == _inGameOverlay)
        {
            _inGameOverlay.Update(gameTime);
        }
    }

    /// <summary>
    ///    Draws the UI. UI root needs to be set to the desired UI elements before calling this method.
    /// </summary>
    public void Draw()
    {
        _desktop.Render();
    }

    // Event handlers.
    /// <summary>
    ///     Resumes the game when the resume button is pressed.
    /// </summary>
    private void OnResumeBtnPressed()
    {
        ANXYGame.Instance.SetGamePaused(false);
    }

#nullable enable
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnResetGameBtnPressed(object? sender, EventArgs? e)
    {
        OnResetGameBtnPressed();
    }
#nullable disable

    /// <summary>
    /// Restarts the game from the beginning.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnResetGameBtnPressed()
    {
        var playerSystem = (PlayerSystem)SystemManager.Instance.FindSystemByType<Player>();
        playerSystem.Reset();

        var camera = (Camera)SystemManager.Instance.FindSystemByType<Camera>().GetFirstComponent();
        camera.Reset();

        _inGameOverlay.Reset();
        ShowWelcomeAndTutorial = true;
        PlayerInput.Instance.AnyMovementKeyPressed += OnStartMoving;
        ANXYGame.Instance.SetGamePaused(false);
    }

    /// <summary>
    ///     Shows the controls menu.
    /// </summary>
    private void OnControlsBtnPressed()
    {
        _controlsMenu.LoadButtonLayout();
        _desktop.Root = _controlsMenu;
    }

    /// <summary>
    ///     Shows the credits of the game.
    /// </summary>
    private void OnCreditsBtnPressed()
    {
        _desktop.Root = _credits;
    }

    /// <summary>
    ///     Shows the pause menu on game pause.
    /// </summary>
    /// <param name="gamePaused"></param>
    private void OnGamePausedChanged(bool gamePaused)
    {
        if (gamePaused)
        {
            _desktop.Root = _pauseMenu;
        }
        else
        {
            _desktop.Root = _inGameOverlay;
        }
    }

    /// <summary>
    ///     Resumes the game.
    /// </summary>
    private void OnReturnClicked()
    {
        _desktop.Root = _pauseMenu;
        _inGameOverlay.RewriteTutorialText();
    }

    /// <summary>
    ///     Loads default keyboard input settings.
    /// </summary>
    private void OnLoadDefaultsClicked()
    {
        PlayerInput.Instance.ResetToDefaults();
    }

    /// <summary>
    ///     Saves the current keyboard input settings from the controls menu.
    /// </summary>
    private void OnSaveChangesClicked()
    {
        PlayerInput.Instance.Save();
    }

#nullable enable
    public void OnExitGamePressed(object? sender, EventArgs? e)
    {
        OnExitGamePressed();
    }
#nullable disable

    /// <summary>
    ///     Stops the game and exits the application.
    /// </summary>
    public static void OnExitGamePressed()
    {
        Environment.Exit(0);
    }

    /// <summary>
    ///     Toggles the FPS overlay.
    /// </summary>
    private void OnFpsToggleShowKeyPressed()
    {
        if (ANXYGame.Instance.GamePaused)
        {
            return;
        }

        _showFps = !_showFps;
        _inGameOverlay.ShowFps(_showFps);
        _inGameOverlay.ResetFpsUI();
    }

    private void OnDebugToggleKeyPressed()
    {
        if (ANXYGame.Instance.GamePaused)
        {
            return;
        }

        _showDebug = !_showDebug;
        _inGameOverlay.ShowDebug(_showDebug);
    }

    private void OnStartMoving()
    {
        ShowWelcomeAndTutorial = false;
        _inGameOverlay.ShowWelcomeAndTutorial(ShowWelcomeAndTutorial);

        PlayerInput.Instance.AnyMovementKeyPressed -= OnStartMoving;
    }

    private void OnEndReached()
    {
        _inGameOverlay.ShowEndReached(true);
    }

    public bool WaitingForKeyPress()
    {
        return _controlsMenu.lblWaitingForKeyPress.Visible;
    }
}
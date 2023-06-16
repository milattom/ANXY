using ANXY.ECS;
using ANXY.ECS.Components;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Myra;
using System;
using static ANXY.Start.EntityFactory;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ANXY.Start;

/// <summary>
///    ANXYGame is the main class of the game.
/// </summary>
public class ANXYGame : Game
{
    /// <summary>
    ///     Event that is fired when the game is paused or unpaused.
    /// </summary>
    public event Action<bool> GamePausedChanged;

    /// <summary>
    ///     Shows if the game is paused or not.
    /// </summary>
    public bool GamePaused { get; private set; } = false;

    // Window size, style.
    private readonly GraphicsDeviceManager _graphics;
    private readonly Rectangle _screenBounds;
    private Rectangle _1080pSize = new(0, 0, 1920, 1080);
    private int _windowHeight;
    private int _windowWidth;

    // Game Map: TileSet, TileMap, Background, Layers.
    private SpriteBatch _spriteBatch;
    private TiledMap _levelTileMap;
    private Texture2D _backgroundSprite;
    private readonly string[] _backgroundLayerNames = { "Ground" };
    private readonly string[] _foregroundLayerNames = { "" };

    // Player: Sprite.
    private Texture2D _playerSprite;

    // Content: Root Directory.
    private readonly string contentRootDirectory = "Content";

    // Singelton Pattern.
    private static readonly Lazy<ANXYGame> lazy = new(() => new ANXYGame());
    public static ANXYGame Instance => lazy.Value;

    /// <summary>
    ///    Singleton Pattern: Private constructor to prevent instantiation.
    /// </summary>
    private ANXYGame()
    {
        // graphics & window setup.
        _screenBounds = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea;

        _graphics = new GraphicsDeviceManager(this);
        if (_screenBounds.Width > _1080pSize.Width
            && _screenBounds.Height > _1080pSize.Height)
        {
            _graphics.PreferredBackBufferWidth = _1080pSize.Width;
            _graphics.PreferredBackBufferHeight = _1080pSize.Height;
        }
        else
        {
            _graphics.PreferredBackBufferWidth = _screenBounds.Width - 100;
            _graphics.PreferredBackBufferHeight = _screenBounds.Height - 100;
        }
        Content.RootDirectory = contentRootDirectory;

        _windowWidth = _graphics.PreferredBackBufferWidth;
        _windowHeight = _graphics.PreferredBackBufferHeight;

        _graphics.GraphicsProfile = GraphicsProfile.HiDef;

        // VSync off.
        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = false;

        IsMouseVisible = false;

        Window.AllowUserResizing = true;
        _graphics.HardwareModeSwitch = true;
        _graphics.IsFullScreen = true;

        // Apply window properties (mouse visible, vsync, window size, etc).
        _graphics.ApplyChanges();
    }

    // Most important methods: Initialize, LoadContent, Update, Draw. (Called in this order)
    /// <summary>
    ///     Perform any initialization before starting to run (runs before LoadContent).
    ///     Run any queries for any required services, load non-graphic realted content.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        CreateDefaultScene();
        SystemManager.Instance.InitializeAll();

        // Center window on screen and set size.
        var xOffset = (_screenBounds.Width - _windowWidth) / 2;
        var yOffset = (_screenBounds.Height - _windowHeight) / 2;
        Window.Position = new Point(xOffset, yOffset);

        // Register event handlers.
        GamePausedChanged += OnGamePausedChanged;
        Window.ClientSizeChanged += OnClientSizeChanged;
    }

    /// <summary>
    ///     Load all content. Called once at the start of the game (after Initialize).
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch. Load background picture, level tile map and player sprite.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundSprite = Content.Load<Texture2D>("Background-2");
        _levelTileMap = Content.Load<TiledMap>("JumpNRun-1");
        _playerSprite = Content.Load<Texture2D>("playerAtlas");

        // Load Myra.
        MyraEnvironment.Game = this;
    }

    /// <summary>
    ///     Runs once per game loop.
    ///     Everything that needs updating is called here.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        SystemManager.Instance.UpdateAll(gameTime);
    }

    /// <summary>
    ///     Draws the game.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        // Background color, if there is nothing to be shown.
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Sprite batch that holds and draws all sprites.
        _spriteBatch.Begin();
        SystemManager.Instance.DrawAll(gameTime, _spriteBatch);
        _spriteBatch.End();

        // Update UI and draw
        UIManager.Instance.Update(gameTime);
        UIManager.Instance.Draw();
    }

    // Other methods.
    /// <summary>
    ///     Initializes the default scene.
    /// </summary>
    private void CreateDefaultScene()
    {
        CreateBackground();
        CreateBackgroundLayers();
        CreatePlayerInput();
        CreatePlayer();
        CreateCamera();
        CreateForegroundLayers();
    }

    /// <summary>
    ///     Create a Background.
    /// </summary>
    private void CreateBackground()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Background, new Object[] { _windowHeight, _windowWidth, _backgroundSprite });
    }

    /// <summary>
    ///     Create all Layers that are set behind the player.
    /// </summary>
    private void CreateBackgroundLayers()
    {
        foreach (String layerName in _backgroundLayerNames)
        {
            InitializeLevelLayer(layerName);
        }
    }

    /// <summary>
    ///     Create all Layers that are set in front of the player (overlapping/hiding the player).
    /// </summary>
    private void CreateForegroundLayers()
    {
        foreach (String layerName in _foregroundLayerNames)
        {
            InitializeLevelLayer(layerName);
        }
    }

    /// <summary>
    ///     Adds all tiles of layer with a given layer name as entities to the EntitySystem.
    /// </summary>
    /// <param name="layerName">Layers with this name will be added.</param>
    private void InitializeLevelLayer(String layerName)
    {
        var tilesIndex = GetLayerIndexByLayerName(layerName);
        if (tilesIndex == -1)
        {
            return;
        }

        var tiles = _levelTileMap.TileLayers[tilesIndex].Tiles;

        // Iterate over all tiles in the layer.
        foreach (var singleTile in tiles)
        {
            EntityFactory.Instance.CreateEntity(EntityType.Tile, new Object[] { singleTile, layerName, _levelTileMap });
        }
    }

    /// <summary>
    ///     For a given layer name, return the index of the layer in the level tile map.
    /// </summary>
    /// <param name="layerName">Name of the layer.</param>
    /// <returns>Index of layer. -1 if nothing found.</returns>
    private int GetLayerIndexByLayerName(String layerName)
    {
        var index = 0;
        foreach (var layer in _levelTileMap.Layers)
        {
            if (layerName != null && layer.Name == layerName)
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    /// <summary>
    ///     Initialize Player Input.
    /// </summary>
    private void CreatePlayerInput()
    {
        PlayerInput.Instance.LimitFpsKeyPressed += ToggleFpsLimit;
        PlayerInput.Instance.FullscreenKeyPressed += ToggleFullscreen;
    }

    /// <summary>
    ///     Creates and initializes Player.
    /// </summary>
    private void CreatePlayer()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Player, new Object[] { _playerSprite });
    }

    /// <summary>
    ///    Creates and initializes Camera.
    /// </summary>
    private void CreateCamera()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Camera, new Object[] { _windowHeight, _windowWidth });
    }

    /// <summary>
    ///    Toggle between fixed and variable time step, VSync.
    /// </summary>
    private void ToggleFpsLimit()
    {
        _graphics.SynchronizeWithVerticalRetrace = !_graphics.SynchronizeWithVerticalRetrace;
        IsFixedTimeStep = !IsFixedTimeStep;

        _graphics.ApplyChanges();
    }

    /// <summary>
    ///    Toggle between fullscreen and windowed mode.
    /// </summary>
    public void ToggleFullscreen()
    {
        if (GamePaused)
        {
            return;
        }
        _graphics.IsFullScreen = !_graphics.IsFullScreen;
        _graphics.ApplyChanges();
    }

    /// <summary>
    ///    Pause or unpause the game.
    /// </summary>
    /// <param name="gamePaused">Is the game paused or running? -> paused = true</param>
    public void SetGamePaused(bool gamePaused)
    {
        GamePaused = gamePaused;
        GamePausedChanged?.Invoke(gamePaused);
    }

    // Event handlers.
    /// <summary>
    ///     Handle window resize by user, reset the window size and center the camera.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void OnClientSizeChanged(object sender, EventArgs eventArgs)
    {
        _windowWidth = Window.ClientBounds.Width;
        _windowHeight = Window.ClientBounds.Height;
        var camera = (Camera)SystemManager.Instance.FindSystemByType<Camera>().GetFirstComponent();
        camera._windowDimensions = new Vector2(_windowWidth, _windowHeight);
    }

    /// <summary>
    ///    Handle game pause state change, set mouse visibility and apply changes.
    /// </summary>
    /// <param name="gamePaused">Is the game paused or running? -> paused = true</param>
    private void OnGamePausedChanged(bool gamePaused)
    {
        IsMouseVisible = gamePaused;
        _graphics.ApplyChanges();
    }
}

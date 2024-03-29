﻿using ANXY.ECS.Components;
using ANXY.ECS.Systems;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Myra;
using System;
using System.Collections.Generic;
using System.Linq;
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
    private bool _debugActive = false;

    // Window size, style.
    private readonly GraphicsDeviceManager _graphics;
    private readonly Rectangle _screenBounds;
    private Rectangle _1080pSize = new(0, 0, 1920, 1080);
    public int WindowHeight { get; private set; }
    public int WindowWidth { get; private set; }

    // Game Map: TileSet, TileMap, Background, Layers.
    private SpriteBatch _spriteBatch;
    private TiledMap _levelTileMap;
    private Texture2D _backgroundSprite;
    private readonly string[] _backgroundLayerNames = { "Ground", "BehindPlayer" };
    public readonly string[] _foregroundLayerNames = { "InFrontOfPlayer" };
    private readonly string[] _spawnLayerNames = { "Spawn" };
    private readonly string[] _dogSpawnLayerNames = { "Dog" };
    private readonly string[] _endLayerNames = { "End" };
    public Vector2 GameLoadSpawnPosition { get; private set; } = Vector2.Zero;
    public Vector2 GameLoadDogSpawnPosition { get; private set; } = Vector2.Zero;
    public Vector2 SpawnPosition { get; private set; } = Vector2.Zero;
    public Vector2 DogSpawnPosition { get; private set; } = Vector2.Zero;


    // Player: Sprite.
    private Texture2D _playerSprite;

    // Dog: Sprite.
    private Texture2D _dogSprite;

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

        WindowWidth = _graphics.PreferredBackBufferWidth;
        WindowHeight = _graphics.PreferredBackBufferHeight;

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
        PlayerInput.Instance.Initialize();

        // Center window on screen and set size.
        var xOffset = (_screenBounds.Width - WindowWidth) / 2;
        var yOffset = (_screenBounds.Height - WindowHeight) / 2;
        Window.Position = new Point(xOffset, yOffset);

        // Register event handlers.
        GamePausedChanged += OnGamePausedChanged;
        Window.ClientSizeChanged += OnClientSizeChanged;
        PlayerSystem.Instance.GetFirstComponent().Entity.GetComponent<Player>().EndReached += OnEndReached;

        ToggleFpsLimit();
    }

    /// <summary>
    ///     Load all content. Called once at the start of the game (after Initialize).
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch. Load background picture, level tile map and player sprite.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundSprite = Content.Load<Texture2D>("Background-2");
        _levelTileMap = Content.Load<TiledMap>("./Tiled/FeedTheDog/FeedTheDog-TileMap");
        _playerSprite = Content.Load<Texture2D>("playerAtlas");
        _dogSprite = Content.Load<Texture2D>("dogAtlas");

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
        PlayerInput.Instance.Update();
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

        UIManager.Instance.Update(gameTime);
        UIManager.Instance.Draw();
        // Update UI and draw
    }

    // Other methods.
    /// <summary>
    ///     Initializes the default scene.
    /// </summary>
    private void CreateDefaultScene()
    {
        CreateBackground();
        CreateBackgroundLayers();

        SetDogSpawn();
        SetSpawn();
        SetEnd();

        CreatePlayerInput();
        CreatePlayer();
        CreateCamera();
        CreateDog();

        CreateForegroundLayers();
        ToggleFpsLimit();
    }

    /// <summary>
    ///     Create a Background.
    /// </summary>
    private void CreateBackground()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Background, new Object[] { WindowHeight, WindowWidth, _backgroundSprite });
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
    ///     Create all Layers that are set behind the player.
    /// </summary>
    private void SetSpawn()
    {
        foreach (String layerName in _spawnLayerNames)
        {
            var tileIndexes = GetLayerIndexByLayerName(layerName);
            if (tileIndexes.Count == 0)
                return;

            foreach(var currentTileIndex in tileIndexes)
            {
                var tiles = _levelTileMap.TileLayers[currentTileIndex].Tiles;

                foreach (var singleTile in tiles)
                {
                    if (singleTile.GlobalIdentifier == 0)
                        continue;

                    GameLoadSpawnPosition = new Vector2(singleTile.X * _levelTileMap.TileWidth, singleTile.Y * _levelTileMap.TileHeight);
                    SpawnPosition = new Vector2(GameLoadSpawnPosition.X, (singleTile.Y + 2) * _levelTileMap.TileHeight);
                    return;
                }
            }
        }
        GameLoadSpawnPosition = new Vector2(1200, 540);
    }

    /// <summary>
    ///     Create all Layers that are set behind the player.
    /// </summary>
    private void SetDogSpawn()
    {
        foreach (String layerName in _dogSpawnLayerNames)
        {
            var tileIndexes = GetLayerIndexByLayerName(layerName);
            if (tileIndexes.Count == 0)
                return;

            foreach (var currentTileIndex in tileIndexes)
            {
                var tiles = _levelTileMap.TileLayers[currentTileIndex].Tiles;

                foreach (var singleTile in tiles)
                {
                    if (singleTile.GlobalIdentifier == 0)
                        continue;

                    GameLoadDogSpawnPosition = new Vector2(singleTile.X * _levelTileMap.TileWidth, singleTile.Y* _levelTileMap.TileHeight-6);
                    return;
                }
            }
        }
        GameLoadDogSpawnPosition = new Vector2(1200, 540);
    }

    /// <summary>
    ///     Create all Layers that are set behind the player.
    /// </summary>
    private void SetEnd()
    {
        foreach (String layerName in _endLayerNames)
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
        var tileIndexes = GetLayerIndexByLayerName(layerName);
        if (tileIndexes.Count == 0)
            return;

        EntityType entityType = _foregroundLayerNames.Contains(layerName) ? EntityType.ForegroundTile : EntityType.BackgroundTile;

        foreach (var currentTileIndex in tileIndexes)
        {
            var tiles = _levelTileMap.TileLayers[currentTileIndex].Tiles;
            var renderSprite = _levelTileMap.TileLayers[currentTileIndex].IsVisible;

            // Iterate over all tiles in the layer.
            var nonemptyTiles = tiles.Where(tile => tile.GlobalIdentifier > 0).ToList();

            nonemptyTiles.ForEach(tile => EntityFactory.Instance.CreateEntity(entityType, new Object[] { tile, layerName, _levelTileMap, renderSprite }));
        }
    }

    /// <summary>
    ///     For a given layer name, return the index of the layer in the level tile map.
    /// </summary>
    /// <param name="layerName">Name of the layer.</param>
    /// <returns>Index of layer. -1 if nothing found.</returns>
    private List<int> GetLayerIndexByLayerName(String layerName)
    {
        List<int> layerIndices = new();

        var index = 0;
        foreach (var layer in _levelTileMap.Layers)
        {
            if (layerName != null && layer.Name.Contains(layerName))
            {
                layerIndices.Add(index);
            }
            index++;
        }
        return layerIndices;
    }

    /// <summary>
    ///     Initialize Player Input.
    /// </summary>
    private void CreatePlayerInput()
    {
        PlayerInput.Instance.FpsCapKeyPressed += ToggleFpsLimit;
        PlayerInput.Instance.FullscreenKeyPressed += ToggleFullscreen;
        PlayerInput.Instance.DebugToggleKeyPressed += SetDebugMode;
        PlayerInput.Instance.DebugSpawnNewPlayerPressed += OnDebugSpawnNewPlayerPressed;
    }

    /// <summary>
    ///     Creates and initializes Player.
    /// </summary>
    private void CreatePlayer()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Player, new Object[] { _playerSprite });
    }

    /// <summary>
    ///     Creates and initializes Player.
    /// </summary>
    private void CreateDog()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Dog, new Object[] { _dogSprite });
    }

    /// <summary>
    ///    Creates and initializes Camera.
    /// </summary>
    private void CreateCamera()
    {
        EntityFactory.Instance.CreateEntity(EntityType.Camera, new Object[] { WindowHeight, WindowWidth });
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

    private void SetDebugMode()
    {
        _debugActive = !_debugActive;
        BoxColliderSystem.ToggleDebugMode(GraphicsDevice, _debugActive);
    }

    private void OnDebugSpawnNewPlayerPressed()
    {
        PlayerFactory.CreatePlayers(10, _playerSprite);
        BoxColliderSystem.ToggleDebugMode(GraphicsDevice, _debugActive);
    }

    // Event handlers.
    /// <summary>
    ///     Handle window resize by user, reset the window size and center the camera.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void OnClientSizeChanged(object sender, EventArgs eventArgs)
    {
        WindowWidth = Window.ClientBounds.Width;
        WindowHeight = Window.ClientBounds.Height;
        SystemManager.Instance.UpdateResolution(new Vector2(WindowWidth, WindowHeight));
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

    private void OnEndReached()
    {
        IsMouseVisible = true;
    }
}

using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using ANXY.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Myra;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ANXY.Start;

/// <summary>
/// This is the main type for your game.
/// </summary>
public class ANXYGame : Game
{
    public bool DebugMode { get; set; } = true;
    private readonly GraphicsDeviceManager _graphics;
    private Texture2D _backgroundSprite;
    private TiledMap _levelTileMap;
    private Texture2D _playerSprite;
    private SpriteFont _arialSpriteFont;
    private SpriteBatch _spriteBatch;
    private readonly Rectangle _screenBounds;
    private Rectangle _1080pSize = new(0, 0, 1920, 1080);
    private int _windowHeight;
    private int _windowWidth;
    private Entity _playerEntity;
    private Entity _cameraEntity;
    private readonly Vector2 _cameraPadding = new Vector2(1 / 5, 1 / 4);
    private readonly string[] _backgroundLayerNames = { "Ground" };
    private readonly string[] _foregroundLayerNames = { "" };
    private readonly string contentRootDirectory = "Content";
    private bool GamePlaying = true;

    ///Singleton Pattern
    private static readonly Lazy<ANXYGame> lazy = new(() => new ANXYGame());

    /// <summary>
    ///     Singleton Pattern return the only instance there is
    /// </summary>
    public static ANXYGame Instance => lazy.Value;

    /// <summary>
    /// This is the constructor for the heart of the game, where everything gets its initial spark.
    /// </summary>
    public ANXYGame()
    {
        IsMouseVisible = false;
        //_graphics.ToggleFullScreen();
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
            _graphics.PreferredBackBufferWidth = _screenBounds.Width;
            _graphics.PreferredBackBufferHeight = _screenBounds.Height;
        }
        //GraphicsDevice.Viewport.Bounds to get the current window size
        //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea to get screen resolution

        //makes it possible for the user to change the window size
        Window.AllowUserResizing = true;

        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = false;

        _graphics.ApplyChanges();

        Content.RootDirectory = contentRootDirectory;
    }


    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well. 
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        InitializeDefaultScene();

        EntitySystem.Instance._InitializeEntities();
        //Debug mode
        if (DebugMode)
        {
            BoxColliderSystem.Instance.EnableDebugMode(_graphics.GraphicsDevice);
        }
    }

    /// <summary>
    /// Initializes the default scene.
    /// </summary>
    private void InitializeDefaultScene()
    {
        _windowWidth = Window.ClientBounds.Width;
        _windowHeight = Window.ClientBounds.Height;
        InitializeBackground();

        InitializeLevelBackgroundLayers();

        InitializeInputController();
        InitializePlayer();

        InitializeLevelForegroundLayers();
        AddPlayerToBackground();
        AddPlayerToLevel();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all content such as sprite batches, textures and maps.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _playerSprite = Content.Load<Texture2D>("playerAtlas");
        _backgroundSprite = Content.Load<Texture2D>("Background-2");
        //_levelTileMap = Content.Load<TiledMap>("TileMapSet2");

        //TODO important level
        //_levelTileMap = Content.Load<TiledMap>("JumpNRun-1");
        _levelTileMap = Content.Load<TiledMap>("JumpNRun-1");

        //Load Fonts
        //Load Myra
        MyraEnvironment.Game = this;
        //_arialSpriteFont = Content.Load<SpriteFont>("Arial");

    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        PlayerInputController.Instance.SetCurrentState();
        EntitySystem.Instance._UpdateEntities(gameTime);
        PlayerInputController.Instance.SetLastState();
        //using the MonoGame gameEngine
        //base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        EntitySystem.Instance.DrawEntities(gameTime, _spriteBatch);
        _spriteBatch.End();

        UIManager.Instance.UpdateFPS(gameTime);
        UIManager.Instance.Draw();
    }

    private void InitializeInputController()
    {
        var playerInputControllerEntity = new Entity();
        EntitySystem.Instance.AddEntity(playerInputControllerEntity);
        playerInputControllerEntity.AddComponent(PlayerInputController.Instance);
        PlayerInputController.Instance.LimitFpsKeyPressed += ToggleFpsLimit;
        PlayerInputController.Instance.GamePausedChanged += ToggleMouseCursorShow;
        UIManager.Instance.PauseToggled += TogglePlayerActiveState;
    }

    /// <summary>
    /// creates the Player Entity. Sets position, Sprite, BoxCollider.
    /// </summary>
    private void InitializePlayer()
    {
        var playerEntity = new Entity { Position = new Vector2(1200, 540) };

        EntitySystem.Instance.AddEntity(playerEntity);

        var player = new Player();

        playerEntity.AddComponent(player);

        var playerSpriteRenderer = new PlayerSpriteRenderer(_playerSprite);
        playerEntity.AddComponent(playerSpriteRenderer);

        var playerBox = new Rectangle(1, 6, 32, 64);
        var playerCollider = new BoxCollider(playerBox, "Player");
        playerEntity.AddComponent(playerCollider);

        BoxColliderSystem.Instance.AddBoxCollider(playerCollider);

        var cameraEntity = new Entity();
        EntitySystem.Instance.AddEntity(cameraEntity);
        var camera = new Camera(player, new Vector2(_windowWidth, _windowHeight), new Vector2(0.25f * _windowWidth, 0.5f * _windowHeight), new Vector2(float.PositiveInfinity, 0.85f * _windowHeight));
        cameraEntity.AddComponent(camera);
    }

    /// <summary>
    /// Creates a Background Entity with Sprite and Background
    /// </summary>
    private void InitializeBackground()
    {
        var backgroundEntity = new Entity();
        EntitySystem.Instance.AddEntity(backgroundEntity);
        backgroundEntity.Position -= new Vector2(0.5f * _windowWidth, 0.5f * _windowHeight);
        backgroundEntity.AddComponent(new Background(_windowWidth, _windowHeight));
        SingleSpriteRenderer backgroundSprite = new SingleSpriteRenderer(_backgroundSprite);
        backgroundEntity.AddComponent(backgroundSprite);
        backgroundSprite.CameraEntity = _cameraEntity;
    }


    /// <summary>
    /// Layer to be drawn below Player. Player will walk over these
    /// </summary>
    private void InitializeLevelBackgroundLayers()
    {
        foreach (String layerName in _backgroundLayerNames)
        {
            InitializeLevelLayer(layerName);
        }
    }

    /// <summary>
    /// Layers to be drawn on top of Player. Player can hide or walk below these.
    /// </summary>
    private void InitializeLevelForegroundLayers()
    {
        foreach (String layerName in _foregroundLayerNames)
        {
            InitializeLevelLayer(layerName);
        }
    }

    /// <summary>
    /// Adds all tiles of layer with layerName as entities to the EntitySystem
    /// </summary>
    /// <param name="layerName">name of the layer</param>
    private void InitializeLevelLayer(String layerName)
    {
        var tilesIndex = GetLayerIndexByLayerName(layerName);
        if (tilesIndex == -1)
        {
            return;
        }

        var tiles = _levelTileMap.TileLayers[tilesIndex].Tiles;

        foreach (var singleTile in tiles)
        {
            if (singleTile.GlobalIdentifier == 0)
            {
                continue;
            }

            //Create Tile Entity
            var newTileEntity = new Entity
            {
                Position = new Vector2(singleTile.X * _levelTileMap.TileWidth, singleTile.Y * _levelTileMap.TileHeight)
            };
            EntitySystem.Instance.AddEntity(newTileEntity);
            newTileEntity.AddComponent(new Level());

            //Set Tile Sprite
            var tileSprite = new SingleSpriteRenderer(_levelTileMap.Tilesets[0].Texture, _levelTileMap.Tilesets[0].GetTileRegion(singleTile.GlobalIdentifier - 1));
            newTileEntity.AddComponent(tileSprite);

            tileSprite.CameraEntity = _cameraEntity;

            //Check for BoxCollider in XML/Json
            TiledMapTilesetTile foundTilesetTile = null;
            foreach (var tile in _levelTileMap.Tilesets[0].Tiles)
            {
                if (tile.LocalTileIdentifier == singleTile.GlobalIdentifier - 1)
                {
                    foundTilesetTile = tile;
                    break;
                }
            }

            //Add all BoxCooliders
            if (foundTilesetTile != null)
            {
                foreach (var collider in foundTilesetTile.Objects)
                {
                    var rectangle = new Rectangle(
                        (int)Math.Round(collider.Position.X)
                        , (int)Math.Round(collider.Position.Y)
                        , (int)Math.Round(collider.Size.Width)
                        , (int)Math.Round(collider.Size.Height)
                        );
                    var tileBoxCollider = new BoxCollider(rectangle, layerName);
                    BoxColliderSystem.Instance.AddBoxCollider(tileBoxCollider);
                    newTileEntity.AddComponent(tileBoxCollider);
                }
            }
        }
    }

    /// <summary>
    /// If there is a LayerName in the XML/Json with the Name of param String layerName, the index of that layer is returned.
    /// Otherwise -1 is returned.
    /// </summary>
    /// <param name="layerName">name of the layer</param>
    /// <returns>index of layer or -1</returns>
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
    /// Add the Player Entity to Background Component
    /// </summary>
    private void AddPlayerToBackground()
    {
        foreach (var levelEntity in EntitySystem.Instance.FindEntityByType<Background>())
        {
            levelEntity.GetComponent<Background>().PlayerEntity = _playerEntity;
        }
    }

    /// <summary>
    /// Add the Player Entity to all Level Components
    /// </summary>
    private void AddPlayerToLevel()
    {
        foreach (var levelEntity in EntitySystem.Instance.FindEntityByType<Level>())
        {
            levelEntity.GetComponent<Level>().PlayerEntity = _playerEntity;
        }
    }

    private void ToggleFpsLimit()
    {
        _graphics.SynchronizeWithVerticalRetrace = !_graphics.SynchronizeWithVerticalRetrace;
        IsFixedTimeStep = !IsFixedTimeStep;

        _graphics.ApplyChanges();
    }

    private void TogglePlayerActiveState()
    {
        IsMouseVisible = !IsMouseVisible;
        _graphics.ApplyChanges();
    }

    private void ToggleMouseCursorShow(bool gamePaused)
    {
        IsMouseVisible = !IsMouseVisible;
        _graphics.ApplyChanges();
    }
}

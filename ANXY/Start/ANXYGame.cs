using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Newtonsoft.Json.Serialization;

namespace ANXY.Start;

/// <summary>
///     This is the main type for your game.
/// </summary>
public class ANXYGame : Game
{
    private Texture2D _backgroundSprite;
    private GraphicsDeviceManager _graphics;
    private Texture2D _levelSprite;
    private TiledMap _levelTileMap;
    private TiledMapRenderer _tiledMapRenderer;
    private Texture2D _playerSprite;
    private SpriteBatch _spriteBatch;
    private string jsonLevelString;

    public ANXYGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        //IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        //_graphics.ToggleFullScreen();
        Window.AllowUserResizing = true; // makes it possible for the user to change the window size
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
    }


    /// <summary>
    ///     Allows the game to perform any initialization it needs to before starting to run.
    ///     This is where it can query for any required services and load any non-graphic
    ///     related content.  Calling base.Initialize will enumerate through any components
    ///     and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();

        InitializeDefaultScene();

        EntityManager.Instance._InitializeEntities();
        //Debug mode
        BoxColliderSystem.Instance.EnableDebugMode(_graphics.GraphicsDevice);
    }

    /// <summary>
    ///     TODO
    /// </summary>
    private void InitializeDefaultScene()
    {
        //Background
        var backgroundEntity = new Entity();
        EntityManager.Instance.AddEntity(backgroundEntity);
        var windowWidth = Window.ClientBounds.Width;
        var windowHeight = Window.ClientBounds.Height;
        backgroundEntity.AddComponent(new Background(windowWidth, windowHeight));
        backgroundEntity.AddComponent(new SingleSpriteRenderer(_backgroundSprite));

        //Player
        var playerEntity = new Entity();
        playerEntity.Position = new Vector2(windowWidth / 2, windowHeight / 2);
        EntityManager.Instance.AddEntity(playerEntity);
        var player = new Player(windowWidth, windowHeight);
        playerEntity.AddComponent(player);
        var playerSpriteRenderer = new PlayerSpriteRenderer(_playerSprite);
        playerEntity.AddComponent(playerSpriteRenderer);
        var playerCollider = new BoxCollider(new Vector2(33, 70), "Player");
        playerEntity.AddComponent(playerCollider);
        BoxColliderSystem.Instance.AddBoxCollider(playerCollider);

        //Level


        //Box1
        var boxEntity = new Entity();
        boxEntity.Position = new Vector2(1200, 800);
        EntityManager.Instance.AddEntity(boxEntity);
        var boxCollider = new BoxCollider(new Vector2(200, 100), "Ground");
        boxEntity.AddComponent(boxCollider);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider);

        //Box2
        var boxEntity2 = new Entity();
        boxEntity2.Position = new Vector2(1300, 600);
        EntityManager.Instance.AddEntity(boxEntity2);
        var boxCollider2 = new BoxCollider(new Vector2(100, 100), "Ground");
        boxEntity2.AddComponent(boxCollider2);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider2);

        TiledMap tiledMap;
        
        //Box3
        var boxEntity3 = new Entity();
        boxEntity3.Position = new Vector2(1000, 400);
        EntityManager.Instance.AddEntity(boxEntity3);
        var boxCollider3 = new BoxCollider(new Vector2(60, 60), "Ground");
        boxEntity3.AddComponent(boxCollider3);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider3);


        backgroundEntity.GetComponent<Background>().playerEntity = playerEntity;
    }

    /// <summary>
    ///     LoadContent will be called once per game and is the place to load
    ///     all of your content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _playerSprite = Content.Load<Texture2D>("playerAtlas");
        _backgroundSprite = Content.Load<Texture2D>("Background-2");
        _levelTileMap = Content.Load<TiledMap>("TileMapSet2");
        _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _levelTileMap);

    }

    /// <summary>
    ///     UnloadContent will be called once per game and is the place to unload
    ///     game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    ///     Allows the game to run logic such as updating the world,
    ///     checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        //TODO remove and diy
        _tiledMapRenderer.Update(gameTime);

        EntityManager.Instance._UpdateEntities(gameTime);
        BoxColliderSystem.Instance.CheckCollisions();
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    /// <summary>
    ///     This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        EntityManager.Instance.DrawEntities(gameTime, _spriteBatch);
        _spriteBatch.End();

        /*
        spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(playerSprite, new Vector2(0, 0), Color.White);*/

        //TODO remove and diy
        _tiledMapRenderer.Draw();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
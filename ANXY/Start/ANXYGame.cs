using System;
using System.Diagnostics;
using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace ANXY.Start;

/// <summary>
///     This is the main type for your game.
/// </summary>
public class ANXYGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private Texture2D _backgroundSprite;
    private Texture2D _levelSprite;
    private TiledMap _levelTileMap;
    private Texture2D _playerSprite;
    private SpriteBatch _spriteBatch;
    private TiledMapRenderer _tiledMapRenderer;
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
        var windowWidth = Window.ClientBounds.Width;
        var windowHeight = Window.ClientBounds.Height;

        //Background
        var backgroundEntity = new Entity();
        EntityManager.Instance.AddEntity(backgroundEntity);
        backgroundEntity.AddComponent(new Background(windowWidth, windowHeight));
        backgroundEntity.AddComponent(new SingleSpriteRenderer(_backgroundSprite));

        //Level
        Debug.WriteLine("This here is the tiles");
        var tiles = _levelTileMap.TileLayers[0].Tiles;
        var index = 0;
        foreach (var singleTile in tiles)
        {
            Debug.Write(singleTile.GlobalIdentifier + ",");
            index++;
            if (index >= _levelTileMap.Width)
            {
                Debug.Write("\n");
                index = 0;
            }

            var newTileEntity = new Entity
            {
                Position = new Vector2(singleTile.X * _levelTileMap.TileWidth,
                    singleTile.Y * _levelTileMap.TileHeight)
            };


            var tileSprite = new SingleSpriteRenderer(_levelTileMap.Tilesets[0].Texture,
                _levelTileMap.Tilesets[0].GetTileRegion(singleTile.GlobalIdentifier - 1));
            newTileEntity.AddComponent(tileSprite);

            if (singleTile.GlobalIdentifier != 0)
            {
                TiledMapTilesetTile foundTilesetTile = null;

                foreach (var tile in _levelTileMap.Tilesets[0].Tiles)
                    if (tile.LocalTileIdentifier == singleTile.GlobalIdentifier - 1)
                    {
                        foundTilesetTile = tile;
                        break;
                    }

                if (foundTilesetTile != null)
                    foreach (var collider in foundTilesetTile.Objects)
                    {
                        var rectangle = new Rectangle((int)Math.Round(collider.Position.X)
                            , (int)Math.Round(collider.Position.Y)
                            , (int)Math.Round(collider.Size.Width)
                            , (int)Math.Round(collider.Size.Height));
                        var tileBoxCollider = new BoxCollider(rectangle, "Ground");
                        BoxColliderSystem.Instance.AddBoxCollider(tileBoxCollider);
                        newTileEntity.AddComponent(tileBoxCollider);
                    }

                EntityManager.Instance.AddEntity(newTileEntity);
            }
        }

        //Player
        var playerEntity = new Entity();
        playerEntity.Position = new Vector2((int)Math.Round(windowWidth / 2.0), (int)Math.Round(windowHeight / 2.0));
        EntityManager.Instance.AddEntity(playerEntity);
        var player = new Player(windowWidth, windowHeight);
        playerEntity.AddComponent(player);
        var playerSpriteRenderer = new PlayerSpriteRenderer(_playerSprite);
        playerEntity.AddComponent(playerSpriteRenderer);
        var playerCollider = new BoxCollider(new Rectangle(0, 0, 33, 70), "Player");
        playerEntity.AddComponent(playerCollider);
        BoxColliderSystem.Instance.AddBoxCollider(playerCollider);

        backgroundEntity.GetComponent<Background>().PlayerEntity = playerEntity;


        

        
        //Box1
        var boxEntity = new Entity();
        boxEntity.Position = new Vector2(1200, 800);
        EntityManager.Instance.AddEntity(boxEntity);
        var boxCollider = new BoxCollider(new Rectangle(0, 0, 200, 100), "Ground");
        boxEntity.AddComponent(boxCollider);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider);

        //Box2
        var boxEntity2 = new Entity();
        boxEntity2.Position = new Vector2(1300, 600);
        EntityManager.Instance.AddEntity(boxEntity2);
        var boxCollider2 = new BoxCollider(new Rectangle(0, 0, 100, 50), "Ground");
        boxEntity2.AddComponent(boxCollider2);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider2);

        //Box3
        var boxEntity3 = new Entity();
        boxEntity3.Position = new Vector2(0, windowHeight-170);
        EntityManager.Instance.AddEntity(boxEntity3);
        var boxCollider3 = new BoxCollider(new Rectangle(0, 0, windowWidth, 40), "Ground");
        boxEntity3.AddComponent(boxCollider3);
        BoxColliderSystem.Instance.AddBoxCollider(boxCollider3);
        
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
        //_levelTileMap = Content.Load<TiledMap>("TileMapSet2");
        _levelTileMap = Content.Load<TiledMap>("JumpNRun-1");
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
        EntityManager.Instance._UpdateEntities(gameTime);
        BoxColliderSystem.Instance.CheckCollisions();
        //base.Update(gameTime);
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
        //base.Draw(gameTime);
    }
}
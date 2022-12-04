using System.Diagnostics;
using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.Start;

/// <summary>
///     This is the main type for your game.
/// </summary>
public class ANXYGame : Game
{
    private Texture2D _backgroundSprite;

    private GraphicsDeviceManager _graphics;
    private Texture2D _playerSprite;
    private SpriteBatch _spriteBatch;

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
        Debug.WriteLine(windowWidth);
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

        // TODO: use this.Content to load your game content here
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


        /*
        spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(playerSprite, new Vector2(0, 0), Color.White);*/
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
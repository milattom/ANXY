using ANXY.EntityComponent.Components;
using ANXY.EntityComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ANXY.Start{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ANXYGame : Game
    {
        private Texture2D playerSprite;
        private Texture2D backgroundSprite;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ANXYGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            InitializeDefaultScene();
        }

        /// <summary>
        ///     TODO
        /// </summary>
        private void InitializeDefaultScene()
        {
            //Background
            Entity backgroundEntity = new Entity();
            EntityManager.Instance.AddEntity(backgroundEntity);
            backgroundEntity.AddComponent(new SingleSpriteRenderer(backgroundSprite));

            //Player
            Entity playerEntity = new Entity();
            EntityManager.Instance.AddEntity(playerEntity);
            Player player = new Player();
            playerEntity.AddComponent(player);
            PlayerSpriteRenderer playerSpriteRenderer = new PlayerSpriteRenderer(playerSprite);
            playerEntity.AddComponent(playerSpriteRenderer);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("PlayerCharacter");
            backgroundSprite = Content.Load<Texture2D>("Background");
            
            // TODO: use this.Content to load your game content here
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
            EntityManager.Instance.UpdateEntities(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            EntityManager.Instance.DrawEntities(gameTime, spriteBatch);

            
            /*
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(playerSprite, new Vector2(0, 0), Color.White);*/
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
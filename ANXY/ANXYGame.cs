using ANXY.ECS.Components;
using ANXY.ECS.Systems;
using ANXY.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ANXY
{
    public class ANXYGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameObjects.Player _player;

        public ANXYGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            await GameContent.Init(Content);


            // TODO: use this.Content to load your game content here
            //Vector pointing to middle of the screen
            Vector2 position = new Vector2(
                _graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            //---------------------- Entity ---------------------------
            // Content shouldnt be parameter for entity, but we would need a contentmanager
            // for loading things like textures, if we dont want to do everything in Game.cs
            _player = new Player(position, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            TransformSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _player.GetComponent<Sprite>().Draw(_spriteBatch, null);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
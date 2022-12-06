using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ANXY.EntityComponent.Components;

public class Background : Component
{
    private Player _playerComponent;
    private Vector2 _screenScrollingDirection;
    private float _screenScrollingSpeed;
    private int _windowHeight;
    private int _windowWidth;

    public Background(int windowWidth, int windowHeight)
    {
        this._windowWidth = windowWidth;
        this._windowHeight = windowHeight;
    }

    public Entity playerEntity { get; set; }

    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        _screenScrollingDirection = Vector2.Zero;
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) _screenScrollingDirection = new Vector2(-1, 0);
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) _screenScrollingDirection = new Vector2(1, 0);

        if (_playerComponent.Velocity.X == 0 && _playerComponent.Inputdirection.X != 0)
            _screenScrollingSpeed = Math.Abs(_playerComponent.ScrollSpeed);
        else
            _screenScrollingSpeed = 0;


        Entity.Position += _screenScrollingDirection * _screenScrollingSpeed *
                           (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public override void Initialize()
    {
        _playerComponent = playerEntity.GetComponent<Player>();
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }
}
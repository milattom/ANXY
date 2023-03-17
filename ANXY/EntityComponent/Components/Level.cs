using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using MonoGame.Extended;

namespace ANXY.EntityComponent.Components
{
    internal class Level : Component
    {
        private Vector2 _screenScrollingDirection;
        private float _screenScrollingSpeed;

        private Player _playerComponent;
        public Entity PlayerEntity { get; set; }
        private bool positionChanged = false;
        private bool somePositionChanged = false;

        public Level() { }

        public override void Update(GameTime gameTime) {
            /*
            var state = Keyboard.GetState();
            _screenScrollingDirection = Vector2.Zero;
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) _screenScrollingDirection = new Vector2(-1, 0);
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) _screenScrollingDirection = new Vector2(1, 0);

            if (_playerComponent.Velocity.X == 0 && _playerComponent.InputDirection.X != 0)
                _screenScrollingSpeed = _playerComponent.ScrollSpeed;
            else
                _screenScrollingSpeed = 0;

            if (Entity.GetComponents<Level>().Count > 1)
            {
                foreach (Level l in Entity.GetComponents<Level>())
                {
                    if (l.positionChanged)
                    {
                        somePositionChanged = true;
                    }
                }
                if (!somePositionChanged)
                {
                    Vector2 addVector = _screenScrollingDirection * _screenScrollingSpeed *
                                   (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_screenScrollingDirection.X > 0)
                    {
                        addVector = addVector.ToAbsoluteSize();
                    }
                    Entity.Position += addVector;
                    positionChanged = true;
                }
            }
            else
            {
                Vector2 addVector = _screenScrollingDirection * _screenScrollingSpeed *
                                   (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_screenScrollingDirection.X > 0)
                {
                    addVector = addVector.ToAbsoluteSize();
                }
                Entity.Position += addVector;
            }*/
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            /*
            positionChanged = false;
            somePositionChanged = false;*/
        }
        public override void Initialize() {
            _playerComponent = PlayerEntity.GetComponent<Player>();
        }
        public override void Destroy() { 
        }
    }
}

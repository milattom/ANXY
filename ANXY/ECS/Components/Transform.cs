using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ANXY.ECS.Systems;
using Microsoft.Xna.Framework;

namespace ANXY.ECS.Components
{
    public class Transform : Component
    {

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.Zero;
        public Vector2 LastPosition { get; set; }= Vector2.Zero;
        public float LayerDepth { get; set; } = 0;
        public float Rotation { get; set; } = 0;

        public Transform()
        {
            TransformSystem.Register(this);
        }

        public void Translate(Vector2 pos)
        {
            Position += pos;
        }

        public void Rotate(float degrees)
        {
            Rotation += degrees;
        }

        public Transform(Vector2 position, float rotation = 0, float layerDepth = 0)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.LayerDepth = layerDepth;
        }

        public override void Update(GameTime gameTime)
        {
            //worldPosition = CalculateWorldPosition();
            LastPosition = Position;
        }
    }
}

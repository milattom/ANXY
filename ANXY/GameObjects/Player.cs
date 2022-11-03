using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.GameObjects
{
    public class Player : Entity
    {
        private Texture2D textureStanding;
        public Player(Vector2 pos, ContentManager content)
        {
            this.Name = "Bob";
            //-------------- Components for Entity --------------------

            //Every Entity need a transform with position, rotation and
            //layerdepth (layerdepth could be described by layers later on?)
            Transform transform = new Transform(pos, 0, 0);
            

            // Sprite component
            textureStanding = content.Load<Texture2D>("PlayerStanding");
            Sprite sprite = new Sprite(textureStanding);

            //Add the components to the _player
            AddComponent(transform);
            AddComponent(sprite);
        }
    }
}

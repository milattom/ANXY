using System;
using System.Collections.Generic;
using System.Linq;
using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace ANXY.Start
{
    internal class BoxColliderSystem
    {
        ///Singleton Pattern
        private static readonly Lazy<BoxColliderSystem> lazy = new(() => new BoxColliderSystem());

        private readonly List<BoxCollider> _boxColliders;

        private BoxColliderSystem()
        {
            _boxColliders = new List<BoxCollider>();
        }

        public static BoxColliderSystem Instance => lazy.Value;

        public void AddBoxCollider(BoxCollider boxCollider)
        {
            _boxColliders.Add(boxCollider);
        }

        /// <summary>
        /// Returns a list of BoxColliders which are colliding with the given box
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public List<BoxCollider>  GetCollisions(BoxCollider box)
        {
            List<BoxCollider> colliders = new List<BoxCollider>();
            foreach (var otherBox in _boxColliders)
            {
                if (box.IsColliding(otherBox))
                {
                    colliders.Add(otherBox);
                }
            }
            return colliders;
        }

         //public void CheckCollisions()
         //{
         //    for (int i = 0; i <= _boxColliders.Count - 1; i++)
         //    {
         //        for (int j = i + 1; j <= _boxColliders.Count - i - 1; j++)
         //        {
         //            var box = _boxColliders[i];
         //            var otherBox = _boxColliders[j];
         //            if (box.IsColliding(otherBox))
         //            {
         //                box.Highlight();
         //                box.isColliding=true;
         //                otherBox.Highlight();
         //                otherBox.isColliding = true;
         //            }
         //        }
         //    }
         //}

         /// <summary>
         /// Checks all box colliders with the one from the player and sets the isColliding = true if they are.
         /// </summary>
         public void CheckCollisions()
         { 
             var playerCollider = EntityManager.Instance.FindEntityByType<Player>()[0].GetComponent<BoxCollider>();
             playerCollider.isColliding = false;
             playerCollider.Dehighlight();
             foreach (var boxCollider in _boxColliders)
            { 
                boxCollider.Dehighlight(); 
                boxCollider.isColliding=false;
                if (!boxCollider.LayerMask.Equals(playerCollider.LayerMask)) 
                {
                   if (playerCollider.IsColliding(boxCollider))
                   {
                       boxCollider.Highlight();
                       boxCollider.isColliding = true;
                       playerCollider.Highlight();
                       playerCollider.isColliding = true;
                       continue;
                   }
                   else
                   {
                       boxCollider.Dehighlight();
                   }
               }
            }
            
         }

        public void EnableDebugMode(GraphicsDevice graphics)
        {
            foreach (var box in _boxColliders)
            {
                box.DebugMode = true;
                Texture2D recTexture = _CreateRectangleTexture(graphics, box.Dimensions);
                box.setRectangleTexture(recTexture);
            }
        }

        public void DisableDebugMode()
        {
            foreach (var box in _boxColliders)
            {
                box.DebugMode = false;
            }
        }

        private Texture2D _CreateRectangleTexture(GraphicsDevice graphics, Vector2 dim)
        {
            Texture2D rect = null;
            var colours = new List<Color>();
            for (int y = 0; y < dim.Y; y++)
            {
                for (int x = 0; x < dim.X; x++)
                {
                    if (x == 0 ||
                        y == 0 ||
                        x == dim.X - 1 ||
                        y == dim.Y - 1)
                    {
                        colours.Add(new Color(255, 255, 255, 255));
                    }
                    else
                    {
                        colours.Add(new Color(0,0,0,0));
                    }
                }
            }

            rect = new Texture2D(graphics, (int) dim.X, (int)dim.Y);
            rect.SetData(colours.ToArray());
            return rect;
        }
    }
}

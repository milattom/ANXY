using System;
using System.Collections.Generic;
using System.Linq;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using MonoGame.Extended;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ANXY.Start
{
    /// <summary>
    /// The Singleton BoxColliderSystem holds a list with all active BoxColliders, detects collisions and
    /// manipulates the BoxCollider component accordingly.
    /// </summary>
    internal class BoxColliderSystem
    {
        ///Singleton Pattern
        private static readonly Lazy<BoxColliderSystem> lazy = new(() => new BoxColliderSystem());

        private readonly List<BoxCollider> _boxColliderList;

        private BoxColliderSystem()
        {
            _boxColliderList = new List<BoxCollider>();
        }

        public static BoxColliderSystem Instance => lazy.Value;

        /// <summary>
        /// Add a box collider to the list of boxCollider
        /// </summary>
        /// <param name="boxCollider"></param>
        public void AddBoxCollider(BoxCollider boxCollider)
        {
            _boxColliderList.Add(boxCollider);
        }

        /// <summary>
        /// Returns a list of BoxCollider which are colliding with the given box
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public List<BoxCollider>  GetCollisions(BoxCollider box)
        {
            var collidingBox = new List<BoxCollider>();
            foreach (var otherBox in _boxColliderList.Where(otherBox => IsColliding(box, otherBox)))
            {
                otherBox.Colliding = true;
                box.Colliding = true;
                collidingBox.Add(otherBox);
            }
            return collidingBox;
        }

         /// <summary>
         /// Checks all box collider with the one from the player and sets Colliding correspondingly.
         /// </summary>
         public void CheckCollisions()
         { 
             var playerCollider = EntitySystem.Instance.FindEntityByType<Player>()[0].GetComponent<BoxCollider>();
             playerCollider.Colliding = false;
             foreach (var boxCollider in _boxColliderList.Where(boxCollider => !boxCollider.LayerMask.Equals(playerCollider.LayerMask)))
             {
                 if (IsColliding(playerCollider, boxCollider))
                 {
                     playerCollider.Colliding = true;
                     boxCollider.Colliding = true;
                 }
                 else
                 {
                     boxCollider.Colliding = false;
                 }
             }
            
         }

         /// <summary>
         /// Enabled debug mode for Boxcolliders, means Bounding box is drawn and
         /// gets highlighted when collision is detected
         /// </summary>
         /// <param name="graphics"></param>
        public void EnableDebugMode(GraphicsDevice graphics)
        {
            foreach (var box in _boxColliderList)
            {
                box.DebugMode = true;
                Texture2D recTexture = CreateRectangleTexture(graphics, box.Dimensions);
                box.SetRectangleTexture(recTexture);
            }
        }

         /// <summary>
         /// Disables Debug mode in every active boxcollider
         /// </summary>
        public void DisableDebugMode()
        {
            foreach (var box in _boxColliderList)
            {
                box.DebugMode = false;
            }
        }

        /// <summary>
        /// Checks if two Boxcolliders are colliding by comparing the distance of their centers
        /// with the max allowed distance. Calls EdgeDetection If true. 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="other"></param>
        /// <returns>True if two boxes are colliding, False if not</returns>
        public bool IsColliding(BoxCollider player, BoxCollider other)
        {
            var dx = (player.Center.X - other.Center.X);
            var dy = (player.Center.Y - other.Center.Y);
            var d = new Vector2(dx, dy);

            var dxMax = (player.Dimensions.X + other.Dimensions.X) / 2;
            var dyMax = (player.Dimensions.Y + other.Dimensions.Y) / 2;
            var dMax = new Vector2(dxMax, dyMax);

            if (Compare(d.ToAbsoluteSize(), dMax) >= 0) return false;
            EdgeDetection(player, other, d, dMax);
            return true;
        }

        /// <summary>
        /// Detects if the edges of the player BoxCollider crosses the max distance to another boundary box
        /// by cross width and cross length and sets the corresponding Edge in the player BoxCollider.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="d">current distance of two centers</param>
        /// <param name="dMax">max distance between two centers</param>
        private static void EdgeDetection(BoxCollider player, BoxCollider other, Vector2 d, Vector2 dMax)
        {
            var crossWidth = dMax.X * d.Y;
            var crossHeight = dMax.Y * d.X;

            if (crossWidth < crossHeight && crossWidth >= (-crossHeight))
            {
                player.CollidingEdge = BoxCollider.Edge.Left;
                player.CollidingEdges.Add((BoxCollider.Edge.Left, other.GetCollisionPosition(BoxCollider.Edge.Left)));
            }

            if (crossWidth >= crossHeight && crossWidth < (-crossHeight))
            {
                player.CollidingEdge = BoxCollider.Edge.Right;
                player.CollidingEdges.Add((BoxCollider.Edge.Right, other.GetCollisionPosition(BoxCollider.Edge.Right)));
            }

            if (crossWidth >= crossHeight && crossWidth >= (-crossHeight))
            {
                player.CollidingEdge = BoxCollider.Edge.Top;
                player.CollidingEdges.Add((BoxCollider.Edge.Top, other.GetCollisionPosition(BoxCollider.Edge.Top)));
            }

            if (crossWidth < crossHeight && crossWidth < (-crossHeight))
            {
                player.CollidingEdge = BoxCollider.Edge.Bottom;
                player.CollidingEdges.Add((BoxCollider.Edge.Bottom, other.GetCollisionPosition(BoxCollider.Edge.Bottom)));
            }
        }


        /// <summary>
        /// Compares two vectors by element.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>
        /// -1 if x_1 <= x_2 && y_1 <= y_2
        /// 1 if x_1 > x_2 && y_1 > y_2
        /// 0 else
        /// </returns>
        private static int Compare(Vector2 v1, Vector2 v2)
        {
            if (v1.X <= v2.X 
                && v1.Y <= v2.Y) return -1;
            if (v1.X > v2.X 
                && v1.Y > v2.Y) return 1;
            return 0;
        }

        /// <summary>
        /// Creates the texture for debugging
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="dim"></param>
        /// <returns>Texture2D</returns>
        private static Texture2D CreateRectangleTexture(GraphicsDevice graphics, Vector2 dim)
        {
            Texture2D rect = null;
            var colors = new List<Color>();
            for (var y = 0; y < dim.Y; y++)
            {
                for (var x = 0; x < dim.X; x++)
                {
                    if (x == 0 ||
                        y == 0 ||
                        x == dim.X - 1 ||
                        y == dim.Y - 1)
                    {
                        colors.Add(new Color(255, 255, 255, 255));
                    }
                    else
                    {
                        colors.Add(new Color(0,0,0,0));
                    }
                }
            }

            rect = new Texture2D(graphics, (int) dim.X, (int)dim.Y);
            rect.SetData(colors.ToArray());
            return rect;
        }
    }
}

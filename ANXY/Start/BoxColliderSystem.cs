using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ANXY.Start
{
    /// <summary>
    /// The Singleton BoxColliderSystem holds a list with all active BoxColliders, detects collisions and
    /// manipulates the BoxCollider component accordingly.
    /// </summary>
    internal class BoxColliderSystem : ComponentSystem<BoxCollider>
    {
        ///Singleton Pattern
        private static readonly Lazy<BoxColliderSystem> Lazy = new(() => new BoxColliderSystem());

        public static BoxColliderSystem Instance => Lazy.Value;

        private BoxColliderSystem() 
            {
            SystemManager.Instance.Register(this);
            }

        /// <summary>
        /// Returns a list of BoxCollider which are colliding with the given box
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public List<BoxCollider> GetCollisions(BoxCollider box)
        {
            //TODO optimize performance of collider detection, e.g. 30 to left right etc ("e.g. Quadtree")
            return components.Where(otherBox => IsColliding(box, otherBox)).ToList();
        }

        /// <summary>
        /// Enabled debug mode for Boxcolliders, means Bounding box is drawn and
        /// gets highlighted when collision is detected
        /// </summary>
        /// <param name="graphics"></param>
        public void EnableDebugMode(GraphicsDevice graphics)
        {
            foreach (var box in components)
            {
                var recTexture = CreateRectangleTexture(graphics, box.Dimensions);
                box.SetRectangleTexture(recTexture);
                box.DebugEnabled = true;
            }
        }

        /// <summary>
        /// Checks if two Boxcolliders are colliding by comparing the distance of their centers
        /// with the max allowed distance. Calls EdgeDetection If true. 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="other"></param>
        /// <returns>True if two boxes are colliding, False if not or the colliders are the same</returns>
        public bool IsColliding(BoxCollider aCollider, BoxCollider bCollider)
        {
            if (aCollider == bCollider)
            {
                return false;
            }

            var dx = (aCollider.Center.X - bCollider.Center.X);
            var dy = (aCollider.Center.Y - bCollider.Center.Y);
            var d = new Vector2(dx, dy);

            var dxMax = (aCollider.Dimensions.X + bCollider.Dimensions.X) / 2;
            var dyMax = (aCollider.Dimensions.Y + bCollider.Dimensions.Y) / 2;
            var dMax = new Vector2(dxMax, dyMax);

            return Compare(d.ToAbsoluteSize(), dMax) < 0;
        }

        /// <summary>
        /// Returns the Edge of the box collider in the second parameter which has collided with object in the first parameter
        /// </summary>
        /// <param name="aCollider"></param>
        /// <param name="bCollider"></param>
        /// <returns>edge of bCollider is returned.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static BoxCollider.Edge DetectEdge(BoxCollider aCollider, BoxCollider bCollider)
        {
            var (crossWidth, crossHeight) = GetCrossWidthAndHeight(aCollider, bCollider);


            if (crossWidth < crossHeight && crossWidth >= (-crossHeight))
            {
                return BoxCollider.Edge.Right;
            }

            if (crossWidth >= crossHeight && crossWidth < (-crossHeight))
            {
                return BoxCollider.Edge.Left;
            }

            if (crossWidth >= crossHeight && crossWidth >= (-crossHeight))
            {
                return BoxCollider.Edge.Bottom;
            }

            if (crossWidth < crossHeight && crossWidth < (-crossHeight))
            {
                return BoxCollider.Edge.Top;
            }
            throw new InvalidOperationException("This should never happen.");
        }

        private static (float crossWidth, float crossHeight) GetCrossWidthAndHeight(BoxCollider aCollider, BoxCollider bCollider)
        {
            var dx = (aCollider.Center.X - bCollider.Center.X);
            var dy = (aCollider.Center.Y - bCollider.Center.Y);
            var d = new Vector2(dx, dy);

            var dxMax = (aCollider.Dimensions.X + bCollider.Dimensions.X) / 2;
            var dyMax = (aCollider.Dimensions.Y + bCollider.Dimensions.Y) / 2;
            var dMax = new Vector2(dxMax, dyMax);

            return (dMax.X * d.Y, dMax.Y * d.X);
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
                        colors.Add(new Color(0, 0, 0, 0));
                    }
                }
            }

            rect = new Texture2D(graphics, (int)dim.X, (int)dim.Y);
            rect.SetData(colors.ToArray());
            return rect;
        }
    }
}
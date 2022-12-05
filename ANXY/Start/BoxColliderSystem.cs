using System;
using System.Collections.Generic;
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
                //minY
                if(box.IsColliding(otherBox))
                    colliders.Add(otherBox);
            }
            return colliders;
        }

        public void EnableDebugMode()
        {
            foreach (var box in _boxColliders)
            {
                box.DebugMode = true;
            }
        }

        public void DisableDebugMode()
        {
            foreach (var box in _boxColliders)
            {
                box.DebugMode = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.GameObjects
{
    public class Player : Entity
    {
        public Player()
        {
            this.Name = "Bob";
        }
    }
}

using ANXY.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANXY.EntityComponent.Components
{
    internal class PlayerInputController
    {
        ///Singleton Pattern
        private static readonly Lazy<PlayerInputController> lazy = new(() => new PlayerInputController());
        //private List<Entity> _gameEntities;

        /// <summary>
        ///     TODO
        /// </summary>
        private PlayerInputController()
        {
        }

        /// <summary>
        ///     Singleton Pattern return the only instance there is
        /// </summary>
        public static PlayerInputController Instance => lazy.Value;
    }
}

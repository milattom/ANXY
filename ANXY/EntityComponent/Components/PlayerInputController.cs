using ANXY.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANXY.EntityComponent.Components
{
    /// <summary>
    /// TODO Player Input Controller
    /// Checking for Keyboard input and setting everything accordingly. Every Class needing to check the Keyboard input must check it through this Class.
    /// </summary>
    internal class PlayerInputController
    {
        ///Singleton Pattern
        private static readonly Lazy<PlayerInputController> lazy = new(() => new PlayerInputController());
        //private List<Entity> _gameEntities;

        /// <summary>
        /// PlayerInputController Class Constructor. Private because of Singleton Pattern.
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

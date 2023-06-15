using ANXY.ECS;
using ANXY.ECS.Components;
using ANXY.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ANXY.Start
{
    /// <summary>
    /// The PlayerFactory is used to easily create player instances and adds them to the PlayerSystem.
    /// </summary>
    public class PlayerFactory
    {
        private static readonly Lazy<PlayerFactory> _lazy = new(() => new PlayerFactory());
        public static PlayerFactory Instance => _lazy.Value;

        /// <summary>
        /// Creates an amount of player instances, specified in the parameter.
        /// Those Players are getting a random position within 1200x540 pixels.
        /// </summary>
        /// <param name="amount">amount of player instances that should be created</param>
        public void CreatePlayers(int amount, Texture2D playerAtlas)
        {
            List<Entity> players = new(amount);
            for (int i = 0; i < amount; i++)
            {
                var mainPlayer = PlayerSystem.Instance.GetComponent();
                var mainPlayerPosition = mainPlayer.Entity.Position;
                int xMin = (int)mainPlayerPosition.X - ANXYGame.Instance.WindowWidth / 2;
                int xMax = (int)mainPlayerPosition.X + ANXYGame.Instance.WindowWidth / 2;
                int yMin = (int)mainPlayerPosition.Y - ANXYGame.Instance.WindowHeight / 2;
                int yMax = (int)mainPlayerPosition.Y + ANXYGame.Instance.WindowHeight / 2;
                Vector2 pos = getRandomVector2(xMin, xMax, yMin, yMax);
                players.Add(CreatePlayer(pos, playerAtlas));
            }
        }

        /// <summary>
        /// Creates a single Player Instance at the position provided as parameter and returns its Entity.
        /// </summary>
        /// <returns>new Player Entity</returns>
        public Entity CreatePlayer(Vector2 position, Texture2D playerAtlas)
        {
            var playerEntity = new Entity { Position = position };
            var player = new Player();
            playerEntity.AddComponent(player);

            var playerSpriteRenderer = new PlayerSpriteRenderer(playerAtlas);
            playerEntity.AddComponent(playerSpriteRenderer);
            playerSpriteRenderer.SetPlayerEntity(playerEntity);

            var playerBox = new Rectangle(1, 6, 32, 64);
            var playerCollider = new BoxCollider(playerBox, "Player");
            playerEntity.AddComponent(playerCollider);

            return playerEntity;
        }

        /// <summary>
        /// Returns a random Vector2 within the bounderies [xMin, xMax] and [yMin, yMax].
        /// /// </summary>
        /// <param name="maxX"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Vector2 getRandomVector2(int xMin, int xMax, int yMin, int yMax)
        {
            Random r = new Random();
            float rx = r.Next(xMin, xMax);
            float ry = r.Next(yMin, yMax);
            return new Vector2(rx, ry);
        }
    }
}

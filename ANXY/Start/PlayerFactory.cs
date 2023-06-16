
using System;
using System.Collections.Generic;
using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY.Start;

/// <summary>
/// The PlayerFactory is used to easily create player instances and adds them to the EntitySystem.
/// Those Players are getting a random position within 1200x540 pixels.
/// </summary>
public class PlayerFactory
{
    private static readonly Lazy<PlayerFactory> _lazy = new(() => new PlayerFactory());
    public static PlayerFactory Instance => _lazy.Value;

    /// <summary>
    /// Creates an amount of player instances, specified in the parameter
    /// </summary>
    /// <param name="amount">amount of player instances that should be created</param>
    public void CreatePlayers(int amount, Texture2D playerAtlas)
    {
        List<Entity> players = new List<Entity>(amount);
        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = getRandomVector2(0, 1200, 0, 540);
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
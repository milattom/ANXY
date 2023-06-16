using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;

namespace ANXY.Start;

/// <summary>
/// Tile-, Background-, Player-, CameraEntity
/// </summary>
internal class EntityFactory
{
    private static readonly Lazy<EntityFactory> _lazy = new(() => new EntityFactory());
    public static EntityFactory Instance => _lazy.Value;

    public enum EntityType
    {
        Background,
        Camera,
        Player,
        Tile
    }

    private Object[] _optional;

    public Entity CreateEntity(EntityType type, Object[] optional = null)
    {
        _optional = optional;
        switch (type)
        {
            case EntityType.Background:
                return CreateBackgroundEntity();
            case EntityType.Camera:
                return CreateCameraEntity();
            case EntityType.Player:
                return CreatePlayerEntity();
            case EntityType.Tile:
                return CreateTileEntity();
            default: return null;
        }
    }

    private Entity CreateBackgroundEntity()
    {
        var (windowHeigtht, windowWidth) = ((int)_optional[0], (int)_optional[1]);
        var backgroundEntity = new Entity();
        backgroundEntity.Position -= new Vector2(0.5f * windowWidth, 0.5f * windowHeigtht);

        Background background = new(windowWidth, windowHeigtht);
        backgroundEntity.AddComponent(background);
        backgroundEntity.AddComponent(new Background(windowWidth, windowHeigtht));

        SingleSpriteRenderer backgroundSprite = new((Texture2D)_optional[2]);
        backgroundEntity.AddComponent(backgroundSprite);
        return backgroundEntity;
    }

    private Entity CreateCameraEntity()
    {
        var (windowHeight, windowWidth) = ((int)_optional[0], (int)_optional[1]);
        var player = (Player)SystemManager.Instance.FindSystemByType<Player>().GetFirstComponent();
        var cameraEntity = new Entity();
        var camera = new Camera(player, new Vector2(windowWidth, windowHeight), new Vector2(0.25f * windowWidth, 0.5f * windowHeight), new Vector2(float.PositiveInfinity, 0.85f * windowHeight));
        cameraEntity.AddComponent(camera);
        return cameraEntity;
    }

    private Entity CreatePlayerEntity()
    {
        var playerSprite = (Texture2D)_optional[0];
        var playerPosition = new Vector2(1200, 540);
        return PlayerFactory.Instance.CreatePlayer(playerPosition, playerSprite);
    }

    private Entity CreateTileEntity()
    {
        var singleTile = (TiledMapTile)_optional[0];
        var layerName = (string)_optional[1];
        var levelTileMap = (TiledMap)_optional[2];
        var newTileEntity = new Entity
        {
            Position = new Vector2(singleTile.X * levelTileMap.TileWidth, singleTile.Y * levelTileMap.TileHeight)
        };
        // Add Sprite to Tile Entity.
        var tileSprite = new SingleSpriteRenderer(levelTileMap.Tilesets[0].Texture, levelTileMap.Tilesets[0].GetTileRegion(singleTile.GlobalIdentifier - 1));
        newTileEntity.AddComponent(tileSprite);
        // Check for BoxColliders in XML.
        TiledMapTilesetTile foundTilesetTile = null;
        foreach (var tile in levelTileMap.Tilesets[0].Tiles)
        {
            if (tile.LocalTileIdentifier == singleTile.GlobalIdentifier - 1)
            {
                foundTilesetTile = tile;
                break;
            }
        }
        // Add BoxCollider to Tile Entity.
        if (foundTilesetTile != null)
        {
            foreach (var collider in foundTilesetTile.Objects)
            {
                var rectangle = new Rectangle(
                    (int)Math.Round(collider.Position.X)
                    , (int)Math.Round(collider.Position.Y)
                    , (int)Math.Round(collider.Size.Width)
                    , (int)Math.Round(collider.Size.Height)
                    );
                var tileBoxCollider = new BoxCollider(rectangle, layerName);
                newTileEntity.AddComponent(tileBoxCollider);
            }
        }
        return newTileEntity;
    }
}
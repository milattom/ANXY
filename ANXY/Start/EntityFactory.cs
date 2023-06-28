using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Linq;
using static ANXY.ECS.Components.SingleSpriteRenderer;

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
        BackgroundTile,
        ForegroundTile
    }

    private Object[] _optional;

    public Entity CreateEntity(EntityType type, Object[] optional = null)
    {
        _optional = optional;
        return type switch
        {
            EntityType.Background => CreateBackgroundEntity(),
            EntityType.Camera => CreateCameraEntity(),
            EntityType.Player => CreatePlayerEntity(),
            EntityType.BackgroundTile => CreateTileEntity(true),
            EntityType.ForegroundTile => CreateTileEntity(false),
            _ => null,
        };
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
        var camera = new Camera(player, new Vector2(windowWidth, windowHeight));
        cameraEntity.AddComponent(camera);
        return cameraEntity;
    }

    private Entity CreatePlayerEntity()
    {
        var playerSprite = (Texture2D)_optional[0];
        return PlayerFactory.CreatePlayer(ANXYGame.Instance.GameLoadSpawnPosition, playerSprite);
    }

    private Entity CreateTileEntity(bool background)
    {
        var singleTile = (TiledMapTile)_optional[0];
        var layerName = (string)_optional[1];
        var levelTileMap = (TiledMap)_optional[2];
        var renderSprite = (bool)_optional[3];

        var newTileEntity = new Entity
        {
            Position = new Vector2(singleTile.X * levelTileMap.TileWidth, singleTile.Y * levelTileMap.TileHeight)
        };
        // Add Sprite to Tile Entity.
        if (renderSprite)
        {
            if (!background)
            {
                var tileSprite = new ForegroundSpriteRenderer(levelTileMap.Tilesets[0].Texture, levelTileMap.Tilesets[0].GetTileRegion(singleTile.GlobalIdentifier - 1));
                newTileEntity.AddComponent(tileSprite);
            }
            else
            {
                var tileSprite = new BackgroundSpriteRenderer(levelTileMap.Tilesets[0].Texture, levelTileMap.Tilesets[0].GetTileRegion(singleTile.GlobalIdentifier - 1));
                newTileEntity.AddComponent(tileSprite);
            }
        }
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
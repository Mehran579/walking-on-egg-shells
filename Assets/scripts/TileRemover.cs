using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileToRemove; // drag the broken pink tile asset here
    public float fkoff;
    [ContextMenu("Remove Tile")]
    public void Start()
    {
        RemoveAllOfTile();
    }
    void RemoveAllOfTile()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        for (int i = 0; i < tiles.Length; i++)
            if (tiles[i] == tileToRemove)
                tiles[i] = null;

        tilemap.SetTilesBlock(bounds, tiles);
    }
}
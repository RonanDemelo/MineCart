using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isCorridorTile;
    [SerializeField] private bool isRoomTile;
    [SerializeField] private Vector3Int tileSize = new Vector3Int(1, 1, 1);
    [SerializeField] GameObject tile;

    public Vector3Int GetTileSize()
    {
        return new Vector3Int(tileSize.x, tileSize.y, tileSize.z);
    }

}

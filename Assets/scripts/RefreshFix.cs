using UnityEngine;
using UnityEngine.Tilemaps;

public class RefreshFix : MonoBehaviour
{
    void Start()
    {
        GetComponent<Tilemap>().RefreshAllTiles();
    }
}
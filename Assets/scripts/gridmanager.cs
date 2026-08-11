using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[System.Serializable]
public class shell
{
    public GameObject prefab;
    public GameObject ghostprefab;
    public Vector2Int size = Vector2Int.one;
}
public class gridmanager : MonoBehaviour
{
    public int ToSpawnShell;
    public shell[] shells;
    public Tilemap tilemap;
    gridcell[,] grid;
    public GameObject pillbug;
    public GameObject pillbugghost;
    public Transform spawnpos;
    public bool canspawn;
    public static int points;
    public crustaceanspawner crustaceanspawner;
    public TMP_Text selectedname;
    //public LayerMask notclickthrough;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = 10;
        grid = new gridcell[tilemap.size.x, tilemap.size.y];
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                grid[x, y] = new gridcell();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            if (!canspawn) return;
            //if (EventSystem.current.IsPointerOverGameObject()) return;
            //Debug.Log(ToSpawnShell);
            if (crustaceanspawner.checkavailablity(ToSpawnShell) <= 0) return;
            //Debug.Log(crustaceanspawner.checkavailablity(ToSpawnShell));
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int cellpos = tilemap.WorldToCell(mousepos);
            if ((cellpos.x < 107 && cellpos.x > 104) && (cellpos.y < 115 && cellpos.y > 113)) return;
            if ((cellpos.x < 107 && cellpos.x > 104) && (cellpos.y < 120 && cellpos.y > 117)) return;
            //Debug.Log(cellpos);
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Mouse.current.position.ReadValue();
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("notclickthrough"))
                    return;
            }
            shell selected = shells[ToSpawnShell];
            if (!canplace(cellpos, selected.size))
                return;
            for (int x = 0; x < selected.size.x; x++)
            {
                for (int y = 0; y < selected.size.y; y++)
                {
                    Vector3Int currentCell = cellpos + new Vector3Int(x, y, 0);

                    grid[currentCell.x, currentCell.y].occupied = true;
                    grid[currentCell.x, currentCell.y].shelltype = selected.prefab;
                }
            }
            Vector3 pos = GetFootprintCenter(cellpos, selected.size);
            GameObject _shell = Instantiate(selected.prefab, spawnpos.position, Quaternion.identity);
            crustaceanspawner.loweramount(ToSpawnShell);
            GameObject ghostshell = Instantiate(selected.ghostprefab, pos, Quaternion.identity);
            _shell.GetComponent<spawning>().toReachlocation = pos;
            _shell.GetComponent<spawning>().size = selected.size;
            _shell.GetComponent<spawning>().startcell = cellpos;
            _shell.GetComponent<spawning>().gridmanager = this;

            //grid[cellpos.x, cellpos.y].occupied = true;
            //grid[cellpos.x,cellpos.y].shelltype = pillbug;
            //Vector3 pos = tilemap.GetCellCenterWorld(cellpos);
            
        }
    }
    bool canplace(Vector3Int startcell, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int currentCell = startcell + new Vector3Int(x, y, 0);

                // Outside the grid
                if (currentCell.x < 0 ||
                    currentCell.x >= grid.GetLength(0) ||
                    currentCell.y < 0 ||
                    currentCell.y >= grid.GetLength(1))
                {
                    return false;
                }

                // Cell already occupied
                if (grid[currentCell.x, currentCell.y].occupied)
                {
                    return false;
                }
            }
        }

        return true;
    }
    Vector3 GetFootprintCenter(Vector3Int startCell, Vector2Int size)
    {
        // Center of the entire footprint
        Vector3 bottomLeft = tilemap.GetCellCenterWorld(startCell);

        Vector3 offset = new Vector3(
            (size.x - 1) * tilemap.cellSize.x / 2f,
            (size.y - 1) * tilemap.cellSize.y / 2f,
            0
        );

        return bottomLeft + offset;
    }

    public void clearcell(Vector3Int pos, Vector2Int size)
    {
        //Vector3Int cellpos = tilemap.WorldToCell(pos);
        //Vector3Int centerCell = tilemap.WorldToCell(pos);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int currentcell = pos + new Vector3Int(x, y, 0);

                grid[currentcell.x, currentcell.y].occupied = false;
                grid[currentcell.x, currentcell.y].shelltype = null;
            }
        }
    }
    public void OnPillBug()
    {
        ToSpawnShell = 0;
        selectedname.text = "selected : Pillbug";
    }
    public void OnTurtle()
    {
        ToSpawnShell = 1;
        selectedname.text = "selected : Turtle";
    }
    public void OnSnail()
    {
        ToSpawnShell = 2;
        selectedname.text = "selected : Cone Snail";
    }
    public void OnPistolShrimp()
    {
        ToSpawnShell = 3;
        selectedname.text = "selected : Pistol Shrimp";
    }
    public void OnHermitCrab()
    {
        ToSpawnShell = 4;
        selectedname.text = "selected : King Crab";
    }
}

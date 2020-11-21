using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTilePopulator : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile tileguy;
    public Tile tileguy2;
    private Grid gridparent;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        gridparent = transform.parent.GetComponent<Grid>();
        //places 1-14
        for(int i = 1; i <= 14; i++)
        {
            //places A-W
            for(int j = 1; j <= 23; j++)
            {
                tilemap.SetTile(new Vector3Int(-i, j, 0), tileguy);
            }
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //get mouse pos
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log("mouse pos"+worldPosition);

            //convert it to cell pos
            var cellPos = gridparent.WorldToCell(worldPosition);
            Debug.Log("cell in grid"+cellPos);

            //get the center of the cell in worldpos
            var worldpos = gridparent.GetCellCenterWorld(cellPos);
            tilemap.SetTile(cellPos, tileguy2);
        }
    }

    string CellPosToName(Vector3Int pos)
    {
        return "Cell:" + (char)(pos.y+64) + -pos.x;
    }

    
}
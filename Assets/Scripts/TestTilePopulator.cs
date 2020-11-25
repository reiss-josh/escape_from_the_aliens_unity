using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTilePopulator : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile[] tileArr;
    private Grid gridparent;
    private int[,] MapArr;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        gridparent = transform.parent.GetComponent<Grid>();
        TestLoadingMaps mapLoader = (TestLoadingMaps)FindObjectOfType(typeof(TestLoadingMaps));
        MapArr = mapLoader.MapArray;
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
            var b = tilemap.GetTile(cellPos);
            Debug.Log(b);
            tilemap.SetTile(cellPos, tileArr[1]);
        }
        if (Input.GetMouseButtonDown(1))
        {
            for(int i = 0; i < MapArr.GetLength(0); i++)
            {
                for(int j = 0; j < MapArr.GetLength(1); j++)
                {
                    tilemap.SetTile(new Vector3Int(j,i+1,0), tileArr[MapArr[i,j]]);
                }
            }
        }
    }

    string CellPosToName(Vector3Int pos)
    {
        return "Cell:" + (char)(pos.y+64) + -pos.x;
    }

    
}
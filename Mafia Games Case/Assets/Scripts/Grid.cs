using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public TileControl tileControl;
    public GameObject _tilePrefab;
    public int _width;
    public int _height;
    public int _spaceing;
    [HideInInspector] public List<GameObject> _grids = new List<GameObject>();
    [HideInInspector] public List<GameObject> _tiles=new List<GameObject>();
    [HideInInspector] public List<Tile> _tilesSc=new List<Tile>();
    private int _listCount;

    private void OnEnable()
    {
        EventManager.getGrid += GetGrid;
    }

    private void OnDisable()
    {
        EventManager.getGrid -= GetGrid;
    }

    void Awake()
    {
        SetUp();
    }

    #region grid creation
    private void SetUp()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 tempPosition = new Vector3(i*_spaceing, j*_spaceing, 0);
                GameObject backgroundTile = Instantiate(_tilePrefab, tempPosition, Quaternion.identity);
                backgroundTile.transform.parent = transform;
                backgroundTile.name="("+i+","+j+")";
                _tiles.Add(backgroundTile);               
                _grids.Add(backgroundTile);

                Tile tileSc = backgroundTile.transform.GetComponent<Tile>();
                _tilesSc.Add(tileSc);
                tileSc.row = i;
                tileSc.coloum = j;
                tileSc.grid= transform.GetComponent<Grid>();
                tileSc._listIndex = _listCount;
                _listCount++;
            }
        }
    }
    #endregion

    #region GetEvents
    private Grid GetGrid()
    {
        return GetComponent<Grid>();
    }
    #endregion
}

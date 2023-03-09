using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public Grid grid;
    [HideInInspector] public int _listIndex;
    [HideInInspector] public bool _isLoaded;
    [HideInInspector] public int row,coloum;
    [HideInInspector] public int _minionType; // 1=redminion, 2=greenminion, 3=blueminion  
    [HideInInspector] public GameObject _minion;
    [HideInInspector] public Tile _topTile, _rightTile, _downTile, _leftTile;

    void Start()
    {
        grid = EventManager.getGrid?.Invoke();
        PairingTile();
    }

    #region checking surrounding tiles
    private void PairingTile()
    {
        if (row > 0) _leftTile = grid._grids[_listIndex - grid._width].transform.GetComponent<Tile>();
        if (row < grid._width-1) _rightTile = grid._grids[_listIndex + grid._width].transform.GetComponent<Tile>();
        if (coloum > 0) _downTile = grid._grids[_listIndex - 1].transform.GetComponent<Tile>();
        if (coloum < grid._height-1) _topTile = grid._grids[_listIndex +1].transform.GetComponent<Tile>();
    }
    #endregion

    #region Checking the Matching Tile
    public void SurroundingTileControl()
    {
        if (_topTile != null)
        {
            if (_minionType == _topTile._minionType)
            {
                if (!grid.tileControl.tiles.Contains(_topTile))
                {
                    grid.tileControl.tiles.Add(_topTile);
                }
            }
        }
        if (_rightTile != null)
        {
            if (_minionType == _rightTile._minionType)
            {
                if (!grid.tileControl.tiles.Contains(_rightTile))
                {
                    grid.tileControl.tiles.Add(_rightTile);
                }
            }
        }
        if (_downTile != null)
        {
            if (_minionType == _downTile._minionType)
            {
                if (!grid.tileControl.tiles.Contains(_downTile))
                {
                    grid.tileControl.tiles.Add(_downTile);
                }
            }
        }
        if (_leftTile != null)
        {
            if (_minionType == _leftTile._minionType)
            {
                if (!grid.tileControl.tiles.Contains(_leftTile))
                {
                    grid.tileControl.tiles.Add(_leftTile);
                }
            }
        }
        grid.tileControl._tilesCount++;
        grid.tileControl.ControlTile();
    }
    #endregion
}

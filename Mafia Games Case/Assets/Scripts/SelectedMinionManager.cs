using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMinionManager : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector] public GameObject _selectedMinion;
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;
    private BoxCollider boxCollider;
    public TileControl tileControl;
    private Grid grid;

    void Start()
    {
        gameManager = EventManager.getGameManager?.Invoke();
        grid = EventManager.getGrid?.Invoke();
    }

    void Update()
    {
        if (gameManager._isGame && !gameManager._iswin)
        {
            Selected();
        }
    }

    #region control the selected minion
    private void Selected()
    {
        _screenPosition = Input.mousePosition;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);

        if (Physics.Raycast(ray, out hit,100))
        {
            _worldPosition = hit.point;
            _worldPosition.z = 0;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.CompareTag(Consts.Tags.Minion))
                {
                    _selectedMinion = hit.transform.gameObject;
                    boxCollider = _selectedMinion.transform.GetComponent<BoxCollider>();
                    boxCollider.enabled = false;
                }
            }

            if (Input.GetMouseButton(0) && _selectedMinion != null)
            {
                _worldPosition.z = 0;
                _selectedMinion.transform.position = _worldPosition;
            }

            if (Input.GetMouseButtonUp(0) && _selectedMinion!=null)
            {
                Minion minionSc = _selectedMinion.transform.GetComponent<Minion>();
                if (hit.transform.CompareTag(Consts.Tags.Tile))
                {
                    Vector3 targetPos = hit.transform.position;
                    _selectedMinion.transform.position = targetPos;                   
                    minionSc._currentPos = targetPos;
                    minionSc._tileSc._isLoaded = false;
                    minionSc._tileSc._minionType = 0;
                    grid._tiles.Add(minionSc._tileSc.transform.gameObject);
                    minionSc._tileSc._minion = null;
                    Tile tileSc = hit.transform.GetComponent<Tile>();
                    minionSc._tileSc = tileSc;
                    minionSc._tileSc._isLoaded = true;
                    minionSc._tileSc._minionType = minionSc._minionType;
                    grid._tiles.Remove(minionSc._tileSc.gameObject);
                    minionSc._tileSc._minion = _selectedMinion;

                    tileControl.tiles.Clear();
                    tileControl._tilesCount = 0;
                    tileControl.tiles.Add(tileSc);
                    tileControl.ControlTile();
                }
                else
                {
                    Vector3 targetPos = minionSc._currentPos;
                    _selectedMinion.transform.position = targetPos;
                }

                boxCollider.enabled = true;
                boxCollider = null;
                _selectedMinion = null;
            }
        }     
    }
    #endregion
}

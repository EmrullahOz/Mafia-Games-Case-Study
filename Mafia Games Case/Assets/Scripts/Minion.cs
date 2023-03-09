using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [HideInInspector] public MinionCreating minionCreating;
    [HideInInspector] public Vector3 _currentPos;
    [HideInInspector] public Tile _tileSc;
    public int _minionType; // 1=redminion, 2=greenminion, 3=blueminion 

    private void OnDisable()
    {
        _tileSc._isLoaded = false;
        _tileSc._minionType = 0;
        if(_tileSc.grid!=null) _tileSc.grid._tiles.Add(_tileSc.transform.gameObject);
        _tileSc._minion = null;
        _tileSc = null;
    }
}

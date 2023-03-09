using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinionCreating : MonoBehaviour
{
    public GameObject[] _minions;
    private Grid _grid;

    void Start()
    {
        _grid = EventManager.getGrid?.Invoke();
        MinionPooling(7);
    }

    #region withdraw minions from the minion pool
    public void MinionPooling(int createCount)
    {
        for (int i = 0; i < createCount; i++)
        {
            int index = Random.Range(0, _grid._tiles.Count);
            Transform target = _grid._tiles[index].transform;
            Vector3 tempPosition = new Vector3(target.transform.position.x, 15, 0);
            int miniontype = Random.Range(0, _minions.Length);
            GameObject minion = _minions[miniontype].transform.GetChild(0).transform.gameObject;
            minion.transform.position = tempPosition;
            minion.SetActive(true);
            minion.transform.parent = transform;
            minion.transform.DOMove(target.position, 0.4f);

            Minion minionSc = minion.transform.GetComponent<Minion>();
            minionSc.minionCreating = this;
            minionSc._currentPos = target.position;
            Tile tileSc = _grid._tiles[index].transform.GetComponent<Tile>();
            minionSc._tileSc = tileSc;
            tileSc._isLoaded = true;
            tileSc._minionType = minionSc._minionType;
            tileSc._minion = minion;
            _grid._tiles.RemoveAt(index);
        }
    }
    #endregion
}

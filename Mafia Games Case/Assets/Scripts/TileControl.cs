using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    private GameManager gameManager;
    private Grid grid;
    public MinionCreating minionCreating;
    [HideInInspector] public List<Tile> tiles=new List<Tile>();
    [HideInInspector] public List<GameObject> minions = new List<GameObject>();
    private Vector3 target;
    private float targetX, targetY;
    [HideInInspector]public int _tilesCount;

    void Start()
    {
        gameManager = EventManager.getGameManager?.Invoke();
        grid = EventManager.getGrid?.Invoke();
    }

    #region control the dropped minion and its surroundings
    public void ControlTile()
    {
        if (tiles.Count!=_tilesCount)
        {           
            for (int i = 0 + _tilesCount; i <tiles.Count ; i++)
            {
                tiles[i].SurroundingTileControl();
            }         
        }
        else if (tiles.Count>=3)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                minions.Add(tiles[i]._minion);
            }
            StartCoroutine(MinionMovement());
        }
    }
    #endregion

    #region animation of matching minions and new minions are coming
    IEnumerator MinionMovement()
    {
        gameManager._isGame = false;
        if (minions.Count >= 3) // minimum number of matches
        {
            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].transform.DOJump(minions[i].transform.position, 1, 3, 0.6f).SetEase(Ease.Linear);
            }

            int value = minions[0].transform.GetComponent<Minion>()._minionType;
            Vector3 uiPos = gameManager.scoreTexts[value-1].transform.position;
            uiPos.z = (transform.position - Camera.main.transform.position).z;
            Vector3 result = Camera.main.ScreenToWorldPoint(uiPos);
            target = result;

            yield return new WaitForSeconds(0.6f);

            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].transform.DOJump(target, 4, 1, 0.6f);
            }

            yield return new WaitForSeconds(0.6f);

            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].transform.parent = minionCreating._minions[value - 1].transform;
                minions[i].SetActive(false);
            }

            if (gameManager.scores[value - 1] < gameManager.targetScore)
            {
                gameManager.scores[value - 1]++;
                gameManager.scoreTexts[value-1].text = gameManager.scores[value - 1].ToString() + "/" + gameManager.targetScore.ToString();
            }

            yield return new WaitForSeconds(0.35f);

            int totalNumber=0;
            for (int i = 0; i < gameManager.scores.Length; i++)
            {
                if (gameManager.scores[i] == gameManager.targetScore) totalNumber++;
            }

            if (totalNumber == gameManager.scoreTexts.Length )
            {
                EventManager.openWinPanel?.Invoke();
            }
            else minionCreating.MinionPooling(minions.Count);           
        }
        minions.Clear();
        tiles.Clear();
        gameManager._isGame = true;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool _isGame, _iswin;
    public GameObject winPanel;
    public TextMeshProUGUI[] scoreTexts;
    public int targetScore;
    [HideInInspector] public int[] scores;

    private void OnEnable()
    {
        EventManager.getGameManager += GetGameManager;
        EventManager.openWinPanel += OpenWinPanel;
    }

    private void OnDisable()
    {
        EventManager.getGameManager -= GetGameManager;
        EventManager.openWinPanel -= OpenWinPanel;
    }

    void Start()
    {
        _isGame = true;
        scores = new int[scoreTexts.Length];
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = "0/" + targetScore.ToString();
        }
    }

    #region GetEvents
    private GameManager GetGameManager()
    {
        return GetComponent<GameManager>();
    }
    private void OpenWinPanel()
    {
        _iswin = true;
        winPanel.SetActive(true);
    }
    #endregion
}

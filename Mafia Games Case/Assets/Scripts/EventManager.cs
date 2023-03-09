using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    #region GameManager Events
    public static Func<GameManager> getGameManager;
    public static Action openWinPanel;
    #endregion

    #region Grid Events
    public static Func<Grid> getGrid;
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton pattern

    /*
    ** Singleton pattern
    */

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public bool IsGameStarted = false;
    public bool IsGameOver = false;
    public bool IsPlayerInControl = false;

    public void SetPlayerControl(bool hasControl)
    {
        IsPlayerInControl = hasControl;
    }

    public void StartGame()
    {
        IsGameStarted = true;
        SetPlayerControl(true);
        WaveManager.Instance.StartWave();
    }

    public void FinishGame()
    {
        IsPlayerInControl = false;
        IsGameOver = true;
    }

}

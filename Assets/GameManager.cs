using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Canvas restartCanvas;

    private void OnEnable()
    {
        instance = this;
    }

    public event Action onEndGame;
    public void EndGame()
    {
        if (onEndGame != null)
            onEndGame();
    }
    public event Action onAdjustCameraForEnding;
    public void AdjustCamera()
    {
        if (onAdjustCameraForEnding != null)
            onAdjustCameraForEnding();
    }



    public void GameLost()
    {
        //todo
        //enable canvas stop the time. start time load scene
        Debug.Log("hey");
        restartCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

}

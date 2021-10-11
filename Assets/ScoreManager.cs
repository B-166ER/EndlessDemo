using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] Text scoreText;
    [SerializeField] Text multiplierText;
    int score;
    int scoreMultiplier=1;

    private void OnEnable()
    {
        instance = this;
    }
    public void setMultiplier(int multip)
    {
        multiplierText.gameObject.SetActive(true);
        scoreMultiplier = multip;
        multiplierText.text = "X" + scoreMultiplier.ToString();
    }
    public void AddScore(int addition)
    {
        score += addition;
        UpdateScore();
    }
    public void UpdateScore()
    {
        Debug.Log("hey "+ score);
        scoreText.text = (score*scoreMultiplier).ToString();
    }

}

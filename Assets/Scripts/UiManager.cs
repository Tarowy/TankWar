using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager uiManager;

    public GameObject gameOverImg;
    public Text playerScore;
    public Text lifeValue;

    private int _score;
    
    private void Awake()
    {
        uiManager = this;
    }

    public void UpdatePlayerScore()
    {
        playerScore.text = (++_score).ToString();
    }

    public void UpdateLifeValue(int value)
    {
        lifeValue.text = value.ToString();
    }

    public void ShowGameOver()
    {
        gameOverImg.SetActive(true);
    }
}

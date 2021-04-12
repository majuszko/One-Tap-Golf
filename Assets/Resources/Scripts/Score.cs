using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score;
    public Text highScoreText;
    public Text scoreTextPanel;
    
    
    private void Awake()
    {
        highScoreText.text = "BEST: " + GetScore().ToString();
    }
    
    public void ScoreAdd()
    {
        score++;
        scoreText.text = score.ToString();
        scoreTextPanel.text = "SCORE: " + score.ToString();
        if (score > GetScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "BEST: " + score.ToString();
        }
    }
    public int GetScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}

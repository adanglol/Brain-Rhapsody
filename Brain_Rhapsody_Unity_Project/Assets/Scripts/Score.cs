
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int score = 0;
    public Text scoreText;

    public int GetScore()
    {
        Debug.Log("Score: " + score);
        return score;
    }

    public void IncrementScore(int points)
    {
        score += points;
        // Update your UI to reflect the new score
        scoreText.text = "Score: " + score;
    }
    public void Update(){
        GetScore();
    }

}
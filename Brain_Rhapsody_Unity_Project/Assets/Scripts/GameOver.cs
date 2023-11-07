using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverManager : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        Cursor.visible = true;
        // Retrieve the score value from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore");

        // Display the final score in the game over scene
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void RestartGame()
    {
        // Reset the score value in PlayerPrefs
        PlayerPrefs.SetInt("FinalScore", 0);
        // Reload the scene
        SceneManager.LoadScene("JO_Workspace");
    }
}

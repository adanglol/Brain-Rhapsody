using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    // reference game objects that will be our Menu UI buttons
    public GameObject startButton;
    public GameObject creditButton;
    public GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Menu Start");
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("JO_Workspace");
    }

    public void Credits()
    {
        Debug.Log("Credits");
        SceneManager.LoadScene("Credit_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void QuitMenu()
    {
        Debug.Log("Quit Menu");
        SceneManager.LoadScene("Menu_Scene");
    }


}

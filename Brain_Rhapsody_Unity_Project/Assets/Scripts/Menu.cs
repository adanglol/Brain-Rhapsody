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

    // reference list sprites to change the button sprite
    public List<Sprite> buttonSprites;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Menu Start");
    }
    public void HoverStart(){
        Debug.Log("Hover Start");
    }

    public void HoverStartExit(){
        Debug.Log("Hover Start Exit");
    }

    public void StartGame()
    {
        // Getting the image game object
        // GameObject imageGameObject = GameObject.Find("ButtonStartImage");
        // Getting the image component
        // Image imageComponent = imageGameObject.GetComponent<Image>();
        // Changing the sprite
        // imageComponent.sprite = buttonSprites[2];
        Debug.Log("Start Game");
        // Change the sprite of the button to the pressed sprite

        // load the game scene
        SceneManager.LoadScene("IntroCutscene");
    }

    public void HoverCredit(){
        Debug.Log("Hover Credit");
    }

    public void HoverCreditExit(){
        Debug.Log("Hover Credit Exit");
    }

    public void Credits()
    {
        Debug.Log("Credits");
        SceneManager.LoadScene("Credit_Scene");
    }

    public void HoverQuit(){
        Debug.Log("Hover Quit");
    }

    public void HoverQuitExit(){
        Debug.Log("Hover Quit Exit");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Credit Scene - Back Button
    public void QuitMenu()
    {
        Debug.Log("Quit Menu");
        SceneManager.LoadScene("Menu");
    }

    public void Guide()
    {
        Debug.Log("Guide");
        SceneManager.LoadScene("Guide 1");
    }


}

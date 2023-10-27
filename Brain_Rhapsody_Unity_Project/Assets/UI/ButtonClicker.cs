using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ButtonClicker : MonoBehaviour
{
    UIDocument buttonDocument;
    Button uiButton;

    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        if (buttonDocument == null)
        {
            Debug.LogError("Button Document is null");

        }

        uiButton = buttonDocument.rootVisualElement.Q("TestButton") as Button;
        
        if (uiButton != null)
        {
            Debug.Log("Button found");
        }

        uiButton.RegisterCallback<ClickEvent>(onButtonClick);
    }
    public void onButtonClick(ClickEvent evt)
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene("SampleScene");
    }
}

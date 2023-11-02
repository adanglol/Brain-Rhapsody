using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageClicker : MonoBehaviour
{
    public GameObject tong; // Reference to your GameObject named "Tong" in the Unity inspector

    void Start()
    {
        if (tong == null)
        {
            Debug.LogError("GameObject 'Tong' not assigned in the inspector");
            return;
        }

        // Find the first child GameObject with an Image component
        Image canvasImage = tong.GetComponentInChildren<Image>();

        if (canvasImage == null)
        {
            Debug.LogError("Image component not found among the children of GameObject 'Tong'");
            return;
        }

        // Register a click event for the Image component
        canvasImage.GetComponent<Button>().onClick.AddListener(OnImageClick);
    }

    void OnImageClick()
    {
        Debug.Log("Image Clicked");
        // Perform the action you want when the image is clicked
    }
}

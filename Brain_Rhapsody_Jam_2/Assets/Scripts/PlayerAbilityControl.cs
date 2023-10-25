using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityControl : MonoBehaviour
{

    // private variables

    private SpriteRenderer rend;
    private int currentSkin;

    [SerializeField] private Sprite[] playerSkins;
    [SerializeField] private Sprite[] weaponSkins;

    // player stats

    [SerializeField] private float fireRate;
    [SerializeField] private float power;


    // Start is called before the first frame update
    void Start()
    {
        currentSkin = 0;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // change sprite form appearance
        if (Input.GetKeyDown("1")) { 
            rend.sprite = playerSkins[0];
        }
        if (Input.GetKeyDown("2"))
        {
            rend.sprite = playerSkins[1];
        }
        if (Input.GetKeyDown("3"))
        {
            rend.sprite = playerSkins[2];
        }
    }

    // private methods
    
    //possible different method of shifting forms
    private void ChangeSkin()
    {
        if (currentSkin == 0)
        {
            rend.sprite = playerSkins[1];
            currentSkin++;
        }
        else if (currentSkin == 1)
        {
            rend.sprite = playerSkins[2];
            currentSkin++;
        }
        else if (currentSkin == 2)
        {
            rend.sprite = playerSkins[0];
            currentSkin = 0;
        }
    }
}

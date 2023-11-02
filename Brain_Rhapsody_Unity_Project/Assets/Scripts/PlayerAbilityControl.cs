using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityControl : MonoBehaviour
{

    //Music Controller Call
    public musicController musicScript;
    
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
        musicScript = GetComponent<musicController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // change sprite form appearance
        if (Input.GetKeyDown("1")) { 
            rend.sprite = playerSkins[0];
            musicScript.playerForm = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            rend.sprite = playerSkins[1];
            musicScript.playerForm = 1;
        }
        if (Input.GetKeyDown("3"))
        {
            rend.sprite = playerSkins[2];
            musicScript.playerForm = 2;
        }
        if (Input.GetKeyDown("4"))
        {
            rend.sprite = playerSkins[3];
            Debug.Log("Form 4");
            musicScript.playerForm = 3;
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

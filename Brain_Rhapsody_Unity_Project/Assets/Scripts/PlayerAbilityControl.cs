using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityControl : MonoBehaviour
{

    // private variables

    private SpriteRenderer rend;

    [SerializeField] private Sprite[] playerSkins;

    // player stats

    [SerializeField] private float fireRate;
    [SerializeField] private float power;
    private float delayTimer = 0f;
    private bool pause;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        // set default skin
        rend.sprite = playerSkins[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) { 
            pause = true;
            rend.sprite = playerSkins[0];

            rend.sprite = playerSkins[1];
        }
        if (Input.GetKeyDown("2"))
        {
            rend.sprite = playerSkins[2];
            pause = true;

        }
        if (Input.GetKeyDown("3"))
        {
            pause = true;
            rend.sprite = playerSkins[3];
        }
        if (Input.GetKeyDown("4"))
        {
            pause = true;
            rend.sprite = playerSkins[4];
            Debug.Log("Form 4");
        }
       

    if (pause)
        {
            Time.timeScale = 0;
            delayTimer += Time.fixedDeltaTime;
            if (delayTimer > 5)
            {
                pause = false;
                delayTimer = 0;
                Time.timeScale = 1;
            }
        }

    }

}

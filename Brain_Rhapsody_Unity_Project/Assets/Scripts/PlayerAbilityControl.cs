using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityControl : MonoBehaviour
{

    // private variables

    private SpriteRenderer rend;

    [SerializeField] private Sprite[] playerSkins;
    private float delayTimer = 0f;
    private bool pause;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) { 
            pause = true;
            rend.sprite = playerSkins[0];
        }
        if (Input.GetKeyDown("2"))
        {
            pause = true;
            rend.sprite = playerSkins[1];
        }
        if (Input.GetKeyDown("3"))
        {
            pause = true;
            rend.sprite = playerSkins[2];
        }
        if (Input.GetKeyDown("4"))
        {
            pause = true;
            rend.sprite = playerSkins[3];
            Debug.Log("Form 4");
        }
       

    if (pause)
        {
            Time.timeScale = 0;
            delayTimer += Time.fixedDeltaTime;
            Debug.Log(delayTimer);
            if (delayTimer > 5)
            {
                pause = false;
                delayTimer = 0;
                Time.timeScale = 1;
            }
        }

    }

}

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


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // read for form change button; change skin and music when pressed
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
        if (Input.GetKeyDown("4"))
        {
            rend.sprite = playerSkins[3];
            Debug.Log("Form 4");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCursor : MonoBehaviour
{

    [SerializeField] public Sprite[] weaponSkins;
    public SpriteRenderer rend;

    // Reference health script
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.formStatus[0] == true){
            if(Input.GetKeyDown("1")){
                rend.sprite = weaponSkins[0];
            }
        }
        if(playerHealth.formStatus[1] == true){
            if(Input.GetKeyDown("2")){
                rend.sprite = weaponSkins[1];
            }
        }
        if(playerHealth.formStatus[2] == true){
            if(Input.GetKeyDown("3")){
                rend.sprite = weaponSkins[2];
            }
        }
        if(playerHealth.formStatus[3] == true){
            if(Input.GetKeyDown("4")){
                rend.sprite = weaponSkins[3];
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCursor : MonoBehaviour
{

    [SerializeField] private Sprite[] weaponSkins;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("1"))
        {
            rend.sprite = weaponSkins[0];
        }
        if (Input.GetKeyDown("2"))
        {
            rend.sprite = weaponSkins[1];
        }
        if (Input.GetKeyDown("3"))
        {
            rend.sprite = weaponSkins[2];
        }
        if (Input.GetKeyDown("4"))
        {
            rend.sprite = weaponSkins[3];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireScript : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletSkins;

    private float timer;
    private GameObject currentFormBullet;

    [SerializeField] private Sprite[] weaponSkins;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private bool canFire;
    [SerializeField] private float fireDelay;

    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        //player starts with base form
        canFire = true;
        currentFormBullet = bulletSkins[0];

        // variable to reference main camera 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate player aim cursor

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimRotation = mousePos - transform.position;
        float zRotation = Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation - 90).normalized;

        //check for form switch, switch player bullet to fire to use
        if (Input.GetKeyDown("1"))
        {
            currentFormBullet = bulletSkins[0];
        }
        if (Input.GetKeyDown("2"))
        {
            currentFormBullet = bulletSkins[1];
        }
        if (Input.GetKeyDown("3"))
        {
            currentFormBullet = bulletSkins[2];
        }

        // check if bullet has not been fired, advance timer
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireDelay)
            {
                canFire = true;
                timer = 0;
            }
        }
        // fire a bullet if player clicks; timer is reset
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
        }
    }
}

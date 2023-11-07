using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponFireScript : MonoBehaviour
{
    [SerializeField] public GameObject[] bulletSkins;

    private float timer;
    [SerializeField]public GameObject currentFormBullet;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private bool canFire;
    [SerializeField] private float fireDelay;

    [SerializeField] private AudioSource astroFire;
    [SerializeField] private AudioSource scubaFire;
    [SerializeField] private AudioSource mobFire;
    [SerializeField] private AudioSource cowboyFire;

    private Camera mainCam;
    private Vector3 mousePos;



   
    // Spread Settings for Cowboy Form
    [SerializeField] private int cowboySpreadBullets = 6;
    [SerializeField] private float cowboySpreadAngle = 60f;

    // playerhealth script reference
    public PlayerHealth playerHealth;

    //private utils
    private int currentForm;

    // Start is called before the first frame update
    void Start()
    {
        // hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        //player starts with base form
        canFire = true;
        currentFormBullet = bulletSkins[0];

        // variable to reference main camera 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // get current form from player health script
        currentForm = playerHealth.currentForm;
        // rotate player aim cursor
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimRotation = mousePos - transform.position;
        float zRotation = Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation - 90).normalized;

        //check for form switch, switch player bullet to fire to use
        // AstroNaut form
        if(playerHealth.formStatus[0] == true)
        {
            if (Input.GetKeyDown("1"))
            {
                Debug.Log("AstroNaut Form");
                currentFormBullet = bulletSkins[0];
            }
        }
        // ScubaDiver form
        if(playerHealth.formStatus[1] == true)
        {
            if (Input.GetKeyDown("2"))
            {
                Debug.Log("ScubaDiver Form");
                currentFormBullet = bulletSkins[1];
            }
        }
        // Mob Boss Form
        if(playerHealth.formStatus[2] == true)
        {
            if (Input.GetKeyDown("3"))
            {
                Debug.Log("Mob Boss Form");
                currentFormBullet = bulletSkins[2];
            }
        }
        // Cowboy Form
        if(playerHealth.formStatus[3] == true)
        {
            if (Input.GetKeyDown("4"))
            {
                Debug.Log("Cowboy Form");
                currentFormBullet = bulletSkins[3];
                // cowboyBulletSpread = currentFormBullet.GetComponent<CowboyBulletSpread>(); // Get the CowboyBulletSpread component for Cowboy Form
            }
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
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            switch(currentForm)
            {
                case 1:
                    // AstroNaut Form SHOOT
                    if(playerHealth.formStatus[0] == true)
                    {
                        Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
                        astroFire.Play();
                    }
                    break;
                case 2:
                    // ScubaDiver Form SHOOT
                    if(playerHealth.formStatus[1] == true)
                    {
                        StartCoroutine(ScubaBurstFire());

                    }
                    break;
                case 4:
                    // Cowboy Form SHOOT
                    if(playerHealth.formStatus[3] == true)
                    {
                        ShootCowboySpread();
                    }
                    break;
                case 0: //base player form is 10
                    break;
            }
        }
        if(playerHealth.formStatus[2] == true){
            if (Input.GetMouseButton(0))
            { // Mob Boss Fire Weapon
                if(currentFormBullet == bulletSkins[2] && canFire && currentForm == 3)
                {
                    Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
                }
            }
        }
    }

    // Method handle Cowboy Form's spread shot
    void ShootCowboySpread()
    {
        Debug.Log("ShootCowboySpread()");

        // Calculate the total angle range for the spread
        float totalSpreadAngle = cowboySpreadBullets * cowboySpreadAngle;

        // Calculate the time delay between each shot
        float delayBetweenShots = 10f; // Adjust as needed

        // Calculate the direction to shoot the bullets
        Vector3 mouseDirection = (mousePos - transform.position).normalized;

        for (int i = 0; i < cowboySpreadBullets; i++)
        {
            // Calculate a rotation based on the spread angle
            float bulletRotation = -totalSpreadAngle / 2 + i * cowboySpreadAngle;

            // Convert the bulletRotation to radians
            float radians = Mathf.Deg2Rad * bulletRotation;

            // Calculate a direction vector for the bullet
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // Calculate the position for each bullet based on the spread, on one side of the sprite
            Vector3 spreadPosition = bulletTransform.position + (transform.up * 0.5f) + (new Vector3(direction.x, direction.y, 0) * 0.5f);

      
            // Calculate the direction to shoot the bullets
            Vector2 playerToMouse = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
            Vector2 bulletDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // Instantiate the bullet at the calculated position
            GameObject bullet = Instantiate(currentFormBullet, spreadPosition, Quaternion.identity);

            //Play sound effect
            cowboyFire.Play();

            // bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            // Set the bullet's velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection.normalized * 5f;

            // Add a delay before the next shot
            StartCoroutine(DelayNextShot(i, delayBetweenShots));
        }

    }

    // Coroutine to add a delay between shots
    IEnumerator DelayNextShot(int shotIndex, float delay)
    {
        yield return new WaitForSeconds(shotIndex * delay);
    }

    // Couroutine for scuba burst fire
    IEnumerator ScubaBurstFire()
    {
        int shotsFired = 0;
        while (shotsFired < 3)
        {
            Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
            scubaFire.Play();
            shotsFired++;
            yield return new WaitForSeconds(0.2f);
        } 

    }

   
    
}

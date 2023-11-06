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

    // AstroNaut Form Settings charge shot
    private bool isCharging = false;
    private float chargeTime = 0.0f;
    public float maxChargeTime = 3.0f;
    public float maxChargeDamage = 10.0f;

   
    // Spread Settings for Cowboy Form
    [SerializeField] private int cowboySpreadBullets = 6;
    [SerializeField] private float cowboySpreadAngle = 60f;

    // playerhealth script reference
    public PlayerHealth playerHealth;

    //private utils
    private int currentForm;
  
    void Awake(){
        currentForm = 10;
    }

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
        currentForm = playerHealth.currentForm;
        Debug.Log("Player Form: aaa" + "" + currentForm);
        // rotate player aim cursor

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimRotation = mousePos - transform.position;
        float zRotation = Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation - 90).normalized;

        //check for form switch, switch player bullet to fire to use
        // AstroNaut form
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("AstroNaut Form");
            currentFormBullet = bulletSkins[0];
        }
        // ScubaDiver form
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("ScubaDiver Form");
            currentFormBullet = bulletSkins[1];
        }
        // Mob Boss Form
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("Mob Boss Form");
            currentFormBullet = bulletSkins[2];
        }
        // Cowboy Form
        if (Input.GetKeyDown("4"))
        {
            Debug.Log("Cowboy Form");
            currentFormBullet = bulletSkins[3];
            // cowboyBulletSpread = currentFormBullet.GetComponent<CowboyBulletSpread>(); // Get the CowboyBulletSpread component for Cowboy Form
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
                case 0:
                    // AstroNaut Form SHOOT
                    Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
                    astroFire.Play();
                    break;
                case 1:
                    // ScubaDiver Form SHOOT
                    StartCoroutine(ScubaBurstFire());
                    break;
                case 3:
                    // Cowboy Form SHOOT
                    ShootCowboySpread();
                    break;
                case 10: //base player form is 10
                    break;
            }
        }
        //Mob Boss Shoot Weapon
        if (Input.GetMouseButton(0)){ 
            if(currentFormBullet == bulletSkins[2] && canFire && currentForm == 2)
            {
                Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);
            }
        }

         if (isCharging)
        {
            chargeTime += Time.deltaTime;
            Debug.Log("Charge Time: " + chargeTime);

            // TBI - add visual feedback for charge

            if (chargeTime >= maxChargeTime)
            {
                // Fire a charged bullet
                Debug.Log("Fire Charged Bullet");
                FireChargedBullet();
                isCharging = false;

            }
        }
        

   

      
       
    }

    // Method to fire a charged bullet
     void FireChargedBullet()
    {
        // Instantiate the charged bullet with the current damage
        GameObject chargedBullet = Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);

        // Set the bullet's damage based on 'chargeTime' and 'maxChargeDamage'

        // Add a cooldown before the next charge can start
        StartCoroutine(RechargeCooldown());
    }

    // Coroutine to add a cooldown before the next charge can start
     IEnumerator RechargeCooldown()
    {
        yield return new WaitForSeconds(2f); // Adjust the recharge cooldown time as needed
        chargeTime = 0; // Reset charge time
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

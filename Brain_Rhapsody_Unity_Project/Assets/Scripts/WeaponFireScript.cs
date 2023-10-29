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

    // Spread Settings for Cowboy Form
    [SerializeField] private int cowboySpreadBullets = 6;
    [SerializeField] private float cowboySpreadAngle = 30f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        //player starts with base form
        canFire = true;
        currentFormBullet = bulletSkins[0];

        // variable to reference main camera 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Debug.Log("WeaponFireScript Start()");
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
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            if (currentFormBullet == bulletSkins[3])
            {
                // Cowboy Form
                ShootCowboySpread();
            }
            else
            {
                Instantiate(currentFormBullet, bulletTransform.position, Quaternion.identity);

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

   
    
}

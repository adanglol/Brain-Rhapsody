using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawnerScript : MonoBehaviour
{
    //List of shooting spread types
    private enum MechType {Straight, Spin };
    //Assign the type of shooting mechanic in inspector
    [SerializeField] private MechType mechType;

    [SerializeField] private GameObject bulletPrefab;

    [Header("Bullet Stats")]
    [SerializeField] private int bulletsPerShot; //enemy bullets per shot
    [SerializeField] private float bulletSpeed; //speed of the bullet
    [SerializeField] private float bulletLife; //lifespan of bullet; how long bullet lasts
    [SerializeField] private float shotDelay; // time between shots

    //private utility variables

    private GameObject spawnedBullet; //for bullet direction manipulation (speed, rotation)
    private float shotDelayTimer; //timer to keep track of shot delay 


    // Start is called before the first frame update
    void Start()
    {
        shotDelayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update timer to keep track of shot delay 
        shotDelayTimer += Time.deltaTime;

        //if the enemy mechanic is circle shooter, rotate this object
        if (mechType == MechType.Spin)
        {
            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z + 1f);
        }

        if (shotDelayTimer >= shotDelay)
        {
            FireShot();
            shotDelayTimer = 0; //reset shot delay timer
        }
    }

    //------------------------------ Firing functions ------------------------------


    //function to shoot bullet
    private void FireShot()
    {
            spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<EnemyBulletScript>().SetSpeed(bulletSpeed);
            spawnedBullet.GetComponent<EnemyBulletScript>().SetBulletLife(bulletLife);
            spawnedBullet.transform.rotation = transform.rotation;
    }
}

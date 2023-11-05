using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //List of shooting spread types
        // "Chaser" enemies only chase player around and do damage upon contact, then disappear
        // "Circle Shooter" enemies are static when spawned and constantly shoot waves of bullets in all directions
        // "Spread Shooter" chases player around and occasionally shoots a bullet spread pattern towards the player
    private enum MechType{Chaser, CircleShooter, ShotgunShooter};
    private enum EnemyType{Astro, Scuba, MobBoss, Cowboy};

    
    //Assign the type of enemy in inspector
    [SerializeField] private MechType mechType;
    [SerializeField] private EnemyType enemyType;

    //Assign player object in inspector
    [SerializeField] private GameObject chaseTarget;


    // Adjust enemy attributes/ stats in inspector
    [Header("Enemy Stats")]
    [SerializeField] private int enemyHealth; //total enemy health points
    [SerializeField] private float enemySpeed; // enemy movement speed
    [SerializeField] private int bulletsPerShot; //enemy bullets per shot
    [SerializeField] private float shotDelay; // time between shots
    
    //Assign the corresponding prefabs for the individual enemy in inspector
    [Header("Enemy Assets")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource enemyDeathSound;


    //private utility variables
    private Rigidbody2D rb;
    private SpriteRenderer enemySprite;
    private float shotDelayTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        shotDelayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //check health every frame
        if(enemyHealth == 0){
            StartCoroutine(enemyDeath());
        }
        //timer to keep track of shot delay 
        shotDelayTimer += Time.deltaTime;

        //FIRE CIRCLE
        //if the enemy is a circle shooter, fire using circle function every n seconds depending on shotDelay set in inspector
        if(mechType == MechType.CircleShooter){

            if(shotDelayTimer >= shotDelay){
                FireCircleSpread();
                shotDelayTimer = 0; //reset shot delay timer
            }
        }
        //FIRE SHOTGUN 
        // if the enemy is a shotgun shooter, fire using shotgun function every n seconds depending on shotDelay set in inspector
        else if(mechType == MechType.ShotgunShooter){
            //Enemy chase player 
            transform.position = Vector2.MoveTowards(transform.position, chaseTarget.transform.position, enemySpeed * Time.deltaTime);

            if(shotDelayTimer >= shotDelay){
                FireShotgunSpread();
                shotDelayTimer = 0; //reset shot delay timer
            }
        }
    }

//------------------------------ Firing functions ------------------------------


    //function to shoot circle spread from enemy origin
    private void FireCircleSpread(){
        int separationDegrees = 360/ bulletsPerShot;
        for(int i = 0; i < bulletsPerShot; i++){
            int direction = 0;
            direction += separationDegrees;
            Quaternion rotation = new Quaternion(0,0,direction, 1);
            Instantiate(bulletPrefab, transform.position, rotation);
        }

    }
    //function to fire shotgun spread toward player
    private void FireShotgunSpread(){
            Debug.Log("Fired Shotgun Spread!");
    }

//------------------------------ Collision detection ------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
//------------------------------ Triggers detection ------------------------------
    private void OnTriggerEnter2D(Collider2D collision){
    if(enemyType == EnemyType.Astro){
        if (collision.gameObject.CompareTag("Astro Bullet"))
        {
            enemySprite.color = Color.red;
            enemyHealth--;
            StartCoroutine(hitAnimation());
        }
    }
    else if(enemyType == EnemyType.Scuba){
        if (collision.gameObject.CompareTag("Scuba Bullet"))
        {
            enemySprite.color = Color.red;
            enemyHealth--;
            StartCoroutine(hitAnimation());
        }
    }
    else if(enemyType == EnemyType.MobBoss){
        if (collision.gameObject.CompareTag("Mob Bullet"))
        {
            enemySprite.color = Color.red;
            enemyHealth--;
            StartCoroutine(hitAnimation());
        }
    }
    else if(enemyType == EnemyType.Cowboy){
        if (collision.gameObject.CompareTag("Cowboy Bullet"))
        {
            enemySprite.color = Color.red;
            enemyHealth--;
            StartCoroutine(hitAnimation());
        }
    }
}
//------------------------------ Coroutines ------------------------------
    IEnumerator hitAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;

    }

    IEnumerator enemyDeath(){
        enemySprite.color = Color.black;
        enemyDeathSound.Play();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}

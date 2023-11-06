using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Types of enemy for collision and trigger detection
    private enum EnemyType{Astro, Scuba, MobBoss, Cowboy};


    //Assign the type of enemy in inspector
    [Header("Enemy Classifications")]
    [SerializeField] private EnemyType enemyType;
    //Assign whether enemy chases player or not
    [SerializeField] private bool chasesPlayer;


    // Adjust enemy attributes/ stats in inspector
    [Header("Enemy Stats")]
    [SerializeField] private int enemyHealth; //total enemy health points
    [SerializeField] private float enemySpeed; // enemy movement speed
    
    //Assign the corresponding prefabs for the individual enemy in inspector
    [Header("Enemy Assets")]
    //[SerializeField] private AudioSource enemyDeathSound;

    // reference score
    public Score score;





    //private utility variables
    private Rigidbody2D rb;
    private SpriteRenderer enemySprite;
    private bool isDead;
    private GameObject chaseTarget;


    void Awake()
    {
        chaseTarget = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //check health every frame
        if(enemyHealth <= 0){
            isDead = true;
            StartCoroutine(enemyDeath());
        }
        if (chasesPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, chaseTarget.transform.position, enemySpeed * Time.deltaTime);
        }
    }


    //------------------------------ Setters and Getters------------------------------
    public GameObject GetTarget()
    {
        return chaseTarget;
    }
    //------------------------------ Triggers detection ------------------------------
    private void OnTriggerEnter2D(Collider2D collision){
        if (isDead == false) //only look for bullet triggers if the enemy is still alive
        {
            if (enemyType == EnemyType.Astro)
            {
                if (collision.gameObject.CompareTag("Astro Bullet"))
                {
                    enemySprite.color = Color.red;
                    enemyHealth--;
                    StartCoroutine(hitAnimation());
                }
            }
            else if (enemyType == EnemyType.Scuba)
            {
                if (collision.gameObject.CompareTag("Scuba Bullet"))
                {
                    enemySprite.color = Color.red;
                    enemyHealth--;
                    StartCoroutine(hitAnimation());
                }
            }
            else if (enemyType == EnemyType.MobBoss)
            {
                if (collision.gameObject.CompareTag("Mob Bullet"))
                {
                    enemySprite.color = Color.red;
                    enemyHealth--;
                    StartCoroutine(hitAnimation());
                }
            }
            else if (enemyType == EnemyType.Cowboy)
            {
                if (collision.gameObject.CompareTag("Cowboy Bullet"))
                {
                    enemySprite.color = Color.red;
                    enemyHealth--;
                    StartCoroutine(hitAnimation());
                }
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
        //enemyDeathSound.Play();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
        score.IncrementScore(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerEnemyScript : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Enemy Stats")]
    [SerializeField] int enemyHealth;
    [SerializeField] float enemySpeed;
    [SerializeField] float enemyRotateSpeed;



    [Header("Enemy Movement Attributes")]
    private Transform target;
    private Rigidbody2D rb;



    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float bulletSpeed = 1f;


    [Header("Shooting Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;


    private GameObject spawnedBullet;
    private SpriteRenderer rend;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check health
        if (enemyHealth == 0)
        {
            Destroy(this.gameObject);
        }

        // Rotate towards target
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardTarget();
        }

        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            //Fire();
            timer = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * enemySpeed;
    }
    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void RotateTowardTarget()
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, enemyRotateSpeed);
    }
    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<EnemyBullet1>().speed = bulletSpeed;
            spawnedBullet.GetComponent<EnemyBullet1>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Scuba Bullet")
        {
            rend.color = Color.red;
            enemyHealth--;
            StartCoroutine(hitAnimation());
        }
    }

    IEnumerator hitAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;

    }

}

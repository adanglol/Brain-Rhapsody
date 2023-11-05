using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    [SerializeField] private float bulletLife = 5f;  // Defines how long before the bullet is destroyed
    [SerializeField] private  float speed = 1f;


    private Vector2 spawnPoint;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
    // --------------------Setter and Getters------------------------
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetBulletLife(float newBulletLife)
    {
        bulletLife = newBulletLife;
    }
    public float GetBulletLife()
    {
        return bulletLife;
    }

}

using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private enum BulletType{Astro, Scuba, MobBoss, Cowboy};

    [Header("Bullet Properties")]
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float bulletSpeed;
    [SerializeField] float bulletLife = 5f;  // Defines how long before the bullet is destroyed

    //private utility variables
    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall"){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(bulletType == BulletType.Astro){
            if(collision.gameObject.CompareTag("Astro Enemies")){
                Destroy(this.gameObject);
            }
        }
        else if(bulletType == BulletType.Scuba){
            if(collision.gameObject.CompareTag("Scuba Enemies")){
                Destroy(this.gameObject);
            }
        }
        else if(bulletType == BulletType.MobBoss){
            if(collision.gameObject.CompareTag("Mob Enemies")){
                Destroy(this.gameObject);
            }
        }
        else if(bulletType == BulletType.Cowboy){
            if(collision.gameObject.CompareTag("Cowboy Enemies")){
                Destroy(this.gameObject);
            }
        }
    }
}

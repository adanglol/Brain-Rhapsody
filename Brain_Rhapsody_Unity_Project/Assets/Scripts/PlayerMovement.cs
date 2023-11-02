using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // adjustable player speed
    [SerializeField] private float playerSpeed;
    
    // private variables
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            playerSpeed = 7.5f;
        }
        if (Input.GetKeyDown("2"))
        {
            playerSpeed = 10.0f;
        }
        if (Input.GetKeyDown("3"))
        {
            playerSpeed = 3.5f;
        }
        if(Input.GetKeyDown("4"))
        {
            playerSpeed = 5.0f;
        }

        // add velocity, move player
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, Input.GetAxisRaw("Vertical") * playerSpeed);
        
    // flip sprite facing direction
        if (rb.velocity.x > 0)
        {
            rend.flipX = true;
        }
        if (rb.velocity.x < 0)
        {
            rend.flipX = false;
        }
    }
}

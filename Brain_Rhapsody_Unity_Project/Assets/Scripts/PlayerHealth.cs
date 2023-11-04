using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioSource hitSound;

    private int currentForm;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        currentForm = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth == 0)
        {
            //TBI Change to death scene
            Debug.Log("Player Dead");
        }
        if (Input.GetKeyDown("1")) { 
            currentForm = 1;
        }
        if (Input.GetKeyDown("2"))
        {
            currentForm = 2;
        }
        if (Input.GetKeyDown("3"))
        {
            currentForm = 3;

        }
        if (Input.GetKeyDown("4"))
        {
            currentForm = 4;
        }

    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Astro Enemies") && currentForm != 1)
        {
            StartCoroutine(hitAnimation());
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (other.gameObject.CompareTag("Scuba Enemies") && currentForm != 2)
        {            
            StartCoroutine(hitAnimation());
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (other.gameObject.CompareTag("Mob Enemies") && currentForm != 3)
        {
            StartCoroutine(hitAnimation());
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (other.gameObject.CompareTag("Cowboy Enemies") && currentForm != 4)
        {
            StartCoroutine(hitAnimation());
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
    }
    IEnumerator hitAnimation()
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;

    }
}

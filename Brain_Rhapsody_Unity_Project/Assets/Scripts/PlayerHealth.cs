using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioSource hitSound;

    private int currentForm;

    // Start is called before the first frame update
    void Start()
    {
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Astro Enemies" && currentForm != 0)
        {
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (collision.collider.tag == "Scuba Enemies" && currentForm != 1)
        {
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (collision.collider.tag == "Mob Enemies" && currentForm != 2)
        {
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
        else if (collision.collider.tag == "Cowboy Enemies" && currentForm != 3)
        {
            currentHealth--;
            hitSound.Play();
            Debug.Log("Player Health: " + currentHealth);
        }
    }
}

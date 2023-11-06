using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // THE MAX HEALTH FOR OUR PLAYER INCLUDING ALL FORMS
    [SerializeField] private int maxHealth;
    // ARRAY TO STORE THE HEALTH FOR EACH FORM
    private int[] formHealth;
    // HIT SOUND FOR WHEN THE PLAYER GETS HIT
    [SerializeField] private AudioSource hitSound;

    // THE CURRENT FORM THE PLAYER IS IN
    public int currentForm;
    // Refer to keep track of how many forms are still alive
    private int remainingForms;
    // Array keep track of status of each form (alive or dead)
    private bool[] formStatus;

    // Reference to the player's health bar for UI for each form
    public RectTransform[] healthBarContainers;

    // sprite for our health unit
    public Sprite healthUnitSprite;

    // PLayer Sprite for each form
    private SpriteRenderer rend;

    [SerializeField] private Sprite[] playerSkins;

    // player stats

    [SerializeField] private float fireRate;
    [SerializeField] private float power;
    private float delayTimer = 0f;
    private bool pause;

    // Reference to the weapon fire script
    public WeaponFireScript weaponFireScript;

    // Referene to weapon cursor script
    public WeaponCursor weaponCursorScript;
  
    void Start()
    {
      
        // Grab the sprite renderer component
        rend = GetComponent<SpriteRenderer>();
        // set default skin
        rend.sprite = playerSkins[0];

        //SETTING THE CURRENT FORM TO 0 base form
        currentForm = 0;
        // Initially all forms are alive
        remainingForms = 4;
        // INITIALIZE HEALTH VALUES FOR FORMS AND STATUS
        formHealth  = new int[4];
        formStatus = new bool[4];
        for(int i = 0; i < 4; i++)
        {
            formHealth[i] = maxHealth / 4; // Each form has 1/4 of the max health
            formStatus[i] = true; // Alive
        }
        // Loop through each form's health container astro - cowboy
        for (int i = 0; i < healthBarContainers.Length; i++)
        {
            // Getting the containers for each form
            RectTransform container = healthBarContainers[i];
            // Calculate the size of each health unit based on the container's size and maxHealth
            float unitWidth = container.rect.width / maxHealth;
            float unitHeight = container.rect.height;
            for (int j = 0; j < maxHealth/4; j++)
            {
                //  Create a new game object for each health unit
                GameObject healthUnit = new GameObject("HealthUnitImage" + j);
                healthUnit.transform.SetParent(container);
                // Create an Image component for each health unit
                Image healthUnitImage = healthUnit.gameObject.AddComponent<Image>();
                float scaleX = 0.5f; // Image will be half the original width
                float scaleY = .3f; // Image will be half the original height
                healthUnitImage.transform.localScale = new Vector3(scaleX, scaleY, 1);
                // Set the size and position of the Image
                RectTransform imageRect = healthUnitImage.rectTransform;
                // Set the size of the Image to match the calculated unit width and container height
                imageRect.sizeDelta = new Vector2(unitWidth, unitHeight);
                float xOffset = .1f; // Adjust this value to move the images to the left
                // Set the Image's position to be calculated based on the index (j) times the unit width
                imageRect.anchoredPosition = new Vector2(j * unitWidth - xOffset, 0);
                // Set the Image's sprite to the health unit sprite
                healthUnitImage.sprite = healthUnitSprite;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("1"))
        {
                pause = true;
                currentForm = 1;
                Debug.Log("Switched to Form " + currentForm);
                rend.sprite = playerSkins[currentForm];
        }

        if (Input.GetKeyDown("2"))
        {
                pause = true;
                currentForm = 2;
                Debug.Log("Switched to Form " + currentForm);
                rend.sprite = playerSkins[currentForm];
        }

        if (Input.GetKeyDown("3"))
        {
                pause = true;
                currentForm = 3;
                Debug.Log("Switched to Form " + currentForm);
                rend.sprite = playerSkins[currentForm];
        }

        if (Input.GetKeyDown("4"))
        {
                pause = true;
                currentForm = 4;
                Debug.Log("Switched to Form " + currentForm);
                rend.sprite = playerSkins[currentForm];
        }
        if (pause)
        {
            Time.timeScale = 0;
            delayTimer += Time.fixedDeltaTime;
            if (delayTimer > 5)
            {
                pause = false;
                delayTimer = 0;
                Time.timeScale = 1;
            }
        }
            
        // Switch between forms shown on the UI for health
        switch(currentForm)
        {
            case 0:
                // Debug.Log(currentForm);
                healthBarContainers[0].gameObject.SetActive(false);
                healthBarContainers[1].gameObject.SetActive(false);
                healthBarContainers[2].gameObject.SetActive(false);
                healthBarContainers[3].gameObject.SetActive(false);
                break;

            case 1:
                // Debug.Log(currentForm);
                healthBarContainers[0].gameObject.SetActive(true);
                healthBarContainers[1].gameObject.SetActive(false);
                healthBarContainers[2].gameObject.SetActive(false);
                healthBarContainers[3].gameObject.SetActive(false);
                break;
            case 2:
                // Debug.Log(currentForm);
                healthBarContainers[0].gameObject.SetActive(false);
                healthBarContainers[1].gameObject.SetActive(true);
                healthBarContainers[2].gameObject.SetActive(false);
                healthBarContainers[3].gameObject.SetActive(false);
                break;
                
            case 3:
                // Debug.Log(currentForm);
                healthBarContainers[0].gameObject.SetActive(false);
                healthBarContainers[1].gameObject.SetActive(false);
                healthBarContainers[2].gameObject.SetActive(true);
                healthBarContainers[3].gameObject.SetActive(false);
                break;
            case 4:
                // Debug.Log(currentForm);
                healthBarContainers[0].gameObject.SetActive(false);
                healthBarContainers[1].gameObject.SetActive(false);
                healthBarContainers[2].gameObject.SetActive(false);
                healthBarContainers[3].gameObject.SetActive(true);
                break;               
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentForm > 0) // Check if it's not the base form
        {
            if (collision.collider.CompareTag("Astro Enemies") && currentForm != 1)
            {
                // TAKE DAMAGE
               TakeDamage();
            }
            else if (collision.collider.CompareTag("Scuba Enemies") && currentForm != 2)
            {
                // TAKE DAMAGE
                TakeDamage();
            }
            else if (collision.collider.CompareTag("Mob Enemies") && currentForm != 3)
            {
                TakeDamage();
            }
            else if (collision.collider.CompareTag("Cowboy Enemies") && currentForm != 4)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        // TAKE DAMAGE
        formHealth[currentForm - 1] -= 1; // Adjust for zero-based indexing
        // PLAY SOUND
        hitSound.Play();
        // UPDATE UI FOR THAT FORM
        UpdateHealthUI();

        // CHECK IF FORM IS DEAD
        if(formHealth[currentForm - 1] <= 0)
        {
            // FORM IS DEAD
            // DECREMENT REMAINING FORMS
            remainingForms--;
            // SET FORM STATUS TO DEAD
            // formStatus[currentForm - 1] = false;
            // Check if there are forms left
            Debug.Log("Remaining Forms: " + remainingForms);
            // Switch to Next Form
            if (remainingForms > 0)
            {
                SwitchToNextForm();
            }
            else // No forms left alive game over
            {
                // GAME OVER
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    private void SwitchToNextForm()
    {
        // Start checking from the next form after the current one
        for (int i = currentForm + 1; i <= currentForm + 4; i++)
        {
            int formIndex = (i - 1) % 4; // Wrap around using modulo to ensure circular behavior
            if (formHealth[formIndex] > 0)
            {
                currentForm = formIndex + 1; // Set the current form to the next available one
                UpdateHealthUI();
                Debug.Log("Switched to Form " + currentForm);
                // Switch to the next form sprite
                rend.sprite = playerSkins[currentForm];
                // Switch to the next bullet skin for the weapon
                weaponFireScript.currentFormBullet = weaponFireScript.bulletSkins[currentForm - 1];
                // Switch the weapon as well
                weaponCursorScript.rend.sprite = weaponCursorScript.weaponSkins[currentForm - 1];

                return;
            }
        }
    }
        

    private void UpdateHealthUI()
    {
        if (currentForm > 0) // Check if it's not the base form
        {
            int currentHealth = formHealth[currentForm - 1]; // Adjust for zero-based indexing
            int maxHealthUnits = maxHealth / 4;
            int currentHealthUnits = Mathf.Clamp(currentHealth, 0, maxHealthUnits);

            RectTransform currentHealthContainer = healthBarContainers[currentForm - 1];

            for (int i = 0; i < maxHealthUnits; i++)
            {
                Image healthUnitImage = currentHealthContainer.GetChild(i).GetComponent<Image>();
                healthUnitImage.enabled = (i < currentHealthUnits);
            }
        }
    }
   
}
    
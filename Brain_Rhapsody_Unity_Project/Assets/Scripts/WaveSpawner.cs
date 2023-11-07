using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    //Assign enemy prefab objects via inspector
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float waveSpawnDelay;
    [SerializeField] private int amountToSpawn;

    [SerializeField] private float xLeftBounds;
    [SerializeField] private float xRightBounds;
    [SerializeField] private float yBottomBounds;
    [SerializeField] private float yTopBounds;

    public PlayerHealth playerHealth;

    //private utility variables
    private bool canSpawn;
    private float waveSpawnTimer;
    private int currentWave;
    private bool gameStarted;
    
    void Awake(){
        gameStarted = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        waveSpawnTimer = 0;
        currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentForm != 0){
            gameStarted = true;
        }
        if(gameStarted){
            
            waveSpawnTimer += Time.deltaTime; //update timer every frame

            if(waveSpawnTimer >= waveSpawnDelay){ //once the timer is met
                canSpawn = true; //spawner can spawn enemies
                waveSpawnTimer = 0; //reset timer
            }
            if(canSpawn){
                SpawnEnemies(amountToSpawn);
                currentWave++;
                canSpawn = false;
            }
        }


        
    }

    //method that spawns enemies at random positions in a rectangular bound that can be adjusted via inspector
    private void SpawnEnemies(int numEnemies){
        Debug.Log("Spawned wave " + currentWave + "!");
        for(int i = 0; i< numEnemies; i++){
            //create a random position within the bounds
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(xLeftBounds, xRightBounds), UnityEngine.Random.Range(yBottomBounds, yTopBounds), 0.0f);
            int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length); //generate a random integer to select an enemy
            GameObject randomEnemy = enemyPrefabs[randomIndex]; //pick a random enemy prefab from the array
            Instantiate(randomEnemy, randomPosition, Quaternion.identity); //spawn enemy at radom location
        }
    }
}

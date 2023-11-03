using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;

    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;
            int randomInt = Random.Range(0, enemyPrefabs.Length);
            GameObject randomEnemy = enemyPrefabs[randomInt];
            Instantiate(randomEnemy, transform.position, Quaternion.identity);
        }
    }
}

using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnMob;
    [SerializeField] private Transform[] spawnPoint;

    public float startSpawnerInterval;
    private float spawnerInterval;

    public int numberOfEnemies;
    public int currentNumberOfEnemies;

    private int randomPoint;

    private void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    private void Update()
    {
        if (spawnerInterval <= 0 && currentNumberOfEnemies < numberOfEnemies)
        {
            randomPoint = Random.Range(0,spawnPoint.Length);

            Instantiate(spawnMob, spawnPoint[randomPoint].transform.position,Quaternion.identity);

            spawnerInterval = startSpawnerInterval;
            currentNumberOfEnemies++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }
    }
    
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Добавляем для работы с TextMeshPro

public class ZombieSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject zombiePrefab; // Префаб зомби
    public Transform spawnPoint; // Точки спавна
    public float checkInterval = 2f; // Интервал проверки живых зомби

    [Header("Настройки количества")]
    public int minZombies = 1;
    public int maxZombies = 3;

    //[Header("TextMeshPro Настройки")]
    //public TextMeshProUGUI killCounterText; // TextMeshPro для отображения счетчика убийств
    //public string killTextFormat = "Убито зомби: {0}";

    private List<GameObject> currentZombies = new List<GameObject>();
    private bool isSpawning = false;
    private int zombiesKilled = 0;

    public void StartWave()
    {
       
            SpawnZombieWave();
            StartCoroutine(CheckZombiesCoroutine());
       
    }

    IEnumerator CheckZombiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (!isSpawning && AreAllZombiesDead())
            {
                SpawnZombieWave();
            }
        }
    }

    private void SpawnZombieWave()
    {
        isSpawning = true;

        currentZombies.RemoveAll(zombie => zombie == null);

        int zombieCount = Random.Range(minZombies, maxZombies + 1);

        Debug.Log($"Создаём волну из {zombieCount} зомби!");

        for (int i = 0; i < zombieCount; i++)
        {
            if (spawnPoint != null && zombiePrefab != null)
            {
                GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

                EnemyAI1 mobAI = zombie.GetComponent<EnemyAI1>();

                currentZombies.Add(zombie);
            }
        }

        isSpawning = false;
    }

    bool AreAllZombiesDead()
    {
        currentZombies.RemoveAll(zombie => zombie == null);

        return currentZombies.Count == 0;
    }

    private void OnZombieDied(GameObject zombie)
    {
        if (currentZombies.Contains(zombie))
        {
            currentZombies.Remove(zombie);
        }

        EnemyAI1 mobAI = zombie.GetComponent<EnemyAI1>();
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

    public void ResetKillCounter()
    {
        zombiesKilled = 0;
    }
}
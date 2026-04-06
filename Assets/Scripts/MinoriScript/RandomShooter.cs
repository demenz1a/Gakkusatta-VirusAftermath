using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShooter : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;         
    [SerializeField] private GameObject bulletPrefab;   
    [SerializeField] private int shotsCount = 5;        
    [SerializeField] private float spawnOffset = 0.5f;   
    [SerializeField] private float delayBetweenShots = 0.3f; 

    public void ShootAtRandomEnemies()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> visibleEnemies = new List<GameObject>();

        foreach (var enemy in enemies)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(enemy.transform.position);

            if (viewportPos.z > 0 &&
                viewportPos.x > 0 && viewportPos.x < 1 &&
                viewportPos.y > 0 && viewportPos.y < 1)
            {
                visibleEnemies.Add(enemy);
            }
        }

        if (visibleEnemies.Count == 0)
        {
            Debug.Log("Нет врагов в поле зрения камеры");
            return;
        }

        int count = Mathf.Min(shotsCount, visibleEnemies.Count);
        List<GameObject> chosenEnemies = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, visibleEnemies.Count);
            chosenEnemies.Add(visibleEnemies[index]);
            visibleEnemies.RemoveAt(index);
        }


        StartCoroutine(SpawnBulletsSequentially(chosenEnemies));
    }

    private IEnumerator SpawnBulletsSequentially(List<GameObject> targets)
    {
        foreach (GameObject target in targets)
        {
            if (target != null) 
            {
                Vector3 spawnPos = target.transform.position + Vector3.up * spawnOffset;
                Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"Выстрел по: {target.name}");
            }

            yield return new WaitForSeconds(delayBetweenShots);
        }
    }
}

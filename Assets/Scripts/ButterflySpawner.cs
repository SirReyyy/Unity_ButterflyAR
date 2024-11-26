using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    public GameObject butterflyPrefab; // Prefab to spawn
    public Transform butterflyHolder; // Parent object to hold all butterflies
    public int maxButterflies = 100; // Maximum number of butterflies to spawn
    public Vector3 spawnArea = new Vector3(800f, 600f, 600f); // Area to spawn butterflies

    private List<GameObject> spawnedButterflies = new List<GameObject>();

    void Start()
    {
        SpawnButterflies();
    }

    void SpawnButterflies()
    {
        for (int i = 0; i < maxButterflies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(0, spawnArea.y),
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            GameObject butterfly = Instantiate(butterflyPrefab, randomPosition, Quaternion.identity, butterflyHolder);
            spawnedButterflies.Add(butterfly);
        }
    }
}

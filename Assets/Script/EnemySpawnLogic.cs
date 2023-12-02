using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLogic : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 2f; // Time between each spawn
    public int maxEnemies = 5; // Maximum number of enemies at one time

    public int countEnemies = 0;
    public float spawnZoneRadius = 5f; // Radius of the spawn zone
    public Color gizmosColor = Color.green; // Color of the Gizmos

    private float timeSinceLastSpawn;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Update()
    {
        // Remove null references from the list (enemies that have been destroyed)
        spawnedEnemies.RemoveAll(item => item == null);

        // Check if it's time to spawn a new enemy and if the max limit hasn't been reached
        if (Time.time - timeSinceLastSpawn >= spawnInterval && spawnedEnemies.Count < maxEnemies && countEnemies < 2)
        {
            SpawnEnemy();
            timeSinceLastSpawn = Time.time;
            countEnemies++;
        }
    }

    void SpawnEnemy()
    {
        // Generate a random position within the spawn zone
        Vector2 randomPosition = Random.insideUnitCircle * spawnZoneRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, transform.position.z);

        // Spawn the enemy and add it to the list
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);
    }

    // Draw Gizmos in the Editor to visualize the spawn zone
    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor; // Set the color of the Gizmos
        Gizmos.DrawWireSphere(transform.position, spawnZoneRadius);
    }
}

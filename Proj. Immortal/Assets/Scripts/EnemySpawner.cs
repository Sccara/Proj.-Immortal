using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnTimer;
    [SerializeField] private Transform player;

    private void Update()
    {
        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
        }

        spawnTimer += Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        spawnTimer = 0f;

        float angle = Random.Range(0, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * spawnRadius;
        float z = Mathf.Sin(angle) * spawnRadius;

        Vector3 spawnPos = new Vector3(player.position.x + x, 1f, player.position.z + z);

        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPos, Quaternion.identity);
    }
}

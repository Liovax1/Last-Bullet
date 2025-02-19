using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player; // R�f�rence au joueur
    public float spawnRadius = 10f; // Distance max de spawn autour du joueur
    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition;
        float distance;

        // G�n�rer une position al�atoire autour du joueur
        do
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );
            spawnPosition = player.position + randomOffset;
            distance = Vector3.Distance(player.position, spawnPosition);
        } while (distance < 3f); // �vite de spawner trop proche du joueur

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб монстра
    public int numberOfEnemies = 3; // Количество монстров, создаваемых при старте

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Выбираем случайную позицию на карте
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

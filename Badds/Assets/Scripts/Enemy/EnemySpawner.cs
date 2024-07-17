using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 60f; // Частота спауна волн врагов в секундах (1 минута)
    [SerializeField] private GameObject[] enemyPrefabs; // Массив префабов врагов
    [SerializeField] private bool canSpawn = true; // Флаг, определяющий, можно ли спаунить врагов
    [SerializeField] private Tilemap spawnTilemap; // Ссылка на Tilemap для спауна врагов
    [SerializeField] private int enemiesPerWave = 3; // Количество врагов в одной волне

    private void Start()
    {
        StartCoroutine(Spawner()); // Запускаем корутину спаунера
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); // Создаем задержку для спауна волн

        while (canSpawn)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int rand = Random.Range(0, enemyPrefabs.Length); // Выбираем случайный префаб врага
                GameObject enemyToSpawn = enemyPrefabs[rand];

                // Случайная позиция в пределах области спауна
                Vector3Int randomTilePosition = GetRandomTilePosition();
                Vector3 spawnPosition = spawnTilemap.GetCellCenterWorld(randomTilePosition);

                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity); // Спауним врага в позиции тайла

                yield return new WaitForSeconds(0.1f); // Небольшая задержка между спаунами врагов в одной волне
            }
            yield return wait; // Ожидаем перед спауном следующей волны
        }
    }

    private Vector3Int GetRandomTilePosition()
    {
        // Получаем случайные координаты тайла в пределах Tilemap
        Vector3Int randomTilePosition = new Vector3Int(
            Random.Range(spawnTilemap.cellBounds.min.x, spawnTilemap.cellBounds.max.x + 1),
            Random.Range(spawnTilemap.cellBounds.min.y, spawnTilemap.cellBounds.max.y + 1),
            0
        );

        return randomTilePosition;
    }

    // Метод для отрисовки области спауна в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnTilemap.cellBounds.size.x, spawnTilemap.cellBounds.size.y, 0));
    }
}

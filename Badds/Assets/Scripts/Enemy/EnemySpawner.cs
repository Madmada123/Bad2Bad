using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 60f; // ������� ������ ���� ������ � �������� (1 ������)
    [SerializeField] private GameObject[] enemyPrefabs; // ������ �������� ������
    [SerializeField] private bool canSpawn = true; // ����, ������������, ����� �� �������� ������
    [SerializeField] private Tilemap spawnTilemap; // ������ �� Tilemap ��� ������ ������
    [SerializeField] private int enemiesPerWave = 3; // ���������� ������ � ����� �����

    private void Start()
    {
        StartCoroutine(Spawner()); // ��������� �������� ��������
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); // ������� �������� ��� ������ ����

        while (canSpawn)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int rand = Random.Range(0, enemyPrefabs.Length); // �������� ��������� ������ �����
                GameObject enemyToSpawn = enemyPrefabs[rand];

                // ��������� ������� � �������� ������� ������
                Vector3Int randomTilePosition = GetRandomTilePosition();
                Vector3 spawnPosition = spawnTilemap.GetCellCenterWorld(randomTilePosition);

                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity); // ������� ����� � ������� �����

                yield return new WaitForSeconds(0.1f); // ��������� �������� ����� �������� ������ � ����� �����
            }
            yield return wait; // ������� ����� ������� ��������� �����
        }
    }

    private Vector3Int GetRandomTilePosition()
    {
        // �������� ��������� ���������� ����� � �������� Tilemap
        Vector3Int randomTilePosition = new Vector3Int(
            Random.Range(spawnTilemap.cellBounds.min.x, spawnTilemap.cellBounds.max.x + 1),
            Random.Range(spawnTilemap.cellBounds.min.y, spawnTilemap.cellBounds.max.y + 1),
            0
        );

        return randomTilePosition;
    }

    // ����� ��� ��������� ������� ������ � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnTilemap.cellBounds.size.x, spawnTilemap.cellBounds.size.y, 0));
    }
}

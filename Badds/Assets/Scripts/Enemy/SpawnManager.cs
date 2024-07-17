using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �������
    public int numberOfEnemies = 3; // ���������� ��������, ����������� ��� ������

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // �������� ��������� ������� �� �����
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

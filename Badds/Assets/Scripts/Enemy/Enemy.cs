using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject healthBarPrefab; // ������ ����� ��������
    private Floating healthBar; // ��������� ����� ��������

    private Transform player; // ������� ������
    public float moveSpeed = 3f; // �������� �������� �������
    public float attackRange = 5f; // ��������� ����� �������
    public GameObject dropItem; // ������ ��������, ����������� ����� ������

    private bool isDead = false; // ���� ��������� ������� (����� ��� ���)

    private void Start()
    {
        currentHealth = maxHealth;

        // ������� ����� �������� ��� ������
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar = healthBarObject.GetComponent<Floating>();
        if (healthBar == null)
        {
            Debug.LogError("Floating component not found on health bar prefab.");
        }
        healthBarObject.transform.SetParent(transform);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // ����� ������ �� ���� "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found with tag 'Player'. Make sure the player object has the correct tag.");
        }
    }

    private void Update()
    {
        // ��������� ������� � ���������� ����� ��������
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // ��������� ���������� �� ������ � ������������ � ����, ���� � �������� ��������� �����
        if (!isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

                // ��������� ������������ � �������
                if (distanceToPlayer < 1f) // ����� 1f � ����������, �� ������� ��������� ������������
                {
                    // ������� ���� ������
                    player.GetComponent<PlayerHealth>().TakeDamage(10f); // �����������, ��� � ������ ���� ��������� PlayerHealth � ������� TakeDamage
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(healthBar.gameObject); // ���������� ����� ��������
        Instantiate(dropItem, transform.position, Quaternion.identity); // ������� �������
        Destroy(gameObject); // ���������� �����
    }
}

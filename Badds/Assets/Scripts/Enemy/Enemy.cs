using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject healthBarPrefab; // Префаб шкалы здоровья
    private Floating healthBar; // Компонент шкалы здоровья

    private Transform player; // Позиция игрока
    public float moveSpeed = 3f; // Скорость движения монстра
    public float attackRange = 5f; // Дистанция атаки монстра
    public GameObject dropItem; // Префаб предмета, выпадающего после смерти

    private bool isDead = false; // Флаг состояния монстра (мертв или нет)

    private void Start()
    {
        currentHealth = maxHealth;

        // Создаем шкалу здоровья над врагом
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar = healthBarObject.GetComponent<Floating>();
        if (healthBar == null)
        {
            Debug.LogError("Floating component not found on health bar prefab.");
        }
        healthBarObject.transform.SetParent(transform);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // Поиск игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found with tag 'Player'. Make sure the player object has the correct tag.");
        }
    }

    private void Update()
    {
        // Обновляем позицию и ориентацию шкалы здоровья
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // Проверяем расстояние до игрока и перемещаемся к нему, если в пределах дистанции атаки
        if (!isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

                // Проверяем столкновение с игроком
                if (distanceToPlayer < 1f) // Здесь 1f — расстояние, на котором считается столкновение
                {
                    // Наносим урон игроку
                    player.GetComponent<PlayerHealth>().TakeDamage(10f); // Предположим, что у игрока есть компонент PlayerHealth с методом TakeDamage
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
        Destroy(healthBar.gameObject); // Уничтожаем шкалу здоровья
        Instantiate(dropItem, transform.position, Quaternion.identity); // Создаем предмет
        Destroy(gameObject); // Уничтожаем врага
    }
}

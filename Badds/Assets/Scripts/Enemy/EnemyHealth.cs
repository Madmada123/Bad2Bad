using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private int attackCount = 0;
    private const int maxAttacks = 5; // Максимальное количество атак, после которых враг умирает

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        attackCount++;

        if (attackCount >= maxAttacks)
        {
            Die();
        }

        Debug.Log("Current Enemy Health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Enemy is dead after " + maxAttacks + " attacks!");
        // Дополнительные действия при смерти врага, например, удаление объекта или анимация смерти
        Destroy(gameObject); // Простой пример - уничтожаем объект врага
    }
}

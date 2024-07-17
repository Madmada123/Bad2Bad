using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject healthBarPrefab; // ������ ����� ��������
    private Floating healthBar; // ��������� ����� ��������


    //Gun
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;


    private float fireTimer;


    private void Start()
    {
        // ������������� ��������
        currentHealth = maxHealth;

        // ������� ����� �������� ��� �������
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar = healthBarObject.GetComponent<Floating>();
        healthBarObject.transform.SetParent(transform);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        // ��������� ������� � ���������� ����� ��������
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

    }

    private void Shoot()
    {
        Instantiate(bulletPreFab, firingPoint.position, firingPoint.rotation);
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
        Destroy(healthBar.gameObject); // ���������� ����� ��������
        Destroy(gameObject); // ���������� ������
    }
}

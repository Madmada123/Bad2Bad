using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private int attackCount = 0;
    private const int maxAttacks = 5; // ������������ ���������� ����, ����� ������� ����� �������

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

        Debug.Log("Current Health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player is dead after " + maxAttacks + " attacks!");
        // �������������� �������� ��� ������ ������, ��������, �������� ������ ����� ��� ������������ ������
        gameObject.SetActive(false); // ������� ������ - ��������� ������ ������
    }
}

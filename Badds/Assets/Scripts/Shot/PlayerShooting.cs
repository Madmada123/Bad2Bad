using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, ������ �������� ����
    public float bulletSpeed = 20f;
    public Animator animator; // �������� ��� �������� ��������

    public void Shoot()
    {
        // ��������������� �������� ��������
        animator.SetTrigger("Shoot");

        // �������� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}

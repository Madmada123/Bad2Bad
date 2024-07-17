using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда стреляют пули
    public float bulletSpeed = 20f;
    public Animator animator; // Аниматор для анимации стрельбы

    public void Shoot()
    {
        // Воспроизведение анимации стрельбы
        animator.SetTrigger("Shoot");

        // Создание пули
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}

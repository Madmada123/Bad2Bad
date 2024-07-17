using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = movementJoystick.Direction;

        if (direction.y != 0 || direction.x != 0)
        {
            rb.velocity = new Vector2(direction.x * playerSpeed, direction.y * playerSpeed);

            // Поворот персонажа
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            // Установка параметра скорости для анимации
            animator.SetFloat("Speed", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}

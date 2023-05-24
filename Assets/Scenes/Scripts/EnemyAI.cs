using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // Цель для преследования
    public float moveSpeed = 5f; // Скорость перемещения врага
    public float jumpForce = 5f; // Сила прыжка врага
    public float maxJumpDistance = 2f; // Максимальная горизонтальная дистанция для прыжка

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            // Преследование по горизонтали
            float direction = target.position.x - transform.position.x;

            if (direction < 0)
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            else if (direction > 0)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // Прыжок
            if (isGrounded && Mathf.Abs(direction) <= maxJumpDistance)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        // Обнаружение земли
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
